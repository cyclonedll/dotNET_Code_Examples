using System.Runtime.InteropServices;

namespace ClickThroughWindowExample;

internal partial class User32Native
{
    [LibraryImport("user32.dll")]
    public static partial int GetWindowLongW(IntPtr hWnd, GWL nIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

    [LibraryImport("user32.dll")]
    public static partial int SetWindowLongW(IntPtr hWnd, GWL nIndex, int dsNewLong);

    public enum GWL { ExStyle = -20 }

    public enum LWA { Alpha = 2, ColorKey = 1 }

    public enum WS_EX { Layered = 524288, Transparent = 32 }

}