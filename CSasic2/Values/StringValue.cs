using System;

namespace CSasic2.Values {
    class StringValue : IValue {
        private string _value;
        public StringValue(string value) {
            _value = value;
        }
        public IValue Evaluate(Interpreter interpreter) {
            return this;
        }
        public string ToStr() {
            return _value;
        }
        public double ToNum() {
            return Double.Parse(_value);
        }
    }
}