namespace MediumDomainModel
{
    public class EditPostCommand
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
    }
}