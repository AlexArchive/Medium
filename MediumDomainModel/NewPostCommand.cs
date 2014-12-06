namespace MediumDomainModel
{
    public class NewPostCommand
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
    }
}