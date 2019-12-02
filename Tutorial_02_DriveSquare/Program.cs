// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Types;

namespace Tutorial_02_DriveSquare
{
    class Program
    {
        /// <summary>
        /// Make Vector drive in a square.
        /// <para>Make Vector drive in a square by going forward and turning left 4 times in a row.</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            // Create a new connection to the first configured Vector
            using (var robot = await Robot.NewConnection())
            {
                Console.WriteLine("Requesting control of Vector...");
                await robot.Control.RequestControl();

                // If Vector is on the charger, drive him off the charger
                if (robot.Status.IsOnCharger)
                {
                    Console.WriteLine("Drive Vector off charger...");
                    await robot.Behavior.DriveOffCharger();
                }

                // Loop 4 times
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("Drive Vector straight...");
                    await robot.Behavior.DriveStraight(200, 50);
                    Console.WriteLine("Turn Vector in place...");
                    await robot.Behavior.TurnInPlace(90.Degrees());
                }
            }
        }
    }
}
