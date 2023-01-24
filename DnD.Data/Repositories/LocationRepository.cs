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

namespace DnD.Data.Repositories
{
    public class LocationRepository
    {
        private readonly IMongoDbConnection _connection;
        public LocationRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<LocationBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<LocationBson>("locations");
            var filter = new FilterDefinitionBuilder<LocationBson>().Empty;
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<LocationBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<LocationBson>("locations");
            var filter = new FilterDefinitionBuilder<LocationBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<LocationBson> GetLatestAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<LocationBson>("locations");
            return await characters.Find(x => true).SortByDescending(d => d.Year).ThenByDescending(d => d.Date).ThenByDescending(d => d.Time).Limit(1).FirstOrDefaultAsync();
        }
        public async Task InsertAsync(LocationBson location, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<LocationBson>("locations");
            await characters.InsertOneAsync(location, cancellationToken: cancellationToken);
        }
        public async Task UpdateAsync(LocationBson location, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<LocationBson>("locations");
            var filter = new FilterDefinitionBuilder<LocationBson>().Eq(c => c.Id, location.Id);
            var updateDefinition = new UpdateDefinitionBuilder<LocationBson>()
                .Set(c => c.X, location.X)
                .Set(c => c.Y, location.Y)
                .Set(c => c.Date, location.Date)
                .Set(c => c.Time, location.Time)
                .Set(c => c.Season, location.Season)
                .Set(c => c.Events, location.Events);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<LocationBson>("locations");
            var filter = new FilterDefinitionBuilder<LocationBson>().Eq(c => c.Id, id);
            await characters.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }
    }
}
