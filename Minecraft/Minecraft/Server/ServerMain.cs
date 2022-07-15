using System.Text;
using slf4net;

namespace Minecraft.Server;

public static class ServerMain
{
    private static readonly ILogger Logger = LoggerFactory.GetLogger(typeof(ServerMain));

    public static void Main(string[] args)
    {

    }

    static ServerMain()
    {
        Thread.CurrentThread.Name = "ServerThread";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }
}