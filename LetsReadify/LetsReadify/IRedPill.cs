using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LetsReadify
{
    public class Constants
    {        
        public const string Namespace = "http://KnockKnock.readify.net";
    }

    [ServiceContract(Namespace = Constants.Namespace, SessionMode = SessionMode.Allowed)]
    public interface IRedPill
    {
        [OperationContract]
        Guid WhatIsYourToken();
        
        [OperationContract]
        [FaultContract(typeof(ArgumentOutOfRangeException))]
        Int64 FibonacciNumber(Int64 n);

        [OperationContract]
        TriangleType WhatShapeIsThis(int a, int b, int c);

        [OperationContract]
        [FaultContract(typeof(ArgumentNullException))]
        string ReverseWords(string s);      
    }          

    [DataContract]
    public enum TriangleType
    {
        [EnumMember]
        Error=0,
        [EnumMember]
        Scalene=1,
        [EnumMember]
        Isosceles = 2,
        [EnumMember]
        Equilateral = 3,
    }
}
