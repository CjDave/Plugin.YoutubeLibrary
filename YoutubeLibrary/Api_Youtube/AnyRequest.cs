using Plugin.Youtube.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Plugin.Youtube.Api_Youtube.RequestClass;

namespace Plugin.Youtube.Api_Youtube
{
    public class AnyRequest
    {
        private Request request;
        private string parameters;
        private clientService service;
        private string _result;
        private ResultClass result;
        private HttpMethod httpMethod;

        internal AnyRequest(clientService Service)
        {
            service = Service;
        }
        private void init()
        {
            request = new Request();
            parameters = "";
            result = new ResultClass();
        }
        public async Task<HttpResponseMessage> makeRequestAsync(Method method, string youtube, string resource, string stringParameter, Parameter[] parameter = null, List<Body_Item> body = null)
        {
            init();
            request.method = method;
            request.resource = resource;
            request.youtube = youtube;
            parameters = stringParameter + "&";
            parameters = parameter == null ? parameters : parameters + valueUtil.getParameter(parameter);
            request.body.body_Items = body == null ? null : body;
            await callAsync();
            return result.httpResponse;
        }
        private async Task callAsync()
        {
            //set parameters
            request.parameter = parameters;
            switch (request.method)
            {
                case Method.GET:
                    result = await service.api.getApiAsync(request);
                    break;
                case Method.POST:
                    result = await service.api.postApiAsync(request);
                    break;
                case Method.PUT:
                    result = await service.api.putApiAsync(request);
                    break;
                case Method.DELETE:
                    result = await service.api.deleteApiAsync(request);
                    break;
                default:
                    throw new Exception("Backend Error");
            }
            if (result.error != null)
            {

                throw new ExceptionHandler.BaseException("Error: " + result.code + "; " + result.error);
            }
            else
            {
                _result = "Success";
            }
        }
    }
}
