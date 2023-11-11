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
    public class UserRepository
    {
        private readonly IMongoDbConnection _connection;
        public UserRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<UserBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var users = _connection.Database.GetCollection<UserBson>("users");
            var filter = new FilterDefinitionBuilder<UserBson>().Empty;
            var cursor = await users.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<UserBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var users = _connection.Database.GetCollection<UserBson>("users");
            var filter = new FilterDefinitionBuilder<UserBson>().Eq(u => u.Id, id);
            var cursor = await users.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<UserBson> GetByEmailAsync (string email, CancellationToken cancellationToken = default)
        {
            var users = _connection.Database.GetCollection<UserBson>("users");
            var filter = new FilterDefinitionBuilder<UserBson>().Eq(u => u.Email, email);
            var cursor = await users.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<UserBson>("users");
            var filter = new FilterDefinitionBuilder<UserBson>().Eq(u => u.Email, email);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var user = await cursor.FirstOrDefaultAsync(cancellationToken);
            return user != null;
        }
        public async Task InsertAsync(UserBson user, CancellationToken cancellationToken = default)
        {
            user.Id = null;
            var users = _connection.Database.GetCollection<UserBson>("users");
            await users.InsertOneAsync(user, cancellationToken: cancellationToken);
        }
        public async Task UpdateAsync(UserBson user, CancellationToken cancellationToken = default)
        {
            var users = _connection.Database.GetCollection<UserBson>("users");
            var filter = new FilterDefinitionBuilder<UserBson>().Eq(c => c.Id, user.Id);
            var updateDefinition = new UpdateDefinitionBuilder<UserBson>()
            .Set(c => c.Name, user.Name)
            .Set(c => c.Email, user.Email)
            .Set(c => c.CharacterId, user.CharacterId);
            await users.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task ChangePasswordAsync(string id, string newPassword, CancellationToken cancellationToken = default)
        {
            var users = _connection.Database.GetCollection<UserBson>("users");
            var filter = new FilterDefinitionBuilder<UserBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<UserBson>()
            .Set(c => c.Password, newPassword);
            await users.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }

    }
}
