using System;
using Xunit;

namespace Prometheus.Language
{
    public class ParserTests
    {
        [Fact]
        public void ParserSimpleObjectType()
        {
            // arrange
            string sourceText = "type a { b: String c: Int }";
            Source source = new Source(sourceText);
            Lexer lexer = new Lexer();

            // act
            Parser parser = new Parser();
            DocumentNode document = parser.Parse(lexer, source);

            // assert
            Assert.Collection(document.Definitions,
                t =>
                {
                    Assert.IsType<ObjectTypeDefinitionNode>(t);
                    var objectTypeDefinition = (ObjectTypeDefinitionNode)t;
                    Assert.Equal(NodeKind.ObjectTypeDefinition, objectTypeDefinition.Kind);
                    Assert.Equal("a", objectTypeDefinition.Name.Value);
                    Assert.Collection(objectTypeDefinition.Fields,
                        f =>
                        {
                            Assert.Equal("b", f.Name.Value);
                            Assert.IsType<NamedTypeNode>(f.Type);
                            Assert.Equal("String", ((NamedTypeNode)f.Type).Name.Value);
                        },
                        f =>
                        {
                            Assert.Equal("c", f.Name.Value);
                            Assert.IsType<NamedTypeNode>(f.Type);
                            Assert.Equal("Int", ((NamedTypeNode)f.Type).Name.Value);
                        });
                });
        }

        [Fact]
        public void ParserSimpleInterfaceType()
        {
            // arrange
            string sourceText = "interface a { b: String c: Int }";
            Source source = new Source(sourceText);
            Lexer lexer = new Lexer();

            // act
            Parser parser = new Parser();
            DocumentNode document = parser.Parse(lexer, source);

            // assert
            Assert.Collection(document.Definitions,
                t =>
                {
                    Assert.IsType<InterfaceTypeDefinitionNode>(t);
                    var objectTypeDefinition = (InterfaceTypeDefinitionNode)t;
                    Assert.Equal(NodeKind.InterfaceTypeDefinition, objectTypeDefinition.Kind);
                    Assert.Equal("a", objectTypeDefinition.Name.Value);
                    Assert.Collection(objectTypeDefinition.Fields,
                        f =>
                        {
                            Assert.Equal("b", f.Name.Value);
                            Assert.IsType<NamedTypeNode>(f.Type);
                            Assert.Equal("String", ((NamedTypeNode)f.Type).Name.Value);
                        },
                        f =>
                        {
                            Assert.Equal("c", f.Name.Value);
                            Assert.IsType<NamedTypeNode>(f.Type);
                            Assert.Equal("Int", ((NamedTypeNode)f.Type).Name.Value);
                        });
                });
        }
    }
}