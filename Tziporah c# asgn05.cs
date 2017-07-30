using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    static class Program
    {
        static void Main(string[] args)
        {
            var vals = new int[]
            {
                5, 9, 4, 7, 15, 16, 10, 11, 14, 19, 18
            };

            var highVals = vals.MaxOverPrevious().ToList();
            foreach (var highVal in highVals)
            {
                Console.Out.Write(" " + highVal);
            }
            Console.Out.WriteLine();
            highVals = vals.MaxOverPrevious(i => i / 2 + 7).ToList();
            foreach (var highval in highVals)
            {
                Console.Out.Write(" " + highval);
            }
            Console.Out.WriteLine();
            highVals = vals.LocalMaxima().ToList();
            foreach (var VARIABLE in highVals)
            {
                Console.Out.Write(" " + VARIABLE);
            }
            Console.Out.WriteLine();
            highVals = vals.LocalMaxima(i => i / 2 + 7).ToList();
            foreach (var highVal in highVals)
            {
                Console.Out.Write(" " + highVal);
            }
            Console.Out.WriteLine();

            var passed = vals.AtLeastK(3, i => i >= 16 && i < 20);
            Console.Out.WriteLine(passed);

            passed = vals.AtLeastK(3, i => i >= 16 && i < 20, i => i / 2 + 7);
            Console.Out.WriteLine(passed);

            passed = vals.AtLeastHalf(i => i >= 16 && i < 20);
            Console.Out.WriteLine(passed);

            passed = vals.AtLeastHalf(i => i >= 16 && i < 20, i => i / 2 + 7);
            Console.Out.WriteLine(passed);

            Console.ReadKey();
        }

        public static IEnumerable<int> MaxOverPrevious(this Int32[] vals)
        {
            int highest = vals[0];
            yield return vals[0];
            foreach (var val in vals)
            {
                if (val > highest)
                {
                    highest = val;
                    yield return val;
                }
            }
            //I used deferred execution by using yield return. That way, the processor only needs to find the number
            //of vals that the user actually needs and does not necessarily have to iterate over the entire collection.
        }

        public static IEnumerable<int> MaxOverPrevious(this Int32[] vals, Func<int, int> transform)
        {
            int[] newVals = new int[vals.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                newVals[i] = transform(vals[i]);
            }

            vals = newVals;
            int highest = vals[0];
            yield return vals[0];
            foreach (var val in vals)
            {
                if (val > highest)
                {
                    highest = val;
                    yield return val;
                }
            }
        }

        public static IEnumerable<int> LocalMaxima(this int[] vals)
        {
            for (int i = 0; i < vals.Length; i++)
            {
                if (i == 0)
                {
                    if (vals[i] > vals[i + 1])
                    {
                        yield return vals[i];
                    }
                }
                else if (i == vals.Length - 1)
                {
                    if (vals[i] > vals[i - 1])
                    {
                        yield return vals[i];
                    }
                }
                else if (vals[i] > vals[i - 1] & vals[i] > vals[i + 1])
                {
                    yield return vals[i];
                }
            }
            //just like in max over previous, yield return ensures that the method will only execute the for loop the
            //to get the number of values that the user needs instead of always going through the entire collection
        }

        public static IEnumerable<int> LocalMaxima(this int[] vals, Func<int, int> transform)
        {
            int[] newVals = new int[vals.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                newVals[i] = transform(vals[i]);
            }
            vals = newVals;
            for (int i = 0; i < vals.Length; i++)
            {
                if (i == 0)
                {
                    if (vals[i] > vals[i + 1])
                    {
                        yield return vals[i];
                    }
                }
                else if (i == vals.Length - 1)
                {
                    if (vals[i] > vals[i - 1])
                    {
                        yield return vals[i];
                    }
                }
                else if (vals[i] > vals[i - 1] & vals[i] > vals[i + 1])
                {
                    yield return vals[i];
                }
            }
        }

        public static bool AtLeastK(this int[] vals, int k, Func<int, bool> test)
        {
            int count = 0;
            foreach (var VARIABLE in vals)
            {
                if (test(VARIABLE))
                {
                    count++;
                }
                if (count >= k)
                {
                    return true;
                }
            }
            
            return false;
            //Deferred execution is not necessary or helpful in this method because we have to go through the whole
            //list to find if it is at least k
        }

        public static bool AtLeastK(this int[] vals, int k, Func<int, bool> test, Func<int, int> transform)
        {
            int[] newVals = new int[vals.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                newVals[i] = transform(vals[i]);
            }
            vals = newVals;
            int count = 0;
            foreach (var VARIABLE in vals)
            {
                if (test(VARIABLE))
                {
                    count++;
                }
                if (count >= k)
                {
                    return true;
                }
            }
            
            return false;
        }



        public static bool AtLeastHalf(this int[] vals, Func<int, bool> test)
        {
            int count = 0;
            foreach (var val in vals)
            {
                if (test(val))
                {
                    count++;
                }
                if (count >= vals.Length / 2)
                {
                    return true;
                }
            }
           
            return false;
            //as in atleastk, deferred execution is of no advantage here because we return as soon as we meet the 
            //condition, and if not
            //we need to go through the whole
            //list anyway
        }

        public static bool AtLeastHalf(this int[] vals, Func<int, bool> test, Func<int, int> transform)
        {
            int[] newVals = new int[vals.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                newVals[i] = transform(vals[i]);
            }
            vals = newVals;
            int count = 0;
            foreach (var val in vals)
            {
                if (test(val))
                {
                    count++;
                }
            }
            if (count >= vals.Length / 2)
            {
                return true;
            }
            return false;
        }




    }
}
