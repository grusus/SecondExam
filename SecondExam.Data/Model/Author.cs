namespace SecondExam.Data.Model
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
#pragma warning disable CS8618
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
#pragma warning restore CS8618
        public List<Material>? AuthorPublications { get; set; }
        public int Counter
        {
            get
            {
                if (AuthorPublications != null)
                {
                    Counter = AuthorPublications.Count;
                }
                else
                {
                    Counter = 0;
                }
                return Counter;
            }
            set
            {
            }
        }
    }
}
