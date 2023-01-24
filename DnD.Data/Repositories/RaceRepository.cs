using DataAdapter.NoSql;
using DnD.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DnD.Data.Repositories
{
    public class RaceRepository
    {
        private readonly IMongoDbConnection _connection;
        public RaceRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<RaceBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var races = _connection.Database.GetCollection<RaceBson>("races");
            var filter = new FilterDefinitionBuilder<RaceBson>().Empty;
            var cursor = await races.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<RaceBson>> GetByCategoryIdAsync(string categoryId, CancellationToken cancellationToken = default)
        {
            var races = _connection.Database.GetCollection<RaceBson>("races");
            var filter = new FilterDefinitionBuilder<RaceBson>().Eq(r => r.CategoryId, categoryId);
            var cursor = await races.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<RaceBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var races = _connection.Database.GetCollection<RaceBson>("races");
            var filter = new FilterDefinitionBuilder<RaceBson>().Eq(c => c.Id, id);
            var cursor = await races.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
