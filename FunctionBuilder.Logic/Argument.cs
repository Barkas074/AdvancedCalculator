using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
	public class Argument
	{
		public string Name { get; set; } = "x";

		public double Value { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
