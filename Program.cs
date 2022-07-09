using System;

namespace monoChip8
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new AppRoot())
                game.Run();
        }
    }
}
