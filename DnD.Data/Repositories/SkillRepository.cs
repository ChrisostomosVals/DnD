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
    public class SkillRepository
    {
        private readonly SqlAdapter _adapter;

        public SkillRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<SkillModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<SkillModel, dynamic>(Procedures.Skill.Get, new { }, cancellationToken);
        public async Task<SkillModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<SkillModel, dynamic>(Procedures.Skill.GetById, new { id }, cancellationToken);

    }
}
