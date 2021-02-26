namespace FunctionBuilder.Logic
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	public class WorkWithFiles
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

		private List<string> ProcessingString(string textFromFile)
		{
			textFromFile = textFromFile.Replace("\t", string.Empty);
			string[] text = textFromFile.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			List<string> dictionaryOperations = new List<string>() { "*", "/", "+", "-", "^", "(", ")", "x", "log", "sin", "cos", "arcsin", "arccos", "tg", "arctg", "!" };
			List<string> textExpression = new List<string>();
			string temp = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				if (float.TryParse(text[i], out _) || dictionaryOperations.Contains(text[i]))
					textExpression.Add(text[i]);
				else
					for (int j = 0; j < text[i].Length; j++)
					{
						temp += text[i][j];
						if (j == text[i].Length - 1)
						{
							if (float.TryParse(temp, out _) || dictionaryOperations.Contains(temp))
							{
								textExpression.Add(temp);
								temp = string.Empty;
							}
						}
						else if (float.TryParse(temp, out _) && text[i][j + 1] >= '0' && text[i][j + 1] <= '9')
						{

						}
						else if (float.TryParse(temp, out _) || dictionaryOperations.Contains(temp))
						{
							textExpression.Add(temp);
							temp = string.Empty;
						}
					}
				temp = string.Empty;
			}
			return textExpression;
		}
	}
}
