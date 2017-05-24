namespace ApiTesting.CSharp.Framework.Models
{
    public class UserContext
    {
        public UserContext()
        {
            Post = new Post();
        }

        public int UserId { get; set; }

        public Post Post { get; set; }
    }
}
