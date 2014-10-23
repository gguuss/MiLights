using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLightWrapper
{
    class Tests
    {

        /// <summary>
        /// Coverage test, goes through all the commands. Warning,
        /// some of the commands are annoying.
        /// </summary>
        static void TestAll()
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
