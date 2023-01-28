﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Connectify_FinalProj_Backend.DAL;
using Connectify_FinalProj_Backend.Models;

namespace Connectify_FinalProj_Backend.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] User user)
        {
            try{
                Users_DAL UDAL = new Users_DAL();
                if (UDAL.postUser(user) == 1) return Content(HttpStatusCode.Created, user);
                return Content(HttpStatusCode.BadGateway, "Couldn't post user");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/Users/login")]
        public IHttpActionResult PostLogin(User user)
        {
            try
            {
                Users_DAL UDAL = new Users_DAL();
                User userToReturn = UDAL.getUserLogin(user);
                if (userToReturn != null) return Content(HttpStatusCode.OK, userToReturn);
                return Content(HttpStatusCode.NotFound, "Credentials are incorrect");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpGet]
        [Route("api/Users/search/{name}")]
        public IHttpActionResult Search(string name)
        {
            try
            {
                Users_DAL UDAL = new Users_DAL();
                List<User> usersToReturn = UDAL.searchUsers(name);
                if (usersToReturn != null) return Content(HttpStatusCode.OK, usersToReturn);
                return Content(HttpStatusCode.NotFound, "There are no users with this name");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpPost]
        [Route("api/Users/friend/{idCurrent}/{idToAdd}")]
        public IHttpActionResult AddFriend(int idCurrent, int idToAdd)
        {
            try
            {
                Users_DAL UDAL = new Users_DAL();
                if(UDAL.addFriend(idCurrent, idToAdd)==1) return Content(HttpStatusCode.Created,"Friend request sent!");
                return Content(HttpStatusCode.BadRequest, "Cannot send a friend request");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpGet]
        [Route("api/Users/{id}/Friends")]
        public IHttpActionResult getUserFriends(int id)
        {
            try
            {
                Users_DAL UDAL = new Users_DAL();
                List<User> userFriends = UDAL.getUserFriends(id);
                if (userFriends != null) return Content(HttpStatusCode.OK, userFriends);
                return Content(HttpStatusCode.NotFound, "Maybe you should add some friends");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpGet]
        [Route("api/Users/{id}/Friends/Pending")]
        public IHttpActionResult getUserPendingRequests(int id)
        {
            try
            {
                Users_DAL UDAL = new Users_DAL();
                List<User> userFriends = UDAL.getUserPendingRequests(id);
                if (userFriends != null) return Content(HttpStatusCode.OK, userFriends);
                return Content(HttpStatusCode.NotFound, "No pending friend requests");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }
    }
}