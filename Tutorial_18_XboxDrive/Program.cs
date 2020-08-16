using System;
using System.Threading;
using System.Threading.Tasks;
using Anki.Vector;
using Anki.Vector.Types;

namespace Tutorial_18_XboxDrive
{
    class Program
    {
        /// <summary>
        /// This is held here so that it doesn't get garbage collected
        /// </summary>
        static Task taskXboxController;

        /// <summary>
        /// Make Vector drive around based on an Xbox controller
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
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

            // Create a task that will receive input from the game controller
            taskXboxController = new Task(() =>
            {
                // Enable the game controller
                XBoxController.XInputEnable(true);

                // a structure to hold the state of the game controller
                XBoxController.XInputState state = new XBoxController.XInputState();

                int prevPacketNumber = 0;
                for (; ; )
                {
                    // try to get input; if no game controller, it will error out
                    var err = XBoxController.XInputGetState(0, ref state);
                    if (0 != err)
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    // skip if the packet number has not changed
                    // (this is unlikely to be needed code)
                    if (prevPacketNumber == state.PacketNumber)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    // Save the packet number
                    prevPacketNumber = state.PacketNumber;

                    // Get the left-right percentage and use that for an angle
                    var turnAngle = state.Gamepad.sThumbRX *-90.0/ 32768.0;
                    var turnAngle2 = turnAngle * turnAngle;
                    if (turnAngle2 > 10.0)
                    {
                        // Send a command to turn by that amount
                        robot.Behavior.TurnInPlace(((float)turnAngle).Degrees());
                    }

                    // Get the forward/back amount
                    var fwd = state.Gamepad.sThumbRY * 200.0 / 32768.0;
                    var fwd2 = fwd * fwd;
                    if (fwd2 > 10.0)
                    {
                        robot.Behavior.DriveStraight((float) fwd, 50);
                    }
                }
            });
            // Start communicating
            taskXboxController.Start();

            Console.WriteLine("Press any key to exit...");
            await Task.Run(() => Console.ReadKey(true));
        }
    }
}
