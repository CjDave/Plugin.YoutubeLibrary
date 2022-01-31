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
        public string getPlaylist(String[] part, bool Mine)
        {
            request.method = Method.GET;
            parts = valueUtil.getPart(part);
            mine = valueUtil.isMine(Mine);
            request.Mine = Mine;
            callAsync();
            return result;

        }
        //playlists request with optional parameters
        public string getPlaylist(String[] part, bool Mine, Parameters.Body[] body)
        {
            request.method = Method.GET;
            parts = valueUtil.getPart(part);
            mine = valueUtil.isMine(Mine);
            request.Mine = Mine;
            callAsync();
            return result;

        }
        public string insertPlaylist(String title, String[] part)
        {
            parts = valueUtil.getPart(part);
            request.method = Method.POST;

            //create new body item for the snippets
            Parameters.Body_Item snippet_BodyItem = new Parameters.Body_Item("snippet", new List<Body_Item>());
            snippet_BodyItem.values.Add(new Body_Item("title", title));

            //Add the body item to the body
            request.body.body_Items.Add(snippet_BodyItem);
            callAsync();
            return result;
        }
        public string insertPlaylist(String title, String[] part, List<Body_Item> _body)
        {
            parts = valueUtil.getPart(part);
            request.method = Method.POST;
            //create new body item for the snippets

            Parameters.Body_Item snippet_BodyItem = new Parameters.Body_Item("snippet", new List<Body_Item>());
            snippet_BodyItem.values.Add(new Body_Item("title", title));
            //Add the body item to the body
            request.body.body_Items = _body;
            request.body.body_Items.Add(snippet_BodyItem);
            // string g = addBody(request.body.body_Items);
            //g = "{" + g + "}";
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
