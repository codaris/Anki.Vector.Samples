// <copyright file="DeleteCommand.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Anki.Vector;
using CommandLine;

namespace VectorConfigure
{
    /// <summary>
    /// Command to delete a robot configuration.
    /// </summary>
    [Verb("delete", HelpText = "Delete a robot configuration.")]
    public class DeleteCommand
    {
        /// <summary>
        /// Gets or sets the robot name or serial number.
        /// </summary>
        [Value(0, HelpText = "Robot name or serial number to delete", Required = true, MetaName = "Name or Serial")]
        public string NameOrSerial { get; set; }

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
                return -1;
            }

            string robotName = Authentication.StandardizeRobotName(NameOrSerial);
            string serial = NameOrSerial.ToLower();

            var robot = robots.FirstOrDefault(r => r.RobotName == robotName || r.SerialNumber.ToLower() == serial);
            if (robot == null)
            {
                Console.WriteLine($"Robot '{NameOrSerial}' was not found.");
                return -1;
            }
            robots.RemoveAt(robots.IndexOf(robot));
            RobotConfiguration.Save(robots);
            Console.WriteLine($"Removed {robot.RobotName} ({robot.SerialNumber}).");
            return 0;
        }
    }
}
