using Plugin.Youtube.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Plugin.Youtube.Api_Youtube.RequestClass;
using Plugin.Youtube.Result;
namespace Plugin.Youtube.Api_Youtube.Playlists
{
    public class PlaylistLists
    {
        private Request request;
        private string parts, parameters, mine, error;
        private clientService service;
        private string _result;
        private ResultClass result;
        private PlaylistResponse playlistResponse;

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
        public async Task<PlaylistResponse> getPlaylistAsync(String[] part, bool Mine)
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
                await callAsync();
                return playlistResponse;
            }
            return null;

        }

        //Get the Playlists request from optional parameters
        public async Task<PlaylistResponse> getPlaylistAsync(String[] part, bool Mine, Parameter[] parameter)
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
                parameters = parameters + valueUtil.getParameter(parameter);
                mine = valueUtil.isMine(Mine);
                request.Mine = Mine;
                await callAsync();//make the call
                return playlistResponse;
            }
            return null;
        }

        //Insert a playlist
        public async Task<PlaylistResponse> insertPlaylistAsync(String title, String Description, String[] part,  Parameter[] parameter = null, List<Body_Item> _body = null)
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
                Body_Item snippet_BodyItem = new Body_Item("snippet", new List<Body_Item>());
                snippet_BodyItem.values.Add(new Body_Item("title", title));
                snippet_BodyItem.values.Add(new Body_Item("description", Description));

                //Add body if provided
                if (_body != null)
                {
                    request.body.body_Items = _body;
                }
                //Add parameters if provided
                if (parameter != null)
                {
                    parameters = parameters + valueUtil.getParameter(parameter);
                }

                //Add the snippet to the body
                request.body.body_Items.Add(snippet_BodyItem);

                await callAsync();
                return playlistResponse;
            }
            return null;
        }

        //Insert a playlist  with optional parameters
        public async Task<PlaylistResponse> insertPlaylistAsync(String[] part, Parameter[] parameter, List<Body_Item> _body)
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

            parts = valueUtil.getPart(part);
            request.method = Method.POST;
            request.body.body_Items = _body;
            parameters = parameters + valueUtil.getParameter(parameter);

            await callAsync();
            return playlistResponse;


        }

        //Update A Playlists
        public async Task<PlaylistResponse> updatePlaylist(String[] part, List<Body_Item> _body)
        {
            if (_body == null)
            {
                error = "Please Enter A Body";
                throwException(error);
                return null;
            }

            parts = valueUtil.getPart(part);
            request.method = Method.PUT;
            request.body.body_Items = _body;

            await callAsync();
            return playlistResponse;
        }

        //Delete Playlist
        public async Task<string> deletePlaylistAsync(String id)
        {
            request.parameter = "id=" + id + "&";
            request.method = Method.DELETE;
            await callAsync();
            return _result;
        }

        //Make the Api Call
        private async Task callAsync()
        {
            //set parameters
            request.parameter = parts + mine + parameters;
            switch (request.method)
            {

                case Method.GET:
                    if (request.parameter != null)
                    {
                        result = await service.api.getApiAsync(request);
                    }
                    break;
                case Method.POST:
                    result = await service.api.postApiAsync(request);
                    break;
                case Method.PUT:
                    result = await service.api.postApiAsync(request);
                    break;
                case Method.DELETE:
                    result = await service.api.postApiAsync(request);
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
                playlistResponse = serializer.jsonConvert<PlaylistResponse>(result.content); //serialize results
            }
        }

        //Throw Exception
        private void throwException(string Error)
        {
            throw new ExceptionHandler.BaseException(Error);
        }

    }

}
