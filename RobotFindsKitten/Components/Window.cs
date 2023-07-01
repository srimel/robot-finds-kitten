using System;
using Mindmagma.Curses;

namespace RobotFindsKitten.Components
{
	public class Window
	{
		public IntPtr WindowPtr { get; }
		public bool IsSubWindow { get; } = false;
		public int xMax { get; }
		public int yMax { get; }

		public Window(IntPtr? parent, int rows, int columns, int yOrigin, int xOrigin)
		{
			if (parent.HasValue)
			{ 
				WindowPtr = NCurses.SubWindow(parent.Value, rows, columns, xOrigin, yOrigin);
				IsSubWindow = true;
			}
			else 
			{
				WindowPtr = NCurses.NewWindow(rows, columns, yOrigin, xOrigin);
			}
			int _yMax, _xMax;
			NCurses.GetMaxYX(WindowPtr, out _yMax, out _xMax);
			yMax = _yMax;
			xMax = _xMax;
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
		}

		public void MoveAddChar(int y, int x, char c)
		{
			NCurses.MoveWindow(WindowPtr, y, x);
			NCurses.WindowAddChar(WindowPtr, c);
		}

		public void MoveAddStr(int y, int x, string msg)
		{
			NCurses.MoveWindowAddString(WindowPtr, y, x, msg);
		}
	}
}

