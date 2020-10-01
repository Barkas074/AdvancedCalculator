using System;

namespace TableOfFunctionValues
{
	class TableOfFunctionValues
	{
		const string LOGARITHM = "логарифм";
		const string PARABOLA = "парабола";
		const string HYPERBOLE = "гипербола";

		static void Main(string[] args)
		{
			Console.WriteLine("Введите название функции (Логарифм - 0, парабола - 1, гипербола - 2)");
			Functions function = ReadFunction();
			Console.Write("Введите шаг построения функции: ");
			string step = ReadInput();
			Console.Write("Введите диапазон значений x по образцу (0 10): ");
			string rangeX = ReadInput();
			string[] rangeXSplit = rangeX.Split(' ');
			CreatingTable(rangeXSplit, step, function);
		}


		enum Functions
		{
			Логарифм, Парабола, Гипербола
		}


		static Functions ReadFunction()
		{
			while (true)
			{
				string input = Console.ReadLine().ToLower();
				int intInput;
				if (int.TryParse(input, out intInput))
					intInput = int.Parse(input);
				else
					intInput = 100;
				if (input == LOGARITHM || intInput == (int) Functions.Логарифм)
					return Functions.Логарифм;
				if (input == PARABOLA || intInput == (int) Functions.Парабола)
					return Functions.Парабола;
				if (input == HYPERBOLE || intInput == (int) Functions.Гипербола)
					return Functions.Гипербола;
				Console.WriteLine("Такой функции нет в программе.");
			}
		}


		static string ReadInput()
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


		static void CreatingTable(string[] rangeXSplit, string step, Functions function)
		{
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

			Console.WriteLine($"|{frameTableX}|{frameTableX}|");
			Console.WriteLine($"|{spaceStart}x{space}|{spaceStart}y{space}|");
			Console.WriteLine($"|{frameTableX}|{frameTableX}|");

			for (double x = double.Parse(rangeXSplit[0]); x <= double.Parse(rangeXSplit[1]); x += double.Parse(step))
			{
				double y = PerformFunction(x, function);
				Console.WriteLine($"|{IsNumberSpace(frameTableX, Math.Round(x, 2), true)}" +
					$"{Math.Round(x, 2)}{IsNumberSpace(frameTableX, Math.Round(x, 2), false)}|" +
					$"{IsNumberSpace(frameTableX, Math.Round(y, 2), true)}" +
					$"{Math.Round(y, 2)}{IsNumberSpace(frameTableX, Math.Round(y, 2), false)}|");
			}

			Console.WriteLine($"|{frameTableX}|{frameTableX}|");
		}


		static string IsNumberSpace(string length, double var, bool deleteSymbol)
		{
			string space = new string(' ', (int) Math.Ceiling((length.Length - var.ToString().Length) / (double) 2));
			if (deleteSymbol && (length.Length - var.ToString().Length) % 2 != 0)
				space = space.Remove(0, 1);
			return space;
		}


		static double PerformFunction(double x, Functions function)
		{
			switch (function)
			{
				case Functions.Логарифм:
					return Math.Log10(x);
				case Functions.Парабола:
					return x * x;
				case Functions.Гипербола:
					return 1 / x;
				default:
					return x;
			}
		}
	}
}
