using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace BasicMvvmSample
{
    internal class Program
    {
        // Точка входа приложения.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Конфигурация Avalonia.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
