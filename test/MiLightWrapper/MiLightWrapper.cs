using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MiLightWrapper
{
    class MiLightWrapper
    {
        // Mi-Light router IPs
        // TODO: arrays
        private static string SERVER_IP = @"192.168.1.174";
        private static int SERVER_PORT = 8899;

        // TODO: Make public? Does anyone use this?
        private static string LIGHTS_OFF = "410055";
        private static string LIGHTS_ON = "420055";
        private static string DISCO_SLOWER = "430055";
        private static string DISCO_FASTER = "440055";
        private static string ZONE_1_ALL_ON = "450055";
        private static string ZONE_1_ALL_OFF = "460055";
        private static string ZONE_2_ALL_ON = "470055";
        private static string ZONE_2_ALL_OFF = "480055";
        private static string ZONE_3_ALL_ON = "490055";
        private static string ZONE_3_ALL_OFF = "4A0055";
        private static string ZONE_4_ALL_ON = "4B0055";
        private static string ZONE_4_ALL_OFF = "4C0055";
        private static string DISCO_MODE = "4D0055";

        // Compound commands, 100ms delay between sending of codes.
        // TODO: Refactor compound commands to separate class
        private static string[] SET_ALL_WHITE = { "420055", "C20055" };
        private static string[] SET_1_WHITE = { "450055", "C50055" };
        private static string[] SET_2_WHITE = { "470055", "C70055" };
        private static string[] SET_3_WHITE = { "490055", "C90055" };
        private static string[] SET_4_WHITE = { "4B0055", "CB0055" };
        
        // TODO: COLORS!

        /// <summary>
        /// Turns all of the lights on and does not affect color or brightness.
        /// </summary>
        static public void LightsOn()
        {
            ApiCall(LIGHTS_ON);
        }

        /// <summary>
        /// Turns all of the lights off.
        /// </summary>
        static public void LightsOff()
        {
            ApiCall(LIGHTS_OFF);
        }

        /// <summary>
        /// Enable the faster disco setting. 
        /// TODO: Better description, what' this do?
        /// </summary>
        static public void DiscoSlower()
        {
            ApiCall(DISCO_SLOWER);
        }

        /// <summary>
        /// Make the disco faster.
        /// TODO: What's this actually do?
        /// </summary>
        static public void DiscoFaster()
        {
            ApiCall(DISCO_FASTER);
        }

        /// <summary>
        /// Turn on all the zone 1 lights.
        /// </summary>
        static public void Zone1AllOn()
        {
            ApiCall(ZONE_1_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 1 lights.
        /// </summary>
        static public void Zone1AllOff()
        {
            ApiCall(ZONE_1_ALL_OFF);
        }

        /// <summary>
        /// Turn on all the zone 2 lights.
        /// </summary>
        static public void Zone2AllOn()
        {
            ApiCall(ZONE_2_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 2 lights.
        /// </summary>
        static public void Zone2AllOff()
        {
            ApiCall(ZONE_2_ALL_OFF);
        }

        /// <summary>
        /// Turn on all the zone 3 lights.
        /// </summary>
        static public void Zone3AllOn()
        {
            ApiCall(ZONE_3_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 3 lights.
        /// </summary>
        static public void Zone3AllOff()
        {
            ApiCall(ZONE_3_ALL_OFF);
        }

        /// <summary>
        /// Turn on all the zone 4 lights.
        /// </summary>
        static public void Zone4AllOn()
        {
            ApiCall(ZONE_4_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 4 lights.
        /// </summary>
        static public void Zone4AllOff()
        {
            ApiCall(ZONE_4_ALL_OFF);
        }

        /// <summary>
        /// Enables disco mode, this annoying thing that dims or blinks 
        /// all the lights.
        /// </summary>
        static public void DiscoMode()
        {
            ApiCall(DISCO_MODE);
        }

        /// <summary>
        /// Sets all the lights to the white color.
        /// </summary>
        static public void SetAllWhite()
        {
            ApiCall(SET_ALL_WHITE, 100);
        }

        /// <summary>
        /// Sets all the zone 1 lights to the white color.
        /// </summary>
        static public void SetZone1White()
        {
            ApiCall(SET_1_WHITE, 100);
        }

        /// <summary>
        /// Sets all the zone 1 lights to the white color.
        /// </summary>
        static public void SetZone2White()
        {
            ApiCall(SET_2_WHITE, 100);
        }

        /// <summary>
        /// Sets all the zone 1 lights to the white color.
        /// </summary>
        static public void SetZone3White()
        {
            ApiCall(SET_3_WHITE, 100);
        }

        /// <summary>
        /// Sets all the zone 1 lights to the white color.
        /// </summary>
        static public void SetZone4White()
        {
            ApiCall(SET_4_WHITE, 100);
        }

        /// <summary>
        /// Performs the discovery command to find all the WiFi radio bridges.
        /// </summary>
        /// <returns>A string with the IP of the first WiFi radio bridge.</returns>
        static public string Discover()
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

            // TODO: Safe console write or use real logging.
            Console.WriteLine("Discovered IP is: " + SERVER_IP + " (Press any key)>");
            return SERVER_IP;
        }


        /// <summary>
        /// Makes an API call using the given opcode.
        /// </summary>
        /// <param name="cmdCode">The command to send to the WiFi transceiver.</param>
        static void ApiCall(string cmdCode)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT); // endpoint where server is listening
            client.Connect(ep);

            // send data
            client.Send(HexStringToByteArray(cmdCode), 3);
        }

        /// <summary>
        /// Makes batch API calls using the given commands and delay.
        /// </summary>
        /// <param name="cmdCode">The commands to send to the WiFi transceiver.</param>
        /// <param name="delay">The amount of time to delay between commands.</param>
        static void ApiCall(string[] cmdCode, int delay)
        {
            for (int i = 0; i < cmdCode.Length; i++)
            {
                ApiCall(cmdCode[i]);
                Thread.Sleep(delay);
            }
        }

        /// <summary>
        /// Utility method for converting the command codes to the 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
