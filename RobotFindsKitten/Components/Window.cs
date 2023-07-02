using System;
using Mindmagma.Curses;
using System.Runtime.InteropServices;

namespace RobotFindsKitten.Components
{
	public class Window
	{
		public IntPtr WindowPtr { get; }
		public bool IsSubWindow { get; } = false;

		public int xMax { get; }
		public int yMax { get; }

		public int xOrigin { get; }
		public int yOrigin { get; }

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
			int _yMax, _xMax;
			NCurses.GetMaxYX(WindowPtr, out _yMax, out _xMax);
			yMax = _yMax;
			xMax = _xMax;

			yOrigin = yStart;
			xOrigin = xStart;
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

		public void MoveAddStr(int y, int x, string msg)
		{
			NCurses.MoveWindowAddString(WindowPtr, y, x, msg);
		}

		public void PopulateWindow()
		{
			Random rng = new Random();
			string characters = "~!@#$%^&*{}QJK";
			int charactersLength = characters.Length;
			int charactersIndex = 0;
			for (int i = 1; i < yMax - 1; i++)
			{ 
				for (int j = 1; j < xMax -1; j++)
				{
					if (charactersIndex >= charactersLength)
					{
						charactersIndex = 0;
					}
					int colorPair = rng.Next(1, 6);
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

