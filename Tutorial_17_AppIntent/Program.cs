// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_17_AppIntent
{
    class Program
    {
        /// <summary>
        /// Inject an intent into Vector's AI
        /// </summary>
        static async Task Main()
        {
            using var robot = await Robot.NewConnection();

            Console.WriteLine($"Asked Vector to go to sleep...");
            await robot.Behavior.Sleep();

            Console.WriteLine("Press any key to exit...");
            await Task.Run(() => Console.ReadKey(true));
        }
    }
}
