namespace CSasic2.Statements {
    public class GotoStatement : IStatement {
        private string _label;
        public GotoStatement(string label) {
            _label = label;
        }
        public void Execute(Interpreter interpreter) {
            if (interpreter.Labels.ContainsKey(_label))
                interpreter.CurrentStatement = interpreter.Labels[_label];
        }
    }
}