using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenie.Templates
{
    public class RandomValueTemplate<TValue> : IGeneratorTemplate<TValue>
    {

        public void Build(BuildContext context, out TValue subject)
        {
            Object obj = default(TValue);
            if (typeof(TValue) == typeof(int) ||                
                typeof(TValue) == typeof(short))
            {
                obj = context.Next();           
            }
            else if (typeof(TValue) == typeof(long))
            {
                // TODO: fix bug for negative lower 32bit
                obj = ((long)context.Next() << 32) + context.Next();
            }

            subject = (TValue)Convert.ChangeType(obj, typeof(TValue));
        }
    }
}
