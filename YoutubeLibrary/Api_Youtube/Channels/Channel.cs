using Plugin.Youtube.Utils;
using Plugin.Youtube.Result;
using System.Threading.Tasks;
using static Plugin.Youtube.Api_Youtube.RequestClass;
using System.Collections.Generic;

namespace Plugin.Youtube.Api_Youtube.Channels
{
    public class Channel
    {
        private Request request;
        private string parts, parameters, mine, error;
        private clientService service;
        private ResultClass result;
        internal Channel(clientService Service)
        {
            init();
            service = Service;
        }
        // initialize/re-initalize variables
        void init()
        {
            request = new Request { resource = "channels?" };
            parts = "";
            parameters = "";
            mine = "";

        }

       
        void createRequest(Parameter[] parameter)
        {
            init();
            request.parameter = parameter == null ? parameters : parameters + valueUtil.getParameter(parameter);
            string[] part = { "snippet", "contentDetails", "statistics" };
            parts = valueUtil.getPart(part);  //get the parts
            request.method = Method.GET;
        }

        //Search for a playlist by Id
        public async Task<ChannelResponse> searchByIdAsync(string id, Parameter[] parameter = null)
        {
            parameters = "id=" + id + "&";
            createRequest(parameter);
            await callAsync();
            return serializer.jsonConvert<ChannelResponse>(result.content);//serialize results

        }
        //Search For a Playlist by Username
        public async Task<ChannelResponse> searchByUsernameAsync(string forUsername, Parameter[] parameter = null)
        {
            parameters = "forUsername=" + forUsername + "&";
            createRequest(parameter);
            await callAsync();
            return serializer.jsonConvert<ChannelResponse>(result.content);//serialize results

        }

        //Get an owners Channel
        public async Task<ChannelResponse> getMyChannelAsync(bool Mine, Parameter[] parameter = null)
        {
            mine = valueUtil.isMine(Mine);
            createRequest(parameter) ; 
            await callAsync();
            return serializer.jsonConvert<ChannelResponse>(result.content); //serialize results
        }
        //Update a channel
        public async Task<ChannelResponse> updateMyChannelAsync(string[] part, List<Body_Item> _body, Parameter[] parameter = null)//Incomplete
        {
            if (part == null)
            {
                error = "Please Enter A Part";
                throwException(error);
                return null;
            }
            if (_body == null)
            {
                error = "Please Enter A Body";
                throwException(error);
                return null;
            }
            request.parameter = parameter == null ? parameters : parameters + valueUtil.getParameter(parameter);
            parts = valueUtil.getPart(part);
            request.method = Method.PUT;
            request.body.body_Items = _body;
            await callAsync();
            return serializer.jsonConvert<ChannelResponse>(result.content); //serialize results
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
