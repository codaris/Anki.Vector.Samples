// <copyright file="AddCommand.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Net;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Exceptions;
using CommandLine;

namespace VectorConfigure
{
    /// <summary>
    /// Command to add new robot configuration to the SDK
    /// </summary>
    [Verb("add", HelpText = "Add a new robot configuration to the SDK.")]
    public class AddCommand
    {
        /// <summary>
        /// The robot configuration to build
        /// </summary>
        private readonly RobotConfiguration robot = new RobotConfiguration();

        /// <summary>
        /// Gets or sets the name of the robot.
        /// </summary>
        [Option('n', "name", HelpText = "The robot name (ex. Vector-A1B2).")]
        public string RobotName { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        [Option('s', "serial", HelpText = "The robot serial number (ex. 00e20100).")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Option('e', "email", HelpText = "The email used by your Anki account.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the ip address string.
        /// </summary>
        [Option('i', "ip", HelpText = "The robot IP address (ex. 192.168.42.42).")]
        public string IPString { get; set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>Return code for the command</returns>
        public int Run()
        {
            Program.DisplayHeader();
            Program.WriteLineWordWrap("Vector requires all requests be authorized by an authenticated Anki user.  This application will enable this device to authenticate with your Vector robot for use with a Vector SDK program.");
            Console.WriteLine();
            if (!AddSerialNumberAndCertificate()) return -1;
            if (!AddRobotName()) return -1;
            if (!AddIPAddress()) return -1;
            if (!AddGuid()) return -1;

            RobotConfiguration.AddOrUpdate(robot);
            Console.WriteLine("Success.");
            return 0;
        }

        /// <summary>
        /// Adds the serial number and certificate.
        /// </summary>
        /// <returns>True if successful</returns>
        private bool AddSerialNumberAndCertificate()
        {
            string serialNumber = SerialNumber;
            bool useInput = string.IsNullOrEmpty(SerialNumber);

            if (useInput)
            {
                Program.WriteLineWordWrap("Please find your robot serial number (ex. 00e20100) located on the underside of Vector, or accessible from Vector's debug screen.");
                Console.WriteLine();
            }

            while (true)
            {
                // If using input, request from user
                if (useInput)
                {
                    Console.Write("Enter robot serial number: ");
                    serialNumber = Console.ReadLine();
                    if (serialNumber == null) return false;
                }
                // Verify serial number is in correct format
                if (!Authentication.SerialNumberIsValid(serialNumber))
                {
                    Console.WriteLine("Serial number is not in the correct format (ex. 00e20100).");
                    Console.WriteLine();
                    if (useInput) continue;
                    return false;
                }
                // Update the robot serial number
                robot.SerialNumber = serialNumber;
                // Attempt to get certificate.
                try
                {
                    Console.WriteLine("Retreiving Vector's security certificate from Anki's servers...");
                    robot.Certificate = GetResult(Authentication.GetCertificate(robot.SerialNumber));
                }
                catch (VectorAuthenticationException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine();
                    if (useInput) continue;
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Adds the name of the robot.
        /// </summary>
        /// <returns>True if successful</returns>
        private bool AddRobotName()
        {
            string robotName = RobotName;
            bool useInput = string.IsNullOrEmpty(robotName);
            if (useInput)
            {
                Program.WriteLineWordWrap("Find your robot name (ex. Vector-A1B2) by placing Vector on the charger and double-clicking Vector's backpack button.");
                Console.WriteLine();
            }

            while (true)
            {
                // If using input, request from user
                if (useInput)
                {
                    Console.Write("Enter robot name: ");
                    robotName = Console.ReadLine();
                    if (robotName == null) return false;
                }
                robotName = Authentication.StandardizeRobotName(robotName);
                // Verify serial number is in correct format
                if (!Authentication.RobotNameIsValid(robotName))
                {
                    Console.WriteLine("Robot name is not in the correct format. (ex. Vector-A1B2).");
                    Console.WriteLine();
                    if (useInput) continue;
                    return false;
                }
                // Update the robot name
                robot.RobotName = robotName;
                return true;
            }
        }

        /// <summary>
        /// Adds the ip address.
        /// </summary>
        /// <returns>True if successful</returns>
        private bool AddIPAddress()
        {
            string ipString = IPString;
            bool useInput = string.IsNullOrEmpty(ipString);
            IPAddress ipAddress;
            if (useInput)
            {
                Console.WriteLine("Attempting to determine your Vector's IP address automatically...");
                ipAddress = GetResult(Authentication.FindRobotAddress(robot.RobotName));
                if (ipAddress != null)
                {
                    Console.WriteLine($"Found Vector at {ipAddress}");
                    Console.WriteLine();
                    robot.IPAddress = ipAddress;
                    return true;
                }
                Program.WriteLineWordWrap("Find your robot ip address (ex. 192.168.42.12) by placing Vector on the charger, double-clicking Vector's backpack button, then raising and lowering his arms. If you see XX.XX.XX.XX on his face, reconnect Vector to your WiFi using the Vector Companion App.");
                Console.WriteLine();
            }

            while (true)
            {
                // If using input, request from user
                if (useInput)
                {
                    Console.Write("Enter robot ip: ");
                    ipString = Console.ReadLine();
                    if (ipString == null) return false;
                }
                if (!IPAddress.TryParse(ipString, out ipAddress))
                {
                    Program.WriteLineWordWrap("IP Address is not in the correct format.  It must be 4 numbers separated by dots (ex. 192.168.42.12).");
                    Console.WriteLine();
                    if (useInput) continue;
                    return false;
                }

                robot.IPAddress = ipAddress;
                return true;
            }
        }

        /// <summary>
        /// Adds the unique identifier.
        /// </summary>
        /// <returns>True if successful</returns>
        private bool AddGuid()
        {
            string email = Email;
            bool useInput = string.IsNullOrEmpty(email);
            if (useInput)
            {
                Program.WriteLineWordWrap("Enter your email and password. Make sure to use the same account that was used to set up your Vector on the companion app.");
                Console.WriteLine();
            }

            while (true)
            {
                // If using input, request from user
                if (useInput)
                {
                    Console.Write("Enter email: ");
                    email = Console.ReadLine();
                    if (email == null) return false;
                }
                Console.Write("Enter password: ");
                string password = Program.ReadPassword();
                if (string.IsNullOrWhiteSpace(password)) continue;
                Console.WriteLine();
                Console.WriteLine("Authenticating with Anki's servers...");
                try
                {
                    var sessionId = GetResult(Authentication.GetSessionToken(email, password));
                    robot.Guid = GetResult(Authentication.GetTokenGuid(sessionId, robot.Certificate, robot.RobotName, robot.IPAddress));
                }
                catch (VectorAuthenticationException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine();
                    useInput = true;
                    continue;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets the result of a task and unpacks aggregate exceptions
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>A task that represents the asynchronous operation; the task result contains the result of the command.</returns>
        private static T GetResult<T>(Task<T> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is VectorAuthenticationException) throw ex.InnerException;
                throw;
            }
        }
    }
}
