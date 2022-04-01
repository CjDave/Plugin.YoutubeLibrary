using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Youtube.Result
{
    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }

    public class Default
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Medium
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class High
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnails
    {
        public Default @default { get; set; }
        public Medium medium { get; set; }
        public High high { get; set; }
    }

    public class Localized
    {
        public string title { get; set; }
        public string description { get; set; }
    }
    public class Player
    {
        public string embedHtml { get; set; }
    }

    public class Snippet
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime publishedAt { get; set; }
        public Thumbnails thumbnails { get; set; }
        public Localized localized { get; set; }
    }

  
    public class Status
    {
        public string privacyStatus { get; set; }
    }

    public class ContentDetails
    {
        public int itemCount { get; set; }
    }


}
