using System;
using System.Diagnostics;
using System.Threading;
using nanoFramework;
using nanoFramework.M5Stack;
using nanoFramework.M5Core2;

namespace NFCore2GameOfLife1
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");
            nanoFramework.M5Stack.M5Core2.InitializeScreen();

            nanoFramework.M5Stack.Console.WriteLine("Hello World!");

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
