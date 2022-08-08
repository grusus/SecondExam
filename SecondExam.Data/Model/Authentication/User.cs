namespace SecondExam.Data.Model.Authentication
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
#pragma warning disable CS8618
        public Credentials Credentials { get; set; }
        [ForeignKey("Credentials")]
        public int CredentialsID { get; set; }
        public Role Role { get; set; }
#pragma warning restore CS8618
    }
}
