using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify_FinalProj_Backend.Models
{
    public class Post
    {
        private int id;
        private int publisher;
        private string userName;
        private DateTime date;
        private int likes;
        private int dislikes;
        private string content;
        private int onWall;

        public Post(int publisher, string userName, DateTime date, int likes, int dislikes, string content, int onWall)
        {
            this.onWall = onWall;
            this.UserName = userName;
            this.Publisher = publisher;
            this.Date = date;
            this.Likes = likes;
            this.Dislikes = dislikes;
            this.Content = content;
        }

        public Post() { }
        public int Publisher { get => publisher; set => publisher = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Likes { get => likes; set => likes = value; }
        public int Dislikes { get => dislikes; set => dislikes = value; }
        public string Content { get => content; set => content = value; }
        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public int OnWall { get => onWall; set => onWall = value; }
    }
}