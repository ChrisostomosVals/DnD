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
    public class RaceCategoryRepository
    {
        private readonly SqlAdapter _adapter;
        public RaceCategoryRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<RaceCategoryModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<RaceCategoryModel, dynamic>(
           Procedures.RaceCategory.Get,
           new { },
           cancellationToken);
        public async Task<RaceCategoryModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<RaceCategoryModel, dynamic>(
            Procedures.RaceCategory.GetById,
            new { id },
            cancellationToken);
    }
}
