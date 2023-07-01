using Mindmagma.Curses;

namespace RobotFindsKitten;

class Program
{
    static void Main(string[] args)
    {
        string welcome = "Hello Curses!";

        var screen = NCurses.InitScreen();

        // Simple animated welcome message
        for (int i = 0; i < welcome.Length; i++)
        {
            NCurses.MoveAddChar(0, i, welcome[i]);
            NCurses.Refresh();
            NCurses.Nap(100);
		}

        // Print a box of chars
        int ROWS = 3;
        int COLS = 5;
        int STARTROW = 3;
        for (int i = STARTROW; i < ROWS + STARTROW; i++)
        { 
            for (int j = 0; j < COLS; j++)
            {
                NCurses.MoveAddChar(i, j, 'o');
			}
		}

        int ENDROW = ROWS + STARTROW + 1;
        NCurses.Move(ENDROW, 0);
        NCurses.AddString("Press any key to end program. ");

        NCurses.Refresh();
        NCurses.GetChar();
        NCurses.EndWin();
    }
}

