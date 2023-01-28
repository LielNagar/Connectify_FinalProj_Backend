using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify_FinalProj_Backend.Models
{
    public class User
    {
        private int id;
        private string userName;
        private string email;
        private string location;
        private string password;
        private string profileImgUrl;

        public User(int id, string userName, string email, string location, string password, string profileImgUrl)
        {
            this.id = id;
            this.userName = userName;
            this.email = email;
            this.location = location;
            this.password = password;
            this.profileImgUrl = profileImgUrl;
        }

        public User() { }

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Email { get => email; set => email = value; }
        public string Location { get => location; set => location = value; }
        public string Password { get => password; set => password = value; }
        public string ProfileImgUrl { get => profileImgUrl; set => profileImgUrl = value; }
    }
}