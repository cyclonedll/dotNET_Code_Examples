//#define sample1
#define sample2

// See https://aka.ms/new-console-template for more information
using PinvokeCustomNativeCode_example;

//DotProuct_native.ImportNativeLib();

static unsafe float Dot(float* a, float* b, uint len)
{
    float r = 0.0f;
    for (int i = 0; i < len; i++)
        r += a[i] * b[i];
    return r;
}

//Modify this value on different platforms
//const int count = int.MaxValue / 10;

//var sw= System.Diagnostics.Stopwatch.StartNew();
//var a = new FixedArray(count);
//for (int i = 0; i < count; i++)
//{
//    var v = i / 10000f;
//    a[i] = v;
//}
//sw.Stop();
//Console.WriteLine(sw.Elapsed);

//sw.Restart();
//var aof = new ArrayOfFloatWrapper(count);
//for (int i = 0; i < count; i++)
//{
//    var v = i / 10000f;
//    aof[i] = v;
//}
//sw.Stop();
//Console.WriteLine(sw.Elapsed);

//Modify this value on different platforms
const int count = int.MaxValue / 10000;
var a = new FixedArray(count);
var aof = new ArrayOfFloatWrapper(count);
var sw = new System.Diagnostics.Stopwatch();
for (int i = 0; i < count; i++)
{
    var v = i / 100000f;
    a[i] = v;
    aof[i] = v;
}
#if sample1
unsafe
{
    Console.WriteLine("======== first time ========");

    Console.WriteLine("managed:");
    sw.Restart();
    Console.WriteLine(Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("native:");
    sw.Restart();
    Console.WriteLine(DotProuct_native.Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine();
    Console.WriteLine("======== second time ========");

    Console.WriteLine("managed:");
    sw.Restart();
    Console.WriteLine(Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("native:");
    sw.Restart();
    Console.WriteLine(DotProuct_native.Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);
}
#endif


#if sample2
unsafe
{
    Console.WriteLine("======== first time ========");

    Console.WriteLine("managed:");
    sw.Restart();
    Console.WriteLine(Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("native:");
    sw.Restart();
    Console.WriteLine(DotProuct_native.Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("native sse:");
    sw.Restart();
    Console.WriteLine(DotProuct_native.Dot_sse(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine();
    Console.WriteLine("======== second time ========");

    Console.WriteLine("managed:");
    sw.Restart();
    Console.WriteLine(Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("native:");
    sw.Restart();
    Console.WriteLine(DotProuct_native.Dot(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("native sse:");
    sw.Restart();
    Console.WriteLine(DotProuct_native.Dot_sse(a.Pointer, aof.Pointer, count));
    sw.Stop();
    Console.WriteLine(sw.Elapsed);
}
#endif