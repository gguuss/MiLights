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
using System.Threading.Tasks;

namespace MiLight
{
    class Program
    {
        // Mi-Light router IPs
        static string SERVER_IP = @"192.168.1.174";
        static int SERVER_PORT = 8899;

        // Set to true if the device hasn't been discovered.
        // Reset the device if discovery fails
        static bool TEST_DISCOVER = false; 

        static void Main(string[] args)
        {
            Console.WriteLine("Discovering WiFi LED controllers...");
            if (TEST_DISCOVER)
            {
                Discover();
                Console.ReadLine();
            }
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
