using Plugin.Youtube.Api_Youtube.BaseRequests;
using Plugin.Youtube.Result;
using Plugin.Youtube.Utils;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace Plugin.Youtube.Api_Youtube.PlaylistItems
{
    public class playlistItems : baseRequests
    {
        private string resource;
        //Constructor
        internal playlistItems(clientService Service) : base(Service)
        {
            resource = "playlistItems?";
        }
        //get PlaylistItems
        public async Task<PlaylistItemsResponse> getPlaylistItemsAsync(String[] part, String playlistId)
        {
            base.init(resource);
            Parameter p = new Parameter("playlistId", playlistId);
            await base.getAsync(part, new Parameter[] { p });
            return serializer.jsonConvert<PlaylistItemsResponse>(base.getResult().content);
        }

        //Get PlaylistItems 
        public async Task<PlaylistItemsResponse> getPlaylistItemsAsync(String[] part, bool Mine)
        {
            base.init(resource);
            await base.getAsync(part, Mine);
            return serializer.jsonConvert<PlaylistItemsResponse>(base.getResult().content);
        }

        //Insert PlaylistItems
        public async Task<PlaylistItemsResponse> insertPlaylistItemsAsync(String title, String Description, String[] part, Parameter[] parameter = null, List<Body_Item> _body = null)
        {
            base.init(resource);
            await base.insertAsync(title, Description, part, parameter, _body);
            return serializer.jsonConvert<PlaylistItemsResponse>(base.getResult().content);
        }

        //Insert a playlist  with optional parameters
        public async Task<PlaylistItemsResponse> insertPlaylistItemsAsync(String[] part, Parameter[] parameter, List<Body_Item> _body)
        {
            base.init(resource);
            await base.insertAsync(part, parameter, _body);
            return serializer.jsonConvert<PlaylistItemsResponse>(base.getResult().content);

        }

        //Update A Playlists
        public async Task<PlaylistItemsResponse> updatePlaylistItems(String[] part, List<Body_Item> _body, Parameter[] parameter = null)
        {
            base.init(resource);
            await base.updateAsync(part, _body, parameter);
            return serializer.jsonConvert<PlaylistItemsResponse>(base.getResult().content);
        }

        //Delete Playlist
        public async Task<string> deletePlaylistAsyncItems(string id)
        {
            base.init(resource);
            await base.deleteAsync(id);
            return "";
        }
    }
}
