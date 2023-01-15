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
    public class CharacterRepository
    {
        private readonly SqlAdapter _adapter;
        public CharacterRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<CharacterModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterModel, dynamic>(
            Procedures.Characters.Get,
            new { },
            cancellationToken);
        public async Task<CharacterModel> GetByIdAsync(string id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterModel, dynamic>(
            Procedures.Characters.GetById,
            new { id },
            cancellationToken);
        public async Task<IEnumerable<CharacterModel>> GetBossAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterModel, dynamic>(
            Procedures.Characters.GetBoss,
            new { },
            cancellationToken);
        public async Task<IEnumerable<CharacterModel>> GetHeroAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterModel, dynamic>(
            Procedures.Characters.GetHero,
            new { },
            cancellationToken);
        public async Task<IEnumerable<CharacterModel>> GetHostileAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterModel, dynamic>(
            Procedures.Characters.GetHostile,
            new { },
            cancellationToken);
        public async Task<IEnumerable<CharacterModel>> GetNpcAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterModel, dynamic>(
            Procedures.Characters.GetnNpc,
            new { },
            cancellationToken);
        public async Task CreateAsync(CharacterModel character, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Create,
            new { id = character.ID, name = character.NAME, classId = character.CLASS_ID, type = character.TYPE, raceId = character.RACE_ID, gender = character.GENDER },
            cancellationToken);
        public async Task UpdateAsync(CharacterModel character, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Update,
            new { id = character.ID, name = character.NAME, classId = character.CLASS_ID, type = character.TYPE, raceId = character.RACE_ID, gender = character.GENDER },
            cancellationToken);
        public async Task Delete(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.Characters.Delete,
           new { id },
           cancellationToken);
    }
}
