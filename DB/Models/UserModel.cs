namespace DB.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}