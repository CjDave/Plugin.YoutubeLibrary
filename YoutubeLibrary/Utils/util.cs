
using System;
using Plugin.Youtube.Api_Youtube;
//utilities for performing various tasks
namespace Plugin.Youtube.Utils
{
    public static class valueUtil
    {
        //Convert the string array to parameters
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
            return mine ? "mine=true&" : "mine=false&";
        }
        //Convert to parameters
        public static string getParameter(Parameter[] pairs)
        {
            string parameter = "";
            foreach (Parameter temp in pairs)
            {
                parameter = parameter + temp.title + "=" + temp.value+"&";
            }
            return parameter;
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
