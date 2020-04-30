using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSasic2.Classes;

namespace CSasic2 {
    public class Tokenizer {
        private Dictionary<Regex, Func<Match, Token>> tokenizerDictionary = null;

        public Tokenizer() {
            Initialize();
        }

        public static Tokenizer Create => new Tokenizer();

        public Regex CreateParseRegex(string pattern) {
            return new Regex($@"^\s*{pattern}(?<rol>.*)?$");
        }
        private void Initialize() {
            tokenizerDictionary = new Dictionary<Regex, Func<Match, Token>> {
                {CreateParseRegex(@"(?<double>[-]?\d+[.]\d+)"), match => new Token(TokenType.Double, double.Parse(match.Groups["double"].Value)) },
                {CreateParseRegex(@"(?<integer>[-]?\d+)"), match => new Token(TokenType.Integer, int.Parse(match.Groups["integer"].Value))},
                {CreateParseRegex(@"(?<symbol>[a-zA-z_]\w*)"), match => new Token(TokenType.Symbol, match.Groups["symbol"].Value)},
                {CreateParseRegex(@"""(?<string>.*)"""), match => new Token(TokenType.String, match.Groups["string"].Value)},

            };
        }

        public List<Token> Tokenize(string sourceCode) {
            var tokens = new List<Token>();
            var splitLines = sourceCode.Split(Environment.NewLine);
            foreach (var line in splitLines) {
                var remainingLine = line;
                while (!string.IsNullOrEmpty(remainingLine.Trim())) {
                    var matched = false;
                    foreach (var regex in tokenizerDictionary.Keys) {
                        if (!regex.IsMatch(remainingLine)) continue;
                        var match = regex.Match(remainingLine);
                        remainingLine = match.Groups["rol"].Value;
                        tokens.Add(tokenizerDictionary[regex].Invoke(match));
                        matched = true;
                        break;
                    }

                    if (!matched) {
                        tokens.Add(new Token(TokenType.Unknown, remainingLine.Trim()));
                        remainingLine = string.Empty;
                    }
                }
            }
            return tokens;
        }
    }
}

//Unknown,
//Symbol,
//Integer,
//Double,
//String,
//Label,
//Line,
//Equals,
//Operator,
//LeftParen,
//RightParen,
//Comment,
//Eof