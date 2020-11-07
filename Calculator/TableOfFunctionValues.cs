using System;
using System.Collections.Generic;

namespace Calculator
{
	class TableOfFunctionValues
	{

		//static void Main(string[] args)
		//{
		//	Console.WriteLine("Введите название функции (Логарифм - 0, парабола - 1, гипербола - 2)");
		//	Functions function = ReadFunction();
		//	Console.Write("Введите шаг построения функции: ");
		//	string step = ReadInput();
		//	Console.Write("Введите диапазон значений x по образцу (0 10): ");
		//	string rangeX = ReadInput();
		//	string[] rangeXSplit = rangeX.Split(' ');
		//	CreatingTable(rangeXSplit, step, function);
		//}

		string ReadInput()
		{
			while (true)
			{
				string input = Console.ReadLine();
				double doubleInput;
				if (double.TryParse(input, out doubleInput))
					return input;

				string[] splitInput = input.Split(' ');
				if (double.TryParse(splitInput[0], out doubleInput) && double.TryParse(splitInput[1], out doubleInput))
					return input;
				Console.WriteLine("Можно вводить только числа.");
			}
		}


		public string CreatingTable(string[] rangeXSplit, string step, List<Object> function)
		{
			string tableWithText;
			int maxLength = 0;
			for (double x = double.Parse(rangeXSplit[0]); x <= double.Parse(rangeXSplit[1]); x += double.Parse(step))
			{
				int yLength = Math.Round(PerformFunction(x, function)).ToString().Length;
				if (yLength > maxLength)
					maxLength = yLength;
			}

			string frameTableX = new string('-', maxLength + 4);
			string space = IsNumberSpace(frameTableX, 1, false);
			string spaceStart = IsNumberSpace(frameTableX, 1, true);

			tableWithText = $"|{frameTableX}|{frameTableX}|\n";
			tableWithText += $"|{spaceStart}x{space}|{spaceStart}y{space}|\n";
			tableWithText += $"|{frameTableX}|{frameTableX}|\n";

			for (double x = double.Parse(rangeXSplit[0]); x <= double.Parse(rangeXSplit[1]); x += double.Parse(step))
			{
				double y = PerformFunction(x, function);
				tableWithText += $"|{IsNumberSpace(frameTableX, Math.Round(x, 2), true)}" +
					$"{Math.Round(x, 2)}{IsNumberSpace(frameTableX, Math.Round(x, 2), false)}|" +
					$"{IsNumberSpace(frameTableX, Math.Round(y, 2), true)}" +
					$"{Math.Round(y, 2)}{IsNumberSpace(frameTableX, Math.Round(y, 2), false)}|\n";
			}

			tableWithText += $"|{frameTableX}|{frameTableX}|";
			return tableWithText;
		}


		string IsNumberSpace(string length, double var, bool deleteSymbol)
		{
			string space = new string(' ', (int) Math.Ceiling((length.Length - var.ToString().Length) / (double) 2));
			if (deleteSymbol && (length.Length - var.ToString().Length) % 2 != 0)
				space = space.Remove(0, 1);
			return space;
		}


		double PerformFunction(double x, List<Object> function)
		{
			return 0;
		}
	}
}
