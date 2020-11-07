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
			Multiplication, Division, Addition, Subtraction
		}

		static void Main()
		{
			string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			string inputFile = @"\input.txt";
			string outputFile = @"\output.txt";
			WorkWithFiles workWithFiles = new WorkWithFiles();
			if (!File.Exists(path + inputFile))
				workWithFiles.WriteTextToFile(path, inputFile, "3.5 + 4 * 2");
			string[] text = workWithFiles.ReadTextFromFile(path, inputFile);
			float value = CalculationValue(text);
			if (float.IsInfinity(value))
				workWithFiles.WriteTextToFile(path, outputFile, "Недопустимая операция");
			else
				workWithFiles.WriteTextToFile(path, outputFile, value.ToString());
		}

		static float CalculationValue(string[] text)
		{
			float value = 0;
			float x = float.Parse(text[0]);
			float y = float.Parse(text[1]);
			for (int i = 2; i < text.Length; i++)
			{
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
			}
			return value;
		}
	}
}
