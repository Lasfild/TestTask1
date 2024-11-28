using System;
using System.Collections.Generic;

public class Hedgehogs
{
    public static int MinMeetingsToUnify(int[] population, int targetColor)
    {
        var queue = new Queue<(int[] population, int steps)>();
        var visited = new HashSet<string>();

        queue.Enqueue((population, 0));
        visited.Add(string.Join(",", population));

        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();
            int red = current[0], green = current[1], blue = current[2];

            if (current[targetColor] == red + green + blue)
                return steps;
            var nextStates = new List<int[]>();

            // Синий + Зелёный -> 2 Красных
            if (green > 0 && blue > 0)
                nextStates.Add(new int[] { red + 2, green - 1, blue - 1 });

            // Красный + Синий -> 2 Зелёных
            if (red > 0 && blue > 0)
                nextStates.Add(new int[] { red - 1, green + 2, blue - 1 });

            // Зелёный + Красный -> 2 Синих
            if (red > 0 && green > 0)
                nextStates.Add(new int[] { red - 1, green - 1, blue + 2 });

            foreach (var nextState in nextStates)
            {
                string stateKey = string.Join(",", nextState);

                if (!visited.Contains(stateKey))
                {
                    queue.Enqueue((nextState, steps + 1));
                    visited.Add(stateKey);
                }
            }
        }

        return -1;
    }

    public static void Main()
    {
        int[] population = { 8, 1, 9 };
        int targetColor = 0;

        int result = MinMeetingsToUnify(population, targetColor);

        Console.WriteLine($"Result: {result}");
    }
}
