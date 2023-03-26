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
        private string firstName;
        private string lastName;
        private string email;
        private string location;
        private string password;
        private string profileImgUrl;
        private DateTime birthday;
        private short gender;
        private List<User> friends;

        public User(int id, string userName, string email, string location, string password, string profileImgUrl, DateTime birthday, short gender, string firstName, string lastName)
        {
            this.Birthday = birthday;
            this.gender = gender;
            this.id = id;
            this.userName = userName;
            this.email = email;
            this.location = location;
            this.password = password;
            this.profileImgUrl = profileImgUrl;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public User() { }

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Email { get => email; set => email = value; }
        public string Location { get => location; set => location = value; }
        public string Password { get => password; set => password = value; }
        public string ProfileImgUrl { get => profileImgUrl; set => profileImgUrl = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public short Gender { get => gender; set => gender = value; }
        public List<User> Friends { get => friends; set => friends = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
    }
}