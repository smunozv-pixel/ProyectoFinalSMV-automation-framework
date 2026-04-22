using NUnit.Framework;
using ProyectoFinalSMV.ApisTest.Utilities;
using RestSharp;

namespace ProyectoFinalSMV.ApisTest.TestData
{
    public class ApiPostTests
    {
        private readonly RestClient _client = new RestClient(ApiEndpoints.BaseUrl);

        [Test]
    public async Task Post_CreatePost_Should_Return_Created()
    {
        var client = ApiRequests.GetClient();
        var request = ApiRequests.CreatePost("Nuevo título", "Contenido de prueba", 1);

        var response = await client.ExecuteAsync(request);

        ApiAssertions.AssertPostCreated(response, "Nuevo título");
        EvidenceHelper.SaveJson(response, "Post_CreatePost");
    }

    [OneTimeTearDown]
        public void Cleanup() => _client.Dispose();
    }
}

