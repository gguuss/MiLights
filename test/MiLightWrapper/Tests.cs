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

namespace MiLightWrapper
{
    class Tests
    {

        /// <summary>
        /// Used for testing new methods. All methods eventually go in TestAll.
        /// </summary>
        static public void TestNew()
        {
            PromptCall("Setting Zone 4 Brightness to 0 (min)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 1);
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.COLOR_LAVENDER);            

            PromptCall("Setting Zone 4 Brightness to 10 (moar)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 10);
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.COLOR_LAVENDER);            

            PromptCall("Setting Zone 4 Brightness to 27 (max)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 20);
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.COLOR_LAVENDER);            

            PromptCall("Setting Zone 4 Brightness to 15 (??)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 15);
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.COLOR_LAVENDER);            
        }

        /// <summary>
        /// Coverage test, goes through all the commands. Warning,
        /// some of the commands are annoying.
        /// TODO: Group tests
        /// </summary>
        static public void TestAll()
        {
            PromptCall("Turning lights off 0x41 (press any key)>");
            MiLightWrapper.LightsOff();
            PromptCall("Lights should be off (press any key)>");

            PromptCall("Turning lights on 0x42 (press any key)>");
            MiLightWrapper.LightsOn();
            PromptCall("Lights should be on");

            PromptCall("Turning lights off zone 1 (press any key)>");
            MiLightWrapper.Zone1AllOff();
            PromptCall("Lights should be off, input to exit...");

            PromptCall("Turning lights on zone 1 (press any key)>");
            MiLightWrapper.Zone1AllOn();
            PromptCall("Lights should be on, input to exit...");

            PromptCall("Turning lights on for zone 2 (press any key)>");
            MiLightWrapper.Zone2AllOff();
            PromptCall("Lights should be off, input to exit...");

            PromptCall("Turning lights on zone 2 (press any key)>");
            MiLightWrapper.Zone2AllOn();
            PromptCall("Lights should be on, input to exit...");

            PromptCall("Turning lights on for zone 3 (press any key)>");
            MiLightWrapper.Zone3AllOff();
            PromptCall("Lights should be off, input to exit...");

            PromptCall("Turning lights on zone 3 (press any key)>");
            MiLightWrapper.Zone3AllOn();
            PromptCall("Lights should be on, input to exit...");

            PromptCall("Turning lights on for zone 4 (press any key)>");
            MiLightWrapper.Zone4AllOff();
            PromptCall("Lights should be off, input to exit...");

            PromptCall("Turning lights on zone 4 (press any key)>");
            MiLightWrapper.Zone4AllOn();
            PromptCall("Lights should be on, input to exit...");

            // I don't like to disco.
            PromptCall("Time to disco (press any key)>");
            MiLightWrapper.DiscoMode();
            PromptCall("Disco activated, dance");

            PromptCall("No more disco (press any key)>");
            MiLightWrapper.LightsOn();
            PromptCall("Disco deactivated, you can now sit back down");

            PromptCall("Setting all white");
            MiLightWrapper.SetAllWhite();

            PromptCall("Setting Zone 1 white");
            MiLightWrapper.SetZone1White();

            PromptCall("Setting Zone 2 white");
            MiLightWrapper.SetZone2White();

            PromptCall("Setting Zone 3 white");
            MiLightWrapper.SetZone3White();

            PromptCall("Setting Zone 4 white");
            MiLightWrapper.SetZone4White();

            PromptCall("Setting Zone 4 Lilac");
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.MICOLOR_LILAC);

            PromptCall("Setting Zone 4 Aqua");
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.MICOLOR_AQUA);

            PromptCall("Setting Zone 4 Green");
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.MICOLOR_GREEN);

            PromptCall("Setting Zone 4 Red");
            MiLightWrapper.SetZoneColor(Zones.ZONE_4, MIColors.MICOLOR_RED);

            PromptCall("Setting Zone 4 Brightness to 0 (min)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 0);

            PromptCall("Setting Zone 4 Brightness to 5");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 5);

            PromptCall("Setting Zone 4 Brightness to 10");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 10);

            PromptCall("Setting Zone 4 Brightness to 27 (full)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 27);

            PromptCall("Setting Zone 4 Brightness to 16 (just right)");
            MiLightWrapper.SetZoneBrightness(Zones.ZONE_4, 16);

        }


        /// <summary>
        /// Wraps test API calls. 
        /// TODO: add support for passing in the API call
        /// </summary>
        /// <param name="prompt"></param>
        static void PromptCall(string prompt)
        {
            Console.WriteLine(prompt);
            // TODO: APIcall
            Console.ReadKey();
        }
    }
}
