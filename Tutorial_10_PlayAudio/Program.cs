// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using System.Threading.Tasks;
using Anki.Vector;
using NAudio.Wave;

namespace Tutorial_10_PlayAudio
{
    class Program
    {
        /// <summary>
        /// Play audio files through Vector's speaker.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static async Task Main(string[] args)
        {
            // Vector's supported sound format
            var outFormat = new WaveFormat(16000, 16, 1);

            //  Music: https://www.bensound.com
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tutorial_10_PlayAudio.bensound-summer.mp3"))
            using (var source = new Mp3FileReader(resourceStream))
            using (var reader = new WaveFormatConversionStream(outFormat, source))
            using (var robot = await Robot.NewConnection())
            {
                // Calculate the framerate
                uint frameRate = (uint)(reader.Length / reader.TotalTime.TotalSeconds / 2);

                // Run the audiostream in the background.
                var playback = robot.Audio.PlayStream(reader, frameRate, 50);

                Console.WriteLine("Press a key to stop playback");
                Console.ReadKey();

                await robot.Audio.CancelPlayback();

                var result = await playback;

                Console.WriteLine($"Playback result: {result}");

                Console.WriteLine("Press a key to end.");
                Console.ReadKey();
            }
        }
    }
}
