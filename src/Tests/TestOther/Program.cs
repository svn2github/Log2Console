using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TestOther
{
    class Program
    {
        const string logBody =
@"<log4j:event logger=""UserQuery"" level=""INFO"" timestamp=""1463245168149"" thread=""12"">
    <log4j:message>{0}</log4j:message>
    <log4j:properties>
        <log4j:data name=""log4japp"" value=""LINQPad Query Server(13380)"" />
        <log4j:data name=""log4jmachinename"" value=""DLM-DEV"" />
    </log4j:properties>
</log4j:event>";

        /// <summary>
        /// Make sure NLog can gracefully handle partial messages, malformed messages, etc
        /// http://www.w3schools.com/xml/xml_syntax.asp
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("x to exit, anything else to continue");
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.X)
            {
                using (TcpClient client = new TcpClient("localhost", 4505))
                {
                    var stream = client.GetStream();

                    SendLogMessage(stream, "Extra text in open",
                        logBody.Replace("<log4j:message>", "<log4j:message            123456  >"));

                    SendLogMessage(stream, "Normal Message");

                    SendLogMessage(stream, "Warn message",
                        logBody.Replace("level=\"INFO\"", "level=\"WARN\""));

                    SendLogMessage(stream, "comments",
                        "<!-- Comments? -->" + logBody);

                    SendLogMessage(stream, "Extra spaces in open",
                        logBody.Replace("<log4j:message>", "<log4j:message              >"));

                    SendLogMessage(stream, "Extra spaces in close",
                        logBody.Replace("</log4j:event>", "</log4j:event            >"));

                    SendLogMessage(stream, "Done with Test messages");

                    //stream.Flush();
                    stream.Close();
                }
                Console.WriteLine("x to exit, anything else to continue");
                key = Console.ReadKey();
            }
        }

        static void SendLogMessage(NetworkStream stream, string logMessage, string logBody = logBody)
        {
            Console.WriteLine(logMessage);
            byte[] buffer = Encoding.UTF8.GetBytes(String.Format(logBody, logMessage));
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
