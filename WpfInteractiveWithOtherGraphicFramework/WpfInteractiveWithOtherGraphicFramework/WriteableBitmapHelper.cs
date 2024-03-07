using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfInteractiveWithOtherGraphicFramework;

public static class WriteableBitmapHelper
{



    public static System.Windows.Media.Imaging.WriteableBitmap
        ToWriteableBitmap(this Emgu.CV.Mat mat)
    {
        var result = new System.Windows.Media.Imaging.WriteableBitmap(mat.Width, mat.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);

        var sourceBuffer = new byte[mat.Width * mat.Height * 3];
        mat.CopyTo<byte>(sourceBuffer);

        result.Lock();
        System.Runtime.InteropServices.Marshal.Copy(sourceBuffer, 0, result.BackBuffer, sourceBuffer.Length);
        result.AddDirtyRect(new System.Windows.Int32Rect(0, 0, result.PixelWidth, result.PixelHeight));
        result.Unlock();

        return result;
    }




    public static System.Windows.Media.Imaging.WriteableBitmap
      ToWriteableBitmap_2(this Emgu.CV.Mat mat)
    {
        var result = new System.Windows.Media.Imaging.WriteableBitmap(mat.Width, mat.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);

        var size = mat.Width * mat.Height * mat.ElementSize;
        result.Lock();
        unsafe
        {
            Buffer.MemoryCopy((void*)mat.DataPointer, (void*)result.BackBuffer, size, size);
        }
        result.AddDirtyRect(new System.Windows.Int32Rect(0, 0, result.PixelWidth, result.PixelHeight));
        result.Unlock();
        return result;
    }



    public static void UpdatePixels(this System.Windows.Media.Imaging.WriteableBitmap target, Emgu.CV.Mat source)
    {
        //? 需要确保 source 和 target 缓冲区大小一致

        var size = source.Width * source.Height * source.ElementSize;
        target.Lock();
        unsafe
        {
            Buffer.MemoryCopy((void*)source.DataPointer, (void*)target.BackBuffer, size, size);
        }
        target.AddDirtyRect(new System.Windows.Int32Rect(0, 0, target.PixelWidth, target.PixelHeight));
        target.Unlock();
    }





    public static System.Windows.Media.Imaging.WriteableBitmap
        ToWriteableBitmap(this SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Bgra32> image)
    {
        // 获得源图的数据
        byte[] pixelBytes = new byte[image.Width * image.Height * System.Runtime.CompilerServices.Unsafe.SizeOf<SixLabors.ImageSharp.PixelFormats.Bgra32>()];
        image.CopyPixelDataTo(pixelBytes);

        //! 结果
        var result = new System.Windows.Media.Imaging.WriteableBitmap(image.Width, image.Height, 96, 96, System.Windows.Media.PixelFormats.Bgra32, null);

        result.Lock();
        Marshal.Copy(pixelBytes, 0, result.BackBuffer, pixelBytes.Length);
        result.AddDirtyRect(new System.Windows.Int32Rect(0, 0, result.PixelWidth, result.PixelHeight));
        result.Unlock();

        return result;
    }


}
