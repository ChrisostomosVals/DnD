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
    public class RaceCategoryRepository
    {
        private readonly IMongoDbConnection _connection;
        public RaceCategoryRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<RaceCategoryBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var raceCategories = _connection.Database.GetCollection<RaceCategoryBson>("race_categories");
            var filter = new FilterDefinitionBuilder<RaceCategoryBson>().Empty;
            var cursor = await raceCategories.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<RaceCategoryBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var raceCategories = _connection.Database.GetCollection<RaceCategoryBson>("race_categories");
            var filter = new FilterDefinitionBuilder<RaceCategoryBson>().Eq(c => c.Id, id);
            var cursor = await raceCategories.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
