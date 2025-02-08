using Npgsql;

namespace CMSJet.Core.Data.Repositories;

public class BaseRepository
{
    private readonly Database _database;

        protected BaseRepository(Database database)
        {
            _database = database;
        }

        protected async Task<List<T>> QueryAsync<T>(string sql, Func<NpgsqlDataReader, T> map, object? parameters = null, CancellationToken cancellationToken = default)
        {
            var results = new List<T>();

            using var conn = await _database.GetConnectionAsync();
            using var cmd = new NpgsqlCommand(sql, conn);
            AddParameters(cmd, parameters);

            using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(map(reader));
            }

            return results;
        }

        protected async Task<T?> QuerySingleAsync<T>(string sql, Func<NpgsqlDataReader, T> map, object? parameters = null, CancellationToken cancellationToken = default)
        {
            using var conn = await _database.GetConnectionAsync();
            using var cmd = new NpgsqlCommand(sql, conn);
            AddParameters(cmd, parameters);

            using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            return await reader.ReadAsync(cancellationToken) ? map(reader) : default;
        }

        protected async Task<int> ExecuteAsync(string sql, object? parameters = null, CancellationToken cancellationToken = default)
        {
            using var conn = await _database.GetConnectionAsync();
            using var cmd = new NpgsqlCommand(sql, conn);
            AddParameters(cmd, parameters);
            return await cmd.ExecuteNonQueryAsync(cancellationToken);
        }

        protected async Task<int> ExecuteScalarAsync(string sql, object? parameters = null, CancellationToken cancellationToken = default)
        {
            using var conn = await _database.GetConnectionAsync();
            using var cmd = new NpgsqlCommand(sql, conn);
            AddParameters(cmd, parameters);
            return Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken));
        }

        private void AddParameters(NpgsqlCommand cmd, object? parameters)
        {
            if (parameters == null) return;
            foreach (var prop in parameters.GetType().GetProperties())
            {
                cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(parameters) ?? DBNull.Value);
            }
        }
}