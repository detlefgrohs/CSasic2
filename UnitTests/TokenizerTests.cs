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
            var sourceCode = @"100.00 -10 -100.123 Symbol ""A String"" 'Single Quote String' !!!! _Symbol2";
            var tokens = Tokenizer.Create.Tokenize(sourceCode);

            tokens.Should().NotBeNull();
            tokens.Count.Should().Be(8);

            tokens[0].ShouldBe(TokenType.Double, 100.0);
            tokens[1].ShouldBe(TokenType.Integer, -10);
            tokens[2].ShouldBe(TokenType.Double, -100.123);
            tokens[3].ShouldBe(TokenType.Symbol, "Symbol");
            tokens[4].ShouldBe(TokenType.String, "A String");
            tokens[5].ShouldBe(TokenType.String, "Single Quote String");
            tokens[6].ShouldBe(TokenType.Unknown, "!!!!");
            tokens[7].ShouldBe(TokenType.Symbol, "_Symbol2");
        }
        [Test]
        public void TokenizerTest2() {
            var sourceCode = @"
100.00 
-10 
-100.123 
Symbol 
""A String""
'Single Quote String'
!!!!
_Symbol2";
            var tokens = Tokenizer.Create.Tokenize(sourceCode);

            tokens.Should().NotBeNull();
            tokens.Count.Should().Be(8);

            tokens[0].ShouldBe(TokenType.Double, 100.0);
            tokens[1].ShouldBe(TokenType.Integer, -10);
            tokens[2].ShouldBe(TokenType.Double, -100.123);
            tokens[3].ShouldBe(TokenType.Symbol, "Symbol");
            tokens[4].ShouldBe(TokenType.String, "A String");
            tokens[5].ShouldBe(TokenType.String, "Single Quote String");
            tokens[6].ShouldBe(TokenType.Unknown, "!!!!");
            tokens[7].ShouldBe(TokenType.Symbol, "_Symbol2");
        }        
    }
    public static class AssertionExtensions {
        public static void ShouldBe(this Token token, TokenType tokenType, object value) {
            token.TokenType.Should().Be(tokenType);
            token.Value.Should().Be(value);
        }
    }
}