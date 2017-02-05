using CookComputing.XmlRpc;

namespace SubMarine.Managers
{
    [XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")]
    public interface IOpenSubConnector : IXmlRpcProxy
    { 
        [XmlRpcMethod("LogIn")]
        object Login(string username, string password, string language, string useragent);

        [XmlRpcMethod("SearchSubtitles")]
        object SearchSubtitles(string token, XmlRpcStruct[] data);

        [XmlRpcMethod("DownloadSubtitles")]
        object DownloadSubtitles(string token, int[] data);

    }
}