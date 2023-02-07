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
    public class ChapterRepository
    {
        private readonly IMongoDbConnection _connection;
        public ChapterRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<ChapterBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var chapters = _connection.Database.GetCollection<ChapterBson>("chapters");
            var filter = new FilterDefinitionBuilder<ChapterBson>().Empty;
            var cursor = await chapters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<ChapterBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var chapters = _connection.Database.GetCollection<ChapterBson>("chapters");
            var filter = new FilterDefinitionBuilder<ChapterBson>().Eq(c => c.Id, id);
            var cursor = await chapters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task CreateAsync(ChapterBson chapter, CancellationToken cancellationToken = default)
        {
            chapter.Id = null;
            var chapters = _connection.Database.GetCollection<ChapterBson>("chapters");
            await chapters.InsertOneAsync(chapter, cancellationToken: cancellationToken);
        }
        public async Task UpdateAsync(ChapterBson chapter, CancellationToken cancellationToken = default)
        {
            var chapters = _connection.Database.GetCollection<ChapterBson>("chapters");
            var filter = new FilterDefinitionBuilder<ChapterBson>().Eq(c => c.Id, chapter.Id);
            var updateDefinition = new UpdateDefinitionBuilder<ChapterBson>()
                .Set(c => c.Name, chapter.Name)
                .Set(c => c.Story, chapter.Story)
                .Set(c => c.Date, chapter.Date);
            await chapters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var chapters = _connection.Database.GetCollection<ChapterBson>("chapters");
            var filter = new FilterDefinitionBuilder<ChapterBson>().Eq(c => c.Id, id);
            await chapters.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }
    }
}
