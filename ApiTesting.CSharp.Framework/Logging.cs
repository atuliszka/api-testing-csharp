using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace ApiTesting.CSharp.Framework
{
    public static class Logging
    {
        private static readonly string FileName = DateTimeOffset.Now.ToUnixTimeSeconds() + ".log";
        private static readonly string FilePath = Path.Combine(GetExecutionPath(), FileName);

        public static void SaveApiCallToLogFile(
            IRestClient client, IRestRequest request, IRestResponse response)
        {
            SaveToLogFile(JsonConvert.SerializeObject(ConvertRestRequestToString(client, request)));
            SaveToLogFile(JsonConvert.SerializeObject(ConvertRestResponseToString(response)));
        }

        private static string ConvertRestRequestToString(IRestClient client, IRestRequest request)
        {
            var convertedRequest = new
            {
                uri = client.BuildUri(request),
                resource = request.Resource,
                method = request.Method.ToString(),
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                })
            };

            return JsonConvert.SerializeObject(convertedRequest);
        }

        private static string ConvertRestResponseToString(IRestResponse response)
        {
            var convertedResponse = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            return JsonConvert.SerializeObject(convertedResponse);
        }

        private static void SaveToLogFile(string log)
        {
            if (!LogFileExists())
            {
               CreateLogFile(); 
            }

            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(log);
            }
        }

        private static string GetExecutionPath()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(location);
        }

        private static bool LogFileExists()
        {
            return File.Exists(FilePath);
        }

        private static void CreateLogFile()
        {
            File.Create(FilePath).Close();
        }
    }
}
