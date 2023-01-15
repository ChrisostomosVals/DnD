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
    public class RaceRepository
    {
        private readonly SqlAdapter _adapter;
        public RaceRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<RaceModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<RaceModel, dynamic>(
           Procedures.Race.Get,
           new { },
           cancellationToken);
        public async Task<IEnumerable<RaceModel>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken) => await _adapter.FindAsync<RaceModel, dynamic>(
           Procedures.Race.GetByCategoryId,
           new { categoryId },
           cancellationToken);
        public async Task<RaceModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<RaceModel, dynamic>(
            Procedures.Race.GetById,
            new { id },
            cancellationToken);
    }
}
