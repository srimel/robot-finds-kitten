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
        public List<Tuple<int, int>> Objects { get; set; }

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
            Objects = new();
        }

        public void Refresh()
        {
            NCurses.WindowRefresh(WindowPtr);
        }

        public int GetChar()
        {
            return NCurses.WindowGetChar(WindowPtr);
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

        public void Clear()
        {
            NCurses.ClearWindow(WindowPtr);
            ShowBorder((char)0, (char)0);
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
            string characters = "~!@+$%^&*{}=";
            GenerateRandomPairs(characters.Length);

            if (Objects.Count != characters.Length)
            {
                throw new Exception("Objects does not match char length");
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                int colorPair = rng.Next(1, 7);
                NCurses.WindowAttributeOn(WindowPtr, NCurses.ColorPair(colorPair));
                NCurses.MoveWindowAddChar(WindowPtr, Objects[i].Item1, Objects[i].Item2, characters[i]);
                NCurses.WindowAttributeOff(WindowPtr, NCurses.ColorPair(colorPair));
                Refresh();
            }
        }

        private void GenerateRandomPairs(int numPairs)
        {
            Random random = new();
            while (Objects.Count < numPairs)
            {
                int y = random.Next(1, ROWS - 1);
                int x = random.Next(1, COLS - 1);
                Tuple<int, int> pair = Tuple.Create(y, x);

                if (!Objects.Contains(pair))
                {
                    Objects.Add(pair);
                }
            }
        }
    }
}

