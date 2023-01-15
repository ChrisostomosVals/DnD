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
    public class CharacterSkillRepository
    {
        private readonly SqlAdapter _adapter;
        public CharacterSkillRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<CharacterSkillModel>> GetByIdAsync(string characterId, CancellationToken cancellationToken) => await _adapter.FindAsync<CharacterSkillModel, dynamic>(Procedures.Characters.Skill.Get, new { characterId }, cancellationToken);
        public async Task<CharacterSkillModel> GetBySkillIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterSkillModel, dynamic>(
            Procedures.Characters.Skill.GetById,
            new { id },
            cancellationToken);
        public async Task<CharacterSkillModel> GetBySkillIdAndCharacterIdAsync(string characterId, int skillId, CancellationToken cancellationToken) => await _adapter.FindOneAsync<CharacterSkillModel, dynamic>(
            Procedures.Characters.Skill.GetBySkillIdAndCharacterId,
            new { characterId, skillId },
            cancellationToken);
        public async Task InsertSkill(CharacterSkillModel characterSkillModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Skill.Insert,
            new 
            { 
                characterId = characterSkillModel.CHARACTER_ID, 
                skillId = characterSkillModel.SKILL_ID, 
                abilityMod = characterSkillModel.ABILITY_MOD, 
                trained = characterSkillModel.TRAINED, 
                ranks = characterSkillModel.RANKS,
                miscMod = characterSkillModel.MISC_MOD },
            cancellationToken);
        public async Task UpdateSkill(CharacterSkillModel characterSkillModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.Characters.Skill.Update,
            new 
            { 
                id = characterSkillModel.ID,
                abilityMod = characterSkillModel.ABILITY_MOD, 
                trained = characterSkillModel.TRAINED,
                ranks = characterSkillModel.RANKS,
                miscMod = characterSkillModel.MISC_MOD },
            cancellationToken);

    }
}
