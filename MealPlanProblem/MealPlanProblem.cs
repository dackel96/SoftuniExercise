using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OldExamsPractice
{
    class MealPlanProblem
    {
        static void Main(string[] args)
        {
            //salad   350
            //soup    490
            //pasta   680
            //steak   790
            Dictionary<string, int> mealsCallories = new Dictionary<string, int>
            {
                {"salad",350 },
                {"soup",490 },
                {"pasta",680 },
                {"steak",790 }
            };
            string[] stringElements = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Queue meals = new Queue(stringElements);

            int[] integerElements = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Stack calories = new Stack(integerElements);
            int mealsNumber = meals.Count;
            int counter = 0;
            while (true)
            {
                string dailyMeal = (string)meals.Dequeue();
                counter++;
                int dailyMealCalls = 0;
                if (mealsCallories.ContainsKey(dailyMeal))
                {
                    dailyMealCalls = mealsCallories[dailyMeal];
                }
                int dailyCallsAllowed = (int)calories.Pop();

                if (dailyCallsAllowed - dailyMealCalls > 0)
                {
                    calories.Push(dailyCallsAllowed - dailyMealCalls);
                }
                else if (dailyCallsAllowed - dailyMealCalls <= 0)
                {
                    int nextDegree = Math.Abs(dailyCallsAllowed - dailyMealCalls);
                    if (calories.Count == 0)
                    {
                        Console.WriteLine($"John ate enough, he had {counter} meals.");
                        List<string> list = new List<string>();
                        foreach (var meal in meals)
                        {
                            list.Add((string)meal);
                        }
                        Console.WriteLine($"Meals left: {string.Join(", ", list)}.");
                        break;
                    }
                    int nextCalls = (int)calories.Pop();
                    calories.Push(nextCalls - nextDegree);
                }
                if (meals.Count == 0)
                {
                    Console.WriteLine($"John had {counter} meals.");
                    List<int> list = new List<int>();
                    foreach (var call in calories)
                    {
                        list.Add((int)call);
                    }
                    Console.WriteLine($"For the next few days, he can eat {string.Join(", ", list)} calories.");
                    break;
                }
            }
        }
    }
}
