// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_03_Motors
{
    class Program
    {
        /// <summary>
        /// Drive Vector's wheels, lift and head motors directly.
        /// <para>This is an example of how you can also have low-level control of Vector's motors
        /// (wheels, lift and head) for fine-grained control and ease of controlling multiple things at once.</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
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

                // Tell the head motor to start lowering the head (at 5 radians per second)
                Console.WriteLine("Lower Vector's head...");
                await robot.Motors.SetHeadMotor(-5.0f);

                // Tell the lift motor to start lowering the lift (at 5 radians per second)
                Console.WriteLine("Lower Vector's lift...");
                await robot.Motors.SetLiftMotor(-5.0f);

                // Tell Vector to drive the left wheel at 25 mmps (millimeters per second),
                // and the right wheel at 50 mmps (so Vector will drive Forwards while also
                // turning to the left.
                Console.WriteLine("Set Vector's wheel motors...");
                await robot.Motors.SetWheelMotors(25, 50);

                // Wait 3 seconds
                await Task.Delay(3000);

                // Tell the head motor to start raising the head (at 5 radians per second)
                Console.WriteLine("Raise Vector's head...");
                await robot.Motors.SetHeadMotor(5);

                // Tell the lift motor to start raising the lift (at 5 radians per second)
                Console.WriteLine("Raise Vector's lift...");
                await robot.Motors.SetLiftMotor(5);

                // Tell Vector to drive the left wheel at 50 mmps (millimeters per second),
                // and the right wheel at -50 mmps (so Vector will turn in-place to the right)
                Console.WriteLine("Set Vector's wheel motors...");
                await robot.Motors.SetWheelMotors(50, -50);

                // Wait for 3 seconds (the head, lift and wheels will move while we wait)
                await Task.Delay(3000);

                // Stop the motors, which unlocks the tracks
                await robot.Motors.StopAllMotors();
            }
        }
    }
}
