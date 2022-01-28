
namespace YoutubeLibrary.youtube
{
    internal class Base
    {
        internal string Api_key { get; set; }
        internal string Access_Token { get; set; }
        public Base(string key)
        {
            Api_key = key;
            Access_Token= null; 
        }
        //for non oauth services
        public Base(string key, string token)
        {
            Api_key = key;
            Access_Token = token;
        }

    }

    public class Parameters
    {
        public const string Youtube = "https://youtube.googleapis.com/youtube/v3/";
        public const string Http = "HTTP/1.1";
        public enum Method { GET, POST, DELETE, UPDATE }
        //Header values
        public record Header
        {
            public string title { get; init; }
            public string value { get; init; }
        }
        public record Pair_Value
        {
            public string pair { get; init; }
            public string value { get; init; }
        }
        //Body value
        public record Body
        {
            public string title { get; init; }
            public Body content { get; init; }
        }
        public struct Request
        {
            public Method method { get; set; } 
            public string resource="";
            public string parameter = "";
            public string youtube = Youtube;
            public Header[] header = null;
            public Body body = null;
            public string http = Http;
            public string uri = "";
            public bool Mine=true;

        };

    }
}
