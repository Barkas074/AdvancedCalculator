using System;
using System.Collections.Generic;
using System.IO;
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

		public string[] ReadTextFromFile(string path, string nameFile)
		{
			using FileStream fStream = File.OpenRead(path + nameFile);
			byte[] array = new byte[fStream.Length];
			fStream.Read(array, 0, array.Length);
			string textFromFile = Encoding.Default.GetString(array);
			Console.WriteLine("Чтение из файла прошло успешно.");
			return ProcessingString(textFromFile);
		}

		string[] ProcessingString(string textFromFile)
		{
			textFromFile = textFromFile.Replace("\t", string.Empty);
			string[] text = textFromFile.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			CheckingText(text);
			return text;
		}

		void CheckingText(string[] text)
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
					Console.WriteLine("Выражение записано неверно.");
					//if (ReadAnswer())
						
					//else
						Environment.Exit(0);
				}
			}
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
