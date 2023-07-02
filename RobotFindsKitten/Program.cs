using Mindmagma.Curses;
using RobotFindsKitten.Components;

namespace RobotFindsKitten;

class Program
{
    static void Main(string[] args)
    {
        IntPtr screen = NCurses.InitScreen();
        NCurses.NoEcho();
        NCurses.SetCursor(1);

        if (!NCurses.HasColors())
        {
            throw new NotSupportedException();
		}

        Color.InitColors();

        int yMax, xMax;
        NCurses.GetMaxYX(screen, out yMax, out xMax);

        string screenDimens = $"Terminal Dimensions Y: {yMax}, X: {xMax}";
        NCurses.AttributeOn(NCurses.ColorPair(1));
        NCurses.MoveAddString(yMax / 4 - 2, xMax / 4 - 2, screenDimens);
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

        // Test for moving cursor within in window (automated)
        for (int i = 1; i < window.yMax - 1; i++)
        { 
            for (int j = 1; j < window.xMax - 1; j++)
            { 
				NCurses.MoveWindowCursor(window.WindowPtr, i, j);
                window.Refresh();
                NCurses.Nap(50);
			}
		}

        window.GetChar();
        NCurses.EndWin();
    }
}

