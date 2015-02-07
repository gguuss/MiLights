/*
 * Copyright 2014 Gus Class All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


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


        // Compound commands, 100ms delay between sending of codes.
        // TODO: Refactor compound commands to separate class
        // NOTE: second command is zone, first is color

        private static string[] SET_ALL_WHITE = { Commands.LIGHTS_ON, Zones.ZONE_ALL };
        private static string[] SET_1_WHITE = { Commands.ZONE_1_ALL_ON, Zones.ZONE_1 };
        private static string[] SET_2_WHITE = { Commands.ZONE_2_ALL_ON, Zones.ZONE_2 };
        private static string[] SET_3_WHITE = { Commands.ZONE_3_ALL_ON, Zones.ZONE_3 };
        private static string[] SET_4_WHITE = { Commands.ZONE_4_ALL_ON, Zones.ZONE_4 };


        /// <summary>
        /// Turns all of the lights on and does not affect color or brightness.
        /// </summary>
        static public void LightsOn()
        {
            ApiCall(Commands.LIGHTS_ON);
        }

        /// <summary>
        /// Turns all of the lights off.
        /// </summary>
        static public void LightsOff()
        {
            ApiCall(Commands.LIGHTS_OFF);
        }

        /// <summary>
        /// Enable the faster disco setting. 
        /// TODO: Better description, what' this do?
        /// </summary>
        static public void DiscoSlower()
        {
            ApiCall(Commands.DISCO_SLOWER);
        }

        /// <summary>
        /// Make the disco faster.
        /// TODO: What's this actually do?
        /// </summary>
        static public void DiscoFaster()
        {
            ApiCall(Commands.DISCO_FASTER);
        }

        /// <summary>
        /// Turn on all the zone 1 lights.
        /// </summary>
        static public void Zone1AllOn()
        {
            ApiCall(Commands.ZONE_1_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 1 lights.
        /// </summary>
        static public void Zone1AllOff()
        {
            ApiCall(Commands.ZONE_1_ALL_OFF);
        }

        /// <summary>
        /// Turn on all the zone 2 lights.
        /// </summary>
        static public void Zone2AllOn()
        {
            ApiCall(Commands.ZONE_2_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 2 lights.
        /// </summary>
        static public void Zone2AllOff()
        {
            ApiCall(Commands.ZONE_2_ALL_OFF);
        }

        /// <summary>
        /// Turn on all the zone 3 lights.
        /// </summary>
        static public void Zone3AllOn()
        {
            ApiCall(Commands.ZONE_3_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 3 lights.
        /// </summary>
        static public void Zone3AllOff()
        {
            ApiCall(Commands.ZONE_3_ALL_OFF);
        }

        /// <summary>
        /// Turn on all the zone 4 lights.
        /// </summary>
        static public void Zone4AllOn()
        {
            ApiCall(Commands.ZONE_4_ALL_ON);
        }

        /// <summary>
        /// Turn off all the zone 4 lights.
        /// </summary>
        static public void Zone4AllOff()
        {
            ApiCall(Commands.ZONE_4_ALL_OFF);
        }

        /// <summary>
        /// Enables disco mode, this annoying thing that dims or blinks 
        /// all the lights.
        /// </summary>
        static public void DiscoMode()
        {
            ApiCall(Commands.DISCO_MODE);
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
        /// Sets a zone to a color.
        /// </summary>
        /// <param name="zone">The zone command string from Zones class.</param>
        /// <param name="color">The color command string from MIColors class.</param>
        static public void SetZoneColor(string zone, string color)
        {
            string[] commands = new string[]{zone, color};
            ApiCall(commands, 100);
        }

        /// <summary>
        /// Sets a zone to a brightness.
        /// </summary>
        /// <param name="zone">The zone command string from Zones class.</param>
        /// <param name="color">The brightness value (0-27).</param>
        static public void SetZoneBrightness(string zone, int brightness, string color = null)
        {
            String command = "4E" + brightness.ToString("X2") + "55";
            string[] commands = null;
            if (color == null)
            {
                commands = new string[] { zone, command };
            }
            else
            {
                commands = new string[] { zone, color, command };
            }
            
            ApiCall(commands, 100);
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
