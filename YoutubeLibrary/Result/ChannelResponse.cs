using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Youtube.Result
{
    public class ChannelResponse
    {
       
        public class RelatedPlaylists
        {
            public string likes { get; set; }
            public string uploads { get; set; }
        }

        public class ContentDetails
        {
            public RelatedPlaylists relatedPlaylists { get; set; }
        }

        public class Statistics
        {
            public string viewCount { get; set; }
            public string subscriberCount { get; set; }
            public bool hiddenSubscriberCount { get; set; }
            public string videoCount { get; set; }
        }

        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
            public ContentDetails contentDetails { get; set; }
            public Statistics statistics { get; set; }
        }

       
            public string kind { get; set; }
            public string etag { get; set; }
            public PageInfo pageInfo { get; set; }
            public List<Item> items { get; set; }
       

    }
}
