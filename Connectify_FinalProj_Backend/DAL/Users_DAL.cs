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
        public int deleteFriendship(int idCurrent, int idToDelete)
        {
            SqlConnection con = Connect();
            SqlCommand command = createDeleteFriendshipCommand(con, idCurrent, idToDelete);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createDeleteFriendshipCommand(SqlConnection con, int idCurrent, int idToDelete)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@idCurrent", idCurrent);
            command.Parameters.AddWithValue("@idToDelete", idToDelete);
            command.CommandText = "spDeleteFriendship";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public User getUserDetails(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUserDetailsCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            User user = new User();
            while (dr.Read())
            {
                
                user.Id = Convert.ToInt32(dr["id"]);
                user.UserName = dr["userName"].ToString();
                user.ProfileImgUrl = dr["profileImgUrl"].ToString();
                user.Email = dr["email"].ToString();
                user.Gender = Convert.ToBoolean(dr["gender"]);
                user.Birthday = Convert.ToDateTime(dr["birthday"]);
            }
            con.Close();
            return user;
        }

        private SqlCommand createGetUserDetailsCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetUserDetails";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

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
            con.Close();
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

        public List<Request> getUserRequests(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUserRequestsCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Request> userRequests = new List<Request>();
            while (dr.Read())
            {
                Request request = new Request();
                request.Status = dr["status"].ToString();
                request.User1_id = Convert.ToInt32(dr["user1_id"]);
                request.User2_id = Convert.ToInt32(dr["user2_id"]);
                userRequests.Add(request);
            }
            con.Close();
            return userRequests;
        }

        private SqlCommand createGetUserRequestsCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetUserRequests";
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
            con.Close();
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
        
        public int confirmFriendRequest(int idCurrent, int idToConfirm)
        {
            SqlConnection con = Connect();
            SqlCommand command = createConfirmFriendRequestCommand(con, idCurrent, idToConfirm);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createConfirmFriendRequestCommand(SqlConnection con, int idCurrent, int idToConfirm)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@idCurrent", idCurrent);
            command.Parameters.AddWithValue("@idToConfirm", idToConfirm);
            command.CommandText = "spConfirmFriendRequest";
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
        
        public List<User> searchUsers(string name, int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createSearchUsersCommand(con, name, id);
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
            con.Close();
            if (users != null) return users;
            return null;
        }

        private SqlCommand createSearchUsersCommand(SqlConnection con, string name, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@id", id);
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
                con.Close();
                return userToReturn;
            }
            else
            {
                con.Close();
                return null;
            }
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
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 id FROM users ORDER BY id DESC", con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read()) user.Id = Convert.ToInt32(dr["id"]);
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
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@birthday", user.Birthday);
            command.Parameters.AddWithValue("@gender", user.Gender);
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