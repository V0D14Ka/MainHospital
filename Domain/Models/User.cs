using System.Security.Principal;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

    }
}