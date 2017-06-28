using System;
using System.Collections.Generic;
using System.Linq;

namespace TestGenie {
    public class BuildContext
    {       
        private Random random;

        public BuildContext()
        {
            random = new Random();
        }

        public BuildContext(int seed)
        {
            random = new Random(seed);
        }

        public int Next()
        {
            return random.Next();
        }

        public T ApplyPattern<T>(string s)
        {
            // TODO: Make this actually work
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(s, typeof(T));
            }
            else
            {
                return default(T);
            }
        }
    }
}