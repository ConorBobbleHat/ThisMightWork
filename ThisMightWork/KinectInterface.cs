using System;
using Microsoft.Kinect;
using LightBuzz.Vitruvius;
using System.Collections.Generic;
using System.Linq;

namespace ThisMightWork
{ 
    class KinectInterface
    {
        public KinectSensor kinect = null;
        MultiSourceFrameReader reader;
        GestureController gestureController = null;

        public KinectInterface()
        {
            this.kinect = KinectSensor.GetDefault();

            if (this.kinect != null)
            {
                this.kinect.Open();

                if (this.kinect.IsOpen)
                {

                    Console.Out.WriteLine("Attemping to open Kinect..." + Console.Out.NewLine);

                    reader = kinect.OpenMultiSourceFrameReader(FrameSourceTypes.Body);
                    reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

                    gestureController = new GestureController();
                    gestureController.GestureRecognized += GestureController_GestureRecognized;
                }
            }
        }

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                   IEnumerable<Body> bodyArray = frame.Bodies();

                   IEnumerable<Body> filteredArray = bodyArray.Where(b => b.TrackingId != 0);
                    if (!(filteredArray == null || filteredArray.Count() == 0))
                    {

                        List<BodyWrapper> l = new List<BodyWrapper>();

                        foreach (Body b in filteredArray)
                        {
                            l.Add(new BodyWrapper(b));
                        }

                        Program.jsServer.SendDataFrame(l);
                    }

                    Body body = bodyArray.Closest();

                    if (body != null)
                    {
                        gestureController.Update(body);
                    }
                }
            }
        }

        void GestureController_GestureRecognized(object sender, GestureEventArgs e)
        {
            //Console.Out.Write(e.GestureType.ToString() + Console.Out.NewLine);
            Program.jsServer.SendGestureData(e);
        }
    }

    class BodyWrapper
    {
        public int rightHandConfidence;
        public int leftHandConfidence;

        public int rightHandState;
        public int leftHandState;
        public bool isTracked;
        public ulong trackingID;
        public Dictionary<JointType, CBPoint> joints = new Dictionary<JointType, CBPoint>();

        public BodyWrapper(Body b)
        {
            this.rightHandConfidence = (int)b.HandRightConfidence;
            this.leftHandConfidence = (int)b.HandLeftConfidence;

            this.rightHandState = (int)b.HandRightState;
            this.leftHandState = (int)b.HandLeftState;

            this.isTracked = b.IsTracked;
            this.trackingID = b.TrackingId;

            foreach (KeyValuePair<JointType, Joint> j in b.Joints)
            {
                this.joints.Add(j.Key, new CBPoint(j.Value));
            }

        }
    }

    public class CBPoint
    {
        public double x;
        public double y;
        public double z;

        public CBPoint (Joint j)
        {
            var tmp = Program.kinect.kinect.CoordinateMapper.MapCameraPointToColorSpace(j.Position);

            this.x = tmp.X;
            this.y = tmp.Y;
            this.z = j.Position.Z;
        }
    }
}
