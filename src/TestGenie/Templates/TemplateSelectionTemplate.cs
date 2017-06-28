using System.Collections.Generic;

namespace TestGenie.Templates
{
    public class TemplateSelectionTemplate<T> : IGeneratorTemplate<T>
    {
        private readonly IEnumerable<IGeneratorTemplate<T>> _values;

        public TemplateSelectionTemplate(IEnumerable<IGeneratorTemplate<T>> values)
        {
            _values = values;
        }

        public void Build(BuildContext context, out T subject)
        {
            subject = default(T);
        }
    }
}