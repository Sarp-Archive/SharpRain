using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpRain
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;
			Console.Clear();

			while (true)
			{
				new RainDrop(Console.BufferWidth, Console.BufferHeight);
				Thread.Sleep(500);
			}
		}
	}

	class RainDrop
	{
		char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890^&$%#@!*?;:".ToCharArray();
		char GetNewChar() 
		{
			return chars[new Random().Next(chars.Length)];
		}
		
		int rainSize = 0;
		int max = 7;

		int col = 0;
		int row = 0;

		int termW, termH;

		static int lastCol;
		
		public RainDrop(int _w, int _h)
		{
			termW = _w;
			termH = _h;

			var rand = new Random().Next(termW);
			while (rand == lastCol)
            {
				rand = new Random().Next(termW);
			}
			col = rand;

			Start();
		}

		public async void Start()
		{
			await Task.Run(() => { StartDropping(); });
		}
		
		async void StartDropping()
		{
			while (MoveDown())
			{
				Thread.Sleep(150);
			}
		}

		bool MoveDown()
		{
			if (row + rainSize == termH)
			{
				rainSize--;
				row++;

			}
			else
			{
				if (rainSize < max)
				{
					rainSize++;
				}
				else
				{
					row++;
				}
			}
			if (row > 2)
			{
				for (int i = row - 3; i < row; i++)
				{
					Utils.WriteAt(col, i, " ", ConsoleColor.Green);
				}
				
			}
			for (int i = 0; i < rainSize; i++)
			{
				if (i == rainSize - 1)
				{
					Utils.WriteAt(col, row + i, GetNewChar(), ConsoleColor.Cyan);
				}
				else
				{
					Utils.WriteAt(col, row + i, GetNewChar(), ConsoleColor.Green);
				}
			}
			if (row == termH)
			{
				return false;
			}
			return true;
		}
	}

	class Utils
	{
		static char c = (char)27;
		
		public static void WriteAt(int left, int top, string text, ConsoleColor forecolor)
		{
			Console.ForegroundColor = forecolor;
			Console.Write(c + "[" + top.ToString() + ";" + left.ToString() + "H" + text);
		}

		public static void WriteAt(int left, int top, char character, ConsoleColor forecolor)
		{
			WriteAt(left, top, character.ToString(), forecolor);
		}
		
		public static void WriteAt(int left, int top, string text, ConsoleColor forecolor, ConsoleColor backcolor)
		{
			Console.BackgroundColor = backcolor;
			WriteAt(left, top, text, forecolor);
		}

		public static void WriteAt(int left, int top, char character, ConsoleColor forecolor, ConsoleColor backcolor)
		{
			Console.BackgroundColor = backcolor;
			WriteAt(left, top, character.ToString(), forecolor);
		}
	}
}