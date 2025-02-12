using Npgsql;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories;

public class MigrationRepository : BaseRepository
{
    public MigrationRepository(Database database) : base(database) { }

        public async Task<IEnumerable<Migration>> GetAllMigrationsAsync(CancellationToken cancellationToken = default)
        {
            const string sql = "SELECT * FROM migrations ORDER BY created_at DESC";
            return await QueryAsync(sql, MapMigration, cancellationToken: cancellationToken);
        }

        public async Task<Migration?> GetMigrationByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            const string sql = "SELECT * FROM migrations WHERE id = @Id LIMIT 1";
            return await QuerySingleAsync(sql, MapMigration, new { Id = id }, cancellationToken);
        }

        public async Task<Guid> AddMigrationAsync(Migration migration, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO migrations (user_id, name, source_connection_id, target_connection_id, status, created_at, updated_at)
                VALUES (@UserId, @Name, @SourceConnectionId, @TargetConnectionId, @Status, NOW(), NOW())
                RETURNING id;";

            return await ExecuteScalarAsync(sql, migration, cancellationToken);
        }
        public async Task<bool> UpdateMigrationAsync(Migration migration)
        {
            const string sql = @"
                UPDATE migrations
                SET 
                    name = @Name,
                    source_connection_id = @SourceConnectionId,
                    target_connection_id = @TargetConnectionId,
                    status = @Status,
                    updated_at = NOW()
                WHERE id = @Id";

            int affectedRows = await ExecuteAsync(sql, migration);
            return affectedRows > 0;
        }
        public async Task<bool> DeleteMigrationAsync(Guid id)
        {
            const string sql = "DELETE FROM migrations WHERE id = @Id";
            int affectedRows = await ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
        private static Migration MapMigration(NpgsqlDataReader reader)
        {
            return new Migration
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                UserId = reader.GetGuid(reader.GetOrdinal("user_id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                SourceConnectionId = reader.GetGuid(reader.GetOrdinal("source_connection_id")),
                TargetConnectionId = reader.GetGuid(reader.GetOrdinal("target_connection_id")),
                Status = reader.GetString(reader.GetOrdinal("status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
            };
        }
}