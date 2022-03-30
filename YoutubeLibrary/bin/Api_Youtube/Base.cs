
using Plugin.Youtube.Api;
using Plugin.Youtube.Api_Youtube.Channels;
using Plugin.Youtube.Api_Youtube.Playlists;

namespace Plugin.Youtube.Api_Youtube
{
    internal class Base
    {
        internal string Api_key { get; set; }
        internal string Access_Token { get; set; }

        public Base(string key)
        {
            Api_key = key;
            Access_Token = null;
        }
        //for non oauth services
        public Base(string key, string token)
        {
            Api_key = key;
            Access_Token = token;
        }
    }
    internal class clientService
    {
        private Base credential { get; set; }
        internal ApiCall api;
        internal bool hasAccessToken;
        internal clientService(Base Credential)
        {
            credential = Credential;
            api = new ApiCall(Credential);
            hasAccessToken = Credential.Access_Token == null ? true : false;
        }
    }
    public class YoutubeClient
    {
        private clientService service;
        public PlaylistLists playlistsList;
        public Channel channel;

        //initialization without 0auth token
        public YoutubeClient(string key)
        {
            service = new clientService(new Base(key));
            initResources();

        }
        //initialization with 0auth token
        public YoutubeClient(string key, string token)
        {
            service = new clientService(new Base(key, token));
            initResources();
        }
        //create all the resources
        private void initResources()
        {
            playlistsList = new PlaylistLists(service);
            channel = new Channel(service);
        }

    }
}
