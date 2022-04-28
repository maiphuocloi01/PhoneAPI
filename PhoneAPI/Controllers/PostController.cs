using PhoneAPI.Models.DAO;
using PhoneAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PhoneAPI.Controllers
{
    public class PostController : ApiController
    {
        [Route("Api/PostController/GetAllPost")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPost()
        {
            return Ok(await PostDAO.Instance.GetAllPost());
        }
        [Route("Api/PostController/AddPost")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddCategory(PostDTO postDTO)
        {
            return Ok(await PostDAO.Instance.AddPost(postDTO));
        }

        [Route("Api/PostController/UpdatePost")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePost(PostDTO postDTO)
        {
            return Ok(await PostDAO.Instance.UpdatePost(postDTO));
        }

        [Route("Api/PostController/DeletePost/{Id}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePost(int Id)
        {
            return Ok(await PostDAO.Instance.DeletePost(Id));
        }

        [Route("Api/PostController/UploadPostImage")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UploadPostImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/Images/Post/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}