<<<<<<< HEAD
﻿using NUnit.Framework;
using ProyectoFinalSMV.ApisTest.Utilities;
using RestSharp;

namespace ProyectoFinalSMV.ApisTest.TestsData
{
    public class ApiPutTests
    {
        private readonly RestClient _client = new RestClient(ApiEndpoints.BaseUrl);

        [Test]
        public async Task Put_UpdatePost_Should_Return_OK()
        {
            var client = ApiRequests.GetClient();   // Cliente
            var request = ApiRequests.UpdatePost(1, "Título actualizado", "Contenido actualizado", 1); // Request

            var response = await client.ExecuteAsync(request); // Ejecutar

            ApiAssertions.AssertPostUpdated(response, "Título actualizado"); // Validar
            EvidenceHelper.SaveJson(response, "PUT_UpdatePost");

        }

        [OneTimeTearDown]
        public void Cleanup() => _client.Dispose();
    }
=======
﻿using NUnit.Framework;
using ProyectoFinalSMV.ApisTest.Utilities;
using RestSharp;

namespace ProyectoFinalSMV.ApisTest.TestsData
{
    public class ApiPutTests
    {
        private readonly RestClient _client = new RestClient(ApiEndpoints.BaseUrl);

        [Test]
        public async Task Put_UpdatePost_Should_Return_OK()
        {
            var client = ApiRequests.GetClient();   
            var request = ApiRequests.UpdatePost(1, "Título actualizado", "Contenido actualizado", 1); // Request

            var response = await client.ExecuteAsync(request); 

            ApiAssertions.AssertPostUpdated(response, "Título actualizado"); 
            EvidenceHelper.SaveJson(response, "PUT_UpdatePost");

        }

        [OneTimeTearDown]
        public void Cleanup() => _client.Dispose();
    }
>>>>>>> e8c7dd8bb42ff9b5a23c779e0e0029d116e3d4a4
}