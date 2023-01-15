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
    public class ClassRepository
    {
        private readonly SqlAdapter _adapter;
        public ClassRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<ClassModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<ClassModel, dynamic>(
           Procedures.Class.Get,
           new {  },
           cancellationToken);
        public async Task<IEnumerable<ClassModel>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken) => await _adapter.FindAsync<ClassModel, dynamic>(
           Procedures.Class.GetByCategoryId,
           new { categoryId },
           cancellationToken);
        public async Task<ClassModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<ClassModel, dynamic>(
            Procedures.Class.GetById,
            new { id },
            cancellationToken);
    }
}
