<<<<<<< HEAD
﻿using NUnit.Framework;
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
=======
﻿using NUnit.Framework;
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
>>>>>>> e8c7dd8bb42ff9b5a23c779e0e0029d116e3d4a4
}