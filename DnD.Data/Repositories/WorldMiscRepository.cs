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
    public class WorldMiscRepository
    {
        private readonly SqlAdapter _adapter;

        public WorldMiscRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<WorldMiscModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<WorldMiscModel, dynamic>(
            Procedures.WorldMisc.Get,
            new { },
            cancellationToken);
        public async Task<WorldMiscModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<WorldMiscModel, dynamic>(
            Procedures.WorldMisc.GetById,
            new { id },
            cancellationToken);
        public async Task<IEnumerable<WorldMiscModel>> GetByDependIdAsync(int dependId, CancellationToken cancellationToken) => await _adapter.FindAsync<WorldMiscModel, dynamic>(
           Procedures.WorldMisc.GetByDependId,
           new { dependId },
           cancellationToken);
        public async Task InsertAsync(WorldMiscModel worldMiscModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.WorldMisc.GetByDependId,
           new 
           { 
               property = worldMiscModel.PROPERTY,
               value = worldMiscModel.VALUE, 
               dependId = worldMiscModel.DEPEND_ID,
               dependLocation = worldMiscModel.DEPEND_LOCATION 
           },
           cancellationToken);
        public async Task UpdateAsync(WorldMiscModel worldMiscModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
           Procedures.WorldMisc.GetByDependId,
           new 
           { 
               id = worldMiscModel.ID,
               value = worldMiscModel.VALUE,
               property = worldMiscModel.PROPERTY
           },
           cancellationToken);
    }
}
