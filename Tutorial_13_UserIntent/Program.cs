// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_13_UserIntent
{
    class Program
    {
        /// <summary>
        /// Return information about a voice commands given to Vector
        /// <para>The user_intent event is only dispatched when the SDK program has requested behavior control and Vector gets a voice command.</para>
        /// <para>After the robot hears "Hey Vector! ..." and a valid voice command is given for example "...what time is it?") the event will be dispatched and displayed.</para>
        /// </summary>
        /// <param name="args">The arguments.</param>
        static async Task Main(string[] args)
        {
            using (var robot = await Robot.NewConnection())
            {
                Console.WriteLine("Requesting control of Vector...");
                await robot.Control.RequestControl();

                robot.Events.UserIntent += (sender, e) =>
                {
                    Console.WriteLine($"Received {e.Intent}");
                    Console.WriteLine(e.IntentData);
                };

                Console.WriteLine("Vector is waiting for a voice command like 'Hey Vector!  What time is it?'  Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
