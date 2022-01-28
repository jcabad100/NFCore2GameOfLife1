using System;
using System.Diagnostics;
using System.Threading;
using System.Device.I2c;
using System.Device.Gpio;
using nanoFramework;
using nanoFramework.M5Stack;
using nanoFramework.M5Core2;
using Iot.Device.Ft6xx6x;


namespace NFCore2GameOfLife1
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");
            M5Core2.InitializeScreen();

            nanoFramework.M5Stack.Console.WriteLine("Core2 Touch test!");

            I2cConnectionSettings settings = new(1, Ft6xx6x.DefaultI2cAddress);
            using I2cDevice device = I2cDevice.Create(settings);
            using GpioController gpio = new();

            using Ft6xx6x sensor = new(device);
            var ver = sensor.GetVersion();
            Debug.WriteLine($"version: {ver}");
            sensor.SetInterruptMode(false);
            Debug.WriteLine($"Period active: {sensor.PeriodActive}");
            Debug.WriteLine($"Period active in monitor mode: {sensor.MonitorModePeriodActive}");
            Debug.WriteLine($"Time to enter monitor: {sensor.MonitorModeDelaySeconds} seconds");
            Debug.WriteLine($"Monitor mode: {sensor.MonitorModeEnabled}");
            Debug.WriteLine($"Proximity sensing: {sensor.ProximitySensingEnabled}");

            //gpio.OpenPin(39, PinMode.Input);
            // This will enable an event on GPIO39 on falling edge when the screen if touched
            gpio.RegisterCallbackForPinValueChangedEvent(39, PinEventTypes.Falling, TouchInterrupCallback);

            while (true)
            {
                Thread.Sleep(20);
            }

            void TouchInterrupCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
            {
                Debug.WriteLine("Touch interrupt");
                var points = sensor.GetNumberPoints();
                if (points == 1)
                {
                    var point = sensor.GetPoint(true);
                    // Some controllers supports as well events, you can get access to them as well thru point.Event
                    Debug.WriteLine($"ID: {point.TouchId}, X: {point.X}, Y: {point.Y}, Weight: {point.Weigth}, Misc: {point.Miscelaneous}");
                }
                else if (points == 2)
                {
                    var dp = sensor.GetDoublePoints();
                    Debug.WriteLine($"ID: {dp.Point1.TouchId}, X: {dp.Point1.X}, Y: {dp.Point1.Y}, Weight: {dp.Point1.Weigth}, Misc: {dp.Point1.Miscelaneous}");
                    Debug.WriteLine($"ID: {dp.Point2.TouchId}, X: {dp.Point2.X}, Y: {dp.Point2.Y}, Weight: {dp.Point2.Weigth}, Misc: {dp.Point2.Miscelaneous}");
                }
            }

            //Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
