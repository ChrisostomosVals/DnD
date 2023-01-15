using DataAdapter.Sql;
using DnD.Data.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DnD.Data.Repositories
{
    public class UserRoleRepository
    {
        private readonly SqlAdapter _adapter;
        public UserRoleRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<Data.Models.UserRoleModel> GetAsync(int id, CancellationToken cancellationToken=default) => await _adapter.FindOneAsync<Data.Models.UserRoleModel, dynamic>(
            Procedures.UserRole.Get,
            new { id },
            cancellationToken);
    }
}
