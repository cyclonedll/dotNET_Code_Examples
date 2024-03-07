using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

//var singles = new float[] { 1, 2, 3, 4, 5 };
//var buffer = new byte[20];

//Benchmark.SinglesToBuffer_BitConverter(singles, buffer);

//foreach (var b in buffer)
//    Console.Write($"{b},");

//Array.Clear(buffer);

//Console.WriteLine();
//Console.WriteLine(new string('-', 10));

//Benchmark.SinglesToBuffer_ManagedPointer(singles, buffer);

//foreach (var b in buffer)
//    Console.Write($"{b},");

//Console.WriteLine();
//Console.WriteLine(new string('-', 10));

//Array.Clear(buffer);

//Benchmark.SinglesToBuffer_UnamangedPointer(singles, buffer);

//foreach (var b in buffer)
//    Console.Write($"{b},");


BenchmarkRunner.Run<Benchmark>();
//for (int i = 1; i <= 10; i++)
//    Console.Write($"{882 * i * 15},");


public class Benchmark
{

    [Params(800, 1600, 8000, 13230, 26460, 39690, 52920, 66150, 79380, 92610, 105840, 119070, 132300, 132300 * 2, 132300 * 4)]
    public int N;

    float[] _samples;
    byte[] _buffer;


    [GlobalSetup]
    public void Setup()
    {
        _samples = new float[N];
        _buffer = new byte[N * 4];
    }


    [Benchmark]
    public void Using_BitConverter()
    {
        SinglesToBuffer_BitConverter(_samples, _buffer);
    }

    [Benchmark]
    public void Using_Safe_Pointer()
    {
        SinglesToBuffer_ManagedPointer(_samples, _buffer);
    }

    [Benchmark]
    public void Using_Unsafe_Pointer() => SinglesToBuffer_UnamangedPointer(_samples, _buffer);



    public static void SinglesToBuffer_BitConverter(float[] array, byte[] buffer)
    {
        for (int readIndex = 0, writeIndex = 0;
            readIndex < array.Length;
            readIndex++, writeIndex += 4)
        {
            var value = array[readIndex];
            var bytes = BitConverter.GetBytes(value);
            buffer[writeIndex] = bytes[0];
            buffer[writeIndex + 1] = bytes[1];
            buffer[writeIndex + 2] = bytes[2];
            buffer[writeIndex + 3] = bytes[3];
        }
    }



    public static void SinglesToBuffer_ManagedPointer(float[] array, byte[] buffer)
    {
        for (int readIndex = 0, writeIndex = 0;
            readIndex < array.Length;
            readIndex++, writeIndex += 4)
        {

            ref float singleAddr = ref array[readIndex];
            ref var bytes = ref Unsafe.As<float, byte>(ref singleAddr);
            ref var outBuffer = ref buffer[writeIndex];

            outBuffer = ref buffer[writeIndex];

            bytes = ref Unsafe.Add(ref bytes, 1);
            outBuffer = ref Unsafe.Add(ref outBuffer, 1);

            outBuffer = bytes;
            bytes = ref Unsafe.Add(ref bytes, 1);
            outBuffer = ref Unsafe.Add(ref outBuffer, 1);

            outBuffer = bytes;
            bytes = ref Unsafe.Add(ref bytes, 1);
            outBuffer = ref Unsafe.Add(ref outBuffer, 1);

            outBuffer = bytes;
        }

    }



    public static void SinglesToBuffer_UnamangedPointer(float[] array, byte[] buffer)
    {
        unsafe
        {
            fixed (float* srcPtr = array)
            {
                fixed (byte* dstPtr = buffer)
                {
                    for (int readIndex = 0, writeIndex = 0;
                         readIndex < array.Length;
                         readIndex++, writeIndex += 4)
                    {

                        var bytePtr = (byte*)(srcPtr + readIndex);
                        *(dstPtr + writeIndex) = *bytePtr;
                        *(dstPtr + writeIndex + 1) = *(bytePtr + 1);
                        *(dstPtr + writeIndex + 2) = *(bytePtr + 2);
                        *(dstPtr + writeIndex + 3) = *(bytePtr + 3);
                    }
                }
            }
        }
    }


}

