namespace FunctionBuilder.Logic
{
	using System;

	public class Operations
	{
		protected enum ListOperations
		{
			UnknownVariable = 0, OpenBracket = 1, CloseBracket = 2, Addition = 3, Subtraction = 4, Multiplication = 5, Division = 6, Degree = 7, Logarithm, Sine, Cosine, Tangent, ArcSine, ArcCosine, ArcTangent, Factorial
		}

		protected static ListOperations TypeOfOperation(string operation)
		{
			switch (operation)
			{
				case "*":
					return ListOperations.Multiplication;
				case "/":
					return ListOperations.Division;
				case "+":
					return ListOperations.Addition;
				case "-":
					return ListOperations.Subtraction;
				case "^":
					return ListOperations.Degree;
				case "(":
					return ListOperations.OpenBracket;
				case ")":
					return ListOperations.CloseBracket;
				case "!":
					return ListOperations.Factorial;
				case "log":
					return ListOperations.Logarithm;
				case "sin":
					return ListOperations.Sine;
				case "cos":
					return ListOperations.Cosine;
				case "tg":
					return ListOperations.Tangent;
				case "arcsin":
					return ListOperations.ArcSine;
				case "arccos":
					return ListOperations.ArcCosine;
				case "arctg":
					return ListOperations.ArcTangent;
				default:
					return ListOperations.UnknownVariable;
			}
		}

		protected double CalculationOfOperation(ListOperations operation, double numbersTwo, double numbersOne)
		{
			switch (operation)
			{
				case ListOperations.Multiplication:
					return numbersOne * numbersTwo;
				case ListOperations.Division:
					return numbersOne / numbersTwo;
				case ListOperations.Addition:
					return numbersOne + numbersTwo;
				case ListOperations.Subtraction:
					return numbersOne - numbersTwo;
				case ListOperations.Degree:
					return Math.Pow(numbersOne, numbersTwo);
				case ListOperations.Logarithm:
					return Math.Log10(numbersOne);
				case ListOperations.Sine:
					return Math.Sin(numbersOne);
				case ListOperations.Cosine:
					return Math.Cos(numbersOne);
				case ListOperations.Tangent:
					return Math.Tan(numbersOne);
				case ListOperations.ArcSine:
					return Math.Asin(numbersOne);
				case ListOperations.ArcCosine:
					return Math.Acos(numbersOne);
				case ListOperations.ArcTangent:
					return Math.Atan(numbersOne);
				case ListOperations.Factorial:
					return ComputeFactorial(numbersOne);
				default:
					return 0;
			}
		}

		private static double ComputeFactorial(double n)
		{
			if (n == 1)
				return 1;
			return ComputeFactorial(n - 1) * n;
		}

	}
}
