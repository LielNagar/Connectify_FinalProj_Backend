using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Connectify_FinalProj_Backend.DAL;
using Connectify_FinalProj_Backend.Models;

namespace Connectify_FinalProj_Backend.Controllers
{
    public class PostsController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                List<Post> posts = PDAL.getPosts();
                if (posts != null) return Content(HttpStatusCode.OK, posts);
                return Content(HttpStatusCode.NotFound, "No posts at the DB");
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
            
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Post post)
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                if (PDAL.postAPost(post) == 1) return Content(HttpStatusCode.Created,post);
                return Content(HttpStatusCode.BadRequest, post);
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
    }
}