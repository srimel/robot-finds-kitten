using Mindmagma.Curses;
using RobotFindsKitten.Components;

namespace RobotFindsKitten;

class Program
{
    static void Main(string[] args)
    {
        IntPtr screen = NCurses.InitScreen();
        NCurses.NoEcho();
        NCurses.SetCursor(0);

        if (!NCurses.HasColors())
        {
            throw new NotSupportedException();
		}

        NCurses.StartColor();
        NCurses.InitPair(1, CursesColor.CYAN, CursesColor.BLACK);
        NCurses.InitPair(2, CursesColor.RED, CursesColor.BLACK);
        NCurses.InitPair(3, CursesColor.GREEN, CursesColor.BLACK);
        NCurses.InitPair(4, CursesColor.MAGENTA, CursesColor.BLACK);
        NCurses.InitPair(5, CursesColor.YELLOW, CursesColor.BLACK);
        NCurses.InitPair(6, CursesColor.BLUE, CursesColor.BLACK);

        int yMax, xMax;
        NCurses.GetMaxYX(screen, out yMax, out xMax);

        string screenDimens = $"Terminal Dimensions Y: {yMax}, X: {xMax}";
        NCurses.AttributeOn(NCurses.ColorPair(1));
        NCurses.AddString(screenDimens);
        NCurses.AttributeOff(NCurses.ColorPair(1));
        NCurses.Refresh();


        Window window = new (null, yMax / 2, xMax / 2, yMax / 4, xMax / 4);
        window.ShowBorder((char) 0, (char) 0);
        window.MoveAddStr(1, 1, $"Window Dimensions Y: {window.yMax}, X: {window.xMax}");
        NCurses.WindowAttributeOn(window.WindowPtr, CursesAttribute.BLINK);
        window.MoveAddStr(2, 1, "Napping for one second...");
        NCurses.WindowAttributeOff(window.WindowPtr, CursesAttribute.BLINK);
        window.MoveAddStr(3, 1, "Get ready for a color explosion!");
        window.Refresh();

        NCurses.Nap(4000);
        window.PopulateWindow();

        int finalMessageRow = yMax / 4 + window.yMax + 3;
        NCurses.AttributeOn(NCurses.ColorPair(3));
        NCurses.MoveAddString(finalMessageRow, 0, "Press any key to end program...");
        NCurses.AttributeOff(NCurses.ColorPair(3));
        NCurses.Refresh();

        window.GetChar();
        NCurses.EndWin();
    }
}

