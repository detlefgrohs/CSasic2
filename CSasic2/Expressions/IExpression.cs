using CSasic2.Values;

namespace CSasic2.Expressions {
    public interface IExpression {
        IValue Evaluate(Interpreter interpreter);
    }
}