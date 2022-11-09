using CSasic2.Expressions;

namespace CSasic2.Statements {
    public class IfThenStatement : IStatement {
        private IExpression _condition;
        private string _label;
        public IfThenStatement(IExpression condition, string label) {
            _condition = condition;
            _label = label;
        }
        public void Execute(Interpreter interpreter) {
            if (interpreter.Labels.ContainsKey(_label)) {
                double value = _condition.Evaluate(interpreter).ToNum();
                if (value != 0) {
                    interpreter.CurrentStatement = interpreter.Labels[_label];
                }
            }
        }
    }
}