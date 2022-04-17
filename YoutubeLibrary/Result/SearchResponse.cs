using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Youtube.Result
{
    public class SearchResponse
    {
        public class Id
        {
            public string kind { get; set; }
            public string videoId { get; set; }
        }
        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public Id id { get; set; }
            public Snippet snippet { get; set; }
        }
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string regionCode { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<Item> items { get; set; }
    }
}
