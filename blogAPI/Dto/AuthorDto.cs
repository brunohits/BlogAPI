namespace blogAPI.Dto
{
    public class AuthorDto
    {
        public string fullName { get; set; }
        public DateOnly birthDate { get; set; }
        public string gender { get; set; }
        public int posts {  get; set; }
        public int likes { get; set; }
        public DateOnly created {  get; set; }
    }
}
