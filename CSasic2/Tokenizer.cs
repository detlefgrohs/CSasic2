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
            return new Regex($@"^\s*{pattern}(?<rosc>.*)$", RegexOptions.Singleline); // | RegexOptions.Multiline);
        }
        private void Initialize() {
            SyntaxDictionary = new Dictionary<Regex, Func<Match, Token>> {
                {CreateParseRegex(@"(?<label>[a-zA-z_]\w*):"), match => new Token(TokenType.Label, match.Groups["label"].Value)},                         //Label,
                {CreateParseRegex(@"(?<symbol>[a-zA-z_]\w*)"), match => new Token(TokenType.Symbol, match.Groups["symbol"].Value)},                         //Symbol,
                {CreateParseRegex(@"""(?<string>.*?)"""), match => new Token(TokenType.String, match.Groups["string"].Value)},                               //String,
                {CreateParseRegex(@"'(?<string>.*?)'"), match => new Token(TokenType.String, match.Groups["string"].Value)},                                //String,
                {CreateParseRegex(@"(?:(?<number>[-]?\d+[.]\d+)|(?<number>[-]?\d+))"),
                    match => new Token(TokenType.Number, double.Parse(match.Groups["number"].Value)) },         //Double,
                //{CreateParseRegex(@"(?<integer>[-]?\d+)"), match => new Token(TokenType.Integer, int.Parse(match.Groups["integer"].Value))},                //Integer,


                {CreateParseRegex(@"(?<equals>=)"), match => new Token(TokenType.Equals, match.Groups["equals"].Value)},                         //Equals,
                {CreateParseRegex(@"//(?<comment>[^\n]*)"), match => new Token(TokenType.Comment, match.Groups["comment"].Value)},                         //Comment,
                
                {CreateParseRegex(@"(?<operator>[+-/*//<>])"), match => new Token(TokenType.Operator, match.Groups["operator"].Value)},                         //Operator,
                {CreateParseRegex(@"(?<leftparen>[(])"), match => new Token(TokenType.LeftParen, match.Groups["leftparen"].Value)},                         //LeftParen,
                {CreateParseRegex(@"(?<rightparen>[)])"), match => new Token(TokenType.RightParen, match.Groups["rightparen"].Value)},                         //RightParen,
                
                { CreateParseRegex(@"(?<unknown>\S+)"), match => new Token(TokenType.Unknown, match.Groups["unknown"].Value)},                               //Unknown,
            };
        }
        public List<Token> Tokenize(string sourceCode) {
            var tokens = new List<Token>();
            while (!string.IsNullOrEmpty(sourceCode.Trim())) {
                var syntax = SyntaxDictionary.First(s => s.Key.IsMatch(sourceCode));
                var match = syntax.Key.Match(sourceCode);
                sourceCode = match.Groups["rosc"].Value;
                tokens.Add(syntax.Value.Invoke(match));
            }
            return tokens;
        }
    }
}

// (?:"(?<String>(?:.|[(?:\\")])*)")|(?:'(?<String>.*?)')|(?s:@"(?<String>.*?)")