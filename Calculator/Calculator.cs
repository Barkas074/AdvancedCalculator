using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Calculator
{
	class Calculator
	{
		enum Operations
		{
			UnknownVariable = 0, OpenBracket = 1, CloseBracket = 2, Addition = 3, Subtraction = 4, Multiplication = 5, Division = 6, Degree = 7, Logarithm, Sine, Cosine, Tangent, ArcSine, ArcCosine, ArcTangent
		}

		static void Main()
		{
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
			}
			//float value = CalculationValue(text);
			//if (float.IsInfinity(value))
			//	workWithFiles.WriteTextToFile(path, outputFile, "Недопустимая операция");
			//else
			//	workWithFiles.WriteTextToFile(path, outputFile, value.ToString());
		}

		static float CalculationValue(List<string> text)
		{
			float value = 0;
			float x = float.Parse(text[0]);
			float y = float.Parse(text[1]);
			for (int i = 2; i < text.Count; i++)
				switch (char.Parse(text[i]))
				{
					case '*':
						value += x * y;
						break;
					case '/':
						value += x / y;
						break;
					case '+':
						value += x + y;
						break;
					case '-':
						value += x - y;
						break;
				}
			return value;
		}

		static List<object> ParseExpression(List<string> textExpression)
		{
			List<object> textParse = new List<object>();
			for (int i = 0; i < textExpression.Count; i++)
				if (float.TryParse(textExpression[i], out _))
					textParse.Add(float.Parse(textExpression[i]));
				else
					textParse.Add(TypeOfOperation(textExpression[i]));
			List<object> reversePolishNotation = new List<object>();
			Stack<int> numbers = new Stack<int>();
			Stack<Operations> operations = new Stack<Operations>();
			for (int i = 0; i < textParse.Count; i++)
			{
				if (textParse[i].GetType() == typeof(float) || ((Operations) textParse[i]) == Operations.UnknownVariable)
				{
					reversePolishNotation.Add(textParse[i]);
				}
				else if (!operations.TryPeek(out _) || (Operations) textParse[i] == Operations.OpenBracket || operations.Peek() < (Operations) textParse[i] || (Operations) textParse[i] == Operations.CloseBracket)
				{
					if ((Operations) textParse[i] == Operations.CloseBracket)
						while (operations.Count != 0)
							if (operations.Peek() != Operations.OpenBracket)
								reversePolishNotation.Add(operations.Pop());
							else
								operations.Pop();
					else
						operations.Push((Operations) textParse[i]);
				}
				else
				{
					reversePolishNotation.Add(operations.Pop());
					bool checkAddition = false;
					bool checkMultiplication = false;
					if (operations.Count != 0 && textParse[i].GetType() != typeof(float))
					{
						checkAddition = (operations.Peek() == Operations.Addition || operations.Peek() == Operations.Subtraction)
							&& ((Operations) textParse[i] == Operations.Addition || (Operations) textParse[i] == Operations.Subtraction);
						checkMultiplication = (operations.Peek() == Operations.Multiplication || operations.Peek() == Operations.Division)
							&& ((Operations) textParse[i] == Operations.Multiplication || (Operations) textParse[i] == Operations.Division);
					}
					if (checkAddition || checkMultiplication)
						reversePolishNotation.Add(operations.Pop());
					operations.Push((Operations) textParse[i]);
				}
				if (i == textParse.Count - 1 && operations.TryPeek(out _))
					while (operations.Count != 0)
						reversePolishNotation.Add(operations.Pop());
			}
			return reversePolishNotation;
		}

		static Operations TypeOfOperation(string operation)
		{
			switch (operation)
			{
				case "*":
					return Operations.Multiplication;
				case "/":
					return Operations.Division;
				case "+":
					return Operations.Addition;
				case "-":
					return Operations.Subtraction;
				case "^":
					return Operations.Degree;
				case "(":
					return Operations.OpenBracket;
				case ")":
					return Operations.CloseBracket;
				//case "x":
				//	return Operations.UnknownVariable;
				case "log":
					return Operations.Logarithm;
				case "sin":
					return Operations.Sine;
				case "cos":
					return Operations.Cosine;
				case "tg":
					return Operations.Tangent;
				case "arcsin":
					return Operations.ArcSine;
				case "arccos":
					return Operations.ArcCosine;
				case "arctg":
					return Operations.ArcTangent;
				default:
					return Operations.UnknownVariable;
			}
		}
	}
}
