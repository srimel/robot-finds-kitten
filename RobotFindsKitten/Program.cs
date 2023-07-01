using Mindmagma.Curses;

namespace RobotFindsKitten;

class Program
{
    static void Main(string[] args)
    {
        IntPtr screen = NCurses.InitScreen();
        NCurses.NoEcho();
        NCurses.SetCursor(0);

        int yMax, xMax;
        NCurses.GetMaxYX(screen, out yMax, out xMax);

        string screenDimens = $"Terminal Dimensions Y: {yMax}, X: {xMax}";
        NCurses.AddString(screenDimens);
        NCurses.Refresh();

        IntPtr window = NCurses.NewWindow(yMax/2, xMax/2, yMax/4, xMax/4);
        NCurses.Box(window, (char) 0, (char) 0);
        int yMaxWin, xMaxWin;
        NCurses.GetMaxYX(window, out yMaxWin, out xMaxWin);
        NCurses.MoveWindowAddString(window, 1, 1, $"Window Dimensions Y: {yMaxWin}, X: {xMaxWin}");
        NCurses.WindowRefresh(window);

        int finalMessageRow = yMax / 4 + yMaxWin + 3;
        NCurses.MoveAddString(finalMessageRow, 0, "Press any key to end program...");
        NCurses.Refresh();

        NCurses.WindowGetChar(window);
        NCurses.EndWin();
    }
}

