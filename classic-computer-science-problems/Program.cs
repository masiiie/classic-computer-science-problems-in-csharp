using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classic_computer_science_problems
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Math.Pow(2, 32));
            //long a = 1836311903 + 1134903170;
            //Console.Write("{0}", a);
            Console.WriteLine(numeroMasGrande(32));
            
            Fibonacci fib = new Fibonacci();
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                // En fibonacci de 48 da el primer negativo
                // fib47 = 1836311903
                // fib46 = 1134903170
                // fib48 = 2971215073
                // 2^32 = 4294967296
                Console.Write("fib{0} = {1}\n", i + 1, fib.calculate(i + 1));
            }            
        }

        static long numeroMasGrande(int n)
        {
            long sol = 0;
            for (int i = 0; i < n; i++)
            {
                sol += (long)Math.Pow(2, i);
            }

            return sol;
        }

        // los llamados recursivos repiten muchas operaciones
        static int fibonacci1(int n)
        {
            if (n == 1) return 0;
            if (n < 4) return 1;
            Console.WriteLine("fibonacci de: {0}", n);
            return fibonacci1(n - 1) + fibonacci1(n - 2);
        }

        static int fibonacci2(int n)
        {
            Console.WriteLine("fibonacci de: {0}", n);
            if (n == 1) return 0;
            if (n < 4) return 1;
            int[] serie = new int[n];
            serie[0] = 0;
            serie[1] = 1;
            serie[2] = 1;
            for (int i = 3; i < n; i++)
            {
                serie[i] = serie[i - 1] + serie[i - 2];
            }
            return serie[n - 1];
        }

        class Fibonacci
        {
            Dictionary<int, long> serie;

            public Fibonacci()
            {
                serie = new Dictionary<int, long>();
                serie[1] = 0;
                serie[2] = 1;
                serie[3] = 1;
            }

            public long calculate(int n)
            {
                //Console.Write("\nfinonacci de: {0} ", n);
                if (serie.Keys.Contains(n)) return serie[n];


                for (int i = serie.Keys.Max() + 1; i < n + 1; i++)
                {
                    serie[i] = serie[i - 1] + serie[i - 2];
                }

                return serie[n];
            }
        }
    }
}
