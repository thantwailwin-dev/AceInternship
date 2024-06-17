using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;

namespace InternshipDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperBlogController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperBlogController()
        {
            /* _sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
             _sqlConnectionStringBuilder.DataSource = ".";
             _sqlConnectionStringBuilder.InitialCatalog = "AceInternship";
             _sqlConnectionStringBuilder.UserID = "sa";
             _sqlConnectionStringBuilder.Password = "twl@123";*/

            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AceInternship",
                UserID = "sa",
                Password = "twl@123"
            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            /*string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                              FROM [dbo].[Tbl_Blog]";*/

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var lst = db.Query<TblBlog>(Quaries.BlogList).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {   
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<TblBlog>(Quaries.BlogById, new { BlogId = id }).FirstOrDefault();
            if(item is null)            
                return NotFound("No Data Found.");            
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog tblBlog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(Quaries.BlogCreate, tblBlog);
            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            return Ok(message);
        }   

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,TblBlog tblBlog)
        {            
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            var item = db.Query<TblBlog>(Quaries.BlogById, new { BlogId = id }).FirstOrDefault();
            if (item is null)
                return NotFound("No Data Found.");

            tblBlog.BlogId = id;
            int result = db.Execute(Quaries.BlogUpdate, tblBlog);
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            var item = db.Query<TblBlog>(Quaries.BlogById, new { BlogId = id }).FirstOrDefault();
            if (item is null)
                return NotFound("No Data Found.");

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            var item = db.Query<TblBlog>(Quaries.BlogById, new { BlogId = id }).FirstOrDefault();
            if (item is null)
                return NotFound("No Data Found.");

            var result = db.Execute(Quaries.BlogDelete, new { BlogId = id });
            string message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            return Ok(message);
        }
    }

    public class TblBlog
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }

    }

    public static class Quaries
    {
        public static string BlogList { get; } = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                              FROM [dbo].[Tbl_Blog]";

        public static string BlogById { get; } = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                              FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

        public static string BlogCreate { get; } = @"INSERT INTO[dbo].[Tbl_Blog]
                                    ([BlogTitle]
                                   , [BlogAuthor]
                                   , [BlogContent])
                             VALUES
                                   (
                                   @BlogTitle,
                                   @BlogAuthor,
                                   @BlogContent
                                   )";
        public static string BlogDelete { get; } = @"DELETE FROM[dbo].[Tbl_Blog]
                            WHERE BlogId = @BlogId";

        public static string BlogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                           WHERE BlogId = @BlogId";
        
    }

}

