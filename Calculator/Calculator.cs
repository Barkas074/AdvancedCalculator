using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Calculator
{
	class Calculator
	{
		static void Main()
		{
			WorkWithFiles();
		}

		static void WriteTextToFile(string path, string nameFile, string text)
		{
			using FileStream fStream = new FileStream(path + nameFile, FileMode.Create);
			byte[] array = Encoding.Default.GetBytes(text);
			fStream.Write(array, 0, array.Length);
			Console.WriteLine("Текст записан в файл.");
		}

		static string ReadTextFromFile(string path, string nameFile)
		{
			using FileStream fStream = File.OpenRead(path + nameFile);
			byte[] array = new byte[fStream.Length];
			fStream.Read(array, 0, array.Length);
			string textFromFile = Encoding.Default.GetString(array);
			Console.WriteLine("Чтение из файла прошло успешно.");
			return textFromFile;
		}

		public static void WorkWithFiles()
		{
			string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			string inputFile = @"\input.txt";
			string outputFile = @"\output.txt";
			float value = 0;
			if (!File.Exists(path + inputFile))
				WriteTextToFile(path, inputFile, "5 10 +");
			value = ProcessingString(ReadTextFromFile(path, inputFile));
			if (float.IsInfinity(value))
				WriteTextToFile(path, outputFile, "Недопустимая операция");
			else
				WriteTextToFile(path, outputFile, value.ToString());
		}

		static float ProcessingString(string textFromFile)
		{
			textFromFile = textFromFile.Replace("\t", string.Empty);
			string[] text = textFromFile.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			CheckingText(text);
			return CalculationValue(text);
		}

		static void CheckingText(string[] text)
		{
			List<char> dictionarySymbols = new List<char>() { '*', '/', '+', '-' };
			bool correctText = true;
			for (int i = 0; i < text.Length; i++)
			{
				if (i < 2)
					correctText = float.TryParse(text[i], out float x);
				else
					correctText = dictionarySymbols.Contains(char.Parse(text[i]));
				if (!correctText)
				{
					Console.WriteLine("Выражение записано неверно. Повторить программу? y/n");
					if (ReadAnswer())
						WorkWithFiles();
					else
						Environment.Exit(0);
				}
			}
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

		static bool ReadAnswer()
		{
			string answer;
			do
			{
				answer = Console.ReadLine();
				if (!IsCorrectAnswer(answer))
					Console.WriteLine("Введите y(es) или n(o).");
			} while (!IsCorrectAnswer(answer));

			return answer == "y" || answer == "yes";
		}

		static bool IsCorrectAnswer(string answer)
		{
			return answer == "y" || answer == "yes" || answer == "n" || answer == "no";
		}
	}
}
