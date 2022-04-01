using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Plugin.Youtube.Result
{
    public class PlaylistResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet snippet { get; set; }
    }
}
