using Mindmagma.Curses;

namespace RobotFindsKitten.Components
{
    public class Screen
    {
        public IntPtr ScreenPtr { get; }

        public int ROWS { get; }
        public int COLS { get; }

        public Screen()
        {
            ScreenPtr = NCurses.InitScreen();
            NCurses.GetMaxYX(ScreenPtr, out int yMax, out int xMax);
            ROWS = yMax;
            COLS = xMax;
        }

        public void ApplySettings()
        {
            //NCurses.ResizeTerminal(86, 42);
            NCurses.NoEcho();
            //NCurses.CBreak();
            NCurses.Keypad(ScreenPtr, true);
            NCurses.SetCursor(1);

            Color.InitColors();
            NCurses.Refresh(); // not sure why I need this?
        }

        public void SetTitle(string title)
        {
            int y = ROWS / 16;
            int midPointStr = title.Length / 2;
            int x = (COLS / 2) - midPointStr;
            NCurses.MoveAddString(y, x, title);
            NCurses.Refresh();
        }
    }
}

