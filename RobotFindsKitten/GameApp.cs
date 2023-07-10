using Mindmagma.Curses;
using RobotFindsKitten.Components;

namespace RobotFindsKitten
{
    public class GameApp
    {
        private readonly Screen screen;
        private readonly Window scoreWin;
        private readonly Window infoWin;
        private readonly Window mainWin;
        private List<Tuple<int, int>> objects;
        private List<string> stringLibrary;
        private List<int> stringMapping;
        private int kitten;
        private bool won = false;
        private int score = 0;

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

            scoreWin = new(null,
                screen.ROWS / 16,
                screen.COLS / 2,
                screen.ROWS / 11,
                screen.COLS / 4);

            Init();
        }

        public void Run()
        {
            int key;
            while ((key = NCurses.GetChar()) != CursesKey.ESC && !won)
            {
                NCurses.GetYX(mainWin.WindowPtr, out int currY, out int currX);
                switch (key)
                {
                    case CursesKey.LEFT:
                        if (currX > 1)
                        {
                            int x;
                            if ((x = GetObjectIndex(currY, currX - 1)) >= 0)
                            {
                                infoWin.Clear();
                                if (x == kitten)
                                {
                                    ShowWinState();
                                }
                                else
                                {
                                    AddInfoString(infoWin, stringLibrary[stringMapping[x]]);
                                    score += 1;
                                    ShowScore();
                                }
                                infoWin.Refresh();
                            }
                            else
                            {
                                mainWin.MoveCursor(currY, currX - 1);
                            }
                        }
                        break;
                    case CursesKey.RIGHT:
                        if (currX < mainWin.COLS - 2)
                        {
                            int x;
                            if ((x = GetObjectIndex(currY, currX + 1)) >= 0)
                            {
                                infoWin.Clear();
                                if (x == kitten)
                                {
                                    ShowWinState();
                                }
                                else
                                {
                                    AddInfoString(infoWin, stringLibrary[stringMapping[x]]);
                                    score += 1;
                                    ShowScore();
                                }
                                infoWin.Refresh();
                            }
                            else
                            {
                                mainWin.MoveCursor(currY, currX + 1);
                            }
                        }
                        break;
                    case CursesKey.UP:
                        if (currY > 1)
                        {
                            int x;
                            if ((x = GetObjectIndex(currY - 1, currX)) >= 0)
                            {
                                infoWin.Clear();
                                if (x == kitten)
                                {
                                    ShowWinState();
                                }
                                else
                                {
                                    AddInfoString(infoWin, stringLibrary[stringMapping[x]]);
                                    score += 1;
                                    ShowScore();
                                }
                                infoWin.Refresh();
                            }
                            else
                            {
                                mainWin.MoveCursor(currY - 1, currX);
                            }
                        }
                        break;
                    case CursesKey.DOWN:
                        if (currY < mainWin.ROWS - 2)
                        {
                            int x;
                            if ((x = GetObjectIndex(currY + 1, currX)) >= 0)
                            {
                                infoWin.Clear();
                                if (x == kitten)
                                {
                                    ShowWinState();
                                }
                                else
                                {
                                    AddInfoString(infoWin, stringLibrary[stringMapping[x]]);
                                    score += 1;
                                    ShowScore();
                                }
                                infoWin.Refresh();
                            }
                            else
                            {
                                mainWin.MoveCursor(currY + 1, currX);
                            }
                        }
                        break;
                }
                mainWin.Refresh();
            }
            if (won)
            {
                MessageScore();
            }
            MessageEndProgram();
            GetCharEndProgram();
        }

        public void Init()
        {
            screen.ApplySettings();
            screen.SetTitle("Robot Finds Kitten");
            infoWin.ShowBorder((char)0, (char)0);
            mainWin.ShowBorder((char)0, (char)0);
            ShowScore();
            ShowPressStart();
            mainWin.PopulateWindow();
            objects = mainWin.Objects;
            HideKitten();
            GenerateStringLibrary();
            MapStrings();
        }

        public void ShowScore()
        {
            scoreWin.ClearNoBorder();
            string scoreString = $"SCORE: {score}";
            NCurses.WindowAddString(scoreWin.WindowPtr, scoreString);
            scoreWin.Refresh();
        }

