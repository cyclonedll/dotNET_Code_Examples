using System.Runtime.InteropServices;

namespace PaintingOnTheScreenExample;

internal class Program
{

    //Private Declare Auto Function GetDesktopWindow Lib "user32.dll" () As IntPtr

    [DllImport("user32.dll")]
    private static extern nint GetDesktopWindow();


    private static Color GetRandomColor()
    {
        return System.Drawing.Color.FromArgb(Random.Shared.Next(0, 255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255));
    }



    static void Main(string[] args)
    {

        var workingArea = Screen.PrimaryScreen.WorkingArea;

        using var g = System.Drawing.Graphics.FromHwnd(GetDesktopWindow());

        int count = 0;

        while (count < 200)
        {

            g.DrawString("你麻痹",
                new System.Drawing.Font(System.Drawing.FontFamily.Families[0].Name, Random.Shared.Next(5, 50)),
                new System.Drawing.SolidBrush(GetRandomColor()),
                new System.Drawing.Point(Random.Shared.Next(0, workingArea.Width), Random.Shared.Next(0, workingArea.Height)));

            count++;
        }



    }
}



//Module Module1

//    Private Declare Auto Function GetDesktopWindow Lib "user32.dll" () As IntPtr
//    'Declare Function GetWindowDC Lib "user32" (ByVal hWnd As IntPtr) As IntPtr


//    Dim g As System.Drawing.Graphics = System.Drawing.Graphics.FromHwnd(GetDesktopWindow)
//    'Dim hdc = GetWindowDC(GetDesktopWindow)

//    Dim count As Integer
//    Dim rnd As New Random

//    Dim workArea As System.Drawing.Rectangle = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea

//    Sub Main(ByVal args() As String)

//        While count <= 200
//            g.DrawString("你不是猪",
//                         New System.Drawing.Font(System.Drawing.FontFamily.Families(3).Name, rnd.Next(9, 50)),
//                         New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255))),
//                         New System.Drawing.Point(rnd.Next(0, workArea.Width), rnd.Next(0, workArea.Height)))
//            count += 1

//        End While
//    End Sub

//End Module