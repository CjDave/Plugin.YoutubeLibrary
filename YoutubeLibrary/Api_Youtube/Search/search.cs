using Plugin.Youtube.Result;
using Plugin.Youtube.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Plugin.Youtube.Api_Youtube.RequestClass;

namespace Plugin.Youtube.Api_Youtube.Search
{
    public class search
    {
        private Request request;
        private string parts, parameters, mine, error;
        private clientService service;
        private ResultClass result;
        internal search(clientService Service)
        {
            init();
            service = Service;
        }
        // initialize/re-initalize variables
        void init()
        {
            request = new Request { resource = "search?" };
            parts = "";
            parameters = "";
            mine = "";

        }
        //Search 
        public async Task<SearchResponse> searchAsync(Parameter[] parameter)
        {
            request.parameter = parameters + valueUtil.getParameter(parameter);
            await callAsync();
            return serializer.jsonConvert<SearchResponse>(result.content);//serialize results

        }
    
        async Task callAsync()
        {
            request.parameter = parts + mine + request.parameter;
            result = await service.api.getApiAsync(request);
        }
        private void throwException(string Error)
        {
            throw new ExceptionHandler.BaseException(Error);
        }

    }
}
