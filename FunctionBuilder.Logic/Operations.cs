namespace FunctionBuilder.Logic
{
	using System;

	public abstract class Operations
	{
		public abstract string Name { get; }
		public abstract int Priority { get; }
		public abstract byte OperandCount { get; }

		protected abstract double CalculationOfOperation(double numbersTwo, double numbersOne);

		public override string ToString()
		{
			return Name;
		}

		public int ToPriority()
		{
			return Priority;
		}

		public double Calculate(double numbersTwo, double numbersOne) => CalculationOfOperation(numbersTwo, numbersOne);
	}
	

	public class Addition : Operations
	{
		public override string Name => "+";
		public override int Priority => 1;
		public override byte OperandCount => 2;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => numbersOne + numbersTwo;
	}

	public class Subtraction : Operations
	{
		public override string Name => "-";
		public override int Priority => 1;
		public override byte OperandCount => 2;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => numbersOne - numbersTwo;
	}

	public class Multiplication : Operations
	{
		public override string Name => "*";
		public override int Priority => 2;
		public override byte OperandCount => 2;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => numbersOne * numbersTwo;
	}

	public class Division : Operations
	{
		public override string Name => "/";
		public override int Priority => 2;
		public override byte OperandCount => 2;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => numbersOne / numbersTwo;
	}

	public class Degree : Operations
	{
		public override string Name => "^";
		public override int Priority => 3;
		public override byte OperandCount => 2;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Pow(numbersOne, numbersTwo);
	}

	public class Logarithm : Operations
	{
		public override string Name => "log";
		public override int Priority => 4;
		public override byte OperandCount => 2;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Log10(numbersOne);
	}

	public class Sine : Operations
	{
		public override string Name => "sin";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Sin(numbersOne);
	}

	public class Cosine : Operations
	{
		public override string Name => "cos";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Cos(numbersOne);
	}

	public class Tangent : Operations
	{
		public override string Name => "tg";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Tan(numbersOne);
	}

	public class ArcSine : Operations
	{
		public override string Name => "arcsin";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Asin(numbersOne);
	}

	public class ArcCosine : Operations
	{
		public override string Name => "arccos";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Acos(numbersOne);
	}

	public class ArcTangent : Operations
	{
		public override string Name => "arctg";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => Math.Atan(numbersOne);
	}

	public class Factorial : Operations
	{
		public override string Name => "!";
		public override int Priority => 4;
		public override byte OperandCount => 1;

		protected override double CalculationOfOperation(double numbersTwo, double numbersOne) => ComputeFactorial(numbersOne);

		private static double ComputeFactorial(double n)
		{
			if (n == 1)
				return 1;
			return ComputeFactorial(n - 1) * n;
		}
	}
}
