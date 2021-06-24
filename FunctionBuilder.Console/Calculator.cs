namespace FunctionBuilder.Console
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using FunctionBuilder.Logic;

	class Calculator
	{
		static void Main()
		{
			Console.Write("Введите диапазон значений x по образцу (0 10): ");
			double[] rangeX = ReadInput();
			Console.Write("Введите шаг построения функции: ");
			double[] step = ReadInput();
			string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			string inputFile = @"\input.txt";
			string outputFile = @"\output.txt";
			WorkWithFiles workWithFiles = new WorkWithFiles();
			if (!File.Exists(path + inputFile))
				workWithFiles.WriteTextToFile(path, inputFile, "log(15^x^x - 25,5 + 5! * 43 - (7 * 5)) * cos(5) - 1");
			ReversePolishNotation reversePolishNotation = new ReversePolishNotation();
			List<string> textExpression = workWithFiles.ReadTextFromFile(path, inputFile);
			List<object> parseExpression = reversePolishNotation.Parse(textExpression);
			foreach (var item in parseExpression)
			{
				Console.WriteLine(item);
			};
			TableOfFunctionValues tableOfFunctionValues = new TableOfFunctionValues();
			string finalText = tableOfFunctionValues.CreatingTable(rangeX, step[1], parseExpression);
			workWithFiles.WriteTextToFile(path, outputFile, finalText);
		}

		static double[] ReadInput()
		{
			while (true)
			{
				string input = Console.ReadLine();
				double[] doubleInput = new double[2];
				if (double.TryParse(input, out doubleInput[1]))
					return doubleInput;
				Console.WriteLine("Можно вводить только числа.");
			}
		}
	}
}
