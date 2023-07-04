using Mindmagma.Curses;
using RobotFindsKitten.Components;

namespace RobotFindsKitten
{
    public class GameApp
    {
        private readonly Screen screen;
        private readonly Window infoWin;
        private readonly Window mainWin;

        public GameApp()
        {
            screen = new();

            infoWin = new(null,
                screen.ROWS / 8,
                screen.COLS / 2,
                screen.ROWS / 8,
                screen.COLS / 4);

            mainWin = new(null,
                screen.ROWS / 2,
                screen.COLS / 2,
                screen.ROWS / 4,
                screen.COLS / 4);
        }

        public void Run()
        {
            Init();
            TestMessageEndProgram();
            TestAutoMoveWindowCursor();
            TestMoveWindowInspect();
            GetCharEndProgram();
        }

        public void Init()
        {
            screen.ApplySettings();
            screen.SetTitle("Robot Finds Kitten");
            infoWin.ShowBorder((char)0, (char)0);
            mainWin.ShowBorder((char)0, (char)0);
            mainWin.PopulateWindow();
        }

        public void GetCharEndProgram()
        {
            mainWin.GetChar();
            NCurses.EndWin();
        }

        public void TestMessageEndProgram()
        {
            int finalMessageRow = screen.ROWS / 4 + mainWin.ROWS + 3;
            NCurses.AttributeOn(NCurses.ColorPair(3));
            NCurses.MoveAddString(finalMessageRow, 0, "Press any key to end program...");
            NCurses.AttributeOff(NCurses.ColorPair(3));
            NCurses.Refresh();
        }

        public void TestAutoMoveWindowCursor()
        {
            // Test for moving cursor within in window (automated)
            for (int i = 1; i < mainWin.ROWS - 1; i++)
            {
                for (int j = 1; j < mainWin.COLS - 1; j++)
                {
                    //NCurses.WindowMove(window.WindowPtr, i, j);
                    mainWin.MoveCursor(i, j);
                    mainWin.Refresh();
                    NCurses.Nap(10);
                }
            }
        }

        public void TestMoveWindowInspect()
        {
            // Example of inspecting char at given cursor position
            int finalMessageRow = screen.ROWS / 4 + mainWin.ROWS + 3;
            var result = NCurses.MoveWindowInspectChar(mainWin.WindowPtr, 1, 1);
            int resultColorPair = (int)((result & CursesAttribute.COLOR) >> 8);
            char resultChar = (char)(result & CursesAttribute.CHARTEXT);
            NCurses.MoveAddString(finalMessageRow + 3, 0, $"First char of window: {resultChar}\nColor Pair: {resultColorPair}");
            NCurses.Refresh();
        }
    }
}

