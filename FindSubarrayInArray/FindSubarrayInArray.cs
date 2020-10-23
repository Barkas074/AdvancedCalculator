using System;

namespace FindSubarrayInArray
{
	class FindSubarrayInArray
	{
		static void Main(string[] args)
		{
			ReadArray();
		}

		static void ReadArray()
		{
			Console.WriteLine("Используем массив массивов (1) или массив из n-мерных массивов (2)?");
			string arrayType = Console.ReadLine();
			int arrayTypeInt;
			while (!int.TryParse(arrayType, out arrayTypeInt) && arrayTypeInt < 1 && arrayTypeInt > 2)
			{
				Console.WriteLine("Введите целое число");
				arrayType = Console.ReadLine();
			}
			if (arrayTypeInt == 1)
				ProcessArrays(ReadArrayOfArrays(), ReadArrayOfArrays());
			else
				ProcessArrays(ReadArrayOfMultidimensionalArrays(), ReadArrayOfMultidimensionalArrays());
		}

		static int[][,] ReadArrayOfArrays()
		{
			Console.WriteLine("Введите количество массивов");
			int arrayCount = DataInput();
			int[][,] array = new int[arrayCount][,];
			for (int i = 0; i < arrayCount; i++)
			{
				Console.WriteLine("Введите количество строк массива");
				int arrayRow = DataInput();
				Console.WriteLine("Введите количество столбцов массива");
				int arrayColumn = DataInput();
				array[i] = new int[arrayRow, arrayColumn];
				Console.WriteLine("Введите элементы массива");
				for (int row = 0; row < arrayRow; row++)
				{
					for (int column = 0; column < arrayColumn; column++)
					{
						array[i][row, column] = DataInput();
					}
				}
			}
			return array;
		}

		static int[,] ReadArrayOfMultidimensionalArrays()
		{
			
			Console.WriteLine("Введите количество строк массива");
			int arrayRow = DataInput();
			Console.WriteLine("Введите количество столбцов массива");
			int arrayColumn = DataInput();
			int[,] array = new int[arrayRow, arrayColumn];
			Console.WriteLine("Введите элементы массива");
			for (int i = 0; i < arrayRow; i++)
			{
				for (int j = 0; j < arrayColumn; j++)
				{
					array[i, j] = DataInput();
				}
			}
			return array;
		}

		static void ProcessArrays(int[][,] arrayOld, int[][,] arrayNew)
		{
			int arrayNewCountArrays = 0;
			int arrayNewRow = 0;
			int arrayNewColumn = 0;
			int numberOfTrueNumbers = 0;
			int trueArray = 0;
			for (int i = 0; i < arrayOld.Length; i++)
			{
				for (int row = 0; row < arrayOld[i].GetLength(0); row++)
				{
					for (int column = 0; column < arrayOld[i].GetLength(1); column++)
					{
						if (arrayOld[i][row, column] == arrayNew[arrayNewCountArrays][arrayNewRow, arrayNewColumn])
						{
							arrayNewColumn++;
							if (arrayNewColumn == arrayNew[arrayNewCountArrays].GetLength(1))
							{
								if (arrayNewRow != arrayNew[arrayNewCountArrays].GetLength(0) - 1)
									arrayNewRow++;
								arrayNewColumn = 0;
								if (numberOfTrueNumbers != 0)
									trueArray++;
							}
							numberOfTrueNumbers++;
						}
						else
						{
							numberOfTrueNumbers = 0;
						}
					}	
				}
				arrayNewRow = 0;
				arrayNewColumn = 0;
				numberOfTrueNumbers = 0;
				if (trueArray != 0 && arrayNewCountArrays != arrayNew.Length - 1)
					arrayNewCountArrays++;
			}
			Console.WriteLine(trueArray == arrayNew.Length ? "TRUE" : "FALSE");
		}

		static void ProcessArrays(int[,] arrayOld, int[,] arrayNew)
		{
			int arrayNewRow = 0;
			int arrayNewColumn = 0;
			int numberOfTrueNumbers = 0;
			int trueArray = 0;
			for (int i = 0; i < arrayOld.GetLength(0); i++)
			{
				for (int j = 0; j < arrayOld.GetLength(1); j++)
				{
					if (arrayOld[i, j] == arrayNew[arrayNewRow, arrayNewColumn])
					{
						arrayNewColumn++;
						if (arrayNewColumn == arrayNew.GetLength(1))
						{
							if (arrayNewRow != arrayNew.GetLength(0) - 1)
								arrayNewRow++;
							arrayNewColumn = 0;
							if (numberOfTrueNumbers != 0)
								trueArray++;
						}
						numberOfTrueNumbers++;
					}
					else
					{
						numberOfTrueNumbers = 0;
					}
				}
			}
			Console.WriteLine(trueArray == arrayNew.GetLength(0) ? "TRUE" : "FALSE");
		}

		static int DataInput()
		{
			string text;
			int textInt;
			do
				text = Console.ReadLine();
			while (!int.TryParse(text, out textInt));
			return textInt;
		}
	}
}
