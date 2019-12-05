// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_11_DriveToCliffAndBackUp
{
    class Program
    {
        /// <summary>
        /// Make Vector drive to a cliff and back up.
        /// <para>Place the robot about a foot from a "cliff" (such as a tabletop edge), then run this script.</para>
        /// </summary>
        public static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            // If Vector is on the charger, drive him off the charger
            if (robot.Status.IsOnCharger)
            {
                Console.WriteLine("Drive Vector off charger...");
                await robot.Behavior.DriveOffCharger();
            }

            Console.WriteLine("Drive Vector straight until he reaches cliff...");
            await robot.Behavior.DriveStraight(5000, 100);

            // Wait for control to be lost
            Console.WriteLine("Wait for control to be lost...");
            await robot.Control.WaitForControlChange();
            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();
            Console.WriteLine("Drive Vector backward away from the cliff...");
            await robot.Behavior.DriveStraight(-300, 100);
        }
    }
}
