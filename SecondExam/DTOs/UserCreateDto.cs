using SecondExam.Data.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondExam.DTOs
{
    public class UserCreateDto
    {
        public int UserId { get; set; }
       // public Credentials Credentials { get; set; }
        [ForeignKey("Credentials")]
        public int CredentialsID { get; set; }
        public Role Role { get; set; }
    }
}
