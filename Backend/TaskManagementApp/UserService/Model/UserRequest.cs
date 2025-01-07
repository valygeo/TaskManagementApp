using System;
using System.Collections.Generic;
using System.Text;

namespace UserServices.Model
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsDeleted { get; set; }
    }
}
