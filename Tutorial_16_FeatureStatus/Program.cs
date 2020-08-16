// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_16_FeatureStatus
{
    class Program
    {
        /// <summary>
        /// Return information about the features that Vector's AI is using
        /// </summary>
        static async Task Main()
        {
            using var robot = await Robot.NewConnection();

            robot.Events.FeatureStatus += (sender, e) =>
            {
                Console.WriteLine($"FeatureStatus: intent {e.FeatureName}");
                Console.WriteLine(e.Source);
            };

            Console.WriteLine("Vector is waiting for a voice command like 'Hey Vector!  What time is it?'  Press any key to exit...");
            await Task.Run(() => Console.ReadKey(true));
        }
    }
}
