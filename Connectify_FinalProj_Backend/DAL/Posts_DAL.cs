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
        public int deletePost(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createDeletePostCommand(con, id);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createDeletePostCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@postId", id);
            command.CommandText = "spDeletePost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        public List<Post> getUserFavoritePosts(int userId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUserFavoritePostsCommand(con, userId);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Post> posts = new List<Post>();
            while (dr.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(dr["postId"]);
                if (dr["isFav"] != System.DBNull.Value) post.IsFav = true;
                if (dr["isLiked"] != System.DBNull.Value) post.IsLiked = true;
                post.Likes = Convert.ToInt32(dr["likes"]);
                post.Dislikes = Convert.ToInt32(dr["dislikes"]);
                post.Publisher = Convert.ToInt32(dr["publisherId"]);
                post.Content = dr["content"].ToString();
                post.Date = Convert.ToDateTime(dr["date_published"]);
                post.UserName = dr["userName"].ToString();
                posts.Add(post);

            }
            con.Close();
            return posts;
        }
        
        private SqlCommand createGetUserFavoritePostsCommand(SqlConnection con, int userId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@userId", userId);
            command.CommandText = "spGetUserFavoritePosts";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        public int MakeAsUnFavoriteAPost(int postId, int userId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createMakeAsUnFavoriteAPost(con, postId, userId);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }
        private SqlCommand createMakeAsUnFavoriteAPost(SqlConnection con, int postId, int userId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@postId", postId);
            command.Parameters.AddWithValue("@userId", userId);
            command.CommandText = "spMakeAsUnFavoriteAPost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;

        }
        public int MakeAsFavoriteAPost(int postId, int userId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createMakeAsFavoriteAPost(con, postId, userId);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }
        private SqlCommand createMakeAsFavoriteAPost(SqlConnection con, int postId, int userId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@postId", postId);
            command.Parameters.AddWithValue("@userId", userId);
            command.CommandText = "spMakeAsFavoriteAPost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;

        }

        public int UnLikeAPost(int postId, int userId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createUnLikeAPost(con, postId, userId);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }
        private SqlCommand createUnLikeAPost(SqlConnection con, int postId, int userId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@postId", postId);
            command.Parameters.AddWithValue("@userId", userId);
            command.CommandText = "spUnLikeAPost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;

        }
        public int LikeAPost(int postId, int userId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createLikeAPost(con, postId, userId);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createLikeAPost(SqlConnection con, int postId, int userId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@postId", postId);
            command.Parameters.AddWithValue("@userId", userId);
            command.CommandText = "spLikeAPost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;

        }
        public int postAPost(Post post) 
        {
            SqlConnection con = Connect();
            SqlCommand command = createPostAPostCommand(con, post);
            int numAffected = command.ExecuteNonQuery();
            if (numAffected == 1)
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 postId FROM Post ORDER BY postId DESC", con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read()) post.Id = Convert.ToInt32(dr["postId"]);
            }
            con.Close();
            return numAffected;
        }

        private SqlCommand createPostAPostCommand(SqlConnection con, Post post)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@publisher", post.Publisher);
            command.Parameters.AddWithValue("@onWall", post.OnWall);
            command.Parameters.AddWithValue("@content", post.Content);
            command.CommandText = "spPostAPost";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public List<Post> getPostsForWall(int currentId, int userId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetPostsForWallCommand(con, currentId, userId);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Post> posts = new List<Post>();
            while (dr.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(dr["postId"]);
                if (dr["isFav"] != System.DBNull.Value) post.IsFav = true;
                if (dr["isLiked"] != System.DBNull.Value) post.IsLiked = true;
                post.Likes = Convert.ToInt32(dr["likes"]);
                post.Dislikes = Convert.ToInt32(dr["dislikes"]);
                post.Publisher = Convert.ToInt32(dr["publisherId"]);
                post.Content = dr["content"].ToString();
                post.Date = Convert.ToDateTime(dr["date_published"]);
                post.UserName = dr["userName"].ToString();
                posts.Add(post);
            }

            con.Close();
            if (posts != null) return posts;
            return null;
        }

        private SqlCommand createGetPostsForWallCommand(SqlConnection con, int currentId, int userId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@currentId", currentId);
            command.Parameters.AddWithValue("@userId", userId);
            command.CommandText = "spGetPostsForWall";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public List<Post> getPosts(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetPostsCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Post> posts = new List<Post>();
            while (dr.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(dr["postId"]);
                if (dr["isFav"] != System.DBNull.Value) post.IsFav = true;
                if (dr["isLiked"] != System.DBNull.Value) post.IsLiked = true;
                post.Likes = Convert.ToInt32(dr["likes"]);
                post.Dislikes = Convert.ToInt32(dr["dislikes"]);
                post.Publisher = Convert.ToInt32(dr["publisherId"]);
                post.Content = dr["content"].ToString();
                post.Date = Convert.ToDateTime(dr["date_published"]);
                post.UserName = dr["userName"].ToString();
                posts.Add(post);
            }
            con.Close();
            if (posts != null) return posts;
            return null;
        }

        private SqlCommand createGetPostsCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetPosts";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
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