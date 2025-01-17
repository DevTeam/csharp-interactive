// ReSharper disable UnusedMember.Local
// ReSharper disable PartialTypeWithSinglePart

namespace CSharpInteractive.Tests.Integration.Core;

using JetBrains.TeamCity.ServiceMessages.Read;
using Pure.DI;

internal partial class TestComposition
{
    private static void Setup() =>
        DI.Setup(nameof(TestComposition))
            .Bind<IFileSystem>().To<FileSystem>()
            .Root<IFileSystem>("FileSystem", null, RootKinds.Static)
            .Bind<IServiceMessageParser>().To<ServiceMessageParser>()
            .Root<IServiceMessageParser>("ServiceMessageParser", null, RootKinds.Static);
}