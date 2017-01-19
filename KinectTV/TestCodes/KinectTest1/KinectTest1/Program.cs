using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

namespace KinectTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            KinectSensor sensor = KinectSensor.KinectSensors[0];

            sensor.SkeletonStream.Enable();
            sensor.SkeletonFrameReady += sensor_DepthFrameReady;

            Console.ForegroundColor = ConsoleColor.Green;

            sensor.Start();
            while(Console.ReadKey().Key != ConsoleKey.Spacebar)
            {}

        }

        static void sensor_DepthFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var depthFrame = e.OpenSkeletonFrame())
            {
                if (depthFrame == null)
                {
                    return;
                }
                Skeleton[] bits = new Skeleton[depthFrame.SkeletonArrayLength];
                depthFrame.CopySkeletonDataTo(bits);
                foreach (var bit in bits)
                {
                    Console.Write(bit);
                }

            }
        }
    }
}
