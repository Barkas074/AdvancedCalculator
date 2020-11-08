using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Calculator
{
	class WorkWithFiles
	{
		public void WriteTextToFile(string path, string nameFile, string text)
		{
			using FileStream fStream = new FileStream(path + nameFile, FileMode.Create);
			byte[] array = Encoding.Default.GetBytes(text);
			fStream.Write(array, 0, array.Length);
			Console.WriteLine("Текст записан в файл.");
		}

		public List<string> ReadTextFromFile(string path, string nameFile)
		{
			using FileStream fStream = File.OpenRead(path + nameFile);
			byte[] array = new byte[fStream.Length];
			fStream.Read(array, 0, array.Length);
			string textFromFile = Encoding.Default.GetString(array);
			Console.WriteLine("Чтение из файла прошло успешно.");
			return ProcessingString(textFromFile);
		}

		List<string> ProcessingString(string textFromFile)
		{
			textFromFile = textFromFile.Replace("\t", string.Empty);
			string[] text = textFromFile.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			List<string> dictionaryOperations = new List<string>() { "*", "/", "+", "-", "^", "(", ")", "x", "log", "sin", "cos", "arcsin", "arccos", "tg", "arctg" };
			List<string> textExpression = new List<string>();
			string temp = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				if (float.TryParse(text[i], out _) || dictionaryOperations.Contains(text[i]))
					textExpression.Add(text[i]);
				else
					for (int j = 0; j < text[i].Length; j++)
					{
						if (dictionaryOperations.Contains(text[i][j].ToString()) && temp != string.Empty)
						{
							Console.WriteLine("Выражение неверное!");
							Environment.Exit(1);
						}
						temp += text[i][j];
						if (float.TryParse(temp, out _) || dictionaryOperations.Contains(temp))
						{
							textExpression.Add(temp);
							temp = string.Empty;
						}
					}
					temp = string.Empty;
			}
			return textExpression;
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
