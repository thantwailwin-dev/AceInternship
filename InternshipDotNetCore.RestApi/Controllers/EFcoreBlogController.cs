using InternshipDotNetCore.RestApi.Db;
using InternshipDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InternshipDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCoreBlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<EFCoreBlogController> _logger;

        public EFCoreBlogController(ILogger<EFCoreBlogController> logger)
        {
            _logger = logger;
            _appDbContext = new AppDbContext();

        }

       /* public EFCoreBlogController()
        {
            _appDbContext = new AppDbContext();
        }*/

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            _appDbContext.Add(blog);
            var result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";            
            return Ok(message);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        [HttpGet("pageNo/{pageNo}/pageSize/{pageSize}")]
        public IActionResult Read(int pageNo,int pageSize)        
        {
			int rowCount = _appDbContext.Blogs.Count();
			int pageCount = rowCount / pageSize;

			if (rowCount % pageSize > 0)			
				pageCount++;

            if(pageNo > pageCount)
            {
                return BadRequest(new { Message = "Invalid PageNo." });
            }
			

			List<BlogModel> lst = _appDbContext.Blogs
                .OrderByDescending(x => x.BlogId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
			.ToList();					


			BlogResponseModel model = new BlogResponseModel();
            model.Data = lst;
            model.PageNo = pageNo;
            model.PageSize = pageSize;
            model.PageCount = pageCount;
            /*model.IsEndOfPage = pageNo == pageCount;*/

            _logger.LogInformation("Count is" + model.PageCount.ToString());
            _logger.LogInformation(JsonConvert.SerializeObject(model,Formatting.Indented));

            return Ok(model);
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
