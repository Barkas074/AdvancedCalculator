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

		//protected static Operations TypeOfOperation(string operation)
		//{
		//	return operation switch
		//	{
		//		"*" => new Multiplication(),
		//		"/" => new Division(),
		//		"+" => new Addition(),
		//		"-" => new Subtraction(),
		//		"^" => new Degree(),
		//		"!" => new Factorial(),
		//		"log" => new Logarithm(),
		//		"sin" => new Sine(),
		//		"cos" => new Cosine(),
		//		"tg" => new Tangent(),
		//		"arcsin" => new ArcSine(),
		//		"arccos" => new ArcCosine(),
		//		"arctg" => new ArcTangent(),
		//		_ => throw new Exception("Неизвестная операция"),
		//	};
		//}

		//static List<object> ParseExpression(List<string> textExpression)
		//{
		//	List<object> textParse = new List<object>();
		//	//for (int i = 0; i < textExpression.Count; i++)
		//	//	if (double.TryParse(textExpression[i], out _))
		//	//		textParse.Add(double.Parse(textExpression[i]));
		//	//	else if (textExpression[i] == "x" || textExpression[i] == "(" || textExpression[i] == ")")
		//	//		textParse.Add(textExpression[i]);
		//	//	else 
		//	//		textParse.Add(TypeOfOperation(textExpression[i]));
		//	List<object> reversePolishNotation = new List<object>();
		//	Stack<object> operations = new Stack<object>();
		//	for (int i = 0; i < textParse.Count; i++)
		//	{
		//		object value = textParse[i];
		//		if (value is double || value is string @string && @string == "x")
		//		{
		//			reversePolishNotation.Add(value);
		//		}
		//		else if (operations.TryPeek(out _))
		//		{
		//			if (operations.Peek() is Operations peek && value is Operations op)
		//			{
		//				if (peek.ToPriority() < op.ToPriority())
		//				{
		//					if ((string)value == ")")
		//						while (operations.Count != 0)
		//							if ((string)operations.Peek() != "(")
		//								reversePolishNotation.Add(operations.Pop());
		//							else
		//								operations.Pop();
		//					else
		//						operations.Push(op);
		//				}
		//				else
		//				{
		//					reversePolishNotation.Add(operations.Pop());
		//					if (operations.Count != 0 && !(value is double) && peek.ToPriority() == op.ToPriority())
		//					{
		//						reversePolishNotation.Add(operations.Pop());
		//					}
		//					operations.Push(op);
		//				}
		//			}
		//			if (i == textParse.Count - 1 && operations.TryPeek(out _))
		//				while (operations.Count != 0)
		//					reversePolishNotation.Add(operations.Pop());
		//			if (value is string /*_value == "(" || (string)value == ")"*/)
		//			{
		//				if ((string)value == ")")
		//					while (operations.Count != 0)
		//						if ((string)operations.Peek() != "(")
		//							reversePolishNotation.Add(operations.Pop());
		//						else
		//							operations.Pop();
		//				else
		//					operations.Push(value);
		//			}
		//		}
		//		else
		//		{
		//			operations.Push(value);
		//		}

		//	}
		//	return reversePolishNotation;
		//}
	}
}
