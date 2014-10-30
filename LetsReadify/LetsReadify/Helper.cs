using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LetsReadify
{
    public class Helper
    {
        public TriangleType GetTriangleType(int a, int b, int c)
        {
            //Trace.WriteLine(string.Format("{0} GetTriangleType a ={1} b={2} c={3}", DateTime.Now.ToString("G"), a, b, c));

            // keeping this as the first check in case someone passes invalid parameters that could also be a triangle type. 
            //Example: -2,-2,-2 could return Equilateral instead of Error without this check.  
            //We also have a catch all at the end that returns Error if no other condition was met.

            if (a <= 0 || b <= 0 || c <= 0)
                return TriangleType.Error;
            //Placing items in an array for processing 
            int[] values = { a, b, c };

            if (values.Distinct().Count() == 1) //There is only one distinct value in the set, therefore all sides are of equal length
                return TriangleType.Equilateral;
            if (values.Distinct().Count() == 2) //There are only two distinct values in the set, therefore two sides are equal and one is not
                return TriangleType.Isosceles;
            if (values.Distinct().Count() == 3) // There are three distinct values in the set, therefore no sides are equal
                return TriangleType.Scalene;
            return TriangleType.Error;
        }

        public Int64 GetFibonacci(Int64 n)
        {
            //Trace.WriteLine(string.Format("{0} GetFibonacci n={1}", DateTime.Now.ToString("G"), n));           
            long a = 0;
            long b = 1;

            if (n > 0)
            {
                // In N steps compute Fibonacci sequence iteratively.
                for (long i = 0; i < n; i++)
                {
                    long temp = a;
                    a = b;
                    b = temp + b;
                }

                return a;
            }
            for (long i = n; i < 0; i++)
            {
                long temp = a;
                a = b;
                b = temp + b;
            }
            return ((n * -1) % 2 == 0) ? a * -1 : a;
        }

        public string GetReverseWord(string s)
        {
           // Trace.WriteLine(string.Format("{0} GetReverseWord s={1}", DateTime.Now.ToString("G"), s));            
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}