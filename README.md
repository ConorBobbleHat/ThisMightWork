# ThisMightWork 

#### The ThisMightWork server.

This is the server half of the ThisMightWork program. This is a program that will stream live body & gesture data (on windows only!) from the Kinect v2 sensor.


You can find the client [here](https://github.com/2BoysAndHats/KinectJSBridge).

## Installation

### Step one: Hardware

For the library to run correctly, you will need

* A Kinect V2 sensor
* A Kinect V2 adaptor
* A 64 bit windows machine, with a USB 3.0 processer [(for more details, see the microsoft page, look under recommended system requirements)](https://www.microsoft.com/en-us/download/details.aspx?id=44561) 

Simply connect according to the instructions on the box. <b>Don't plug it into your pc just yet.</b>

### Step two: Plugging in

Once you've linked everything together, plug the usb end into a <b>USB 3.0</b> port on your computer. USB 2.0 will not work.

You should see the standard windows driver installation window. When it's done, reboot your pc.

### Step three: Installing the software

Now download the latest ThisMightWork server software from [here](https://github.com/2BoysAndHats/ThisMightWork/raw/master/TMW_Setup.exe)

Run the installer, and when you're done, you're ready to try the demos (included in the [client software](https://github.com/2BoysAndHats/KinectJSBridge).), or start coding!

## Credits

This library is built on 

* [The Microsoft v2 Kinect api](https://developer.microsoft.com/en-us/windows/kinect)
* [The Vitruvius framework](http://vitruviuskinect.com/)
* [websocket-sharp](https://github.com/sta/websocket-sharp)
