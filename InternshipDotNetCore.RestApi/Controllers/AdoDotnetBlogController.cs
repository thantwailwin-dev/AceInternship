using InternshipDotNetCore.RestApi.Models;
using InternshipDotNetCore.RestApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace InternshipDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoDotNetBlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = "select * from tbl_blog";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            /*List<BlogModel> list = new List<BlogModel>();
            foreach(DataRow dr in dt.Rows)
            {
                BlogModel blogModel = new BlogModel
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    BlogTitle = Convert.ToString(dr["BlogTitle"]),
                    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                    BlogContent = Convert.ToString(dr["BlogContent"])
                };
                list.Add(blogModel);
            }*/

            List<BlogModel> list = dt.AsEnumerable().Select(dr => new BlogModel {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = "select * from tbl_blog where BlogId=@BlogId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found!");                
            }

            DataRow dr = dt.Rows[0];

            var blog = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent])
                             VALUES
                                   (@BlogTitle
                                   ,@BlogAuthor
                                   ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";            

            return Ok(message);
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id,BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle,
                               [BlogAuthor] = @BlogAuthor,
                              [BlogContent] = @BlogContent
                         WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blog.BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = "delete from tbl_blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            return Ok(message);
        }
    }
}
