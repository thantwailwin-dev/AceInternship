using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using InternshipDotNetCore.ConsoleApp.Dtos;
using InternshipDotNetCore.ConsoleApp.Services;

namespace InternshipDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        public void Run()
        {
            Read();
            /*Edit(20);*/
            /*Delete(20);*/
            /*Create("new title", "new author", "new content");*/
            /*Update(19, "new test title", "new test author", "new test content");*/
        }

        private void Create(string title, string author, string content)
        {
            var blog = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            int result = db.SaveChanges();

            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            Console.WriteLine(message);
        }

        private void Read()
        {
            AppDbContext db = new AppDbContext();
            List<BlogDto> lst = db.Blogs.ToList();

            foreach (BlogDto blog in lst)
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
            AppDbContext db = new AppDbContext();
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(blog is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }
            
                Console.WriteLine("Blog Id => " + blog.BlogId);
                Console.WriteLine("Blog Title => " + blog.BlogTitle);
                Console.WriteLine("Blog Author => " + blog.BlogAuthor);
                Console.WriteLine("Blog Content => " + blog.BlogContent);
                Console.WriteLine("------------------------------------");    
        }

        private void Update(int id, string apple01, string mango01, string orange01)
        {
            AppDbContext db = new AppDbContext();
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            blog.BlogTitle = apple01;
            blog.BlogAuthor = mango01;
            blog.BlogContent = orange01;

            int result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }           

            db.Remove(blog); 
            int result = db.SaveChanges();

            string message = result > 0 ? "Deleting Successful!" : "Deleting Successful!";
            Console.WriteLine(message);
        }
    }
}
