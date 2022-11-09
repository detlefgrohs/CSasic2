using CSasic2.Expressions;
using CSasic2.Statements;
using CSasic2.Values;
using CSasic2.Classes;
using System;
using System.Collections.Generic;

namespace CSasic2 {
    public class Parser {
        private List<Token> _tokens;
        private int _position = 0;
        public Parser(List<Token> tokens) {
            _tokens = tokens;
        }
        public static Parser Create(List<Token> tokens) {
            return new Parser(tokens);
        }
        public (Dictionary<string, int>, List<IStatement>) Parse() {
            var labels = new Dictionary<string, int>();
            var statements = new List<IStatement>();

            while (true) {
                if (Match(TokenType.Label)) labels.Add(Get(-1).Value as string, statements.Count);
                else if (Match(TokenType.Symbol, TokenType.Equals)) statements.Add(new AssignStatement(Get(-2).Value as string, GetExpression()));
                else if (Match("print")) statements.Add(new PrintStatement(GetExpression()));
                else if (Match("input")) statements.Add(new InputStatement(Consume(TokenType.Symbol).Value as string));
                else if (Match("goto")) statements.Add(new GotoStatement(Consume(TokenType.Symbol).Value as string));
                else if (Match("if")) {
                    var condition = GetExpression();
                    Consume("then");
                    var label = Consume(TokenType.Symbol).Value as string;
                    statements.Add(new IfThenStatement(condition, label));
#pragma warning disable CS0642 // Possible mistaken empty statement
                } else if (Match(TokenType.Comment)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                else break;
            }
            return (labels, statements);
        }
        private IExpression GetExpression() {
            var left = GetSubExpression();
            while (Match(TokenType.Operator) || Match(TokenType.Equals)) {
                var op = (Get(-1).Value as string)[0];
                var right = GetSubExpression();
                left = new OperatorExpression(left, op, right);
            }
            return left;
        }
        private IExpression GetSubExpression() {
            if (Match(TokenType.Symbol)) return new VariableExpression(Get(-1).Value as string);
            else if (Match(TokenType.Number)) return new NumberValue((double) Get(-1).Value);
            else if (Match(TokenType.String)) return new StringValue(Get(-1).Value as string);
            else if (Match(TokenType.LeftParen)) {
                var expression = GetExpression();
                Consume(TokenType.RightParen);
                return expression;
            }
            throw new Exception("Unable to parse expression.");
        }
        private Token Get(int offset) {
            if (_position + offset >= _tokens.Count)
                return new Token(TokenType.Eof, string.Empty);
            return _tokens[_position + offset];
        }
        private bool Match(TokenType type) {
            if (Get(0).TokenType != type) return false;
            _position++;
            return true;
        }
        private bool Match(TokenType type1, TokenType type2) {
            if (Get(0).TokenType != type1) return false;
            if (Get(1).TokenType != type2) return false;
            _position += 2;
            return true;
        }
        private bool Match(string name) {
            if (Get(0).TokenType != TokenType.Symbol) return false;
            if (!(Get(0).Value as string).Equals(name)) return false;
            _position++;
            return true;
        }
        private Token Consume(TokenType type) {
            if (Get(0).TokenType != type) throw new Exception($"Expected {type} but got {Get(0).TokenType}.");
            return _tokens[_position++];
        }
        private Token Consume(string name) {
            if (!Match(name)) throw new Exception($"Expected {name}.");
            return Get(-1);
        }
    }
}