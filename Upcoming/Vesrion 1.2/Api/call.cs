using YoutubeLibrary.youtube;
using YoutubeLibrary.youtube.Parameters;
using YoutubeLibrary.util;
using static YoutubeLibrary.youtube.Parameters.RequestClass;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace YoutubeLibrary.Api
{
    public class ApiCall
    {
        private Base credential;
        private HttpClient client;
        private HttpMethod httpMethod;
        private HttpRequestMessage request;
        private string requestUri;
        private string key_Parameter;
        private string accesstoken_Parameter;
        private StringContent body;
        private RequestClass.Request requestData;
        internal ApiCall(Base Credential)
        {
            credential = Credential;
            client = new HttpClient();
            key_Parameter = "&key=" + credential.Api_key + " ";
            accesstoken_Parameter = "Bearer " + credential.Access_Token;

        }
        ~ApiCall()
        {
            client = new HttpClient();
            requestData = new RequestClass.Request();
            requestUri = null;
            request = null;
            httpMethod = null;
            key_Parameter = null;
        }

        internal async Task postApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            addUri();
            addBody();
           // HttpClient clients = new HttpClient();
            var response = new HttpResponseMessage();
            addHeader();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = body
            };


        }
        internal async Task<string> callApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            // checkExceptions();
            addUri();
            setMethod();
            addMessage(requestUri);
            addHeader();
            var response = new HttpResponseMessage();

            try
            {

                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                exceptionHandler.returnException(ex);
            }
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                exceptionHandler.showError(response.ReasonPhrase);
            }
            return "Error";
        }
        //check Exceptions
        void checkExceptions()
        {
            //an access token is required for Mine requests
            if (requestData.Mine == true && credential.Access_Token == null)
            {
                exceptionHandler.showError("not authenticated");
            }

        }
        //create the request Uri
        void addUri()
        {
            requestUri = requestData.youtube + requestData.resource + requestData.parameter + key_Parameter + requestData.http;
        }
        //add the Headers to the request
        void addHeader()
        {
            request.Headers.Add("Authorization", accesstoken_Parameter);
            request.Headers.Add("Accept", "application/json");
            if (requestData.header != null)
            {
                foreach (Header pair in requestData.header)
                {
                    request.Headers.Add(pair.title, pair.value);
                }
            }
        }
        //add body to the request
        async Task addBody()
        {
            // the the json format of the body items
            string jsonBody = addBodyItems(requestData.body.body_Items);
            jsonBody = "{" + jsonBody + "}";

            //add it to the request
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonBody);

            body = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        }
        //serialize the body items to a json style
        string addBodyItems(List<Body_Item> items)
        {
            string jsonString = "";
            foreach (Body_Item item in items)
            {
                jsonString = jsonString + '\u0022' + item.title + '\u0022' + ":";
                //contains a value
                if (item.value != "")
                {
                    jsonString = jsonString + '\u0022' + item.value + '\u0022';
                }
                //contains multiple values
                else
                {
                    jsonString = jsonString + "{" + addBodyItems(item.values) + "}";
                }
                //if its not the last item add a comma
                if (item != items.Last())
                {
                    jsonString = jsonString + ",";
                }
            }
            return jsonString;
        }
        //add the request message 
        void addMessage(string message)
        {
            request = new HttpRequestMessage()
            {
                RequestUri = new Uri(message),
                Method = httpMethod
            };
        }
        //set the method of the request
        void setMethod()
        {
            switch (requestData.method)
            {
                case Method.GET:
                    httpMethod = HttpMethod.Get;
                    break;

                case Method.UPDATE:
                    httpMethod = HttpMethod.Post;
                    break;

                case Method.POST:
                    httpMethod = HttpMethod.Put;
                    break;

                case Method.DELETE:
                    httpMethod = HttpMethod.Delete;
                    break;
            }
        }
    }
}
