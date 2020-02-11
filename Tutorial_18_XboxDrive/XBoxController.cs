using System;
using System.Runtime.InteropServices;

public partial class XBoxController
{
    // Uncomment to support whichever version of the dll you have
//    [DllImport("xinput1_3.dll")]
    [DllImport("xinput1_4.dll")]
    public static extern void XInputEnable([MarshalAs(UnmanagedType.U1)] bool enable);

    [StructLayout(LayoutKind.Explicit)]
    public struct XInputGamepad
    {
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(0)]
        public short wButtons;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(2)]
        public byte bLeftTrigger;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(3)]
        public byte bRightTrigger;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(4)]
        public short sThumbLX;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(6)]
        public short sThumbLY;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(8)]
        public short sThumbRX;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(10)]
        public short sThumbRY;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct XInputState
    {
        [FieldOffset(0)]
        public int PacketNumber;

        [FieldOffset(4)]
        public XInputGamepad Gamepad;
    }

    // Uncomment to support whichever version of the dll you have
//    [DllImport("xinput1_3.dll")]
    [DllImport("xinput1_4.dll")]
    public static extern int XInputGetState
                             (
                                 int dwUserIndex,        // [in] Index of the gamer associated with the device
                                 ref XInputState state  // [out] Receives the current state
                             );

    [StructLayout(LayoutKind.Sequential)]
    public struct XInputVibration
    {
        [MarshalAs(UnmanagedType.I2)]
        public ushort LeftMotorSpeed;

        [MarshalAs(UnmanagedType.I2)]
        public ushort RightMotorSpeed;
    }

    // Uncomment to support whichever version of the dll you have
//    [DllImport("xinput1_3.dll")]
    [DllImport("xinput1_4.dll")]
    public static extern int XInputSetState
                             (
                                int dwUserIndex,  // [in] Index of the gamer associated with the device
                                ref XInputVibration pVibration    // [in, out] The vibration information to send to the controller
                             );
}
