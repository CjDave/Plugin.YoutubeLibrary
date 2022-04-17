using Plugin.Youtube.Result;
using Plugin.Youtube.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Plugin.Youtube.Api_Youtube.RequestClass;

namespace Plugin.Youtube.Api_Youtube.BaseRequests
{
    public class baseRequests
    {
        protected Request request;
        protected string parts, parameters, mine, error;
        private clientService service;
        private string _result;
        private ResultClass result;

     
        //Constructor
        internal baseRequests(clientService Service)
        {
            service = Service;
        }
        protected void init(string resource)
        {
            request = new Request { resource = resource };
            parts = "";
            parameters = "";
            mine = "";
            error = "";
            result = new ResultClass();
        }
        //return the Http result Class
        protected ResultClass getResult()
        {
            ResultClass tempresult = result;
            return tempresult;
        }

        //standard playlists request
        protected async Task getAsync(String[] part, bool Mine)
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
            }
        }

        //Get the Playlists request from optional parameters
        protected async Task getAsync(String[] part,  Parameter[] parameter)
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
                parameters = parameter == null ? parameters : parameters + valueUtil.getParameter(parameter);
                await callAsync();//make the call
            }
        }

        //Insert a playlist
        protected async Task insertAsync(String title, String Description, String[] part, Parameter[] parameter = null, List<Body_Item> _body = null)
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
            }
        }

        //Insert a playlist  with optional parameters
        protected async Task insertAsync(String[] part, Parameter[] parameter, List<Body_Item> _body)
        {
            if (part == null)
            {
                error = "Please Enter A Part";
                throwException(error);
            }
            if (_body == null)
            {
                error = "Please Enter A Body";
                throwException(error);
            }
            //Add parameters if provided
            if (parameter != null)
            {
                parameters = parameters + valueUtil.getParameter(parameter);
            }
            parts = valueUtil.getPart(part);
            request.method = Method.POST;
            request.body.body_Items = _body;
            await callAsync();
        }

        //Update A Playlists
        protected async Task updateAsync(String[] part, List<Body_Item> _body, Parameter[] parameter = null)
        {
            if (_body == null)
            {
                error = "Please Enter A Body";
                throwException(error);
            }
            //Add parameters if provided
            if (parameter != null)
            {
                parameters = parameters + valueUtil.getParameter(parameter);
            }
            parts = valueUtil.getPart(part);
            request.method = Method.PUT;
            request.body.body_Items = _body;
            await callAsync();
        }

        //Delete Playlist
        protected async Task deleteAsync(string id)
        {
            parameters = "id=" + id + "&";
            request.method = Method.DELETE;
            await callAsync();
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

        //Throw Exception
        private void throwException(string Error)
        {
            throw new ExceptionHandler.BaseException(Error);
        }

    }

}

