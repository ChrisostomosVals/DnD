using DataAdapter.NoSql;
using DataAdapter.Sql;
using DnD.Data.Internal;
using DnD.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DnD.Data.Internal.Procedures;

namespace DnD.Data.Repositories
{
    public class WorldObjectRepository
    {
        private readonly IMongoDbConnection _connection;
        public WorldObjectRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<WorldObjectBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<WorldObjectBson>("world_objects");
            var filter = new FilterDefinitionBuilder<WorldObjectBson>().Empty;
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<WorldObjectBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<WorldObjectBson>("world_objects");
            var filter = new FilterDefinitionBuilder<WorldObjectBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<WorldObjectPropBson>?> GetPropertiesAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<WorldObjectBson>("world_objects");
            var filter = new FilterDefinitionBuilder<WorldObjectBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var worldObject = await cursor.FirstOrDefaultAsync(cancellationToken);
            return worldObject.Properties;
        }
        public async Task InsertAsync(WorldObjectBson worldObject, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<WorldObjectBson>("world_objects");
            await characters.InsertOneAsync(worldObject, cancellationToken: cancellationToken);
        }
        public async Task UpdateAsync(WorldObjectBson worldObject, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<WorldObjectBson>("world_objects");
            var filter = new FilterDefinitionBuilder<WorldObjectBson>().Eq(c => c.Id, worldObject.Id);
            var updateDefinition = new UpdateDefinitionBuilder<WorldObjectBson>()
                .Set(c => c.Name, worldObject.Name)
                .Set(c => c.Type, worldObject.Type)
                .Set(c => c.Description, worldObject.Description)
                .Set(c => c.Properties, worldObject.Properties);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<WorldObjectBson>("world_objects");
            var filter = new FilterDefinitionBuilder<WorldObjectBson>().Eq(c => c.Id, id);
            await characters.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }
    }
}
