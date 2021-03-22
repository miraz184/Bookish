using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class User
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
