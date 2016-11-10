using System;
using WebSocketSharp.Server;

namespace ThisMightWork
{
    class Program
    {

        public static JSWebSocketServer jsServer;
        public static KinectInterface kinect;

        static void Main(string[] args)
        {
            kinect = new KinectInterface();

            //var mKinect = new MockKinectInterface();    

            var wssv = new WebSocketServer(8070);

            wssv.AddWebSocketService<JSWebSocketServer>("/kinect", () => (jsServer = new JSWebSocketServer()));

            wssv.Start();
            Console.Out.Write("Press any key to stop." + Console.Out.NewLine);
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
