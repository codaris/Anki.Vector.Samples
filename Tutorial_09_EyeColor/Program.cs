// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_09_EyeColor
{
    class Program
    {
        /// <summary>
        /// Set Vector's eye color.
        /// <para>Note that Vector's eye color will return to normal when the connection terminates.</para>
        /// </summary>
        /// <param name="args">The arguments.</param>
        static async Task Main(string[] args)
        {
            using (var robot = await Robot.NewConnection())
            {
                Console.WriteLine("Set Vector's eye color to purple...");
                await robot.Behavior.SetEyeColor(0.83f, 0.76f);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
