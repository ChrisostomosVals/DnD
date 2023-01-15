using DataAdapter.Sql;
using DnD.Data.Internal;
using DnD.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DnD.Data.Repositories
{
    public class CharacterMainStatsRepository
    {
        private readonly SqlAdapter _adapter;
        public CharacterMainStatsRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<CharacterMainStatsModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterMainStatsModel, dynamic>(
            Procedures.Characters.MainStats.Get,
            new { },
            cancellationToken);
        public async Task<CharacterMainStatsModel> GetByIdAsync(string characterId, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterMainStatsModel, dynamic>(
            Procedures.Characters.MainStats.GetById,
            new { characterId },
            cancellationToken);
        public async Task InsertAsync(CharacterMainStatsModel characterMainStatsModel, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterMainStatsModel, dynamic>(
            Procedures.Characters.MainStats.Insert,
            new
            { 
                characterId = characterMainStatsModel.CHARACTER_ID, 
                strength = characterMainStatsModel.STRENGTH, 
                charisma = characterMainStatsModel.CHARISMA, 
                dexterity = characterMainStatsModel.DEXTERITY,
            constitution = characterMainStatsModel.CONSTITUTION, 
                wisdom = characterMainStatsModel.WISDOM, 
                intelligence = characterMainStatsModel.INTELLIGENCE, 
                level = characterMainStatsModel.LEVEL,
                hp = characterMainStatsModel.HEALTH_POINTS
            },
            cancellationToken);
        public async Task UpdateAsync(CharacterMainStatsModel characterMainStatsModel, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterMainStatsModel, dynamic>(
            Procedures.Characters.MainStats.Update,
            new 
            { 
                characterId = characterMainStatsModel.CHARACTER_ID, 
                strength = characterMainStatsModel.STRENGTH, 
                charisma = characterMainStatsModel.CHARISMA, 
                dexterity = characterMainStatsModel.DEXTERITY,
            constitution = characterMainStatsModel.CONSTITUTION, 
                wisdom = characterMainStatsModel.WISDOM, 
                intelligence = characterMainStatsModel.INTELLIGENCE, 
                level = characterMainStatsModel.LEVEL,
                hp = characterMainStatsModel.HEALTH_POINTS
            },
            cancellationToken);

    }
}
