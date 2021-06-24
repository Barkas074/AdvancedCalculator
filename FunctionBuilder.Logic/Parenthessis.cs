using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
	public class Parenthessis
	{
		public bool IsOpening { get; private set; }

		public Parenthessis(string parenthesis)
		{
			IsOpening = parenthesis == "(";
		}

		public override string ToString()
		{
			return IsOpening ? "(" : ")";
		}
	}
}
