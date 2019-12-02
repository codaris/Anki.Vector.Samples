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
        /// <param name="args">The arguments.</param>
        static async Task Main(string[] args)
        {
            using (var robot = await Robot.NewConnection())
            {
                Console.WriteLine("Requesting control of Vector...");
                await robot.Control.RequestControl();

                Console.WriteLine("Enabling face detection...");
                await robot.Vision.EnableFaceDetection();

                Console.WriteLine("Move Vector's Head and Lift to make it easy to see a face.");
                await robot.Behavior.SetHeadAngle(45.Degrees());
                await robot.Behavior.SetLiftHeight(0);

                bool sawFace = false;
                robot.Events.ObservedFace += async (sender, e) =>
                {
                    Console.WriteLine("Vector sees a face");
                    await robot.Behavior.SayText("I see a face");
                    sawFace = true;
                };

                while (!sawFace && !Console.KeyAvailable)
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}
