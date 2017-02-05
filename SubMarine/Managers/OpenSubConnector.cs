using System;
using CookComputing.XmlRpc;

namespace SubMarine.Managers
{
    public class OpenSubConnector 
    {
        private IOpenSubConnector connector { get; }

        public string Token { get; private set; }

        public OpenSubConnector()
        {
            connector = (IOpenSubConnector)XmlRpcProxyGen.Create(typeof(IOpenSubConnector));
        }

        public void Login(string username, string password, string language, string useragent)
        {
            try
            {
                var result = (XmlRpcStruct) connector.Login(username, password, language, useragent);
                Token = (string) result["token"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public object SearchSubtitles(string token, XmlRpcStruct[] data)
        {
            throw new NotImplementedException();
        }

        public object DownloadSubtitles(string token, int[] data)
        {
            throw new NotImplementedException();
        }
    }
}