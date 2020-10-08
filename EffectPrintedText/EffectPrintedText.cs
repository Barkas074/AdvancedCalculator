using System;
using System.Threading;

namespace EffectPrintedText
{
	class EffectPrintedText
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Введите любую строку:");
			string[] text = ReadText();
			Console.WriteLine("Введите время задержки:");
			string delay = Console.ReadLine();
			int intDelay = int.Parse(delay);
			Console.Clear();

			PrintText(text, intDelay);
			Console.WriteLine("\nПовторить?");
			string repeat = Console.ReadLine();
			if (repeat.ToLower() == "да")
				PrintText(text, intDelay);
		}

		static void PrintText(string[] allText, int delay)
		{
			ConsoleKeyInfo keyInfo;
			foreach (string text in allText)
			{
				for (int i = 0; i < text.Length; i++)
				{
					Console.Write(text[i]);
					Thread.Sleep(delay);
				}
				keyInfo = Console.ReadKey();
				while (keyInfo.Key != ConsoleKey.Enter)
					keyInfo = Console.ReadKey();
				Console.Clear();
			}
		}

		static string[] ReadText()
		{
			string[] allText;
			string[] allTextOld;
			string text;
			int count = 0;
			allText = new string[count];
			do
			{
				text = Console.ReadLine();
				if (text != "")
				{
					count++;
					allTextOld = new string[count];
					for (int i = 0; i < allTextOld.Length - 1; i++)
						allTextOld[i] = allText[i];
					allTextOld[count - 1] = text;
					allText = allTextOld;
				}
			}
			while (text != "");
			return allText;
		}
	}
}
