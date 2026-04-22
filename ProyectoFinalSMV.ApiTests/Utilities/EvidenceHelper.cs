using System;
using System.IO;
using RestSharp;

namespace ProyectoFinalSMV.ApisTest.Utilities
{
    public static class EvidenceHelper
    {
        public static void SaveJson(RestResponse response, string testName)
        {
            var folder = Path.Combine("Evidence", "Json");
            Directory.CreateDirectory(folder);
            var filePath = Path.Combine(folder, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            File.WriteAllText(filePath, response.Content ?? string.Empty);
        }

        public static void SaveStatus(RestResponse response, string testName)
        {
            var folder = Path.Combine("Evidence", "Status");
            Directory.CreateDirectory(folder);
            var filePath = Path.Combine(folder, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            File.WriteAllText(filePath, $"StatusCode: {response.StatusCode}");
        }
    }
}