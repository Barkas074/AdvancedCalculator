﻿using System;

namespace MovementOfFiguresInChess
{
	class MovementOfFiguresInChess
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Введите № фигуры (1-Король; 2-Ферзь; 3-Ладья; 4-Слон; 5-Конь; 6-Пешка): ");
			string figure = Console.ReadLine();
			int figureToInt;
			while (!int.TryParse(figure, out figureToInt) || figureToInt < 1 || figureToInt > 6)
			{
				Console.WriteLine("Могут быть введены только целые числа от 1 до 6");
				figure = Console.ReadLine();
			}

			Console.WriteLine("Введите начальные координаты фигуры: ");
			string coordinatesFigureBefore = Console.ReadLine().ToUpper();
			while (coordinatesFigureBefore.Length != 2 || coordinatesFigureBefore[0] < 65 || coordinatesFigureBefore[0] > 72 || coordinatesFigureBefore[1] < 49 || coordinatesFigureBefore[1] > 56)
			{
				Console.WriteLine("Координаты могут состоять из 2 символов. Первый - от A до H, второй - от 1 до 8");
				coordinatesFigureBefore = Console.ReadLine().ToUpper();
			}

			Console.WriteLine("Введите конечные координаты фигуры: ");
			string coordinatesFigureAfter = Console.ReadLine().ToUpper();
			while (coordinatesFigureAfter.Length != 2 || coordinatesFigureAfter[0] < 65 || coordinatesFigureAfter[0] > 72 || coordinatesFigureAfter[1] < 49 || coordinatesFigureAfter[1] > 56)
			{
				Console.WriteLine("Координаты могут состоять из 2 символов. Первый - от A до H, второй - от 1 до 8");
				coordinatesFigureAfter = Console.ReadLine().ToUpper();
			}

			if (ChoiceOfFigure(figureToInt, coordinatesFigureBefore, coordinatesFigureAfter))
			{
				Console.WriteLine("Верно");
			}
			else
			{
				Console.WriteLine("Неверно");
			}
		}

		static bool ChoiceOfFigure(int figureToInt, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			int sum = Math.Abs(coordinatesFigureAfter[0] - coordinatesFigureBefore[0]) + Math.Abs(coordinatesFigureAfter[1] - coordinatesFigureBefore[1]);
			switch (figureToInt)
			{
				case 1:
					return CheckingTheKingMovement(sum, coordinatesFigureBefore, coordinatesFigureAfter);
				case 2:
					return CheckingTheQueenMovement(sum, coordinatesFigureBefore, coordinatesFigureAfter);
				case 3:
					return CheckingTheRookMovement(coordinatesFigureBefore, coordinatesFigureAfter);
				case 4:
					return CheckingTheElephantMovement(sum, coordinatesFigureBefore, coordinatesFigureAfter);
				case 5:
					return CheckingTheHorseMovement(sum, coordinatesFigureBefore, coordinatesFigureAfter);
				case 6:
					return CheckingThePawnMovement(coordinatesFigureBefore, coordinatesFigureAfter);
				default:
					return false;
			}
		}


		static bool CheckingTheKingMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			if (sum == 1 || (Math.Abs(coordinatesFigureAfter[0] - coordinatesFigureBefore[0]) == 1 && Math.Abs(coordinatesFigureAfter[1] - coordinatesFigureBefore[1]) == 1))
			{
				return true;
			}
			else
				return false;
		}

		static bool CheckingTheQueenMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			if (sum % 2 == 0 || coordinatesFigureAfter[0] == coordinatesFigureBefore[0] || coordinatesFigureAfter[1] == coordinatesFigureBefore[1])
			{
				return true;
			}
			else
				return false;
		}

		static bool CheckingTheRookMovement(string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			if (coordinatesFigureAfter[0] == coordinatesFigureBefore[0] || coordinatesFigureAfter[1] == coordinatesFigureBefore[1])
			{
				return true;
			}
			else
				return false;
		}

		static bool CheckingTheElephantMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			if (sum % 2 == 0 && (coordinatesFigureAfter[0] - coordinatesFigureBefore[0]) != 0 && (coordinatesFigureAfter[1] - coordinatesFigureBefore[1]) != 0)
			{
				return true;
			}
			else
				return false;
		}

		static bool CheckingTheHorseMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			if (sum == 3)
			{
				return true;
			}
			else
				return false;
		}

		static bool CheckingThePawnMovement(string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			int sum = coordinatesFigureAfter[1] - coordinatesFigureBefore[1];
			if ((coordinatesFigureBefore[0] == coordinatesFigureAfter[0]) && (sum == 1 || (coordinatesFigureBefore[1] == 50 && sum == 2)))
				return true;
			else
				return false;
		}
	}
}
