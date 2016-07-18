using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Operators;
using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator
{
    public class BurdaCalculator
    {
        public HashSet<IUnaryOperator> UnaryOperators { get; set; }
        public HashSet<IBinaryOperator> BinaryOperators { get; set; }

        private Stack<string> _operatorsStack;
        private Stack<decimal> _operandsStack;

        private string _token;
        private int _tokenPos;
        private string _expression;

        private string _endToken = ";";
        private string _pleft = "(";
        private string _pright = ")";
        private string _sentinel = "#";

        public BurdaCalculator(HashSet<IUnaryOperator> unaryOperators, HashSet<IBinaryOperator> binaryOperators)
        {
            UnaryOperators = unaryOperators;
            BinaryOperators = binaryOperators;
            Reset();
        }

        private void Reset()
        {
            _tokenPos = -1;
            _token = " ";
            _operandsStack = new Stack<decimal>();
            _operatorsStack = new Stack<string>();
        }

        private void Reset(string expression)
        {
            Reset();
            _expression = expression;
        }

        /// <summary>
        /// Calculates (and parses) mathematical expression
        /// </summary>
        /// <param name="expression">The math expression</param>
        /// <returns>Returns calculated result.</returns>
        public decimal Calculate(string expression)
        {
            Reset(expression);
            if (Normalize(ref _expression))
            {
                return ParseAndCalculate();
            }

            throw new Exception("Blank input expression");
        }

        /// <summary>
        /// Calculates binary operation
        /// </summary>
        /// <param name="op">Binary operator's notation (token)</param>
        /// <param name="operand1">First operand</param>
        /// <param name="operand2">Second operand</param>
        private void Calculate(string op, decimal operand1, decimal operand2)
        {
            var binaryOperator = BinaryOperators.First(b => b.OperatorNotationOutput.Equals(op));
            var result = binaryOperator.Calculate(operand1, operand2);

            _operandsStack.Push(result);
        }

        /// <summary>
        /// Calculates unary operation (eg. function)
        /// </summary>
        /// <param name="op">Unary operator's notation (token)</param>
        /// <param name="operand1">Operand</param>
        private void Calculate(string op, decimal operand1)
        {
            var unaryOperator = UnaryOperators.First(b => b.OperatorNotationOutput.Equals(op));
            var result = unaryOperator.Calculate(operand1);

            _operandsStack.Push(result);
        }

        private decimal ParseAndCalculate()
        {
            ParseBinary();
            Expect(_endToken);
            return _operandsStack.Peek();
        }

        /// <summary>
        /// Parse binary operations
        /// </summary>
        private void ParseBinary()
        {
            ParsePrimary();

            while (OperatorsHelper.IsTokenBinary(_token, BinaryOperators))
            {
                PushOperator(_token);
                NextToken();
                ParsePrimary();
            }

            while (_operatorsStack.Count > 0 && _operatorsStack.Peek() != _sentinel)
                PopOperator();
        }

        /// <summary>
        /// Parse primary tokens: digits, functions, parentheses etc
        /// </summary>
        private void ParsePrimary()
        {
            if (OperatorsHelper.IsTokenDigit(_token)) // numbers
            {
                ParseDigit();
            }
            else if (OperatorsHelper.IsTokenName(_token)) // function
            {
                ParseName();
            }
            else if (OperatorsHelper.IsTokenUnary(_token, UnaryOperators)) // unary operator (eg unary minus)
            {
                PushOperator(OperatorsHelper.ConvertOperator(GetOperatorByInputNotation(_token)));
                NextToken();
                ParsePrimary();
            }
            else if (_token == _pleft) // parentheses
            {
                NextToken();
                _operatorsStack.Push(_sentinel); // add sentinel to operators stack
                ParseBinary();
                Expect(_pright);
                _operatorsStack.Pop(); // pop sentinel from the stack

                TryRightSideOperator();
            }
            else
                throw new Exception("Syntax error");
        }

        private void ParseDigit()
        {
            var tmpNumber = new StringBuilder();

            while (OperatorsHelper.IsTokenDigit(_token))
            {
                CollectToken(ref tmpNumber);
            }

            _operandsStack.Push(decimal.Parse(tmpNumber.ToString(), System.Globalization.CultureInfo.InvariantCulture));
            TryRightSideOperator();
        }


        private void Expect(params string[] expectedTokens)
        {
            if (expectedTokens.Any(t => _token == t))
            {
                NextToken();
                return;
            }

            throw new Exception("Syntax error: " + expectedTokens[0] + "  expected");
        }


        /// <summary>
        /// Turn name into a function
        /// </summary>
        private void ParseName()
        {
            var tmpName = new StringBuilder();

            while (OperatorsHelper.IsTokenName(_token))
            {
                CollectToken(ref tmpName);
            }

            var name = tmpName.ToString();

            if (OperatorsHelper.IsTokenFunction(name, UnaryOperators)) // Execute operand in case of driver's function (Sin, Cos e.t.c)
            {
                PushOperator(name);
                ParsePrimary();
            }
        }

        /// <summary>
        /// Check for right-side operators (eg factorial) 
        /// </summary>
        private void TryRightSideOperator()
        {
            var rightSideOperator = UnaryOperators.FirstOrDefault(uo => uo.UnaryOperatorType == UnaryOperatorType.RightSide && uo.OperatorNotationInput == _token);
            if (rightSideOperator != null)
            {
                PushOperator(rightSideOperator.OperatorNotationInput);
                NextToken();
            }
        }

        /// <summary>
        /// Read token character by character
        /// </summary>
        /// <param name="sb">Temporary buffer</param>
        private void CollectToken(ref StringBuilder sb)
        {
            sb.Append(_token);
            NextToken();
        }

        private void PushOperator(string op)
        {
            while (_operatorsStack.Count > 0 && _operatorsStack.Peek() != _sentinel && OperatorsHelper.Compare(GetOperatorByOutputNotation(_operatorsStack.Peek()),
                       GetOperatorByOutputNotation(op)) > 0)
            {
                PopOperator();
            }

            _operatorsStack.Push(op);
        }

        private void PopOperator()
        {
            if (OperatorsHelper.IsTokenBinary(_operatorsStack.Peek(), BinaryOperators))
            {
                var operand2 = _operandsStack.Pop();
                var operand1 = _operandsStack.Pop();
                Calculate(_operatorsStack.Pop(), operand1, operand2);
            }
            else // unary operator
            {
                var operand1 = _operandsStack.Pop();
                Calculate(_operatorsStack.Pop(), operand1);
            }
        }

        /// <summary>
        /// Gets operator by input notation. This is useful when you need operator for any task related to parsing expression
        /// </summary>
        /// <param name="op">Token name (operator's notation)</param>
        /// <returns>Returns related IBaseOperator.</returns>
        private IBaseOperator GetOperatorByInputNotation(string op)
        {
            var oper = BinaryOperators.FirstOrDefault(o => o.OperatorNotationInput.Equals(op));
            if (oper != null) return oper;

            return UnaryOperators.FirstOrDefault(o => o.OperatorNotationInput.Equals(op));
        }

        /// <summary>
        /// Gets operator by output notation. This is useful when you need operator for any task non-related to parsing expression
        /// </summary>
        /// <param name="op">Token name (operator's notation)</param>
        /// <returns>Returns related IBaseOperator.</returns>
        private IBaseOperator GetOperatorByOutputNotation(string op)
        {
            var oper = BinaryOperators.FirstOrDefault(o => o.OperatorNotationOutput.Equals(op));
            if (oper != null) return oper;

            return UnaryOperators.FirstOrDefault(o => o.OperatorNotationOutput.Equals(op));
        }

        /// <summary>
        /// Gets next token from the expression
        /// </summary>
        private void NextToken()
        {
            if (_token != _endToken)
            {
                _token = _expression[++_tokenPos].ToString();
            }
        }

        /// <summary>
        /// Normalizes expression.
        /// </summary>
        /// <param name="s">Expression string</param>
        /// <returns>Returns true, if expression is suitable for calculating.</returns>
        private bool Normalize(ref string s)
        {
            s = s.Replace(" ", "").Replace("\t", " ").ToLower() + _endToken;

            if (s.Length >= 2)
            {
                NextToken();
                return true;
            }

            return false;
        }
    }
}
