using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Connectify_FinalProj_Backend.Models;

namespace Connectify_FinalProj_Backend.DAL
{
    public class Posts_DAL
    {
        public int postAPost(Post post) 
        {
            SqlConnection con = Connect();
            SqlCommand command = createPostAPostCommand(con, post);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createPostAPostCommand(SqlConnection con, Post post)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@publisher", post.Publisher);
            command.Parameters.AddWithValue("@content", post.Content);
            command.CommandText = "spPostAPost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public List<Post> getPosts()
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand("SELECT * FROM Post ORDER BY postId DESC", con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Post> posts = new List<Post>();
            while (dr.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(dr["postId"]);
                post.Likes = Convert.ToInt32(dr["likes"]);
                post.Dislikes = Convert.ToInt32(dr["dislikes"]);
                post.Publisher = Convert.ToInt32(dr["publisherId"]);
                post.Content = dr["content"].ToString();
                post.Date = Convert.ToDateTime(dr["date_published"]);
                posts.Add(post);
            }
            con.Close();
            if (posts != null) return posts;
            return null;
        }

        private SqlConnection Connect() //CONNECTION TO DB FUNCTION
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);
            // open the database connection
            con.Open();
            return con;
        }
    }
}