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
    public class LocationRepository
    {
        private readonly SqlAdapter _adapter;
        public LocationRepository(SqlAdapter adatper)
        {
            _adapter = adatper;
        }
        public async Task<IEnumerable<LocationModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<LocationModel, dynamic>(
            Procedures.Location.Get,
            new {  },
            cancellationToken);
        public async Task<LocationModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<LocationModel, dynamic>(
           Procedures.Location.GetById,
           new { id },
           cancellationToken);
        public async Task<LocationModel> GetLatestAsync(CancellationToken cancellationToken) => await _adapter.FindOneAsync<LocationModel, dynamic>(
          Procedures.Location.GetLatest,
          new {  },
          cancellationToken);
        public async Task InsertAsync(LocationModel locationModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
          Procedures.Location.GetLatest,
          new 
          { 
              xAxis = locationModel.X_AXIS,
              yAxis = locationModel.Y_AXIS,
              date = locationModel.DATE,
              time = locationModel.TIME,
              year = locationModel.YEAR,
              season = locationModel.SEASON
          },
          cancellationToken);
        public async Task UpdateAsync(LocationModel locationModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
          Procedures.Location.GetLatest,
          new
          {
              id = locationModel.ID,
              xAxis = locationModel.X_AXIS,
              yAxis = locationModel.Y_AXIS,
              date = locationModel.DATE,
              time = locationModel.TIME,
              year = locationModel.YEAR,
              season = locationModel.SEASON
          },
          cancellationToken);
        public async Task DeleteAsync(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
          Procedures.Location.GetLatest,
          new
          { id },
          cancellationToken);
    }
}
