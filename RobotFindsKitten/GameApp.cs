using Mindmagma.Curses;
using RobotFindsKitten.Components;

namespace RobotFindsKitten
{
    public class GameApp
    {
        private readonly Screen screen;
        private readonly Window infoWin;
        private readonly Window mainWin;
        private List<Tuple<int, int>> objects;

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
            int key = 0;
            while ((key = NCurses.GetChar()) != CursesKey.ESC)
            {
                NCurses.GetYX(mainWin.WindowPtr, out int currY, out int currX);
                switch (key)
                {
                    case CursesKey.LEFT:
                        if (currX > 1)
                        {
                            mainWin.MoveCursor(currY, currX - 1);
                        }
                        break;
                    case CursesKey.RIGHT:
                        if (currX < mainWin.COLS - 2)
                        {
                            mainWin.MoveCursor(currY, currX + 1);
                        }
                        break;
                    case CursesKey.UP:
                        if (currY > 1)
                        {
                            mainWin.MoveCursor(currY - 1, currX);
                        }
                        break;
                    case CursesKey.DOWN:
                        if (currY < mainWin.ROWS - 2)
                        {
                            mainWin.MoveCursor(currY + 1, currX);
                        }
                        break;
                }
                mainWin.Refresh();
            }
            TestMessageEndProgram();
            GetCharEndProgram();
        }

        public void Init()
        {
            screen.ApplySettings();
            screen.SetTitle("Robot Finds Kitten");
            infoWin.ShowBorder((char)0, (char)0);
            mainWin.ShowBorder((char)0, (char)0);
            mainWin.PopulateWindow();
            objects = mainWin.Objects;
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

