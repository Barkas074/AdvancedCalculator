using System;

namespace GreetingUser
{
	class GreetingUser
	{
		static void Main(string[] args)
		{
			int birthDay;
			int birthMonth;
			int birthYear;
			DateTime birthDate;
			Console.WriteLine("Привет! Как тебя зовут?");
			string nameUser = Console.ReadLine();
			Console.WriteLine("Какого числа у тебя день рождения?");
			//Проверяем вносимые данные
			try
			{
				birthDay = int.Parse(Console.ReadLine());
				while (birthDay < 1 || birthDay > 31)
				{
					Console.WriteLine("В месяце могут быть числа от 1 до 31!");
					birthDay = int.Parse(Console.ReadLine());
				}
				Console.WriteLine("Месяц?");
				birthMonth = Convert.ToInt32(Console.ReadLine());
				while (birthMonth < 1 || birthMonth > 12)
				{
					Console.WriteLine("В году только 12 месяцев!");
					birthMonth = Convert.ToInt32(Console.ReadLine());
				}
				Console.WriteLine("Год?");
				birthYear = Convert.ToInt32(Console.ReadLine());
				if (birthYear < (DateTime.Today.Year - 122))
				{
					Console.WriteLine("Официально самому старейшему человеку в мире было 122 года. И не говори, что ты Ли Цинъюнь. Укажи правильный год:");
				}
				if (birthYear >= DateTime.Today.Year)
				{
					Console.WriteLine("Так и поверил. Укажи правильный год:");
				}
				while (birthYear < (DateTime.Today.Year - 122) || birthYear >= DateTime.Today.Year)
				{
					birthYear = Convert.ToInt32(Console.ReadLine());
				}
				birthDay = ValidateDate(birthYear, birthMonth, birthDay);
				birthDate = new DateTime(birthYear, birthMonth, birthDay);
			}
			catch (FormatException e)
			{
				Console.WriteLine("А вот так делать не стоило!");
				return;
			}
			catch (ArgumentOutOfRangeException e)
			{
				Console.WriteLine("А вот так делать не стоило!");
				return;
			}
			//Определение возраста
			int age = DateTime.Today.Year - birthYear;
			if (birthDate > DateTime.Today.AddYears(-age))
			{
				age--;
			}
			//Использование слов "лет" и "год" в зависимости от возраста
			string text = "лет";
			int ageLastSymbols = Convert.ToInt32(age.ToString().Substring(age.ToString().Length - 1));
			if (ageLastSymbols == 1)
			{
				text = "год";
			}
			if (ageLastSymbols >= 2 && ageLastSymbols <= 4)
			{
				text = "года";
			}

			Console.WriteLine($"Привет, {nameUser}! Твой возраст - {age} {text}. Приятно познакомиться.");
		}

		static int ValidateDate(int birthYear, int birthMonth, int birthDay)
		{
			//Проверяем високосный ли год, а также число в феврале.
			try
			{
				if (DateTime.IsLeapYear(birthYear))
				{
					if (birthMonth == 2)
					{
						while (birthDay < 1 || birthDay > 29)
						{
							Console.WriteLine("Год високосный! В феврале могут быть числа от 1 до 29!");
							birthDay = int.Parse(Console.ReadLine());
						}
					}
				}
				else
				{
					if (birthMonth == 2)
					{
						while (birthDay < 1 || birthDay > 28)
						{
							Console.WriteLine("В феврале могут быть числа от 1 до 28!");
							birthDay = int.Parse(Console.ReadLine());
						}
					}
				}
				return birthDay;
			}
			catch (FormatException e)
			{
				return 0;
			}
			
		}

		//static int InputDate()
		//{
		//	return 0;
		//}

	}

}
