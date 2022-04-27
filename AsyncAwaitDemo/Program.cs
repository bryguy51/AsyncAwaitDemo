// See https://aka.ms/new-console-template for more information
using AsyncAwaitLib;

IService asyncAwaitLibService = new Service();

Console.WriteLine("Running without async....");
string elapsedTime = asyncAwaitLibService.executeSync();
Console.WriteLine(elapsedTime);
Console.WriteLine("Run without async end....");


Console.WriteLine("Running with async....");
string elapsedAsyncTime = await asyncAwaitLibService.executeAsync();
Console.WriteLine(elapsedAsyncTime);
Console.WriteLine("Run with async end....");
