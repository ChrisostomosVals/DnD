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
    public class CharacterGearRepository
    {
        private readonly SqlAdapter _adapter;
        public CharacterGearRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<CharacterGearModel>> GetAsync(string characterId, CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterGearModel, dynamic>(
            Procedures.Characters.Gear.Get,
            new { characterId },
            cancellationToken);
        public async Task<CharacterGearModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterGearModel, dynamic>(
            Procedures.Characters.Gear.GetById,
            new { id },
            cancellationToken);
        public async Task<bool> CheckGearAsync(string characterId, string name, CancellationToken cancellationToken) => Convert.ToBoolean(await _adapter.FindOneAsync<bool, dynamic>(
            Procedures.Characters.Gear.CheckGear,
            new { characterId, name },
            cancellationToken));
        public async Task<CharacterGearModel> GetMoneyByCharacterIdAsync(string characterId, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterGearModel, dynamic>(
           Procedures.Characters.Gear.GetMoney,
           new { characterId },
           cancellationToken);

        public async Task InsertItemAsync(CharacterGearModel characterGeatModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Gear.InsertItem,
            new 
            {
                characterId = characterGeatModel.CHARACTER_ID, 
                name = characterGeatModel.NAME, 
                quantity = characterGeatModel.QUANTITY,
                weight = characterGeatModel.WEIGHT
            },
            cancellationToken);
        public async Task UpdateItemAsync(CharacterGearModel characterGeatModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.Characters.Gear.UpdateItem,
           new {
               id = characterGeatModel.ID, 
               name = characterGeatModel.NAME, 
               quantity = characterGeatModel.QUANTITY,
               weight = characterGeatModel.WEIGHT
           },
           cancellationToken);
        public async Task TransferItemAsync(CharacterGearModel characterGeatModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
          Procedures.Characters.Gear.TransferItem,
          new
          {
              id = characterGeatModel.ID,
              characterId = characterGeatModel.CHARACTER_ID,
              name = characterGeatModel.NAME,
              quantity = characterGeatModel.QUANTITY,
              weight = characterGeatModel.WEIGHT
          },
          cancellationToken);
        public async Task UpdateQuantityAsync(CharacterGearModel characterGeatModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.Characters.Gear.UpdateQuantity,
           new { id = characterGeatModel.ID, quantity = characterGeatModel.QUANTITY },
           cancellationToken);
        public async Task DeleteItemAsync(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.Characters.Gear.DeleteItem,
           new { id },
           cancellationToken);
    }
}
