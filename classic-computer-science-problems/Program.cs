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
            /*
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
            */

            /*
            Console.WriteLine("masiel " + "amor");
            //Console.WriteLine(insercionesPalindromo1("abcb", 0));
            //Console.WriteLine(insercionesPalindromo1("abecba", 0));
            
            // "zzazz"
            */

            travelling_salesman_problem_codeforces();

            //Console.WriteLine(5 % 2);
            //Console.WriteLine(6 % 2);
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
            /*
             * Aqui el array es innecesario
             * Se puede expresar con dos variables: ultimo y penultimo
             * e ir swapeando
             */
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

        static int fibonacciLeetcode(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int ultimo = 1;
            int penultimo = 0;
            
            for (int i = 2; i < n; i++)
            {
                int temp = ultimo;
                ultimo = ultimo + penultimo;
                penultimo = temp;
            }
            return ultimo;
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

        /*
         * Numero de letras que se deben insertar para convertir
         * una cadena en palindromo
         */
         // Time limit exceeded. Leetcode.
        static int insercionesPalindromo1(string s, int deep)
        {
            Func<string, int, string> compose = null;
            compose = (string str, int times) => {
                if (times > 0) return str + compose(str, times - 1);
                else return "";
            };

            if (s.Length < 2) return 0;

            HashSet<char> set = new HashSet<char>(s);
            if (set.Count() == s.Length) return s.Length - 1;

            Console.WriteLine("{0}{1}", compose("-", deep), s);
            if (s[0] == s[s.Length - 1]) return insercionesPalindromo1(s.Substring(1, s.Length - 2), deep + 1);
            int way1 = insercionesPalindromo1(s.Substring(0, s.Length - 1), deep + 1);
            int way2 = insercionesPalindromo1(s.Substring(1, s.Length - 1), deep + 1);
            return Math.Min(way1, way2) + 1;
        }

        // Acepted! Leetcode. :)
        static int insercionesPalindromo2(string s)
        {
            int[,] dp = new int[s.Length + 1, s.Length];

            for (int i = 2; i < s.Length + 1; i++)
            {
                for (int j = 0; j < s.Length - i + 1; j++)
                {
                    //Console.WriteLine("i={0}    j={1}", i, j);
                    if (s[j] == s[j + i - 1]) dp[i, j] = dp[i - 2, j + 1];
                    else dp[i, j] = Math.Min(dp[i - 1, j + 1], dp[i - 1, j]) + 1;
                }
            }

            return dp[s.Length, 0];
        }

        /*
         Idea:
            - Mover al auxiliar n-1 discos
            - Mover el mayor que esta en origen a destino
            - Mover los n-1 que estan en auxiliar a destino

        Mientras mayor sea el valor del numero en la pila significa que menor 
        es el disco.
        */
        static void Hanoi_Towers(Stack<int> source, Stack<int> auxiliar, Stack<int> destiny, int n)
        {
            // A:source B:auxiliar C:destiny
            // base case
            if (n == 2)
            {
                auxiliar.Push(source.Pop());
                destiny.Push(source.Pop());
                destiny.Push(auxiliar.Pop());
            }
            else
            {
                Hanoi_Towers(source, destiny, auxiliar, n - 1);
                destiny.Push(source.Pop());
                Hanoi_Towers(auxiliar, source, destiny, n - 1);
            }
        }

        /*
         - Las ciudades son solo los indices de la matriz: 0,1,2,...n
         - El array camino inicialmente es 0,1,2...n
         - Se puede hacer una optimizacion en cuanto a partir del paso en el q se este y las ciudades que 
         falten por visitar para no repetir llamados.
        */
        static int Naive_Travelling_Salesman_Problem(ICalculateCosto calculateCosto, int N,
            out int[] camino, Func<int[], int, bool> parada)
        {
            camino = new int[N];
            for (int i = 0; i < N; i++) camino[i] = -1;
            List<int> allCities = new List<int>();
            for (int i = 0; i < N; i++) allCities.Add(i);
            return Naive_Travelling_Salesman_Problem_aux(calculateCosto, camino, 0, parada, allCities);
        }

        static int Naive_Travelling_Salesman_Problem_aux(ICalculateCosto calculateCosto, int[] camino, 
            int step, Func<int[], int, bool> parada, List<int> noVisited)
        {
            if (step == camino.Length) return 0;
            if (parada(camino, step)) return int.MaxValue;

            int minCost = int.MaxValue;
            int[] real_way = new int[camino.Length];

            foreach (var item in noVisited)
            {
                camino[step] = item;
                var noVisitedNow = new List<int>(noVisited);
                noVisitedNow.Remove(item);
                //noVisited.Remove(item);
                int thisWay = Naive_Travelling_Salesman_Problem_aux(calculateCosto, camino, step + 1, parada, noVisitedNow);
                //noVisited.Add(item);
                if (step > 0) thisWay += calculateCosto.Calculate(camino[step - 1], item);
                if (thisWay < minCost && thisWay >= 0)
                {
                    //Console.WriteLine($"Encontramos un valor menor!! Que es {thisWay}");
                    minCost = thisWay;
                    camino.CopyTo(real_way, 0);
                }
            }

            camino = real_way;
            return minCost;
        }

        interface ICalculateCosto
        {
            int Calculate(int city1, int city2);
        }

        class CalculateCostoCodeforces : ICalculateCosto
        {
            List<Tuple<int, int>> costos;
            public int Calculate(int city1, int city2)
            {
                int v = Math.Max(costos[city1].Item2, costos[city2].Item1 - costos[city1].Item1);
                //Console.WriteLine($"Calculando costo desde {city1} a {city2} que da {v}");
                return v;
            }

            public CalculateCostoCodeforces(List<Tuple<int,int>> costos)
            {
                this.costos = costos;
            }
        }

        // https://codeforces.com/problemset/problem/1503/C
        static void travelling_salesman_problem_codeforces()
        {
            int n;
            int[] camino;
            int.TryParse(Console.ReadLine().Split()[0], out n);
            List<Tuple<int, int>> costos = new List<Tuple<int, int>>();
            int j = n;
            while (j > 0)
            {
                var values = Console.ReadLine().Split();
                int ai;  int ci;
                int.TryParse(values[0], out ai);
                int.TryParse(values[1], out ci);

                costos.Add(new Tuple<int, int>(ai, ci));
                j--;
            }

            var calculateCosto = new CalculateCostoCodeforces(costos);
            Func<int[], int, bool> parada = (int[] way, int step) =>
              {
                  if (step == 1) return false;
                  if (step > 0 && way[step - 1] == 0) return true;
                  else return false;
              };

            camino = new int[n + 1];
            for (int i = 1; i < camino.Length; i++) camino[i] = -1;

            List<int> allCities = new List<int>();
            for (int i = 0; i < n; i++) allCities.Add(i);
            int sol = Naive_Travelling_Salesman_Problem_aux(calculateCosto, camino, 1, parada, allCities);
            Console.WriteLine(sol);
            /*
            foreach (var item in camino)
            {
                Console.Write($"{item} ");
            }
            */
        }



        // Sorting
        static void burbuja<T>(T[] array) where T:IComparable
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if(array[j].CompareTo(array[j + 1]) == 1)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        static void minimos_sucesivos<T>(T[] array) where T:IComparable
        {
            for (int i = 0; i < array.Length; i++)
            {
                int min_index = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[min_index]) == -1) min_index = j;
                }

                T temp = array[i];
                array[i] = array[min_index];
                array[min_index] = temp;
            }
        }

        static void ordenacion_por_insercion<T>(T[] array) where T:IComparable
        {
            for (int i = 1; i < array.Length; i++)
            {

                for (int j = i; j > 0; j--)
                {
                    if (array[j].CompareTo(array[j - 1]) == -1)
                    {
                        T temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                    }
                    else break;
                }
            }
        }
    }
}
