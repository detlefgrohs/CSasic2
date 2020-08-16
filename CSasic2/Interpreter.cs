using CSasic2.Statements;
using CSasic2.Values;
using System.Collections.Generic;

namespace CSasic2 {
    public class Interpreter {
        public Dictionary<string, int> Labels;
        public List<IStatement> Statements;
        public Dictionary<string, IValue> Variables;
        public int CurrentStatement;
        public Interpreter() { }
        public static Interpreter Create() { return new Interpreter(); }
        public void Interpret(string sourceCode) {
            Variables = new Dictionary<string, IValue>();
            var tokens = Tokenizer.Create.Tokenize(sourceCode);
            (Labels, Statements) = Parser.Create(tokens).Parse();

            CurrentStatement = 0;
            while (CurrentStatement < Statements.Count)
                Statements[CurrentStatement++].Execute(this);
        }
        public void SetVariable(string name, IValue value) {
            if (Variables.ContainsKey(name))
                Variables[name] = value;
            else
                Variables.Add(name, value);
        }
    }
}