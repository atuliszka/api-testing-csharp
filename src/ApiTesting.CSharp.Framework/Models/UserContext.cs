namespace ApiTesting.CSharp.Framework.Models
{
    public class UserContext
    {
        public UserContext()
        {
            Post = new Post();
        }

        public Post Post { get; set; }
    }
}
