// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Types;

namespace Tutorial_14_FaceEvent
{
    class Program
    {
        /// <summary>
        /// Wait for Vector to see a face, and then print output to the console.
        /// <para>This script demonstrates how to set up a listener for an event. It subscribes to event ObservedFace.  When that event is dispatched, 
        /// the lambda is called, which prints text to the console.  Vector will also say "I see a face" one time, and the program will exit when
        /// he finishes speaking.</para>
        /// </summary>
        static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            Console.WriteLine("Enabling face detection...");
            await robot.Vision.EnableFaceDetection();

            Console.WriteLine("Move Vector's Head and Lift to make it easy to see a face.");
            await robot.Behavior.SetHeadAngle(45.Degrees());
            await robot.Behavior.SetLiftHeight(0);

            bool sawFace = false;
            bool exit = false;

            robot.Events.ObservedFace += async (sender, e) =>
            {
                if (sawFace) return;
                sawFace = true;
                Console.WriteLine("Vector sees a face");
                await robot.Behavior.SayText("I see a face");
                exit = true;
            };

            Console.WriteLine("Press a key to exit before Vector sees a face...");
            while (!exit && !Console.KeyAvailable)
            {
                await Task.Delay(500);
            }
        }
    }
}
