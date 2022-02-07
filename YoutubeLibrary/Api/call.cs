using YoutubeLibrary.youtube;
using YoutubeLibrary.youtube.Parameters;
using YoutubeLibrary.util;
using static YoutubeLibrary.youtube.Parameters.RequestClass;
using System.Text;

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
        private HttpResponseMessage response;
        private RequestClass.Request requestData;
        internal ApiCall(Base Credential)
        {
            credential = Credential;
            client = new HttpClient();
            key_Parameter = "&key=" + credential.Api_key + " ";
            accesstoken_Parameter = "Bearer " + credential.Access_Token;
            response = new HttpResponseMessage();
        }
        ~ApiCall()
        {
            client = new HttpClient();
            requestData = new RequestClass.Request();
            requestUri = null;
            request = null;
            httpMethod = null;
            key_Parameter = null;
            response = new HttpResponseMessage();
        }

        internal async Task<string> postApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            addUri();
            setMethod();
            addBody();
            addMessage(requestUri);
            request.Content = body;
            addHeader();
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
            return "Error";//incomplete

        }
        internal async Task<string> callApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            // checkExceptions();
            addUri();
            setMethod();
            addMessage(requestUri);
            addHeader();

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
            return "Error";//incomplete
        }
        //check Exceptions
        private void checkExceptions()//incomplete
        {
            //an access token is required for Mine requests
            if (requestData.Mine == true && credential.Access_Token == null)
            {
                exceptionHandler.showError("not authenticated");
            }

        }
        //create the request Uri
        private void addUri()
        {
            requestUri = requestData.youtube + requestData.resource + requestData.parameter + key_Parameter + requestData.http;
        }
        //add the Headers to the request
        private void addHeader()
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
        private void addBody()
        {
            // Get the json format of the body items
            string jsonBody = addBodyItems(requestData.body.body_Items);
            jsonBody = "{" + jsonBody + "}";
            //Convert and add the json to the request
            body = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        }
        //serialize the body items to a json style
        private string addBodyItems(List<Body_Item> items)
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
        private void addMessage(string message)
        {
            request = new HttpRequestMessage()
            {
                RequestUri = new Uri(message),
                Method = httpMethod
            };
        }
        //set the http method of the request
        private void setMethod()
        {
            switch (requestData.method)
            {
                case Method.GET:
                    httpMethod = HttpMethod.Get;
                    break;

                case Method.UPDATE:
                    httpMethod = HttpMethod.Put;
                    break;

                case Method.POST:
                    httpMethod = HttpMethod.Post;
                    break;

                case Method.DELETE:
                    httpMethod = HttpMethod.Delete;
                    break;
            }
        }
    }
}
