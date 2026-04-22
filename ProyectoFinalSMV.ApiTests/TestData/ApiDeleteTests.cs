using NUnit.Framework;
using ProyectoFinalSMV.ApisTest.Utilities;
using RestSharp;

namespace ProyectoFinalSMV.ApisTest.TestsData
{
    public class ApiDeleteTests
    {
        private readonly RestClient _client = new RestClient(ApiEndpoints.BaseUrl);

        [Test]
        public async Task Delete_Post_Should_Return_OK()
        {
            var client = ApiRequests.GetClient();         
            var request = ApiRequests.DeletePost(1);       
            var response = await client.ExecuteAsync(request); 

            ApiAssertions.AssertDeleted(response);         
            EvidenceHelper.SaveStatus(response, "DELETE_Post"); 


        }

        [OneTimeTearDown]
        public void Cleanup() => _client.Dispose();
    }
}