using RestSharp;

namespace ProyectoFinalSMV.ApisTest
{
    public static class ApiRequests
    {
           public static RestClient GetClient()
            {
                return new RestClient("https://jsonplaceholder.typicode.com");
            }

        public static RestRequest GetUsers()
        {
            return new RestRequest("/users", Method.Get);
        }


        public static RestRequest CreatePost(string title, string body, int userId)
        {
            var request = new RestRequest(ApiEndpoints.Posts, Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new { title, body, userId });
            return request;
        }

        public static RestRequest UpdatePost(int id, string title, string body, int userId)
        {
            var request = new RestRequest($"/posts/{id}", Method.Put);
            request.AddJsonBody(new { id, title, body, userId });
            return request;
        }

        public static RestRequest DeletePost(int id)
        {
            return new RestRequest($"/posts/{id}", Method.Delete);
        }

    }
}