using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace ProyectoFinalSMV.ApisTest.Utilities
{
    public static class ApiAssertions
    {
        public static void AssertGetUsers(RestResponse response)
        {
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK),
                "El GET de usuarios no devolvió 200 OK");

           
            var json = JArray.Parse(response.Content ?? "[]");
            Assert.That(json.Count, Is.GreaterThan(0), "La lista de usuarios está vacía");
        }

        public static void AssertPostCreated(RestResponse response, string expectedTitle)
        {
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created),
                "El POST no devolvió 201 Created");

            var json = JObject.Parse(response.Content ?? "{}");
            Assert.That(json["title"]?.ToString(), Is.EqualTo(expectedTitle),
                "El título del post creado no coincide");
        }

        public static void AssertPostUpdated(RestResponse response, string expectedTitle)
        {
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK),
                "El PUT no devolvió 200 OK");

            var json = JObject.Parse(response.Content ?? "{}");
            Assert.That(json["title"]?.ToString(), Is.EqualTo(expectedTitle),
                "El título del post actualizado no coincide");
        }

        public static void AssertDeleted(RestResponse response)
        {
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK),
                "El DELETE no devolvió 200 OK");
        }
    }
}