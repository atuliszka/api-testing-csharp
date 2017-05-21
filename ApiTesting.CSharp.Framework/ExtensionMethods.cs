using RestSharp;

namespace ApiTesting.CSharp.Framework
{
    public static class ExtensionMethods
    {
        public static IRestResponse<T> ExecuteAndLog<T>(this RestClient client, RestRequest request) 
            where T : new()
        {
            var response = client.Execute<T>(request);
            Logging.SaveApiCallToLogFile(client, request, response);

            return response;
        }
    }
}
