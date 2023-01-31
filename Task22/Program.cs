using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> arrayFunc = new Func<object, int[]>(GetArray);
            Task<int[]> arrayTask = new Task<int[]>(arrayFunc, n);

            Func<Task<int[]>, int> sumFunc = new Func<Task<int[]>, int>(GetSum);
            Task<int> sumTask = arrayTask.ContinueWith<int>(sumFunc);

            Action<Task<int[]>> maxAction = new Action<Task<int[]>>(GetMax);
            Task maxTask = arrayTask.ContinueWith(maxAction);

            arrayTask.Start();
            Console.WriteLine($"Сумма всех элементов массива:: {sumTask.Result}");
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            Console.WriteLine("Массив:");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }

        static int GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        static void GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            Console.WriteLine($"Максимальное число в массиве: {max}");
        }

    }
}
