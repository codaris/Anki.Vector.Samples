// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Types;

namespace Tutorial_07_DockWithCube
{
    class Program
    {
        /// <summary>
        /// Tell Vector to drive up to a seen cube.
        /// <para>This example demonstrates Vector driving to and docking with a cube, without picking it up.
        /// Vector will line his arm hooks up with the cube so that they are inserted into the cube's corners.
        /// You must place a cube in front of Vector so that he can see it.</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static async Task Main(string[] args)
        {
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

                Console.WriteLine("Setting head angle and lift height...");
                await robot.Behavior.SetHeadAngle(-5.Degrees());
                await robot.Behavior.SetLiftHeight(0);

                Console.WriteLine("Connecting to a cube...");
                await robot.World.ConnectCube();

                ActionResult dockingResult = null;

                if (robot.World.LightCube != null)
                {
                    Console.WriteLine("Begin cube docking...");
                    dockingResult = await robot.Behavior.DockWithCube(robot.World.LightCube, numRetries: 3);
                    Console.WriteLine("Disconnect the cube...");
                    await robot.World.DisconnectCube();

                    if (!dockingResult.IsSuccess)
                    {
                        Console.WriteLine($"Cube docking failed with code '{dockingResult.Result}'");
                    }
                }
                else
                {
                    Console.WriteLine("Could not connect to light cube");
                }
            }
        }
    }
}
