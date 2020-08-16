namespace CSasic2.Values {
    class NumberValue : IValue {
        private double _value;
        public NumberValue(double value) {
            _value = value;
        }
        public IValue Evaluate(Interpreter interpreter) {
            return this;
        }
        public string ToStr() {
            return _value.ToString();
        }
        public double ToNum() {
            return _value;
        }
    }
}