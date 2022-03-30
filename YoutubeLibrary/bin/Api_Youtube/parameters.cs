using System.Collections.Generic;


namespace Plugin.Youtube.Api_Youtube
{
    public class Header
    {
        public string title { get; set; }
        public string value { get; set; }
    }
    //Parameter
    public class Parameter
    {
        public string title { get; set; }
        public string value { get; set; }
        public Parameter(string Title, string Value)
        {
            title = Title;
            value = Value;
        }
    }
    //Body Item
    public class Body_Item
    {
        public string title { get; set; }
        public string value { get; set; }
        public List<Body_Item> values;
        public Body_Item(string Title)
        {
            title = Title;
            value = "";
            values = new List<Body_Item>();
        }

        public Body_Item(string Title, string Value)
        {
            title = Title;
            value = Value;
            values = new List<Body_Item>();
        }
        public Body_Item(string Title, List<Body_Item> Values)
        {
            title = Title;
            values = Values;
            value = "";
        }

    }
    //Body 
    public class Body
    {
        public List<Body_Item> body_Items;
        public Body()
        {
            body_Items = new List<Body_Item>();
        }
    }

    public class RequestClass
    {
        public const string Youtube = "https://youtube.googleapis.com/youtube/v3/";
        public const string Http = "HTTP/1.1";
        public enum Method { GET, POST, DELETE, PUT }
        //Header values
        public class Request
        {
            public Method method { get; set; }
            public string resource = "";
            public string parameter = "";
            public string youtube = Youtube;
            public Header[] header = null;
            public Body body = new Body();
            public string http = Http;
            public string uri = "";
            public bool Mine = true;

        };
    }
    public class ResultClass
    {
        internal string error { get; set; }
        internal int code { get; set; }
        internal string content { get; set; }

        internal ResultClass()
        {
            error = null;
            code = 0;
            content = null;
        }
    }
}
