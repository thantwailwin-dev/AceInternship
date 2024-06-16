using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        public void Run()
        {
            Read();

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
    }
}
