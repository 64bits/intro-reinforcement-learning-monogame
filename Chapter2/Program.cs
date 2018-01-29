using System;

namespace Chapter2
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new App())
                game.Run();
        }
    }
}
