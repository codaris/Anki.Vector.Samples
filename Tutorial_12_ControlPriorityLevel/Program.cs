// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_12_ControlPriorityLevel
{
    class Program
    {
        /// <summary>
        /// Vector maintains SDK behavior control after being picked up
        /// <para>During normal operation, SDK programs cannot maintain control over Vector when he is at a cliff, stuck on an edge or obstacle, tilted or inclined, picked up, in darkness, etc.</para>
        /// <para>This script demonstrates how to use the highest level SDK behavior control <see cref="Anki.Vector.ControlPriority.OverrideBehaviors"/> to make Vector perform actions that 
        /// normally take priority over the SDK.  These commands will not succeed at <see cref="Anki.Vector.ControlPriority.Default"/>.</para>
        /// </summary>
        static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting override control of Vector...");
            await robot.Control.RequestControl(ControlPriority.OverrideBehaviors);

            // Say pick me up and wait for a key press to exit
            await robot.Behavior.SayText("Pick me up!");
            Console.WriteLine("Waiting for Vector to be picked up, press any key to exit...");

            // Wait for vector to be picked up or key press
            while (!robot.Status.IsPickedUp && !Console.KeyAvailable)
            {
                // Wait 0.5 seconds
                await Task.Delay(500);
            }

            // Exit if key pressed
            if (Console.KeyAvailable) return;

            // If Vector is picked up move him around
            Console.WriteLine("Vector is picked up...");
            await robot.Behavior.SayText("Hold on tight");
            Console.WriteLine("Setting wheel motors...");
            await robot.Motors.SetWheelMotors(75, -75);
            await Task.Delay(500);
            await robot.Motors.SetWheelMotors(-75, 75);
            await Task.Delay(500);
            await robot.Motors.StopAllMotors();
        }
    }
}
