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
        public static async Task Main()
        {
            // Vector's supported sound format
            var outFormat = new WaveFormat(16000, 16, 1);

            //  Music: https://www.bensound.com
            using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tutorial_10_PlayAudio.bensound-summer.mp3");
            using var source = new Mp3FileReader(resourceStream);
            using var reader = new WaveFormatConversionStream(outFormat, source);

            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            // Calculate the framerate
            uint frameRate = (uint)(reader.Length / reader.TotalTime.TotalSeconds / 2);

            // Run the audiostream in the background.
            var playback = robot.Audio.PlayStream(reader, frameRate, 50);

            Console.WriteLine("Press a key to stop playback");
            await Task.Run(() => Console.ReadKey(true));

            await robot.Audio.CancelPlayback();

            var result = await playback;

            Console.WriteLine($"Playback result: {result}");

            Console.WriteLine("Press a key to end.");
            await Task.Run(() => Console.ReadKey(true));
        }
    }
}
