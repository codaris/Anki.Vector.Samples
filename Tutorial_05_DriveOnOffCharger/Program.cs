// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_05_DriveOnOffCharger
{
    class Program
    {
        /// <summary>
        /// Tell Vector to drive on and off the charger.
        /// </summary>
        public static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            Console.WriteLine("Drive Vector on charger...");
            await robot.Behavior.DriveOnCharger();

            Console.WriteLine("Drive Vector off charger...");
            await robot.Behavior.DriveOffCharger();
        }
    }
}
