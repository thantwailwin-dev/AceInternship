using Dapper;
using InternshipDotNetCore.ConsoleApp.Dtos;
using InternshipDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InternshipDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        public void Run()
        {
            Read();
            /*Edit(11);*/
            /* Delete(15);*/
            /*Create("title", "author", "content");*/
            /*Update(18, "test title", "test author", "test content");*/
        }

        private void Create(string apple, string mango, string orange)
        {
            var blog = new BlogDto
            {
                BlogTitle = apple,
                BlogAuthor = mango,
                BlogContent = orange
            };

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent])
                             VALUES
                                   (@BlogTitle
		                           ,@BlogAuthor
		                           ,@BlogContent)";

            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            Console.WriteLine(message);
        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = "select * from tbl_blog";
            List<BlogDto> blogLst = db.Query<BlogDto>(query).ToList();

            foreach (BlogDto blog in blogLst)
            {
                Console.WriteLine("Blog Id => " + blog.BlogId);
                Console.WriteLine("Blog Title => " + blog.BlogTitle);
                Console.WriteLine("Blog Author => " + blog.BlogAuthor);
                Console.WriteLine("Blog Content => " + blog.BlogContent);
                Console.WriteLine("------------------------------------");
            }

        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = "select * from tbl_blog where blogId = @BlogId";
            var blog = db.Query<BlogDto>(query, new BlogDto { BlogId = id }).FirstOrDefault();

            if (blog is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            Console.WriteLine("Blog Id => " + blog.BlogId);
            Console.WriteLine("Blog Title => " + blog.BlogTitle);
            Console.WriteLine("Blog Author => " + blog.BlogAuthor);
            Console.WriteLine("Blog Content => " + blog.BlogContent);
        }

        private void Update(int id, string apple01, string mango01, string orange01)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            string query = "select * from tbl_blog where blogId = @BlogId";
            var item = db.Query<BlogDto>(query, new BlogDto { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            string query01 = @"UPDATE [dbo].[Tbl_Blog]
                             SET [BlogTitle] = @BlogTitle
                                  ,[BlogAuthor] = @BlogAuthor
                                  ,[BlogContent] = @BlogContent
                             WHERE BlogId = @BlogId";

            var blog = new BlogDto
            {
                BlogId = id,
                BlogTitle = apple01,
                BlogAuthor = mango01,
                BlogContent = orange01
            };

            int result = db.Execute(query01, blog);

            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = "select * from tbl_blog where blogId = @BlogId";
            var item = db.Query<BlogDto>(query, new BlogDto { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            string query01 = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            int result = db.Execute(query01, new BlogDto { BlogId = id });

            string message = result > 0 ? "Deleting Successful!" : "Deleting Successful!";
            Console.WriteLine(message);
        }
    }
}
