using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CalculateFactorial
{
	class CalculateFactorial
	{
		static void Main(string[] args)
		{
			int n = ReadText();
			ulong factorial = ComputeFactorial((ulong) n);
			OutputValue(factorial);
		}

		static int ReadText()
		{
			string factorial;
			int factorialInt;
			do
			{
				Console.WriteLine("Введите число, факториал которого необходимо вычеслить:");
				factorial = Console.ReadLine();
			}
			while (!int.TryParse(factorial, out factorialInt));
			return factorialInt;
		}

		static ulong ComputeFactorial(ulong n)
		{
			if (n == 1)
				return 1;
			return ComputeFactorial(n - 1) * n;
		}

		static void OutputValue(ulong factorial)
		{
			byte value = 1;
			int countText = factorial.ToString().Count() + 4;
			int centerX;
			int centerY;
			string[] lines = new string[3];
			lines[0] = "\u2554" + new string('\u2550', countText - 2) + "\u2557";
			lines[1] = "\u2551 " + factorial + " \u2551";
			lines[2] = "\u255A" + new string('\u2550', countText - 2) + "\u255D";
			while (true)
			{
				centerX = (Console.WindowWidth / 2) - (countText / 2);
				centerY = (Console.WindowHeight / 2) - 2;
				value = ColorChange(value);
				Console.Clear();
				for (int i = 0; i < lines.Length; i++)
				{
					Console.SetCursorPosition(centerX, centerY);
					Console.WriteLine(lines[i]);
					centerY = Console.CursorTop;
				}
				Console.SetCursorPosition(centerX, Console.WindowHeight);
				Thread.Sleep(500);
			}
		}

		static byte ColorChange(byte value)
		{
			switch (value)
			{
				case 1:
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					value = 2;
					break;
				case 2:
					Console.BackgroundColor = ConsoleColor.DarkGray;
					Console.ForegroundColor = ConsoleColor.White;
					value = 3;
					break;
				case 3:
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.ForegroundColor = ConsoleColor.Black;
					value = 4;
					break;
				case 4:
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					value = 1;
					break;
			}
			return value;
		}
	}
}
