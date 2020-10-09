namespace FaultTrack.Shell
{
    using System.Windows;

    public interface IUIPackage : IPackage
    {
        string ExtensionPoint { get; }
        dynamic DataContext { get; }
        dynamic Template { get;}
    }
}