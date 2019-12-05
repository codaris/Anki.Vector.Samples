// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Objects;
using Anki.Vector.Types;

namespace Tutorial_15_FaceFollower
{
    class Program
    {
        /// <summary>
        /// Make Vector turn toward a face.
        /// <para>This script shows off the turn_towards_face behavior.  It will wait for a face and then constantly turn towards it to keep it in frame.</para>
        /// </summary>
        static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            Console.WriteLine("Enabling face detection...");
            await robot.Vision.EnableFaceDetection();

            // If Vector is on the charger, drive him off the charger
            if (robot.Status.IsOnCharger)
            {
                Console.WriteLine("Drive Vector off charger...");
                await robot.Behavior.DriveOffCharger();
            }

            Console.WriteLine("Move Vector's Head and Lift to make it easy to see a face.");
            await robot.Behavior.SetHeadAngle(45.Degrees());
            await robot.Behavior.SetLiftHeight(0);

            // The face to follow
            Face faceToFollow = null;

            // Observe objects and save the first face found
            robot.World.ObjectObserved += (sender, e) =>
            {
                if (e.Object is Face && faceToFollow == null)
                {
                    Console.WriteLine("Found a face to track.");
                    faceToFollow = (Face)e.Object;
                }
            };

            Console.WriteLine("Press any key to exit...");
            while (!Console.KeyAvailable)
            {
                if (faceToFollow != null) await robot.Behavior.TurnTowardsFace(faceToFollow);
                await Task.Delay(100);
            }
        }
    }
}
