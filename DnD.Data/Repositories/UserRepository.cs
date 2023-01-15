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
    public class UserRepository
    {
        private readonly SqlAdapter _adapter;
        public UserRepository(SqlAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<IEnumerable<UserModel>> GetAsync(string role, CancellationToken cancellationToken) => await _adapter.FindAsync<UserModel, dynamic>(
            Procedures.User.Get,
            new { role },
            cancellationToken);
        public async Task<UserModel> GetByIdAsync(string id, CancellationToken cancellationToken = default) => await _adapter.FindOneAsync<UserModel, dynamic>(
          Procedures.User.GetById,
          new { id },
          cancellationToken);
        public async Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default) => await _adapter.FindOneAsync<bool, dynamic>(
          Procedures.User.CheckEmail,
          new { email },
          cancellationToken);
        public async Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default) => await _adapter.FindOneAsync<UserModel, dynamic>(
         Procedures.User.GetByEmail,
         new { email },
         cancellationToken);
        public async Task<UserModel> ValidateAsync(string email, string password, CancellationToken cancellationToken = default) => await _adapter.FindOneAsync<UserModel, dynamic>(
          Procedures.User.UserValidate,
          new { email, password },
          cancellationToken);
        public async Task InsertAsync(UserModel userModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
      Procedures.User.Insert,
      new
      {
          id = userModel.ID,
          characterId = userModel.CHARACTER_ID,
          name = userModel.NAME,
          email = userModel.EMAIL,
          password = userModel.PASSWORD,
      },
      cancellationToken);
        public async Task UpdateAsync(UserModel userModel, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
        Procedures.User.Update,
        new
        {
            id = userModel.ID,
            characterId = userModel.CHARACTER_ID,
            name = userModel.NAME,
            email = userModel.EMAIL,
        },
        cancellationToken);
        public async Task ChangePasswordAsync(string id, string newPassword, CancellationToken cancellationToken) => await _adapter.SaveAsync<dynamic>(
        Procedures.User.UpdatePassword,
        new
        {
            id,
            newPassword,
        },
        cancellationToken);

    }
}
