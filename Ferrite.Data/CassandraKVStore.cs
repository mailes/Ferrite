// 
// Project Ferrite is an Implementation of the Telegram Server API
// Copyright 2022 Aykut Alparslan KOC <aykutalparslan@msn.com>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
// 

using System.Text;
using Cassandra;
using Ferrite.Data.Repositories;

namespace Ferrite.Data;

public class CassandraKVStore : IKVStore
{
    private readonly ICassandraContext _context;
    private readonly TableDefinition _table;
    private const string IntStr = "int";
    private const string BoolStr = "boolean";
    private const string LongStr = "bigint";
    private const string FloatStr = "float";
    private const string DoubleStr = "double";
    private const string DateStr = "date";
    private const string StringStr = "text";
    private const string BytesStr = "blob";
    private static string GetTypeStr(DataType type) => type switch
    {
        DataType.Bool => BoolStr,
        DataType.Int => IntStr,
        DataType.Long => LongStr,
        DataType.Float => FloatStr,
        DataType.Double => DoubleStr,
        DataType.DateTime => DateStr,
        DataType.String => StringStr,
        DataType.Bytes => BytesStr,
        _ => ""
    };
    private static Type GetManagedType(DataType type) => type switch
    {
        DataType.Bool => typeof(bool),
        DataType.Int => typeof(int),
        DataType.Long => typeof(long),
        DataType.Float => typeof(float),
        DataType.Double => typeof(double),
        DataType.DateTime => typeof(DateTime),
        DataType.String => typeof(string),
        DataType.Bytes => typeof(byte[]),
        _ => typeof(object)
    };
    public CassandraKVStore(ICassandraContext context, TableDefinition table)
    {
        _context = context;
        _table = table;
        CreateSchema();
    }
    private void CreateSchema()
    {
        StringBuilder sb = new StringBuilder($"CREATE TABLE IF NOT EXISTS {_table.Keyspace}.{_table.Name} (");
        bool first = true;
        int pcount = 0;
        foreach (var c in _table.PrimaryKey.Columns)
        {
            pcount++;
            if (!first)
            {
                sb.Append(", ");
            }
            first = false;
            sb.Append($"{c.Name} {GetTypeStr(c.Type)}");
        }

        if (pcount > 0)
        {
            sb.Append(", ");
        }
        pcount = 0;
        sb.Append($"{_table.Name}_data blob, ");
        sb.Append("PRIMARY KEY (");
        first = true;
        foreach (var c in _table.PrimaryKey.Columns)
        {
            if (!first)
            {
                sb.Append(", ");
            }
            first = false;
            sb.Append($"{c.Name}");
        }
        sb.Append("));");
        var statement = new SimpleStatement(sb.ToString());
        _context.Execute(statement);
        foreach (var sc in _table.SecondaryIndices)
        {
            pcount = 0;
            sb = new StringBuilder($"CREATE TABLE IF NOT EXISTS {_table.Keyspace}.{_table.Name}_{sc.Name} (");
            first = true;
            foreach (var c in sc.Columns)
            {
                pcount++;
                if (!first)
                {
                    sb.Append(", ");
                }
                first = false;
                sb.Append($"{c.Name} {GetTypeStr(c.Type)}");
            }
            first = true;
            if (pcount > 0)
            {
                sb.Append(", ");
            }
            pcount = 0;
            foreach (var c in _table.PrimaryKey.Columns)
            {
                pcount++;
                if (!first)
                {
                    sb.Append(", ");
                }
                first = false;
                sb.Append($"pk_{c.Name} {GetTypeStr(c.Type)}");
            }
            if (pcount > 0)
            {
                sb.Append(", ");
            }
            sb.Append("PRIMARY KEY (");
            first = true;
            foreach (var c in sc.Columns)
            {
                if (!first)
                {
                    sb.Append(", ");
                }
                first = false;
                sb.Append($"{c.Name}");
            }
            sb.Append("));");
            statement = new SimpleStatement(sb.ToString());
            _context.Execute(statement);
        }
    }

    public async Task<bool> Put(byte[] data, params object[] keys)
    {
        if (keys.Length != 0)
        {
            throw new Exception("Parameter count mismatch.");
        }
        StringBuilder sb = new StringBuilder($"UPDATE {_table.Keyspace}.{_table.Name} SET {_table.Name}_data = ? ");
        sb.Append($"WHERE ");
        bool first = true;
        for (int i = 0; i < keys.Length; i++)
        {
            var col = _table.PrimaryKey.Columns[i];
            if (keys[i].GetType() != GetManagedType(col.Type))
            {
                throw new Exception($"Expected type was {GetManagedType(col.Type)} and " +
                                    $"the parameter was of type {keys[i].GetType()}");
            }
            if (!first)
            {
                sb.Append($" AND ");
            }
            first = false;
            sb.Append($"{col.Name} = ? ");
        }
        var statement = new SimpleStatement(sb.ToString(), data, keys);
        _context.Enqueue(statement);
        
        foreach (var sc in _table.SecondaryIndices)
        {
            first = true;
            List<object> secondaryParams = new();
            foreach (var c in sc.Columns)
            {
                secondaryParams.Add(keys[_table.PrimaryKey.GetOrdinal(c.Name)]);
            }
            sb = new StringBuilder($"UPDATE {_table.Keyspace}.{_table.Name}_{sc.Name} SET ");
            foreach (var c in _table.PrimaryKey.Columns)
            {
                if (!first)
                {
                    sb.Append(", ");
                }
                first = false;
                sb.Append($"pk_{c.Name} = ?");
            }
            sb.Append($"WHERE ");
            first = true;
            for (int i = 0; i < secondaryParams.Count; i++)
            {
                var col = sc.Columns[i];
                if (!first)
                {
                    sb.Append($" AND ");
                }
                first = false;
                sb.Append($"{col.Name} = ? ");
            }
            var indexStatement = new SimpleStatement(sb.ToString(), keys, secondaryParams);
            _context.Enqueue(indexStatement);
        }
        await _context.ExecuteQueueAsync();
        return true;
    }

    public async Task<bool> Delete(params object[] keys)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(string indexName, params object[] keys)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Commit()
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> Get(params object[] keys)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> Get(string indexName, params object[] keys)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<byte[]> Iterate(params object[] keys)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<byte[]> Iterate(string indexName, params object[] keys)
    {
        throw new NotImplementedException();
    }
}