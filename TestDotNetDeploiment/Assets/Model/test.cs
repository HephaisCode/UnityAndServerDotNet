using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Request
    {
        public string Method;
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, string> RequestParameters = new Dictionary<string, string>();
        public string Body;
    }

    public class test
    {
        public static string HelloStatic()
        {
            return "Yessss I won! by static method";
        }
        public string Hello()
        {
            return "Yessss I won! by method";
        }

        public string HelloWithRequest(Request sRequest, string sMoreInfos = "")
        {
            string tParam = string.Empty;
            string tHeaders = string.Empty;
            foreach (KeyValuePair<string, string> keyValuePair in sRequest.RequestParameters)
            {
                tParam += "<li>" + keyValuePair.Key + " : " + keyValuePair.Value + "</li>";
            }
            foreach (KeyValuePair<string, string> keyValuePair in sRequest.Headers)
            {
                tHeaders += "<li>" + keyValuePair.Key + " : " + keyValuePair.Value + "</li>";
            }
            return "<html><body><h1>Yessss I won!</h1> " +
                "<h2>Method </h2><p>" + sRequest.Method + "</p>" +
                "<h2>Request Param </h2><ul>" + tParam + "</ul>" +
                "<h2>Headers Param </h2><ul>" + tHeaders + "</ul>" +
                "<h2>More infos </h2><p>" + sMoreInfos + "</p>" +
                "<h2>Body </h2><p>" + sRequest.Body + "</p>" +
                "</body></html>";
        }
    }
}