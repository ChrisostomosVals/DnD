using DataAdapter.NoSql;
using DataAdapter.Sql;
using DnD.Data.Internal;
using DnD.Data.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DnD.Data.Repositories
{
    public class CharacterRepository
    {
        private readonly IMongoDbConnection _connection;
        public CharacterRepository(IMongoDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<CharacterBson>> GetAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Empty;
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<CharacterBson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<CharacterBson>> GetBossAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Type, "BOSS");
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<CharacterBson>> GetHeroAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Type, "HERO");
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<CharacterBson>> GetHostileAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Type, "MONSTER");
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<CharacterBson>> GetNpcAsync(CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Type, "NPC");
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        public async Task<GearBson> GetMoneyAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.Gear.FirstOrDefault(g => g.Name == "Money");
        }
        public async Task<IEnumerable<GearBson>?> GetGearAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.Gear;
        }
        public async Task<IEnumerable<ArsenalBson>?> GetArsenalAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.Arsenal;
        }
        public async Task<IEnumerable<PropertyBson>?> GetPropertiesAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.Properties;
        }
        public async Task<IEnumerable<SkillBson>?> GetSkillsAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.Skills;
        }
        public async Task<IEnumerable<string>?> GetFeatsAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.Feats;
        }
        public async Task<IEnumerable<string>?> GetSpecialAbilitiesAsync(string id, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var cursor = await characters.FindAsync(filter, cancellationToken: cancellationToken);
            var character = await cursor.FirstOrDefaultAsync(cancellationToken);
            return character.SpecialAbilities;
        }

        public async Task CreateAsync(CharacterBson character, CancellationToken cancellationToken = default)        {
            character.Id = null;
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            await characters.InsertOneAsync(character, cancellationToken: cancellationToken);
        }
        public async Task UpdateAsync(CharacterBson character, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, character.Id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Name, character.Name)
                .Set(c => c.RaceId, character.RaceId)
                .Set(c => c.ClassId, character.ClassId)
                .Set(c => c.Type, character.Type);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdateGearAsync(string id, IList<GearBson> gear, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Gear, gear);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdateArsenalAsync(string id, IList<ArsenalBson> arsenal, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Arsenal, arsenal);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdateSkillsAsync(string id, IList<SkillBson> skills, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Skills, skills);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdateFeatsAsync(string id, IList<string> feats, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Feats, feats);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdateSpecialAbilitiesAsync(string id, IList<string> specialAbilities, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Feats, specialAbilities);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdateStatsAsync(string id, IList<StatBson> stats, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Stats, stats);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task UpdatePropertiesAsync(string id, IList<PropertyBson> properties, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Properties, properties);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        public async Task ChangeVisibilityAsync(string id, bool visible, CancellationToken cancellationToken = default)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            var updateDefinition = new UpdateDefinitionBuilder<CharacterBson>()
                .Set(c => c.Visible, visible);
            await characters.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
        }
        
        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var characters = _connection.Database.GetCollection<CharacterBson>("characters");
            var filter = new FilterDefinitionBuilder<CharacterBson>().Eq(c => c.Id, id);
            await characters.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }
    }
}
