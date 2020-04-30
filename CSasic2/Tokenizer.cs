using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSasic2.Classes;

namespace CSasic2 {
    public class Tokenizer {
        private Dictionary<Regex, Func<Match, Token>> SyntaxDictionary = null;
        public Tokenizer() {
            Initialize();
        }
        public static Tokenizer Create => new Tokenizer();
        public Regex CreateParseRegex(string pattern) {
            return new Regex($@"^\s*{pattern}(?<rosc>.*)?$", RegexOptions.Singleline);
        }
        private void Initialize() {
            SyntaxDictionary = new Dictionary<Regex, Func<Match, Token>> {
                {CreateParseRegex(@"(?<double>[-]?\d+[.]\d+)"), match => new Token(TokenType.Double, double.Parse(match.Groups["double"].Value)) },         //Double,
                {CreateParseRegex(@"(?<integer>[-]?\d+)"), match => new Token(TokenType.Integer, int.Parse(match.Groups["integer"].Value))},                //Integer,
                {CreateParseRegex(@"(?<symbol>[a-zA-z_]\w*)"), match => new Token(TokenType.Symbol, match.Groups["symbol"].Value)},                         //Symbol,
                {CreateParseRegex(@"""(?<string>.*)"""), match => new Token(TokenType.String, match.Groups["string"].Value)},                               //String,
                {CreateParseRegex(@"'(?<string>.*?)'"), match => new Token(TokenType.String, match.Groups["string"].Value)},                                //String,

                //Label,
//Line,
//Equals,
//Operator,
//LeftParen,
//RightParen,
//Comment,
//Eof
                {CreateParseRegex(@"(?<unknown>\S+)"), match => new Token(TokenType.Unknown, match.Groups["unknown"].Value)},                               //Unknown,
            };
        }
        public List<Token> Tokenize(string sourceCode) {
            var tokens = new List<Token>();

            while (!string.IsNullOrEmpty(sourceCode.Trim())) {
                var syntax = SyntaxDictionary.First(syntax => syntax.Key.IsMatch(sourceCode));
                var match = syntax.Key.Match(sourceCode);
                sourceCode = match.Groups["rosc"].Value;
                tokens.Add(syntax.Value.Invoke(match));
            }
            return tokens;
        }
    }
}