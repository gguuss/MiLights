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
using System.Text;
using System.Threading;

namespace MiLightWrapper
{
    class Program
    {

        static Dictionary<String, String> shCommands = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Discovering WiFi LED controllers...");
            Console.WriteLine("IP was: " + MiLightWrapper.Discover());
            InitCommands();                       
            
            bool keepGoing = true;
            while (keepGoing)
            {
                ColorPrompt();
                if (!HandleCommand(Console.ReadLine()))
                {
                    keepGoing = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Later, gator!");
                    Thread.Sleep(250);
                }
                Console.Clear();
            }
        }

        /// <summary>
        /// Sets up the command strings for simplifying the help
        /// message that displays on the console.
        /// </summary>
        static void InitCommands()
        {
            shCommands.Add("alo", "Turn all lights on");
            shCommands.Add("alk", "Kill all lights");
            shCommands.Add("z1o", "Turn zone 1 lights on");
            shCommands.Add("z1k", "Kill zone 1 lights");
            shCommands.Add("z2o", "Turn zone 2 lights on");
            shCommands.Add("z2k", "Kill zone 2 lights");
            shCommands.Add("z3y", "Turn zone 3 lights on");
            shCommands.Add("z3k", "Kill zone 3 lights");
            shCommands.Add("z4y", "Turn zone 4 lights on");
            shCommands.Add("z4k", "Kill zone 4 lights");
            shCommands.Add("blk", "Blink the zones!");
            shCommands.Add("blr", "Baller brightness fading (all zones)!");
            shCommands.Add("swp", "Sweep the zones!");
            shCommands.Add("rgb", "Set a zone to a color!");
            shCommands.Add("rgz", "Loop all the colors!");
            shCommands.Add("all", "Test all the commands!");
            shCommands.Add("new", "Run any new tests.");
            shCommands.Add("cus", "Set all zones to preset custom setting.");
            shCommands.Add("brz", "Set brightness for a zone.");
            shCommands.Add("gho", "Ghost out (quit).");
        }


