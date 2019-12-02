// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Types;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace Tutorial_06_FaceImage
{
    class Program
    {
        /// <summary>
        /// Display an image on Vector's face.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static async Task Main(string[] args)
        {
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tutorial_06_FaceImage.cozmo_image.jpg"))
            using (var image = Image.Load<Rgb24>(resourceStream))
            using (var robot = await Robot.NewConnection())
            {
                // Data in Rgb24 format
                var data = MemoryMarshal.AsBytes(image.GetPixelSpan()).ToArray();

                Console.WriteLine("Requesting control of Vector...");
                await robot.Control.RequestControl();

                Console.WriteLine("Setting head angle and lift height...");
                await robot.Behavior.SetHeadAngle(45.Degrees());
                await robot.Behavior.SetLiftHeight(0);

                Console.WriteLine("Displaying image for 5 seconds...");
                await robot.Screen.DisplayImageRgb24(data, 5000);
                await Task.Delay(5000);
            }
        }
    }
}
