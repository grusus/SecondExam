namespace SecondExam.DTOs
{
    public class UserCreateDto
    {
        public int UserId { get; set; }
        [ForeignKey("Credentials")]
        public int CredentialsID { get; set; }
        public Role Role { get; set; }
    }
}
