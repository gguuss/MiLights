using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiLight
{
    class Program
    {
        // Mi-Light router IPs
        static string SERVER_IP = @"192.168.1.174";
        static int SERVER_PORT = 8899;

        static void Main(string[] args)
        {
            Console.WriteLine("Discovering WiFi LED controllers...");
            Discover();
            Console.ReadLine();          
            Console.WriteLine("Turning lights off 0x41 (press any key)>");
            Console.ReadLine();
            LightsOff();
            Console.WriteLine("Lights should be off (press any key)>");
            Console.ReadLine();
            
            Console.WriteLine("Turning lights on 0x42 (press any key)>");
            Console.ReadLine();
            LightsOn();
            Console.WriteLine("Lights should be on, input to exit...");
            Console.ReadLine();
        }

        static void LightsOn()
        {
            ApiCall("420055");
        }

        static void LightsOff()
        {
            ApiCall("410055");
        }


        static void Discover()
        {
            Console.WriteLine("Discovering...");
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 48899); // endpoint where server is listening
            //client.Connect(ep);

            // send multiple discovery requests
            byte[] command = Encoding.ASCII.GetBytes("Link_Wi-Fi");
            for (int i = 0; i < 20; i++)
            {
                client.Send(command, command.Length, ep);
                System.Threading.Thread.Sleep(50);
            }

            // then receive data, the server ip            
            var receivedData = client.Receive(ref ep);                                    
            SERVER_IP = ep.ToString().Split(':')[0];

            Console.WriteLine("Discovered IP is: " + SERVER_IP + " (Press any key)>");
        }


        /// <summary>
        /// Makes an API call using the given opcode.
        /// </summary>
        /// <param name="opCode">Try "41" and "42" for all off all on.</param>
        static void ApiCall(string opCode)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT); // endpoint where server is listening
            client.Connect(ep);

            // send data
            client.Send(HexStringToByteArray(opCode), 3);
        }


        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
