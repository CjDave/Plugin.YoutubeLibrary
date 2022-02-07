using YoutubeLibrary.util;
using YoutubeLibrary.youtube.Parameters;
using static YoutubeLibrary.youtube.Parameters.RequestClass;

namespace YoutubeLibrary.youtube
{
    public class PlaylistLists
    {
        private Request request;
        private string parts, parameters, mine;
        private clientService service;
        private string result { get; set; }

        internal PlaylistLists(clientService Service)
        {
            request = new Request { resource = "playlists?" };
            parts = "";
            parameters = "";
            service = Service;
        }
        //standard playlists request
        public string getPlaylist(String[] _parameter, bool Mine)
        {  
            //get the parts if they are specified
            if (_parameter != null)
            {
                parts = valueUtil.getPart(_parameter);
            }
            request.method = Method.GET;   
            mine = valueUtil.isMine(Mine);
            request.Mine = Mine;
            callAsync();
            return result;

        }
        //playlists request with optional parameters
        public string getPlaylist(String[] _parameter, bool Mine, List<Body_Item> _body)
        {

            //get the parts if they are specified
            if (_parameter != null)
            {
                parts = valueUtil.getPart(_parameter);
            }
            request.body.body_Items = _body;
            request.method = Method.GET;
            mine = valueUtil.isMine(Mine);
            request.Mine = Mine;
            callAsync();
            return result;

        }
        public string insertPlaylist(String title, String[] _parameter = null)
        {
            //get the parts if they are specified
            if (_parameter != null)
            {
                parts = valueUtil.getPart(_parameter);
            }
            request.method = Method.POST;

            //create new body item for the snippets
            Parameters.Body_Item snippet_BodyItem = new Parameters.Body_Item("snippet", new List<Body_Item>());
            snippet_BodyItem.values.Add(new Body_Item("title", title));

            //Add the body item to the body
            request.body.body_Items.Add(snippet_BodyItem);
            callAsync();
            return result;
        }
        public string insertPlaylist(List<Body_Item> _body, String[] _parameter = null)
        {
            //get the parts if they are specified
            if (_parameter != null)
            {
                parts = valueUtil.getPart(_parameter);
            }

            request.method = Method.POST;

            request.body.body_Items = _body;
            callAsync();
            return result;
        }

        private async Task callAsync()
        {
            request.parameter = parts + mine + parameters;
            switch (request.method)
            {
                case Method.POST:
                    await service.api.postApiAsync(request);
                    break;
                case Method.GET:
                    if (request.parameter != null)
                    {
                        result = await service.api.callApiAsync(request);
                    }
                    break;
            }
            result = "Error";
        }
    }


}
