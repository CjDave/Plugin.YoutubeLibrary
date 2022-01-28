
using YoutubeLibrary.Api;

namespace YoutubeLibrary.youtube
{
    internal class clientService
    {
        private Base credential { get;  set; }
        internal ApiCall api;
        internal clientService(Base Credential)
        {
            credential = Credential;
            api = new ApiCall(Credential);
        }
    }
    public class YoutubeClient
    {
        private clientService service;
        public PlaylistLists playlistsList;
      
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
        }

    }
}