        public void ShowWinState()
        {
            AddInfoString(infoWin, "You found the kitten!");
            won = true;
        }

        public void ShowPressStart()
        {
            mainWin.MoveAddStr(1, 1, "Welcome to Robot Finds Kitten!");
            mainWin.MoveAddStr(2, 1, "The goal of the game is to find");
            mainWin.MoveAddStr(3, 1, "the kitten by moving around the robot");
            mainWin.MoveAddStr(4, 1, "with the arrow keys.");
            NCurses.WindowAttributeOn(mainWin.WindowPtr, CursesAttribute.BLINK);
            mainWin.MoveAddStr(5, 1, "Press any key to start the game!");
            NCurses.WindowAttributeOff(mainWin.WindowPtr, CursesAttribute.BLINK);
            mainWin.MoveAddStr(6, 1, "Press ESC at any time to end the game.");
            mainWin.GetChar();
            mainWin.Clear();
        }

        private void HideKitten()
        {
            Random rng = new();
            kitten = rng.Next(0, objects.Count);
        }

        private bool IsKitten(int y, int x)
        {
            return y == objects[kitten].Item1 && x == objects[kitten].Item2;
        }

        // Returns -1 if no object is at coordinates
        private int GetObjectIndex(int y, int x)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (y == objects[i].Item1 && x == objects[i].Item2)
                {
                    return i;
                }
            }
            return -1;
        }

        private void GenerateStringLibrary()
        {
            stringLibrary = new()
            {
                "Huh, an old floppy disc. It's soaking wet!",
                "Here's some old fishing gear, it smells really bad.",
                "This is someone's punch card. They better not be late for work!",
                "I found a cathode-ray tube. Why is it glowing omniously? I should probably keep walking.",
                "Wow, a prestine Smith Corona typewriter! I can finally write my novel.",
                "A copy VHS copy of Raiders of the Lost Ark! I'll watch this later.",
                "Here is an old film reel-to-reel film projector, looks out of commission.",
                "I found a glorious abacus! I'll be a great mathematician someday!",
                "Oh god, I stepped on a vinyl copy of Kraftwerk. I wonder if it will still play?",
                "Ooo, I found a gold pocket watch laying a puddle of mud. Maybe I can clean it up.",
                "Ahh, here's an open can of tuna, it has all been eaten! I must be getting close!",
                "Is this a rotary telephone? * picks up reciever * Hello? * distant meowing is heard on the other line *",
                "There's a dot matrix printer, it looks like it's working on rendering a mandlebrot set. Very cool."
            };
        }

        private void MapStrings()
        {

            stringMapping = new();
            Random rng = new();
            while (stringMapping.Count != objects.Count)
            {
                var x = rng.Next(0, stringLibrary.Count);
                if (!stringMapping.Contains(x))
                {
                    stringMapping.Add(x);
                }
            }

        }

        private void AddInfoString(Window win, string msg)
        {
            int s = 0;
            for (int i = 1; i < win.ROWS - 1; i++)
            {
                for (int j = 1; j < win.COLS - 1; j++)
                {
                    if (s < msg.Length)
                    {
                        NCurses.MoveWindowAddChar(win.WindowPtr, i, j, msg[s]);
                        s++;
                    }
                }
            }
        }

        public void GetCharEndProgram()
        {
            mainWin.GetChar();
            NCurses.EndWin();
        }

        public void MessageScore()
        {
            int messageRow = screen.ROWS / 4 + mainWin.ROWS + 3;
            string finalScore = $"You looked at {score} objects before finding kitten!";
            NCurses.MoveAddString(messageRow, 0, finalScore);
        }

        public void MessageEndProgram()
        {
            int finalMessageRow = (screen.ROWS / 4 + mainWin.ROWS + 3) + 1;
            NCurses.AttributeOn(NCurses.ColorPair(3) | CursesAttribute.BLINK);
            NCurses.MoveAddString(finalMessageRow, 0, "Press any key to end program...");
            NCurses.AttributeOff(NCurses.ColorPair(3) | CursesAttribute.BLINK);
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

