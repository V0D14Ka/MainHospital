using System.Security.Principal;

namespace Domain
{
    public class User
    {
        public int ID { get; set; }
        public int Phone { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }

    }
}