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
    public class CharacterPropRepository
    {
        private readonly SqlAdapter _adapter;
        public CharacterPropRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<CharacterPropModel>> GetAsync(string characterId, CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterPropModel, dynamic>(
            Procedures.Characters.Properties.Get,
            new { characterId },
            cancellationToken);
        public async Task<CharacterPropModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterPropModel, dynamic>(
            Procedures.Characters.Properties.GetById,
            new { id },
            cancellationToken);
        public async Task<CharacterPropModel> GetByTypeAsync(string characterId, string type, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterPropModel, dynamic>(
          Procedures.Characters.Properties.GetByType,
          new { characterId, type },
          cancellationToken);
        public async Task InsertAsync(CharacterPropModel characterPropModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Properties.Insert,
            new
            {
                characterId = characterPropModel.CHARACTER_ID,
                name = characterPropModel.NAME,
                value = characterPropModel.VALUE,
                type = characterPropModel.TYPE,
                description = characterPropModel.DESCRIPTION,
            },
            cancellationToken);
        public async Task UpdateAsync(CharacterPropModel characterPropModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Properties.Update,
                new
                {
                    id = characterPropModel.ID,
                    name = characterPropModel.NAME,
                    value = characterPropModel.VALUE,
                    type = characterPropModel.TYPE,
                    description = characterPropModel.DESCRIPTION,
                },
            cancellationToken);
        public async Task DeleteAsync(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.Characters.Properties.Delete,
               new { id },
           cancellationToken);
    }
}
