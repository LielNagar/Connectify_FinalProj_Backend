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
    public class Users_DAL
    {
        public List<User> getUserPendingRequests(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUserPendingRequestsCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> userFriends = new List<User>();
            while (dr.Read())
            {
                User user = new User();
                user.Id = Convert.ToInt32(dr["id"]);
                user.UserName = dr["userName"].ToString();
                user.ProfileImgUrl = dr["profileImgUrl"].ToString();
                userFriends.Add(user);
            }
            return userFriends;
        }

        private SqlCommand createGetUserPendingRequestsCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetUserPendingRequests";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public List<User> getUserFriends(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUserFriendsCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> userFriends = new List<User>();
            while (dr.Read())
            {
                User user = new User();
                user.Id = Convert.ToInt32(dr["user2_id"]);
                user.UserName = dr["userName"].ToString();
                user.ProfileImgUrl = dr["profileImgUrl"].ToString();
                userFriends.Add(user);
            }
            return userFriends;
        }

        private SqlCommand createGetUserFriendsCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetUserFriends";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public int addFriend(int idCurrent, int idToAdd)
        {
            SqlConnection con = Connect();
            SqlCommand command = createAddFriendCommand(con, idCurrent, idToAdd);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createAddFriendCommand(SqlConnection con, int idCurrent, int idToAdd)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@idCurrent", idCurrent);
            command.Parameters.AddWithValue("@idToAdd", idToAdd);
            command.CommandText = "spPostAddFriend";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        
        public List<User> searchUsers(string name)
        {
            SqlConnection con = Connect();
            SqlCommand command = createSearchUsersCommand(con, name);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> users = new List<User>();
            while (dr.Read())
            {
                User user = new User();
                user.Id = Convert.ToInt32(dr["id"]);
                user.UserName = dr["userName"].ToString();
                user.Email = dr["email"].ToString();
                user.Location = dr["location"].ToString();
                user.ProfileImgUrl = dr["profileImgUrl"].ToString();
                users.Add(user);
            }
            if (users != null) return users;
            return null;
        }

        private SqlCommand createSearchUsersCommand(SqlConnection con, string name)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@name", name);
            command.CommandText = "spGetSearchUsers";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public User getUserLogin(User user)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUserLoginCommand(con, user);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            User userToReturn = new User();
            if (dr.Read())
            {
                userToReturn.Id = Convert.ToInt32(dr["id"]);
                userToReturn.Location = dr["location"].ToString();
                userToReturn.Email = dr["email"].ToString();
                userToReturn.Password = dr["password"].ToString();
                userToReturn.ProfileImgUrl = dr["profileImgUrl"].ToString();
                userToReturn.UserName = dr["userName"].ToString();
                return userToReturn;
            }
            else return null;
        }

        private SqlCommand createGetUserLoginCommand(SqlConnection con, User user)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);
            command.CommandText = "spGetUserLogin";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public int postUser(User user)
        {
            SqlConnection con = Connect();
            SqlCommand command = createPostUserCommand(con, user);
            int numAffected = command.ExecuteNonQuery();
            if (numAffected == 1)
            {

            }
            con.Close();
            return numAffected;
        }

        private SqlCommand createPostUserCommand(SqlConnection con, User user)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@location", user.Location);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@userName", user.UserName);
            command.Parameters.AddWithValue("@profileImgUrl", user.ProfileImgUrl);
            command.Parameters.AddWithValue("@email", user.Email);
            command.CommandText = "spPostUser";
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