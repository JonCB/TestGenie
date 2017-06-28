using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestGenie
{
    public class GeneratorContext
    {
        private readonly Dictionary<Type, object> _genTemplates = new Dictionary<Type, object>();

        public void AddTemplate(params Type[] templates)
        {
            foreach (var item in templates.SelectMany(v => RetrieveTemplateTypes(v).Select(k => new { k, v })))
            {
                _genTemplates.Add(item.k, Activator.CreateInstance(item.v));
            }

        }

        public void AddFromAssembly(Assembly assembly)
        {
            AddTemplate(assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IGeneratorTemplate<>))).ToArray());
        }
        private IEnumerable<Type> RetrieveTemplateTypes(Type item)
        {
            return item.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IGeneratorTemplate<>))
                .Select(i => i.GenericTypeArguments[0]);
        }


        public T Generate<T>()
        {
            var bc = new BuildContext();

            if (!_genTemplates.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"No Generator found for type {typeof(T).FullName}");
            }



            T output;
            ((IGeneratorTemplate<T>)_genTemplates[typeof(T)]).Build(bc, out output);
            return output;
        }
    }
}
