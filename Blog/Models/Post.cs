namespace Blog.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string LongDesc { get; set; }
		public string ShortDesc { get; set; }
		public string Author { get; set; }
		public DateTime Created { get; set; }
		
	}
}
