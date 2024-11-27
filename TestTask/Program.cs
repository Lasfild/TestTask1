using System;

public class Hedgehogs
{
    public static int MinMeetingsToUnify(int[] population)
    {
        // Находим цвет с наибольшим количеством ёжиков
        int targetColor = 0;
        if (population[1] > population[targetColor])
            targetColor = 1;
        if (population[2] > population[targetColor])
            targetColor = 2;

        int total = population[0] + population[1] + population[2];

        // Если все уже одного цвета
        if (population[targetColor] == total)
            return 0;

        // Проверка на невозможность
        if ((population[0] % 2 + population[1] % 2 + population[2] % 2) % 2 != 0)
            return -1;

        int meetings = 0;

        // Пока хотя бы два цвета остаются с ненулевой популяцией
        while ((population[0] > 0 && population[1] > 0) ||
               (population[1] > 0 && population[2] > 0) ||
               (population[0] > 0 && population[2] > 0))
        {
            // Вручную выбираем два цвета, которые будут перекрашены в третий
            int first = -1, second = -1;

            if (population[0] > 0 && population[1] > 0)
            {
                first = 0;
                second = 1;
            }
            else if (population[1] > 0 && population[2] > 0)
            {
                first = 1;
                second = 2;
            }
            else if (population[0] > 0 && population[2] > 0)
            {
                first = 0;
                second = 2;
            }

            // Если не удалось найти два цвета, цикл завершается
            if (first == -1 || second == -1)
                break;

            // Третий цвет — тот, в который перекрашиваются два выбранных
            int third = 3 - first - second;

            // Перекрашиваем двух ёжиков в третий цвет
            population[first]--;
            population[second]--;
            population[third] += 2;

            // Увеличиваем счётчик встреч
            meetings++;
        }

        // Проверяем, удалось ли достичь целевого цвета
        if (population[targetColor] == total)
            return meetings;

        return -1; // Если не удалось достичь цели
    }

    public static void Main()
    {
        // Пример данных
        int[] population = { 8, 1, 9 }; // 8 красных, 1 зелёный, 9 синих

        int result = MinMeetingsToUnify(population);
        Console.WriteLine(result); // Вывод количества итераций
    }
}
