using CSasic2.Expressions;

namespace CSasic2.Values {
    public interface IValue : IExpression {
        string ToStr();
        double ToNum();
    }
}