namespace CSasic2.Classes {
    public class Token {
        public TokenType TokenType { get; set; }
        public object Value { get; set; }
        public Token(TokenType tokenType, object value) {
            TokenType = tokenType;
            Value = value;
        }
    }
}
