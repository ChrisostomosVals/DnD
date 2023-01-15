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
    public class WorldObjectRepository
    {
        private readonly SqlAdapter _adapter;

        public WorldObjectRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }

        public async Task<IEnumerable<WorldObjectModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<WorldObjectModel, dynamic>(
            Procedures.WorldObject.Get,
            new { },
            cancellationToken);
        public async Task<IEnumerable<WorldObjectModel>> GetMapAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<WorldObjectModel, dynamic>(
            Procedures.WorldObject.GetMap,
            new { },
            cancellationToken);
        public async Task<WorldObjectModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<WorldObjectModel, dynamic>(
           Procedures.WorldObject.GetById,
           new { id },
           cancellationToken);
        public async Task CreateAsync(WorldObjectModel worldObjectModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
           Procedures.WorldObject.Create,
           new 
           { 
               name = worldObjectModel.NAME, 
               type = worldObjectModel.TYPE, 
               description = worldObjectModel.DESCRIPTION
           },
           cancellationToken);
        public async Task Update(WorldObjectModel worldObjectModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
           Procedures.WorldObject.Update,
           new 
           { 
               id= worldObjectModel.ID, 
               name = worldObjectModel.NAME, 
               type = worldObjectModel.TYPE, 
               description = worldObjectModel.DESCRIPTION
           },
           cancellationToken);
        public async Task Delete(int id, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
           Procedures.WorldObject.Delete,
           new { id },
           cancellationToken);
    }
}
