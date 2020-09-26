using System;

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
			string coordinatesFigureBefore = ReadString(); 
			
			Console.WriteLine("Введите конечные координаты фигуры: ");
			string coordinatesFigureAfter = ReadString();

			if (ChoiceOfFigure(figureToInt, coordinatesFigureBefore, coordinatesFigureAfter))
				Console.WriteLine("Верно");
			else
				Console.WriteLine("Неверно");
		}


		static string ReadString()
		{
			string line = Console.ReadLine().ToUpper();
			while (line.Length != 2 || line[0] < 65 || line[0] > 72 || line[1] < 49 || line[1] > 56)
			{
				Console.WriteLine("Координаты могут состоять из 2 символов. Первый - от A до H, второй - от 1 до 8");
				line = Console.ReadLine().ToUpper();
			}
			return line;
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
			return (sum == 1 || (Math.Abs(coordinatesFigureAfter[0] - coordinatesFigureBefore[0]) == 1 && Math.Abs(coordinatesFigureAfter[1] - coordinatesFigureBefore[1]) == 1));
		}

		static bool CheckingTheQueenMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			return (sum % 2 == 0 || coordinatesFigureAfter[0] == coordinatesFigureBefore[0] || coordinatesFigureAfter[1] == coordinatesFigureBefore[1]);
		}

		static bool CheckingTheRookMovement(string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			return (coordinatesFigureAfter[0] == coordinatesFigureBefore[0] || coordinatesFigureAfter[1] == coordinatesFigureBefore[1]);
		}

		static bool CheckingTheElephantMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			return (sum % 2 == 0 && (coordinatesFigureAfter[0] - coordinatesFigureBefore[0]) != 0 && (coordinatesFigureAfter[1] - coordinatesFigureBefore[1]) != 0);
		}

		static bool CheckingTheHorseMovement(int sum, string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			return (sum == 3 && (coordinatesFigureAfter[0] - coordinatesFigureBefore[0]) != 0 && (coordinatesFigureAfter[1] - coordinatesFigureBefore[1]) != 0);
		}

		static bool CheckingThePawnMovement(string coordinatesFigureBefore, string coordinatesFigureAfter)
		{
			int sum = coordinatesFigureAfter[1] - coordinatesFigureBefore[1];
			return ((coordinatesFigureBefore[0] == coordinatesFigureAfter[0]) && (sum == 1 || (coordinatesFigureBefore[1] == 50 && sum == 2)));
		}
	}
}
