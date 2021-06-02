namespace ServerApp.Entities
{
    public class UserToUser
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public User Follower { get; set; }
        public int FollowerId { get; set; }
        
    }
}