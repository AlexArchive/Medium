namespace MediumDomainModel
{
    public class Response<TResponse>
    {
        public TResponse Data { get; set; }

        public bool Successful
        {
            get { return Data != null; }
        }

        public static Response<TResponse> UnsuccessfulResponse = new Response<TResponse>();
    }
}