using Plugin.Youtube.Api_Youtube;
using Plugin.Youtube.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Plugin.Youtube.Api_Youtube.RequestClass;

namespace Plugin.Youtube.Api
{
    public class ApiCall
    {
        private Api_Youtube.Base credential;
        private HttpClient client;
        private HttpMethod httpMethod;
        private HttpRequestMessage request;
        private string requestUri, key_Parameter, accesstoken_Parameter, content;
        private StringContent body;
      //  private HttpResponseMessage response;
        private Request requestData;
        private ExceptionHandler.BaseException exception;
        private ResultClass result;

        //Constructor
        internal ApiCall(Base Credential)
        {
            credential = Credential;
            client = new HttpClient();
            key_Parameter = "key=" + credential.Api_key + " ";
            accesstoken_Parameter = "Bearer " + credential.Access_Token;
           
            result = new ResultClass();
            result.httpResponse = new HttpResponseMessage();
        }

        //Destructor
        ~ApiCall()
        {
            client = new HttpClient();
            requestData = new RequestClass.Request();
            requestUri = null;
            request = null;
            httpMethod = null;
            key_Parameter = null;
            result.httpResponse = new HttpResponseMessage();
        }

        //Put Api
        internal async Task<ResultClass> putApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            addData(true);

            await makeRequestAsync();
            return result;
        }
        //Post Api
        internal async Task<ResultClass> postApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            addData(true);

            await makeRequestAsync();
            return result;
        }
        //Get Api 
        internal async Task<ResultClass> getApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            if (checkExceptions())
            {
                addData(false);
                await makeRequestAsync();
            }
            else
            {
                throw exception;
            }
            return result;

        }
        //Delete Api
        internal async Task<ResultClass> deleteApiAsync(RequestClass.Request _request)
        {
            requestData = _request;
            addData(false);

            await makeRequestAsync();
            return result;
        }

        //Add request data
        private void addData(bool hasBody)
        {
            addUri();
            setMethod();
            addMessage(requestUri);
            addHeader();

            //Add the body
            if (hasBody)
            {
                addBody();
                request.Content = body;
            }
        }
        //Send the Api Request
        private async Task makeRequestAsync()
        {

            try
            {
                result.httpResponse = await client.SendAsync(request);
                if (result.httpResponse.IsSuccessStatusCode)//Api request is a Success
                {
                    result.content = await result.httpResponse.Content.ReadAsStringAsync();
                }
                else //Api request failed
                {
                    result.error = result.httpResponse.ReasonPhrase;
                    result.code = (int)result.httpResponse.StatusCode;
                }
            }
            catch (Exception ex)
            {
                result.error = ex.Message;
            }
            
        }
        //check Exceptions
        private bool checkExceptions()//incomplete...
        {
            //An access token is required for Mine requests
            if (requestData.Mine == true && credential.Access_Token == null)
            {
                exception = new ExceptionHandler.BaseException("Access Token Is Required For Mine Requests");
                return false;
            }
            return true;
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

                case Method.PUT:
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
