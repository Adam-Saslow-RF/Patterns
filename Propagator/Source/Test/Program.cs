using System.Collections.Generic;

namespace PropagatorPattern
{
    class Program
    {
        static IntWithPropagator[] intProps = new IntWithPropagator[10];

        static void Main(string[] args)
        {
            /*for(int i = 0; i < intProps.Length; i++)
            {   
                intProps[i] = new IntWithPropagator(0);
                if(i-1 >= 0)
                {
                    intProps[i-1].AddDependent(intProps[i], false);
                }
            }

            for(int i = 0; i < intProps.Length; i++)
                intProps[i].Increment();
                */

            IntWithPropagator A = new IntWithPropagator(5);
            IntWithPropagator B = new IntWithPropagator(0);
            
            A.AddDependent(B, true);
            A.Increment();

            B.Decrement();

            A.RemoveDependent(B, true);
            A.Decrement();
        }
    }
}
