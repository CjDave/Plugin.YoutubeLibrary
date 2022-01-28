using YoutubeLibrary.util;
using static YoutubeLibrary.youtube.Parameters;

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
            request = new Request { method = Method.GET, resource = "playlists?" };
            parts = "";
            parameters = "";
            service = Service;
        }
        //standard playlists request
        public string getPlaylist(String[] part, bool Mine)
        {
            parts = valueUtil.getPart(part);
            mine = valueUtil.isMine(Mine);
            request.Mine = Mine;
            callAsync();
            return result;

        }
        //playlists request with optional parameters
        public string getPlaylist(String[] part, bool Mine, Parameters.Body[] body)
        {
            parts = valueUtil.getPart(part);
            mine = valueUtil.isMine(Mine);
            request.Mine = Mine;
            callAsync();
            return result;

        }
        private async Task callAsync()
        {
            request.parameter = parts + mine + parameters;
            if (request.parameter != null)
            {
                result = await service.api.callApiAsync(request);
            }
            result = "Error";
        }
    }


}
