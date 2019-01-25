using System;
using System.Collections.Generic;
using System.Text;

namespace NasGrad.DBEngine
{
    public class NasGradUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string RoleId { get; set; }
    }
}
