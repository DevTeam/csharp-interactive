// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace CSharpInteractive.Core;

internal interface ICISettings
{
    CIType CIType { get; }

    string Version { get; }

    string FlowId { get; }

    string ServiceMessagesPath { get; }
}