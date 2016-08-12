using System.Collections.Generic;

namespace Features
{
    public static class MyLinq
    {
        // important notes: I can define EM to extend any types: class, inteface, it can be a sealed class like string
        // for an EM to be available the namespace where that EM lives needs to be in effect.
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            int count = 0;
            foreach (var item in sequence)
            {
                count += 1;
            }
            return count;
        }
    }
}
