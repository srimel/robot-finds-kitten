using Mindmagma.Curses;

namespace RobotFindsKitten.Components
{
    public static class Color
    {
        public static void InitColors()
        {
            NCurses.StartColor();
            NCurses.InitPair(1, CursesColor.CYAN, CursesColor.BLACK);
            NCurses.InitPair(2, CursesColor.RED, CursesColor.BLACK);
            NCurses.InitPair(3, CursesColor.GREEN, CursesColor.BLACK);
            NCurses.InitPair(4, CursesColor.MAGENTA, CursesColor.BLACK);
            NCurses.InitPair(5, CursesColor.YELLOW, CursesColor.BLACK);
            NCurses.InitPair(6, CursesColor.BLUE, CursesColor.BLACK);
        }
    }
}

