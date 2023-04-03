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
            return null;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                List<Post> posts = PDAL.getPosts(id);
                if (posts != null) return Content(HttpStatusCode.OK, posts);
                return Content(HttpStatusCode.NotFound, "No posts at the DB");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpGet]
        [Route("api/Posts/Wall/{userId}")]
        public IHttpActionResult GetForWall(int userId)
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                List<Post> posts = PDAL.getPostsForWall(userId);
                if (posts != null) return Content(HttpStatusCode.OK, posts);
                return Content(HttpStatusCode.NotFound, "No posts at the DB");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpGet]
        [Route("api/Posts/{userId}/Favorite")]
        public IHttpActionResult getUserFavoritePosts(int userId)
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                List<Post> posts = PDAL.getUserFavoritePosts(userId);
                if (posts.Count > 0) return Content(HttpStatusCode.OK, posts);
                return Content(HttpStatusCode.NotFound, "How about to mark some posts?");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Post post)
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                if (PDAL.postAPost(post) == 1) return Content(HttpStatusCode.Created, post);
                return Content(HttpStatusCode.BadRequest, post);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }
        }

        [HttpPost]
        [Route("api/Posts/Likes/{postId}/{userId}")]
        public IHttpActionResult LikeAPost(int postId, int userId)
        {
            Posts_DAL PDAL = new Posts_DAL();
            if (PDAL.LikeAPost(postId, userId) == 2) return Content(HttpStatusCode.Created, postId);
            return Content(HttpStatusCode.BadRequest, postId);
        }

        [HttpPost]
        [Route("api/Posts/Favorite/{postId}/{userId}")]
        public IHttpActionResult MakeAsFavoriteAPost(int postId, int userId)
        {
            Posts_DAL PDAL = new Posts_DAL();
            if (PDAL.MakeAsFavoriteAPost(postId, userId) == 1) return Content(HttpStatusCode.Created, postId);
            return Content(HttpStatusCode.BadRequest, postId);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id)
        {
            //NO NEED FOR THIS AT THE MOMENT
            return null;
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Posts_DAL PDAL = new Posts_DAL();
                if (PDAL.deletePost(id) > 0) return Content(HttpStatusCode.OK, "Post has been deleted");
                return Content(HttpStatusCode.BadRequest, "Cannot delete post");
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.BadGateway, e.Message);
            }

        }

        [HttpDelete]
        [Route("api/Posts/Likes/{postId}/{userId}")]
        public IHttpActionResult UnLikeAPost(int postId, int userId)
        {
            Posts_DAL PDAL = new Posts_DAL();
            if (PDAL.UnLikeAPost(postId, userId) == 2) return Content(HttpStatusCode.OK, postId);
            return Content(HttpStatusCode.BadRequest, postId);
        }

        [HttpDelete]
        [Route("api/Posts/Favorite/{postId}/{userId}")]
        public IHttpActionResult MakeAsUnFavoriteAPost(int postId, int userId)
        {
            Posts_DAL PDAL = new Posts_DAL();
            if (PDAL.MakeAsUnFavoriteAPost(postId, userId) == 1) return Content(HttpStatusCode.OK, postId);
            return Content(HttpStatusCode.BadRequest, postId);
        }

        [HttpGet]
        [Route("api/Posts/initTheDBwithUsersPosts")]
        public IHttpActionResult initTheDBWithUsersPosts()
        {
            Posts_DAL PDAL = new Posts_DAL();
            if (PDAL.initTheDBWithPostsForUsers() > 0) return Content(HttpStatusCode.Created, "Successfully initialized the DB with users posts");
            else return Content(HttpStatusCode.BadRequest, "Something Bad Happened");
        }

    }
}