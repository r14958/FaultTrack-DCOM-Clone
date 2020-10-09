namespace FaultTrack.Shell
{
    using Infrastructure;

    public class ShellFactory
    {
        public static IShell GetInstance()
        {
            return new Shell();
        }
    }
}