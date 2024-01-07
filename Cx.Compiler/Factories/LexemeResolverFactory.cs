using System.Reflection;
using Cx.Compiler.Attributes;
using Cx.Compiler.Interfaces;

namespace Cx.Compiler.Factories
{
    public class LexemeResolverFactory
    {
        public static LexemeResolverFactory Instance
        {
            get 
            {
                _instance ??= new LexemeResolverFactory();
                return _instance;
            }
        }

        private static LexemeResolverFactory _instance;
        private Dictionary<char, ILexemeResolver> _resolvers { get; set; } = [];

        private LexemeResolverFactory()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resolverTypes = assembly
                .GetTypes()
                .Where(type => type
                    .GetInterfaces()
                    .Contains(typeof(ILexemeResolver))
                );

            foreach (var resolverType in resolverTypes)
            {
                var attributes = resolverType.GetCustomAttributes<LexemeAttribute>();
                foreach (var attribute in attributes)
                {
                    var resolverInstance = Activator.CreateInstance(resolverType) as ILexemeResolver;

                    resolverInstance.Lexeme = $"{attribute.Character}";
                    resolverInstance.TokenType = attribute.TokenType;
                    
                    if(_resolvers.ContainsKey(attribute.Character))
                    {
                        throw new Exception($"Duplicate Resolvers for ({attribute.Character}) found");
                    }
                    else
                    {
                        _resolvers[attribute.Character] = resolverInstance;
                    }
                }
            }
        }

        public ILexemeResolver GetResolver(char character)
        {
            if(_resolvers.ContainsKey(character))
            {
                return _resolvers[character];
            }
            else
            {
                throw new Exception($"Could not find a resolver for ({character})");
            }
        }
    }
}