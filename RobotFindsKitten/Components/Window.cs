using Mindmagma.Curses;

namespace RobotFindsKitten.Components
{
    public class Window
    {
        public IntPtr WindowPtr { get; }
        public bool IsSubWindow { get; } = false;

        public int COLS { get; }
        public int ROWS { get; }

        public int OriginX { get; }
        public int OriginY { get; }

        public Window(IntPtr? parent, int rows, int columns, int yStart, int xStart)
        {
            if (parent.HasValue)
            {
                WindowPtr = NCurses.SubWindow(parent.Value, rows, columns, yStart, xStart);
                IsSubWindow = true;
            }
            else
            {
                WindowPtr = NCurses.NewWindow(rows, columns, yStart, xStart);
            }
            NCurses.GetMaxYX(WindowPtr, out int _yMax, out int _xMax);
            ROWS = _yMax;
            COLS = _xMax;

            OriginY = yStart;
            OriginX = xStart;
        }

        public void Refresh()
        {
            NCurses.WindowRefresh(WindowPtr);
        }

        public void GetChar()
        {
            NCurses.WindowGetChar(WindowPtr);
        }

        public void ShowBorder(char vert, char horz)
        {
            NCurses.Box(WindowPtr, vert, horz);
            Refresh();
        }

        public void MoveAddStr(int y, int x, string msg)
        {
            NCurses.MoveWindowAddString(WindowPtr, y, x, msg);
        }

        public void MoveCursor(int y, int x)
        {
            NCurses.WindowMove(WindowPtr, y, x);
        }

        public uint InspectChar()
        {
            return NCurses.WindowInspectChar(WindowPtr);
        }

        public uint MoveInspectChar(int y, int x)
        {
            return NCurses.MoveWindowInspectChar(WindowPtr, y, x);
        }

        public void PopulateWindow()
        {
            Random rng = new Random();
            string characters = "~!@#$%^&*{}QJK";
            int charactersLength = characters.Length;
            int charactersIndex = 0;
            for (int i = 1; i < ROWS - 1; i++)
            {
                for (int j = 1; j < COLS - 1; j++)
                {
                    if (charactersIndex >= charactersLength)
                    {
                        charactersIndex = 0;
                    }
                    int colorPair = rng.Next(1, 7);
                    NCurses.WindowAttributeOn(WindowPtr, NCurses.ColorPair(colorPair));
                    MoveAddStr(i, j, characters[charactersIndex].ToString());
                    NCurses.WindowAttributeOff(WindowPtr, NCurses.ColorPair(colorPair));
                    Refresh();
                    charactersIndex++;
                }
            }
        }
    }
}

