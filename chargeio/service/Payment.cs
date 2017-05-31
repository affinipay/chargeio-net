using System;
using ChargeIo.Interface;

namespace ChargeIo.Service
{
    public class Payment : IPayment
    {
        public void DoThing(string Thing)
        {
            Console.WriteLine("Doing the thing: " + Thing);
        }
    }
}