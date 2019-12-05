// <copyright file="Program.cs" company="Wayne Venables">
//     Copyright (c) 2019 Wayne Venables. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Anki.Vector;

namespace Tutorial_04_Animation
{
    class Program
    {
        /// <summary>
        /// Play animations on Vector.
        /// <para>Play an animation using a trigger, and then another animation by name.</para>
        /// </summary>
        static async Task Main()
        {
            // Create a new connection to the first configured Vector
            using var robot = await Robot.NewConnection();

            Console.WriteLine("Requesting control of Vector...");
            await robot.Control.RequestControl();

            // If Vector is on the charger, drive him off the charger
            if (robot.Status.IsOnCharger)
            {
                Console.WriteLine("Drive Vector off charger...");
                await robot.Behavior.DriveOffCharger();
            }

            // Play an animation via a trigger.
            // A trigger can pick from several appropriate animations for variety.
            Console.WriteLine("Playing Animation Trigger 1:");
            await robot.Animation.PlayAnimationTrigger("GreetAfterLongTime");

            // Play the same trigger, but this time ignore the track that plays on the
            // body (i.e. don't move the wheels). See the play_animation_trigger documentation
            // for other available settings.
            Console.WriteLine("Playing Animation Trigger 2: (Ignoring the body track)");
            await robot.Animation.PlayAnimationTrigger("GreetAfterLongTime", ignoreBodyTrack: true);

            // Play an animation via its name.
            var animation = "anim_pounce_success_02";
            Console.WriteLine("Playing animation by name: " + animation);
            await robot.Animation.PlayAnimation(animation);
        }
    }
}