        /// <summary>
        /// Display a color prompt because color prompts are 1337.
        /// </summary>
        static void ColorPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Commands: ");
            Dictionary<string, string>.KeyCollection commands = shCommands.Keys;
            foreach (string key in commands)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [" + key + "]: " + shCommands[key]);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("MiLight command");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(":>");
        }


        /// <summary>
        /// Method that processes and handles the commands from the console.
        /// </summary>
        /// <param name="command">The 3-character command string.</param>
        /// <returns>True if the program should continue.</returns>
        static bool HandleCommand(string commandStr)
        {
            string[] args = commandStr.Split(' ');

            if (args.Length < 1)
            {
                return true;
            }

            string command = args[0];

            switch (command)
            {
                case "alo":
                    MiLightWrapper.LightsOn();
                    break;
                case "alk":
                    MiLightWrapper.LightsOff();
                    break;
                case "z1o":
                    MiLightWrapper.Zone1AllOn();
                    break;
                case "z1k":
                    MiLightWrapper.Zone1AllOff();
                    break;
                case "z2o":
                    MiLightWrapper.Zone2AllOn();
                    break;
                case "z2k":
                    MiLightWrapper.Zone2AllOff();
                    break;
                case "z3o":
                    MiLightWrapper.Zone3AllOn();
                    break;
                case "z3k":
                    MiLightWrapper.Zone3AllOff();
                    break;
                case "z4o":
                    MiLightWrapper.Zone4AllOn();
                    break;
                case "z4k":
                    MiLightWrapper.Zone4AllOff();
                    break;
                case "blk":
                    BlinkenZonen();
                    break;
                case "rgb":
                    RGBZone();
                    break;
                case "rgz":
                    RGBZoneLoop();
                    break;
                case "new":
                    Tests.TestNew();
                    break;
                case "all":
                    Tests.TestNew();
                    break;
                case "cus":
                    CustomZonesExmaple();
                    break;
                case "brz":
                    BrightnessZone();
                    break;
                case "brr":
                    BrightnessZoneColor();
                    break;
                case "blr":
                    BrightnessAllZone();
                    break;
                case "swp":                                        
                    int speedMs = 300; // speed in milliseconds
                    int repeats = 3; // Sweep play repeats, sweep play repeats.
                    if (args.Length > 1)
                    {
                        if (!int.TryParse(args[1], out speedMs))
                        {
                            speedMs = 300;
                        }
                        if (args.Length > 2)
                        {
                            if (!int.TryParse(args[2], out repeats))
                            {
                                repeats = 1;
                            }
                        }

                    }
                    ZoneSweep(speedMs, repeats);
                    break;
                case "gho":
                    return false;
                default:
                    Console.WriteLine("Nope. Check that command, yo.");
                    Console.ReadLine();
                    break;
            }            
            return true;
        }

        // TODO: Add configuration method for zones
        // TODO: figure out color dimming wtf from R the FM:
        /*
         *  The LimitlessLED bulb remembers its own Brightness setting memory 
         *  separately for ColorRGB and separately for White. For example 
         *  dimming Green, then switching to White full brightness, 
         *  and when you go back to a specific color the brightness returns 
         *  to what the ColorRGB was before. 
         */

        /// <summary>
        /// Interactively lets the user set a zone color.
        /// </summary>
        static void RGBZone()
        {            
            string zoneStr = IntToZoneStr();

            Console.WriteLine("Example Color Strings: VIOLET - 400055, RED- 40B055 ... 40D055");
            Console.Write("Input a COLOR string>");
            string colorStr = Console.ReadLine();

            MiLightWrapper.SetZoneColor(zoneStr, colorStr);
        }

        /// <summary>
        /// Interactively lets the user set a zone color.
        /// </summary>
        static void RGBZoneLoop()
        {
            string zoneStr = IntToZoneStr();

            for (var i = 0; i < MIColors.BASIC_COLORS.Length; i++)
            {                
                MiLightWrapper.SetZoneColor(zoneStr, MIColors.BASIC_COLORS[i]);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Interactively lets the user set a zone color.
        /// </summary>
        static void BrightnessZone()
        {            
            string zoneStr = IntToZoneStr();
            
            Console.Write("Input a brightness value [0...27]>");
            string brightStr = Console.ReadLine();
            int brightness = 0;
            if (!int.TryParse(brightStr, out brightness))
            {
                brightness = 1;
            }

            MiLightWrapper.SetZoneBrightness(zoneStr, brightness);
        }

        /// <summary>
        /// Interactively lets the user set a zone color.
        /// </summary>
        static void BrightnessZoneColor()
        {
            string zoneStr = IntToZoneStr();
            string colorStr = IntToColorStr();

            // TODO: Put integer prompt into loop.
            Console.Write("Input a brightness value [0...27]>");
            string brightStr = Console.ReadLine();
            int brightness = 0;
            if (!int.TryParse(brightStr, out brightness))
            {
                brightness = 1;
            }

            MiLightWrapper.SetZoneBrightness(zoneStr, brightness, colorStr);
        }

        /// <summary>
        /// Not too exciting right now, just sets all zone brightness.
        /// </summary>
        static void BrightnessAllZone()
        {
            string[] loopedColors = { 
                    MIColors.MICOLOR_MINT, 
                    MIColors.MICOLOR_RED, 
                    MIColors.MICOLOR_ROYAL_BLUE 
            };

            // Loop through color array and fade brightness
            for (int c = 0; c < loopedColors.Length; c++)
            {
                for (var direction = -1; direction < 2; direction += 2)
                {
                    int speed = 7 * direction;
                    int limit = 27;
                    bool shouldContinue = true;

                    for (var i = 0; (i  * direction) < limit; i += speed)
                    {
                        MiLightWrapper.SetZoneBrightness(Zones.ZONE_ALL, i * direction, loopedColors[c]);
                        Thread.Sleep(700);       
                    }
                }
            }
        }


        /// <summary>
        /// Gets a string representing the Lights zone given an integer [1...4].
        /// </summary>
        /// <param name="zone">The zone number [1...4].</param>
        /// <returns></returns>
        static string IntToZoneStr()
        {
            Console.Write("Input a zone [1...4]>");
            string zoneStr = Console.ReadLine();
            int zone = 0;
            if (!int.TryParse(zoneStr, out zone))
            {
                zone = 1;
            }

            // Set to the protocol zone string.
            switch (zone)
            {
                case 1:
                    return Zones.ZONE_1;
                case 2:
                    return Zones.ZONE_2;
                case 3:
                    return Zones.ZONE_3;
                case 4:
                    return Zones.ZONE_4;
                default:
                    return Zones.ZONE_ALL;
            }
        }


        /// <summary>
        /// Gets a string representing a basic color.
        /// </summary>
        /// <param name="zone">The color number [1...#colors].</param>
        /// <returns>String representing the color.</returns>
        static string IntToColorStr()
        {
            Console.Write("Input a Color [0..." + MIColors.BASIC_COLORS.Length + "]>");
            string colorStr = Console.ReadLine();
            int color = 0;
            if (!int.TryParse(colorStr, out color))
            {
                color = 1;
            }

            if (color > MIColors.BASIC_COLORS.Length || color < 0)
            {
                color = 0;
            }

            return MIColors.BASIC_COLORS[color];
        }


        /// <summary>
        /// Demonstrates a custom zone configuration being set zone by zone.
        /// </summary>
        static void CustomZonesExmaple()
        {
            MiLightWrapper.SetZone1White();
            MiLightWrapper.SetZoneColor(Zones.ZONE_2, MIColors.MICOLOR_LIME);
            MiLightWrapper.SetZoneColor(Zones.ZONE_2, MIColors.MICOLOR_RED); 
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.MICOLOR_LILAC);           
        }

        /// <summary>
        /// Basic animation test that blinks all the zones.
        /// </summary>
        static void BlinkenZonen()
        {
            for (int i = 0; i < 4; i++)
            {
                MiLightWrapper.LightsOn();
                
                // Sleeping the same is boring, make it get faster.
                // TODO: What's the speed limit for calls to the API?
                // TODO: Wrap API calls with throttling using queue<cmd>.
                Thread.Sleep(500 - (100*i));

                MiLightWrapper.LightsOff();
            }
            Thread.Sleep(250);
            MiLightWrapper.LightsOn();
        }

        /// <summary>
        /// Demo showing turning off / on the lights in sequence.
        /// </summary>        
        /// <param name="sleepTime">Time to sleep while you sweep.</param>
        /// <param name="repeats">Number of times to replay the sweep.</param>
        static void ZoneSweep(int sleepTime, int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                int[] sweepOrderHack = { 4, 2, 1, 3 };
                SweepHelper(false, sleepTime, sweepOrderHack);

                // NOTE: Reverse ordering on back sweep
                sweepOrderHack = new int[] { 3, 1, 2, 4 };
                SweepHelper(true, sleepTime, sweepOrderHack);
            }
        }

        // Integer array corresponding to zones. Can be of any length.
        // This lets you animate the sweep in any order. For example, if
        // you have zones set where:
        //      bath is 1
        //      den is 2
        //      hallway is 3
        //      bedroom is 4
        // And you want to 
        //
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="sleepTime"></param>
        static void SweepHelper(bool isOn, int sleepTime, int[] orderHack)
        {
            if (orderHack.Length == 0 || orderHack == null)
            {
                orderHack = new int[] { 4, 2, 1, 3 };
            }
            for (int i=0; i < orderHack.Length; i++){
                Thread.Sleep(sleepTime);

                // Uses the hack to animate light on/off.
                switch (orderHack[i])
                {
                    case 1:
                        if (isOn)
                        {
                            MiLightWrapper.Zone1AllOn();
                        }
                        else
                        {
                            MiLightWrapper.Zone1AllOff();
                        }
                        break;
                    case 2:
                        if (isOn)
                        {
                            MiLightWrapper.Zone2AllOn();
                        }
                        else
                        {
                            MiLightWrapper.Zone2AllOff();
                        }
                        break;
                    case 3:
                        if (isOn)
                        {
                            MiLightWrapper.Zone3AllOn();
                        }
                        else
                        {
                            MiLightWrapper.Zone3AllOff();
                        }
                        break;
                    case 4:
                        if (isOn)
                        {
                            MiLightWrapper.Zone4AllOn();
                        }
                        else
                        {
                            MiLightWrapper.Zone4AllOff();
                        }
                        break;
                    default:
                        // Default to do nothing.
            
                        break;
                }
            }
        }

    }
}
