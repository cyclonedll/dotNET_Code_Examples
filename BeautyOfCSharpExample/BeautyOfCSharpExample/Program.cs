

// See https://aka.ms/new-console-template for more information
using FixedArray_test;
using Libs;




var r = new Random();
int start = 333, length = 7000000;

var v = new Vector<double>(int.MaxValue - 5000000);
//var v = new Vector<double>(500000000);
//var v = new Vector<double>(10);

//Vector<double> v = new(1000);
for (int i = 0; i < v.Count; i++)
{
    v[i] = r.NextDouble();
}

Console.WriteLine(v);


Console.WriteLine("============ Max ==================");

Console.WriteLine("Sequentail :");
var sw = System.Diagnostics.Stopwatch.StartNew();
Console.WriteLine(v.Max());
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);

Console.WriteLine("4 Cores :");
sw.Restart();
Console.WriteLine(await v.MaxAsync(numberOfWorkers: 4));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores (TPL):");
sw.Restart();
Console.WriteLine(await v.MaxAsync(useTPL: true));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores:");
sw.Restart();
Console.WriteLine(await v.MaxAsync(numberOfWorkers: 64));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("============ Locate Max ==================");

Console.WriteLine("Sequentail :");
sw.Restart();
Console.WriteLine(v.LocateMax());
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores (TPL):");
sw.Restart();
Console.WriteLine(await v.LocateMaxAsync(useTPL: true));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores :");
sw.Restart();
Console.WriteLine(await v.LocateMaxAsync(numberOfWorkers: 64));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);

Console.WriteLine("============ Min ==================");

Console.WriteLine("Sequentail :");
sw.Restart();
Console.WriteLine(v.Min());
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores (TPL):");
sw.Restart();
Console.WriteLine(await v.MinAsync(useTPL: true));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores :");
sw.Restart();
Console.WriteLine(await v.MinAsync(numberOfWorkers: 64));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);



Console.WriteLine("============ Locate Min ==================");

Console.WriteLine("Sequentail :");
sw.Restart();
Console.WriteLine(v.LocateMin());
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores (TPL):");
sw.Restart();
Console.WriteLine(await v.LocateMinAsync(useTPL: true));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);


Console.WriteLine("All Cores :");
sw.Restart();
Console.WriteLine(await v.LocateMinAsync(numberOfWorkers: 64));
sw.Stop();
Console.WriteLine(sw.Elapsed);
Console.WriteLine("----------------");
Thread.Sleep(1000);
