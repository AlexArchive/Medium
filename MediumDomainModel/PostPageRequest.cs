using MediatR;

namespace Medium.DomainModel
{
    public class PostPageRequest : IRequest<PostPage>
    {
        public int PageNumber { get; set; }
        public int PostsPerPage { get; set; }

        public PostPageRequest(int pageNumber, int postsPerPage)
        {
            PageNumber = pageNumber;
            PostsPerPage = postsPerPage;
        }
    }
}