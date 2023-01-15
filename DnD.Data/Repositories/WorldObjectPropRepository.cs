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
    public class WorldObjectPropRepository
    {
        private readonly SqlAdapter _adapter;
        public WorldObjectPropRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<WorldObjectPropModel>> GetAsync(int worldId, CancellationToken cancellationToken) => await _adapter.FindAsync<WorldObjectPropModel, dynamic>(
            Procedures.WorldObject.Prop.GetByWorldId,
            new { worldId },
            cancellationToken);
        public async Task<WorldObjectPropModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<WorldObjectPropModel, dynamic>(
            Procedures.WorldObject.Prop.GetById,
            new { id },
            cancellationToken);
        public async Task<WorldObjectPropModel> GetMapAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<WorldObjectPropModel, dynamic>(
            Procedures.WorldObject.Prop.GetMap,
            new { id },
            cancellationToken);
        public async Task InsertAsync(WorldObjectPropModel worldObjectPropModel, CancellationToken cancellationToken) => await _adapter.SaveAsync(
            Procedures.WorldObject.Prop.Insert,
            new 
            { 
                worldId = worldObjectPropModel.WORLD_OBJECT_ID, 
                property = worldObjectPropModel.PROPERTY, 
                value = worldObjectPropModel.VALUE 
            },
            cancellationToken);
        public async Task UpdateAsync(WorldObjectPropModel worldObjectPropModel, CancellationToken cancellationToken) => await _adapter.FindOneAsync<WorldObjectPropModel, dynamic>(
            Procedures.WorldObject.Prop.Update,
            new
            { 
                id = worldObjectPropModel.ID, 
                property = worldObjectPropModel.PROPERTY, 
                value = worldObjectPropModel.VALUE 
            },
            cancellationToken);
        public async Task DeleteAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<WorldObjectPropModel, dynamic>(
           Procedures.WorldObject.Prop.Delete,
           new { id },
           cancellationToken);
    }
}
