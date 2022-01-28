using YoutubeLibrary.youtube;

//utilities for performing various tasks
namespace YoutubeLibrary.util
{
    public static class valueUtil
    {
        public static string getPart(String[] part)
        {
            string parts = "";
            foreach (string temp in part)
            {
                parts = parts + "part=" + temp + "&";
            }
            return parts;
        }
        public static string isMine(bool mine)
        {
            return mine ? "mine=true" : "&mine=false";
        }
        public static string getPairs(Parameters.Pair_Value[] pairs)
        {
            string parameters = "";
            foreach (Parameters.Pair_Value temp in pairs)
            {
                parameters = temp.pair + "=" + temp.value;
            }
            return parameters;
        }
    }
    public static class exceptionHandler
    {
        public static string returnException(Exception e)
        {
            return e.Message;
        }
        public static string showError(string error)
        {
            return error;
        }
    }
}
