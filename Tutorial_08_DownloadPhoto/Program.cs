// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_08_DownloadPhoto
{
    class Program
    {
        /// <summary>
        /// Downloads all the photos stored in Vector
        /// <para>Before running this script, please make sure you have successfully had Vector take a photo by saying, "Hey Vector! Take a photo."</para>
        /// </summary>
        static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            var photos = (await robot.Photos.GetPhotoInfo()).ToList();
            if (photos.Count == 0)
            {
                Console.WriteLine("No photos found on Vector. Ask him to take a photo first by saying, 'Hey Vector! Take a photo.'");
                return;
            }
            foreach (var photo in photos)
            {
                string fileName = $"photo{photo.PhotoId}.jpg";
                Console.WriteLine($"Writing photo {fileName}");
                var data = await robot.Photos.GetPhoto(photo);
                File.WriteAllBytes(fileName, data);
            }
        }
    }
}
