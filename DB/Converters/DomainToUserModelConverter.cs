using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters
{
    public static class DomainToUserModelConverter
    {
        public static UserModel? ToUserModel(this User model)
        {
            return new UserModel
            {
                Id = model.Id,
                UserName = model.UserName,
                Role = model.Role,
                Name = model.Name,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            };
        }
    }
}
