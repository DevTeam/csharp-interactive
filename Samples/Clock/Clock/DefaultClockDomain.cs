// ReSharper disable UnusedType.Global
// ReSharper disable PartialTypeWithSinglePart
namespace Clock
{
    using System;
    using Models;
    using ViewModels;
    using Pure.DI;
    using static Pure.DI.Lifetime;

    public static partial class DefaultClockDomain
    {
        static DefaultClockDomain() => 
            DI.Setup()
            // View Models
            .Bind<IClockViewModel>().To<ClockViewModel>()

            // Models
            .Bind<ILog<TT>>().As(Singleton).To<Log<TT>>()
            .Bind<ITimer>().As(Singleton).To(_ => new Timer(TimeSpan.FromSeconds(1)))
            .Bind<IClock>().As(Singleton).To<SystemClock>();
    }
}