namespace SecondExam.Data.Model.Authentication
{
    public class Credentials
    {
        [Key]
        public int CredentialsID { get; set; }
#pragma warning disable CS8618
        public User User { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
#pragma warning restore CS8618
    }
}
