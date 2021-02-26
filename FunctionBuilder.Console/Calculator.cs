namespace FunctionBuilder.Console
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using FunctionBuilder.Logic;

	class Calculator : Operations
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
				workWithFiles.WriteTextToFile(path, inputFile, "3.5 + 4 * 2");
			List<string> textExpression = workWithFiles.ReadTextFromFile(path, inputFile);
			List<object> parseExpression = ParseExpression(textExpression);
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

		static List<object> ParseExpression(List<string> textExpression)
		{
			List<object> textParse = new List<object>();
			for (int i = 0; i < textExpression.Count; i++)
				if (double.TryParse(textExpression[i], out _))
					textParse.Add(double.Parse(textExpression[i]));
				else
					textParse.Add(TypeOfOperation(textExpression[i]));
			List<object> reversePolishNotation = new List<object>();
			Stack<ListOperations> operations = new Stack<ListOperations>();
			for (int i = 0; i < textParse.Count; i++)
			{
				if (textParse[i].GetType() == typeof(double) || ((ListOperations) textParse[i]) == ListOperations.UnknownVariable)
				{
					reversePolishNotation.Add(textParse[i]);
				}
				else if (!operations.TryPeek(out _) || (ListOperations) textParse[i] == ListOperations.OpenBracket || operations.Peek() < (ListOperations) textParse[i] || (ListOperations) textParse[i] == ListOperations.CloseBracket)
				{
					if ((ListOperations) textParse[i] == ListOperations.CloseBracket)
						while (operations.Count != 0)
							if (operations.Peek() != ListOperations.OpenBracket)
								reversePolishNotation.Add(operations.Pop());
							else
								operations.Pop();
					else
						operations.Push((ListOperations) textParse[i]);
				}
				else
				{
					reversePolishNotation.Add(operations.Pop());
					bool checkAddition = false;
					bool checkMultiplication = false;
					if (operations.Count != 0 && textParse[i].GetType() != typeof(double))
					{
						checkAddition = (operations.Peek() == ListOperations.Addition || operations.Peek() == ListOperations.Subtraction)
							&& ((ListOperations) textParse[i] == ListOperations.Addition || (ListOperations) textParse[i] == ListOperations.Subtraction);
						checkMultiplication = (operations.Peek() == ListOperations.Multiplication || operations.Peek() == ListOperations.Division)
							&& ((ListOperations) textParse[i] == ListOperations.Multiplication || (ListOperations) textParse[i] == ListOperations.Division);
					}
					if (checkAddition || checkMultiplication)
						reversePolishNotation.Add(operations.Pop());
					operations.Push((ListOperations) textParse[i]);
				}
				if (i == textParse.Count - 1 && operations.TryPeek(out _))
					while (operations.Count != 0)
						reversePolishNotation.Add(operations.Pop());
			}
			return reversePolishNotation;
		}
	}
}
