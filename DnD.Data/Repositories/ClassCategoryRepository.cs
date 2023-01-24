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
    public class ClassCategoryRepository
    {
        private readonly IMongoDbConnection _connection;
        public ClassCategoryRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<ClassCategoryBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<ClassCategoryBson>("class_categories");
            var filter = new FilterDefinitionBuilder<ClassCategoryBson>().Empty;
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<ClassCategoryBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<ClassCategoryBson>("class_categories");
            var filter = new FilterDefinitionBuilder<ClassCategoryBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
