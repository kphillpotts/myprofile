using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;

namespace LetsReadify
{
    [ServiceBehavior(Namespace = Constants.Namespace, InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RedPill : IRedPill
    {
        public Guid WhatIsYourToken()
        {
            return Guid.Empty;
            //new Guid("7b6e7e26-ee95-4d19-b304-615ae4123510");
        }

        public Int64 FibonacciNumber(Int64 n)
        {
            if (n < -92)
                throw new ArgumentOutOfRangeException();
            if (n == 0) 
                return 0;

            return Helper.GetFibonacci(n);
        }

        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            return Helper.GetTriangleType(a, b, c);
        }

        public string ReverseWords(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException();
            return Helper.GetReverseWord(s);
        }
    }
}
