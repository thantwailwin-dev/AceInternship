namespace InternshipDotNetCore.RestApi.Models
{
	public class BlogResponseModel
	{
		public int PageNo { get; set; }
		public int PagSize { get; set; }
		public int PageCount { get; set; }
		/*public bool IsEndOfPage { get; set; }*/
		public bool IsEndOfPage => PageNo == PageCount;
		public List<BlogModel> Data { get; set; }
	}
}
