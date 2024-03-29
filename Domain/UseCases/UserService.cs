﻿using Domain.Logic;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class UserService
    {
        private readonly IUserRepository _db;

        public UserService(IUserRepository db)
        {
            _db = db;
        }

        public Result<User> Register(User user)
        {
            var result = user.IsValid();
            if (result.IsFailure)
                return Result.Fail<User>("Invalid user: " + result.Error);

            if (_db.IsUserExist(user.UserName))
                return Result.Fail<User>("Username already exists");


            return _db.Create(user) ? Result.Ok(user) : Result.Fail<User>("Unable to create user");
        }

        public Result<User> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Invalid login");

            var user = _db.GetUserByLogin(login);

            return user != null ? Result.Ok(user) : Result.Fail<User>("Unable to find user");
        }

        public Result<User> GetUserById(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return Result.Fail<User>("Invalid id");

            var user = _db.GetUserByID(id);

            return user != null ? Result.Ok(user) : Result.Fail<User>("Unable to find user");
        }

        public Result<bool> IsUserExists(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<bool>("Invalid login");

            return Result.Ok(_db.IsUserExist(login));
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
