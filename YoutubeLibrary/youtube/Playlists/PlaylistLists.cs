using YoutubeLibrary.util;
using YoutubeLibrary.youtube.Parameters;
using static YoutubeLibrary.youtube.Parameters.RequestClass;

namespace YoutubeLibrary.youtube
{
    public class PlaylistLists
    {
        private Request request;
        private string parts, parameters, mine, error;
        private clientService service;
        private string result;
       

        //Constructor
        internal PlaylistLists(clientService Service)
        {
            request = new Request { resource = "playlists?" };
            parts = "";
            parameters = "";
            mine = "";
            service = Service;
        }

        //standard playlists request
        public string getPlaylist(String[] part, bool Mine)
        {
            if (part == null)
            {
                error = "Please Enter A Part";
                throwException(error);
            }
            else
            {
                parts = valueUtil.getPart(part);  //get the parts
                request.method = Method.GET;
                mine = valueUtil.isMine(Mine);
                request.Mine = Mine;
                callAsync();
                return result;
            }
            return error;

        }

        //Get the Playlists request from optional parameters
        public string getPlaylist(String[] part, bool Mine, Parameter[] parameter)
        {
            if (part == null)
            {
                error = "Please Enter A Part";
                throwException(error);
            }
            else
            {
                request.method = Method.GET;
                parameters = parameters + valueUtil.getParameter(parameter);
                mine = valueUtil.isMine(Mine);
                request.Mine = Mine;
                callAsync();
                return result;
            }
            return error;
        }

        //Insert a playlist
        public string insertPlaylist(String title, String[] part, String Description = "", List<Body_Item> _body = null)
        {
            if (part == null)
            {
                error = "Please Enter A Part";
                throwException(error);
            }
            else
            {
                parts = valueUtil.getPart(part);
                request.method = Method.POST;

                //create new body item for the snippets
                Parameters.Body_Item snippet_BodyItem = new Parameters.Body_Item("snippet", new List<Body_Item>());
                snippet_BodyItem.values.Add(new Body_Item("title", title));
                snippet_BodyItem.values.Add(new Body_Item("description", Description));

                //Add body if provided
                if (_body != null)
                {
                    request.body.body_Items = _body;
                }

                //Add the snippet to the body
                request.body.body_Items.Add(snippet_BodyItem);

                callAsync();
                return result;
            }
            return error;
        }

        //Insert a playlist  with optional parameters
        public string insertPlaylist(List<Body_Item> _body, String[] part, Parameter[] parameter)
        {
            if (part == null)
            {
                error = "Please Enter A Part";
                throwException(error);
                return error;
            }
            if (_body == null)
            {
                error = "Please Enter A Body";
                throwException(error);
                return error;
            }

            parts = valueUtil.getPart(part);
            request.method = Method.POST;
            request.body.body_Items = _body;
            parameters = parameters + valueUtil.getParameter(parameter);

            callAsync();
            return result;


        }


        //Make the Api Call
        private async Task callAsync()
        {
            //set parameters
            request.parameter = parts + mine + parameters;
            switch (request.method)
            {
                case Method.POST:
                    result = await service.api.postApiAsync(request);
                    break;
                case Method.GET:
                    if (request.parameter != null)
                    {
                        result = await service.api.callApiAsync(request);
                    }
                    break;
                default:
                    throw new Exception("Backend Error");
                    break;
            }

        }

        //Throw Exception
        private void throwException(string Error)
        {
            throw new ExceptionHandler.BaseException(Error);
        }

    }


}
