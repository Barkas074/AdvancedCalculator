using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
	public class ReversePolishNotation
	{
		Stack<Parenthessis> stackParenthessis = new Stack<Parenthessis>();
		public List<object> Parse(List<string> textExpression)
		{
			List<object> expression = ParseExpression(textExpression);
			List<object> rpn = new List<object>();
			ToReversePolishNotation(expression, 0, ref rpn);
			return rpn;
		}

		private int ToReversePolishNotation(List<object> expression, int i, ref List<object> rpn)
		{
			List<object> rpnBlank = new List<object>();
			Stack<Operations> operations = new Stack<Operations>();
			for (; i < expression.Count; i++)
			{
				object value = expression[i];
				if (value is double number)
				{
					rpnBlank.Add(number);
				}
				else if (value is Argument arg)
				{
					rpnBlank.Add(arg);
				}
				else if (value is Operations op)
				{
					if (operations.Count == 0)
					{
						operations.Push(op);
					}
					else if (operations.Count != 0 && operations.Peek().ToPriority() < op.ToPriority())
					{
						operations.Push(op);
					}
					else
					{
						rpnBlank.Add(operations.Pop());
						if (operations.Count != 0 
							&& (operations.Peek().ToPriority() == op.ToPriority() 
							|| operations.Peek().ToPriority() > op.ToPriority()))
						{
							rpnBlank.Add(operations.Pop());
						}
						operations.Push(op);
					}
				}
				else if (value is Parenthessis parenthessis)
				{
					if (parenthessis.IsOpening)
					{
						stackParenthessis.Push(parenthessis);
						List<object> subRPN = new List<object>();
						i = ToReversePolishNotation(expression, i + 1, ref subRPN);
						rpnBlank.AddRange(subRPN);
					}
					else
					{
						while (operations.Count != 0)
						{
							rpnBlank.Add(operations.Pop());
						}
						if (stackParenthessis.Count != 0 && stackParenthessis.Peek().IsOpening)
						{
							stackParenthessis.Pop();
							break;
						}
						throw new Exception("Неправильная вложенность скобок");
					}
				}
				if (i == expression.Count - 1 && operations.Count != 0)
					while (operations.Count != 0)
						rpnBlank.Add(operations.Pop());
			}
			rpn.AddRange(rpnBlank);
			return i;
		}

		public List<object> ParseExpression(List<string> textExpression)
		{
			List<object> textParse = new List<object>();
			for (int i = 0; i < textExpression.Count; i++)
				if (double.TryParse(textExpression[i], out _))
					textParse.Add(double.Parse(textExpression[i]));
				else if (textExpression[i] == "(" || textExpression[i] == ")")
					textParse.Add(new Parenthessis(textExpression[i]));
				else
					textParse.Add(TypeOfOperation(textExpression[i]));
			return textParse;
		}
		private object TypeOfOperation(string operation)
		{
			return operation switch
			{
				"*" => new Multiplication(),
				"/" => new Division(),
				"+" => new Addition(),
				"-" => new Subtraction(),
				"^" => new Degree(),
				"!" => new Factorial(),
				"log" => new Logarithm(),
				"sin" => new Sine(),
				"cos" => new Cosine(),
				"tg" => new Tangent(),
				"arcsin" => new ArcSine(),
				"arccos" => new ArcCosine(),
				"arctg" => new ArcTangent(),
				"x" => new Argument(),
				_ => throw new Exception("Неизвестная операция"),
			};
		}
	}
}
