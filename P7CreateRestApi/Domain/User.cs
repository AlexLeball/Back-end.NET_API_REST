using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
    }
}