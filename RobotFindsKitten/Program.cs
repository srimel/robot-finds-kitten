using Mindmagma.Curses;
using RobotFindsKitten.Components;

namespace RobotFindsKitten;

class Program
{
    static void Main(string[] args)
    {
        Screen screen = new();
        screen.ApplySettings();
        screen.SetTitle("Robot Finds Kitten");

        Window window = new(null, screen.ROWS / 2, screen.COLS / 2, screen.ROWS / 4, screen.COLS / 4);
        window.ShowBorder((char)0, (char)0);
        window.PopulateWindow();

        int finalMessageRow = screen.ROWS / 4 + window.ROWS + 3;
        NCurses.AttributeOn(NCurses.ColorPair(3));
        NCurses.MoveAddString(finalMessageRow, 0, "Press any key to end program...");
        NCurses.AttributeOff(NCurses.ColorPair(3));
        NCurses.Refresh();

        // Test for moving cursor within in window (automated)
        for (int i = 1; i < window.ROWS - 1; i++)
        {
            for (int j = 1; j < window.COLS - 1; j++)
            {
                NCurses.WindowMove(window.WindowPtr, i, j);
                window.Refresh();
                NCurses.Nap(10);
            }
        }

        // Example of inspecting char at given cursor position
        var result = NCurses.MoveWindowInspectChar(window.WindowPtr, 1, 1);
        int resultColorPair = (int)((result & CursesAttribute.COLOR) >> 8);
        char resultChar = (char)(result & CursesAttribute.CHARTEXT);
        NCurses.MoveAddString(finalMessageRow + 3, 0, $"First char of window: {resultChar}\nColor Pair: {resultColorPair}");
        NCurses.Refresh();

        window.GetChar();
        NCurses.EndWin();
    }
}

