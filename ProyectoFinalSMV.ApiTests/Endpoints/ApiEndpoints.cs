namespace ProyectoFinalSMV.ApisTest
{
    public static class ApiEndpoints
    {
        public const string BaseUrl = "https://jsonplaceholder.typicode.com";

        // Endpoints
        public const string Users = "users";          // GET lista de usuarios
        public const string Posts = "posts";          // POST crear recurso
        public const string SinglePost = "posts/{id}"; // PUT/DELETE recurso por ID
    }
}