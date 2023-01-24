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
    public class ClassRepository
    {
        private readonly IMongoDbConnection _connection;
        public ClassRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<ClassBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<ClassBson>("classes");
            var filter = new FilterDefinitionBuilder<ClassBson>().Empty;
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<ClassBson>> GetByCategoryIdAsync(string categoryId, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<ClassBson>("classes");
            var filter = new FilterDefinitionBuilder<ClassBson>().Eq(r => r.CategoryId, categoryId);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<ClassBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<ClassBson>("classes");
            var filter = new FilterDefinitionBuilder<ClassBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
