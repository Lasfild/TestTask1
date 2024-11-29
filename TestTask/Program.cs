using System;
using System.Collections.Generic;

public class Hedgehogs
{
    public static int MinMeetingsToUnify(int[] population, int targetColor)
    {
        var visitedPaths = new HashSet<string>();
        var queue = new Queue<(int[] state, int steps, List<string> path)>();

        queue.Enqueue((population, 0, new List<string> { $"Start: [{string.Join(", ", population)}]" }));
        visitedPaths.Add(string.Join(",", population));

        while (queue.Count > 0)
        {
            var (current, steps, path) = queue.Dequeue();
            int red = current[0], green = current[1], blue = current[2];

            if (current[targetColor] == red + green + blue)
            {
                foreach (var step in path)
                {
                    Console.WriteLine(step);
                }
                return steps;
            }

            var nextStates = new List<(int[] newState, string transition)>
            {
                // Синий + Зелёный = 2 Красных
                (green > 0 && blue > 0
                    ? new int[] { red + 2, green - 1, blue - 1 }
                    : null,
                "Blue + Green = 2 Red"),

                // Красный + Синий = 2 Зелёных
                (red > 0 && blue > 0
                    ? new int[] { red - 1, green + 2, blue - 1 }
                    : null,
                "Red + Blue = 2 Green"),

                // Зелёный + Красный = 2 Синих
                (red > 0 && green > 0
                    ? new int[] { red - 1, green - 1, blue + 2 }
                    : null,
                "Green + Red = 2 Blue")
            };

            foreach (var (newState, transition) in nextStates)
            {
                if (newState == null) continue;

                string newStateKey = string.Join(",", newState);

                // Проверка на цикл
                if (visitedPaths.Contains(newStateKey))
                    continue;

                visitedPaths.Add(newStateKey);

                var newPath = new List<string>(path)
                {
                    $"Step {steps + 1}: {transition} => [{string.Join(", ", newState)}]"
                };

                queue.Enqueue((newState, steps + 1, newPath));
            }
        }
        return -1;
    }

    public static void Main()
    {
        int[] population = { 8, 1, 9 };
        int targetColor = 1; // 0 1 2 = RGB 

        int result = MinMeetingsToUnify(population, targetColor);

        Console.WriteLine($"Result: {result}");
    }
}

// попробовал 3 разных алгоритма, опираясь на результаты которых могу с 99% уверенностью сказать что что с конфигурацией ежиков 8, 1, 9
// достижение поставленной цели в перекрашивании всех ежиков в любой из трех цветов невыполнимо