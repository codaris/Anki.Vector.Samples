// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
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
        public static async Task Main()
        {
            // Get image
            using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tutorial_06_FaceImage.cozmo_image.jpg");
            using var image = SixLabors.ImageSharp.Image.Load<Rgb24>(resourceStream);

            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            var data = ConvertImageToByteArray(image);

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            Console.WriteLine("Setting head angle and lift height...");
            await robot.Behavior.SetHeadAngle(45.Degrees());
            await robot.Behavior.SetLiftHeight(0);

            Console.WriteLine("Displaying image for 5 seconds...");
            await robot.Screen.DisplayImageRgb24(data, 5000);
            await Task.Delay(5000);
        }

        /// <summary>
        /// Converts the image data to a byte array
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Cannot extract pixel data from image.</exception>
        private static byte[] ConvertImageToByteArray(Image<Rgb24> image)
        {
            if (image.TryGetSinglePixelSpan(out var pixelSpan))
            {
                return MemoryMarshal.AsBytes(pixelSpan).ToArray();
            }
            throw new NotSupportedException("Cannot extract pixel data from image.");
        }
    }
}
