using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipDotNetCore.ConsoleApp
{
    internal class BlogDto
    {
        public int BlogId {  get; set; }        
        public string BlogTitle {  get; set; }
        public string BlogAuthor {  get; set; }
        public string BlogContent {  get; set; }
    }
}
