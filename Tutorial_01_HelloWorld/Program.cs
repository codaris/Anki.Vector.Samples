// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_01_HelloWorld
{
    class Program
    {
        /// <summary>
        /// Hello World
        /// <para>Make Vector say 'Hello World' in this simple Vector SDK example program.</para>
        /// </summary>
        public static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            Console.WriteLine("Saying 'Hello World'...");
            await robot.Behavior.SayText("Hello World");
        }
    }
}