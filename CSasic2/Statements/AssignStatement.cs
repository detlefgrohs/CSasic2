using CSasic2.Expressions;

namespace CSasic2.Statements {
    public class AssignStatement : IStatement {
        private string _name;
        private IExpression _value;
        public AssignStatement(string name, IExpression value) {
            _name = name;
            _value = value;
        }
        public void Execute(Interpreter interpreter) {
            interpreter.SetVariable(_name, _value.Evaluate(interpreter));
        }
    }
}
