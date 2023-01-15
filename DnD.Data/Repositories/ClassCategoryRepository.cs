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
    public class ClassCategoryRepository
    {
        private readonly SqlAdapter _adapter;
        public ClassCategoryRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<ClassCategoryModel>> GetAsync(CancellationToken cancellationToken) => await _adapter.FindAsync<ClassCategoryModel, dynamic>(
           Procedures.ClassCategory.Get,
           new {  },
           cancellationToken);
        public async Task<ClassCategoryModel> GetByIdAsync(int id, CancellationToken cancellationToken) => await _adapter.FindOneAsync<ClassCategoryModel, dynamic>(
           Procedures.ClassCategory.Get,
           new { id },
           cancellationToken);
    }
}
