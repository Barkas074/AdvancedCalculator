namespace FunctionBuilder.Logic
{
	using System;
	using System.Collections.Generic;

	public class TableOfFunctionValues : Operations
	{
		private double CalculationValue(List<object> parseExpression, double x)
		{
			double value = 0;
			Stack<double> numbers = new Stack<double>();
			for (int i = 0; i < parseExpression.Count; i++)
			{
				if (parseExpression[i].GetType() == typeof(double))
					numbers.Push((double) parseExpression[i]);
				else if ((ListOperations) parseExpression[i] == ListOperations.UnknownVariable)
					numbers.Push(x);
				else
					if ((int) parseExpression[i] > 7)
				{
					value = CalculationOfOperation((ListOperations) parseExpression[i], 0, numbers.Pop());
					numbers.Push(value);
				}
				else
				{
					value = CalculationOfOperation((ListOperations) parseExpression[i], numbers.Pop(), numbers.Pop());
					numbers.Push(value);
				}
			}
			return value;
		}

		public string CreatingTable(double[] rangeXSplit, double step, List<object> parseExpression)
		{
			int maxLength = 0;
			for (double x = rangeXSplit[0]; x <= rangeXSplit[1]; x += step)
			{
				int yLength = Math.Round(CalculationValue(parseExpression, x)).ToString().Length;
				if (yLength > maxLength)
					maxLength = yLength;
			}

			string frameTableX = new string('-', maxLength + 4);
			string space = IsNumberSpace(frameTableX, 1, false);
			string spaceStart = IsNumberSpace(frameTableX, 1, true);

			string finalText = string.Empty;
			finalText += $"|{frameTableX}|{frameTableX}|\n";
			finalText += $"|{spaceStart}x{space}|{spaceStart}y{space}|\n";
			finalText += $"|{frameTableX}|{frameTableX}|\n";

			for (double x = rangeXSplit[0]; x <= rangeXSplit[1]; x += step)
			{
				double y = CalculationValue(parseExpression, x);
				finalText += $"|{IsNumberSpace(frameTableX, Math.Round(x, 2), true)}" +
					$"{Math.Round(x, 2)}{IsNumberSpace(frameTableX, Math.Round(x, 2), false)}|" +
					$"{IsNumberSpace(frameTableX, Math.Round(y, 2), true)}" +
					$"{Math.Round(y, 2)}{IsNumberSpace(frameTableX, Math.Round(y, 2), false)}|\n";
			}

			finalText += $"|{frameTableX}|{frameTableX}|\n";
			return finalText;
		}

		private string IsNumberSpace(string length, double var, bool deleteSymbol)
		{
			string space = new string(' ', (int) Math.Ceiling((length.Length - var.ToString().Length) / (double) 2));
			if (deleteSymbol && (length.Length - var.ToString().Length) % 2 != 0)
				space = space.Remove(0, 1);
			return space;
		}
	}
}
