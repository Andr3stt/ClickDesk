using System.Collections.Generic;

namespace ClickDesk.Models
{
    public class UserAccountDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool EmailVerified { get; set; }
        public List<string> Roles { get; set; }
    }
}
