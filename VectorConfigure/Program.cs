// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using CommandLine;

namespace VectorConfigure
{
    public static class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Return code from the command</returns>
        public static int Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Environment.Exit(-1);
            };

            var result = Parser.Default.ParseArguments<ListCommand, AddCommand, DeleteCommand>(args);
            return result.MapResult(
                (ListCommand list) => ((ListCommand)list).Run(),
                (AddCommand add) => ((AddCommand)add).Run(),
                (DeleteCommand delete) => ((DeleteCommand)delete).Run(),
                errs => 1);
        }

        /// <summary>
        /// Reads the password.
        /// </summary>
        /// <returns>The password</returns>
        public static string ReadPassword()
        {
            string pass = string.Empty;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, pass.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                }
            }
            while (true);
            return pass;
        }

        /// <summary>
        /// Writes the line word wrap.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        /// <param name="tabSize">Size of the tab.</param>
        public static void WriteLineWordWrap(string paragraph, int tabSize = 8)
        {
            if (string.IsNullOrEmpty(paragraph)) return;

            string[] lines = paragraph
                .Replace("\t", new string(' ', tabSize))
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string process = lines[i];
                List<string> wrapped = new List<string>();

                while (process.Length > Console.WindowWidth)
                {
                    int wrapAt = process.LastIndexOf(' ', Math.Min(Console.WindowWidth - 1, process.Length));
                    if (wrapAt <= 0) break;

                    wrapped.Add(process.Substring(0, wrapAt));
                    process = process.Remove(0, wrapAt + 1);
                }

                foreach (string wrap in wrapped)
                {
                    Console.WriteLine(wrap);
                }

                Console.WriteLine(process);
            }
        }

        /// <summary>
        /// Displays the header.
        /// </summary>
        public static void DisplayHeader()
        {
            Console.WriteLine(CommandLine.Text.HeadingInfo.Default);
            Console.WriteLine(CommandLine.Text.CopyrightInfo.Default);
            Console.WriteLine();
        }
    }
}
