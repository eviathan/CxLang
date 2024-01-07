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
        private Dictionary<string, ILexemeResolver> _resolvers { get; set; } = [];

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

                    resolverInstance.Lexeme = attribute.Pattern;
                    resolverInstance.TokenType = attribute.TokenType;
                    
                    if(_resolvers.ContainsKey(attribute.Pattern))
                    {
                        throw new Exception($"Duplicate Resolvers for ({attribute.Pattern}) found");
                    }
                    else
                    {
                        _resolvers[attribute.Pattern] = resolverInstance;
                    }
                }
            }
        }

        public ILexemeResolver GetResolver(string pattern)
        {
            if(_resolvers.ContainsKey(pattern))
            {
                return _resolvers[pattern];
            }
            else
            {
                throw new Exception($"Could not find a resolver for ({pattern})");
            }
        }
    }
}