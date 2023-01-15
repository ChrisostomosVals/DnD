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
    public class CharacterArsenalRepository
    {
        private readonly SqlAdapter _adapter;
        public CharacterArsenalRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<CharacterArsenalModel>> GetAsync(string characterId, CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterArsenalModel, dynamic>(
            Procedures.Characters.Arsenal.Get,
            new { characterId },
            cancellationToken);
        public async Task<CharacterArsenalModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterArsenalModel, dynamic>(
            Procedures.Characters.Arsenal.GetById,
            new { id },
            cancellationToken);
        public async Task InsertAsync(CharacterArsenalModel characterArsenalModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Arsenal.Insert,
            new
            {
                characterId = characterArsenalModel.CHARACTER_ID,
                gearId = characterArsenalModel.GEAR_ID,
                type = characterArsenalModel.TYPE,
                range = characterArsenalModel.RANGE,
                damage = characterArsenalModel.DAMAGE,
                attackBonus = characterArsenalModel.ATTACK_BONUS,
                critical = characterArsenalModel.CRITICAL
            },
            cancellationToken);
        public async Task UpdateAsync(CharacterArsenalModel characterArsenalModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Arsenal.Update,
                new
                {
                    id = characterArsenalModel.ID,
                    gearId = characterArsenalModel.GEAR_ID,
                    type = characterArsenalModel.TYPE,
                    range = characterArsenalModel.RANGE,
                    damage = characterArsenalModel.DAMAGE,
                    attackBonus = characterArsenalModel.ATTACK_BONUS,
                    critical = characterArsenalModel.CRITICAL
                },
            cancellationToken);
        public async Task DeleteAsync(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.Characters.Arsenal.Delete,
               new { id },
           cancellationToken);
    }
}
