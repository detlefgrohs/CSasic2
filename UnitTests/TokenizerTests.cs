using CSasic2;
using CSasic2.Classes;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests {
    public class Tests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void TokenizerTest() {
            var sourceCode = @"100.00 -10 -100.123 Symbol ""A String"" !!!!";
            var tokens = Tokenizer.Create.Tokenize(sourceCode);


            tokens.Should().NotBeNull();
            tokens.Count.Should().Be(6);

            tokens[0].TokenType.Should().Be(TokenType.Double);
            tokens[0].Object.Should().Be(100.00);

            tokens[1].TokenType.Should().Be(TokenType.Integer);
            tokens[1].Object.Should().Be(-10);

            tokens[2].TokenType.Should().Be(TokenType.Double);
            tokens[2].Object.Should().Be(-100.123);
            
            tokens[3].TokenType.Should().Be(TokenType.Symbol);
            tokens[3].Object.Should().Be("Symbol");

            tokens[4].TokenType.Should().Be(TokenType.String);
            tokens[4].Object.Should().Be("A String");

            tokens[5].TokenType.Should().Be(TokenType.Unknown);
            tokens[5].Object.Should().Be("!!!!");

        }
    }
}