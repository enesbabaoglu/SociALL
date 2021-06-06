namespace ServerApp.Entities
{
    public class UserQueryParams
    {
        public int UserId { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }

        public bool Followers { get; set; }
        public bool Followings { get; set; }

        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}