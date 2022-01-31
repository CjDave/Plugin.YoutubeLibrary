
namespace YoutubeLibrary.youtube.Parameters
{
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
    public class Body_Item
    {
        public string title { get; init; }
        public string value { get; init; }
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
    //Body value
    public record Body
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
        public enum Method { GET, POST, DELETE, UPDATE }
        //Header values
        public struct Request
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
}
