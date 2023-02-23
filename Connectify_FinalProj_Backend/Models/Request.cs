using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify_FinalProj_Backend.Models
{
    public class Request
    {
        private string status;
        private int user1_id;
        private int user2_id;

        public Request(string status, int user1_id, int user2_id)
        {
            this.Status = status;
            this.User1_id = user1_id;
            this.User2_id = user2_id;
        }

        public Request() { }

        public string Status { get => status; set => status = value; }
        public int User1_id { get => user1_id; set => user1_id = value; }
        public int User2_id { get => user2_id; set => user2_id = value; }
    }
}