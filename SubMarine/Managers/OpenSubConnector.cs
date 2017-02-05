using System;
using CookComputing.XmlRpc;

namespace SubMarine.Managers
{
    public class OpenSubConnector
    {
        private IOpenSubConnector _connector { get; }

        private XmlRpcStruct subtitleInfo { get; set; }

        public string Token { get; private set; }


        public OpenSubConnector()
        {
            _connector = (IOpenSubConnector)XmlRpcProxyGen.Create(typeof(IOpenSubConnector));
        }

        public void Login()
        {
            try
            {
                //TODO Extract 
                var result = (XmlRpcStruct)_connector.Login("", "", "es", "EasySubtitles");
                Token = (string)result["token"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SearchSubtitles(string language, FileInfo fileInfo)
        {
            var detail = new XmlRpcStruct[1];
            detail[0] = new XmlRpcStruct { { "sublanguageid", language }, { "moviehash", fileInfo.Hash }, { "moviebytesize", fileInfo.Size } };

            try
            {
                subtitleInfo = (XmlRpcStruct)_connector.SearchSubtitles(Token, detail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void DownloadSubtitles( int[] data)
        {
        
        }
    }
}