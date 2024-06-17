using InternshipDotNetCore.RestApi.Db;
using InternshipDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCoreBlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EFCoreBlogController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            _appDbContext.Add(blog);
            var result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";            
            return Ok(message);
        }

        [HttpGet]
        public IActionResult Read()
        {
            List<BlogModel> lst = _appDbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound("No Data Fond!");
            }
            return Ok(blog);
        }
        

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blogModel)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound("No Data Fond!");
            }

            blog.BlogTitle = blogModel.BlogTitle;
            blog.BlogAuthor = blogModel.BlogAuthor;
            blog.BlogContent = blogModel.BlogContent;

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id,BlogModel blogModel)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound("No Data Fond!");
            }

            if (!string.IsNullOrEmpty(blogModel.BlogTitle))
            {
                blog.BlogTitle = blogModel.BlogTitle;
            }

            if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
            {
                blog.BlogAuthor = blogModel.BlogAuthor;

            }

            if (!string.IsNullOrEmpty(blogModel.BlogContent))
            {
                blog.BlogContent = blogModel.BlogContent;
            }

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound("No Data Fond!");
            }            
            _appDbContext.Remove(blog);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            return Ok(message);            
        }
    }
}
