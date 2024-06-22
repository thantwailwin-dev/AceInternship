using InternshipDotNetCore.RestApi.Models;
using InternshipDotNetCore.RestApi.Services;
using InternshipDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace InternshipDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoDotNetServiceBlogController : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);        

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";            

            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_blog where BlogId=@BlogId";
            /*AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            parameters[0] = new AdoDotNetParameter("@BlogId", id);*/
            /*_adoDotNetService.Query<BlogModel>(query, parameters);*/

            var blog = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query,new AdoDotNetParameter("@BlogId", id));
           

            if (blog is null)
            {
                return NotFound("No Data Found!");                
            }            

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent])
                             VALUES
                                   (@BlogTitle
                                   ,@BlogAuthor
                                   ,@BlogContent)";           

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";            

            return Ok(message);
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id,BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle,
                               [BlogAuthor] = @BlogAuthor,
                              [BlogContent] = @BlogContent
                         WHERE BlogId=@BlogId";            

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", blog.BlogId),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            string query = "delete from tbl_blog where BlogId = @BlogId";           

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", id)
                );

            string message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            return Ok(message);
        }
    }
}
