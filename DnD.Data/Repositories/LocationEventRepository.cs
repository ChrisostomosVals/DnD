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
    public class LocationEventRepository
    {
        private readonly SqlAdapter _adapter;
        public LocationEventRepository(SqlAdapter adatper)
        {
            _adapter = adatper;
        }
        public async Task<IEnumerable<LocationEventModel>> GetAsync(int locationId, CancellationToken cancellationToken) => await _adapter.FindAsync<LocationEventModel, dynamic>(
            Procedures.Location.Event.Get,
            new { locationId },
            cancellationToken);
        public async Task<LocationEventModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<LocationEventModel, dynamic>(
           Procedures.Location.Event. GetById,
           new { id },
           cancellationToken);
        public async Task CreateAsync(LocationEventModel locationEventModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
         Procedures.Location.Event.Create,
         new
         {
             locationId = locationEventModel.LOCATION_ID,
             description = locationEventModel.DESCRIPTION
         },
         cancellationToken);
        public async Task UpdateAsync(LocationEventModel locationEventModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
        Procedures.Location.Event.Update,
        new
        {
            id = locationEventModel.ID,
            description = locationEventModel.DESCRIPTION
        },
        cancellationToken);
        public async Task DeleteAsync(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
        Procedures.Location.Event.Delete,
        new { id },
        cancellationToken);
    }
}
