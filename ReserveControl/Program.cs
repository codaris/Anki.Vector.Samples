// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using Anki.Vector;

namespace ReserveControl
{
    class Program
    {
        /// <summary>
        /// Reserve SDK Behavior Control
        /// <para>While this script runs, other SDK scripts may run and Vector will not perform most default behaviors before/after they complete.This will keep Vector still.</para>
        /// <para>High priority behaviors like returning to the charger in a low battery situation, or retreating from a cliff will still take precedence.</para>
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            using (var behaviorControl = new ReserveBehaviorControl())
            {
                Console.WriteLine("Vector behavior control reserved for SDK.  Hit 'Enter' to release control.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }
        }
    }
}
