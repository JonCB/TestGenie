using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using TestGenie.Templates;

namespace TestGenie
{
    public class Generator<T>
    {
        private List<PropListItem> _items = new List<PropListItem>();

        public Generator<T> WithOneOf<TValue>(Expression<Func<T, TValue>> prop, TValue[] values)
        {
            _items.Add(new PropValue<TValue>(prop, new ItemSelectionTemplate<TValue>(values)));
            return this;
        }

        public Generator<T> WithOneOf<TValue>(Expression<Func<T, TValue>> prop, IGeneratorTemplate<TValue>[] templates)
        {
            _items.Add(new PropValue<TValue>(prop, new TemplateSelectionTemplate<TValue>(templates)));
            return this;
        }

        public Generator<T> WithRandom<TValue>(Expression<Func<T, TValue>> prop)
        {
            _items.Add(new PropValue<TValue>(prop, new RandomValueTemplate<TValue>()));
            return this;
        }

        public T Build(int? seed = null)
        {
            var context = seed.HasValue 
                ? new BuildContext(seed.Value) 
                : new BuildContext();

            // TODO: support items with constructor parameters
            var obj = Activator.CreateInstance<T>();

            foreach (var item in _items)
            {
                Apply(context, obj, item);
            }

            return obj;
        }

        private void Apply(BuildContext context, T obj, PropListItem item)
        {
            // TODO: No-one goes full reflection. Is there a better way to do this?
            var t = item.GetPropertyType();

            GetType().GetMethod("ApplyPropValue", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(t)
                .Invoke(this, new object[] { context, obj, item });
        }

        private void ApplyPropValue<TValue>(BuildContext context, T obj, PropValue<TValue> prop)
        {
            var expr = (prop.Property.Body as MemberExpression);
            if (expr != null)
            {
                TValue newVal;
                prop.Template.Build(context, out newVal);

                switch (expr.Member.MemberType)
                {
                    case MemberTypes.Field:
                        (expr.Member as FieldInfo).SetValue(obj, newVal);
                        break;

                    case MemberTypes.Property:
                        (expr.Member as PropertyInfo).SetValue(obj, newVal);
                        break;
                }
            }
        }

        internal class PropValue<TValue> : PropListItem
        {
            public Expression<Func<T,TValue>> Property { get;  }
            public IGeneratorTemplate<TValue> Template { get; }

            public PropValue(Expression<Func<T, TValue>> prop, IGeneratorTemplate<TValue> template)
            {
                Property = prop;
                Template = template;
            }

            public Type GetPropertyType()
            {
                return typeof(TValue);
            }
        }

        public interface PropListItem
        {
            Type GetPropertyType();
        }
    }
}
