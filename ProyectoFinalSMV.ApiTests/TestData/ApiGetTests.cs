using NUnit.Framework;
using ProyectoFinalSMV.ApisTest.Utilities;
using RestSharp;

namespace ProyectoFinalSMV.ApisTest.TestData
{
    public class ApiGetTests
    {
        private readonly RestClient _client = new RestClient(ApiEndpoints.BaseUrl);

        [Test]
        public async Task Get_Users_Should_Return_OK()
        {
            var client = ApiRequests.GetClient();         
            var request = ApiRequests.GetUsers();         

            var response = await client.ExecuteAsync(request); 

            ApiAssertions.AssertGetUsers(response);
            EvidenceHelper.SaveJson(response, "GET_Users");
        }

        [OneTimeTearDown]
        public void Cleanup() => _client.Dispose();
    }
}
