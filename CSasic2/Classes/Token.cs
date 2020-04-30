using System;
using System.Collections.Generic;
using System.Text;

namespace CSasic2.Classes {
    public class Token {
        public TokenType TokenType { get; set; }
        public object Object { get; set; }
        public Token(TokenType tokenType, object obj) {
            TokenType = tokenType;
            Object = obj;
        }
    }
}
