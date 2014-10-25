MiLights
========
Project showing discovery and using UDP calls for the Mi-Lights WiFi LEDs.

# Background #
The Mi-Lights are inexpensive RGB light bulbs ($14 / piece) that behave similar to 
the Phillips Hue and other WiFi bulbs. These bulbs are controlled over a radio network.
There is a WiFi unit that bridges the radio network to the bulbs. The WiFi unit is 
simply controlled using UDP broadcast messages. The messages are listed on 
the [LimitLess LED developer site](http://www.limitlessled.com/dev/).

This project was done in C# and includes a simple test app and a console app.

# What works #
* WiFi connector/bridge discovery
* Setting colors of lights
* Setting brightness of lights
* Animating lights (it's limited, the lights turn white on every change)

# TODO #
* Add support for multiple WiFi boxes
* Determine optimal light change settings
* Add more modes
