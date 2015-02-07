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
    class Commands
    {
        // TODO: Make public? Does anyone use this?
        public static string LIGHTS_OFF = "410055";
        public static string LIGHTS_ON = "420055";
        public static string DISCO_SLOWER = "430055";
        public static string DISCO_FASTER = "440055";
        public static string ZONE_1_ALL_ON = "450055";
        public static string ZONE_1_ALL_OFF = "460055";
        public static string ZONE_2_ALL_ON = "470055";
        public static string ZONE_2_ALL_OFF = "480055";
        public static string ZONE_3_ALL_ON = "490055";
        public static string ZONE_3_ALL_OFF = "4A0055";
        public static string ZONE_4_ALL_ON = "4B0055";
        public static string ZONE_4_ALL_OFF = "4C0055";
        public static string DISCO_MODE = "4D0055";
    }
}
