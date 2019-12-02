// <copyright file="ListCommand.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Anki.Vector;
using CommandLine;

namespace VectorConfigure
{
    /// <summary>
    /// Command to list all the configured robots.
    /// </summary>
    [Verb("list", HelpText = "List all the configured robots.")]
    public class ListCommand
    {
        /// <summary>
        /// Gets or sets a value indicating whether the results of this <see cref="ListCommand"/> are verbose.
        /// </summary>
        [Option(HelpText = "Display verbose information about configured robots.")]
        public bool Verbose { get; set; }

        /// <summary>
        /// Runs this command.
        /// </summary>
        /// <returns>Return code for the command</returns>
        public int Run()
        {
            Program.DisplayHeader();
            var robots = RobotConfiguration.Load().ToList();
            if (robots.Count == 0)
            {
                Console.WriteLine("There are no Vector robots configured.");
                return 0;
            }

            Console.WriteLine("Configured Vector Robots:");
            if (Verbose) DisplayRobotsVerbose(robots);
            else DisplayRobotsNormal(robots);
            return 0;
        }

        /// <summary>
        /// Displays the robot normally.
        /// </summary>
        /// <param name="robots">The robots.</param>
        private void DisplayRobotsNormal(IEnumerable<RobotConfiguration> robots)
        {
            foreach (var robot in robots)
            {
                Console.WriteLine($" {robot.RobotName} ({robot.SerialNumber})");
            }
        }

        /// <summary>
        /// Displays the robots verbose.
        /// </summary>
        /// <param name="robots">The robots.</param>
        private void DisplayRobotsVerbose(IEnumerable<RobotConfiguration> robots)
        {
            foreach (var robot in robots)
            {
                Console.WriteLine();
                Console.WriteLine($"   Robot Name: {robot.RobotName}");
                Console.WriteLine($"Serial Number: {robot.SerialNumber}");
                Console.WriteLine($"   IP Address: {robot.IPAddress}");
                Console.WriteLine($"         GUID: {robot.Guid}");
                Console.WriteLine($"{robot.Certificate}");
            }
        }
    }
}
