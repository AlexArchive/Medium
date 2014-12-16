using MediatR;

namespace Medium.DomainModel
{
    public class PostPageRequest : IRequest<PostPage>
    {
        public int PageNumber { get; set; }
        public int PostsPerPage { get; set; }
        public bool IncludeDrafts { get; set; }

        public PostPageRequest(int pageNumber, int postsPerPage, bool includeDrafts)
        {
            PageNumber = pageNumber;
            PostsPerPage = postsPerPage;
            IncludeDrafts = includeDrafts;
        }
    }
}