using CSasic2.Values;
using System;

namespace CSasic2.Expressions {
    public class OperatorExpression : IExpression {
        private IExpression _left;
        private char _op;
        private IExpression _right;
        public OperatorExpression(IExpression left, char op, IExpression right) {
            _left = left;
            _op = op;
            _right = right;
        }
        public IValue Evaluate(Interpreter interpreter) {
            var leftVal = _left.Evaluate(interpreter);
            var rightVal = _right.Evaluate(interpreter);

            switch (_op) {
                case '=':
                    return leftVal is NumberValue ?
                        new NumberValue((leftVal.ToNum() == rightVal.ToNum()) ? 1 : 0) :
                        new NumberValue((leftVal.ToStr().Equals(rightVal.ToStr())) ? 1 : 0);
                case '+':
                    return leftVal is NumberValue ?
                        (IValue)new NumberValue(leftVal.ToNum() + rightVal.ToNum()) :
                        (IValue)new StringValue(leftVal.ToStr() + rightVal.ToStr());
                case '-':
                    return new NumberValue(leftVal.ToNum() - rightVal.ToNum());
                case '*':
                    return new NumberValue(leftVal.ToNum() * rightVal.ToNum());
                case '/':
                    return new NumberValue(leftVal.ToNum() / rightVal.ToNum());
                case '<':
                    return leftVal is NumberValue ?
                        new NumberValue((leftVal.ToNum() < rightVal.ToNum()) ? 1 : 0) :
                        new NumberValue((leftVal.ToStr().CompareTo(rightVal.ToStr()) < 0) ? 1 : 0);
                case '>':
                    return leftVal is NumberValue ?
                        new NumberValue((leftVal.ToNum() > rightVal.ToNum()) ? 1 : 0) :
                        new NumberValue((leftVal.ToStr().CompareTo(rightVal.ToStr()) > 0) ? 1 : 0);
            }
            throw new NotImplementedException();
        }
    }
}
