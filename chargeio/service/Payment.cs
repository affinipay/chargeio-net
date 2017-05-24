using System;

namespace chargeio
{
    public class Payment : IPayment
    {
        public void DoThing(string Thing)
        {
            Console.WriteLine("Doing the thing: " + Thing);
        }
    }
}