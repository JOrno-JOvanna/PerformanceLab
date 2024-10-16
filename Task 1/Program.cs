class Task1
{
    static void Main(string[] args)
    {
        int n, m;

        while (true)
        {
            string? stringN = args[0];

            if (int.TryParse(stringN, out n) && n >= 1)
                break;

            Console.WriteLine("Размером массива может быть только целое число больше 0");
        }

        while (true)
        {
            string? stringM = args[1];

            if (int.TryParse(stringM, out m) && m > 0)
                break;

            Console.WriteLine("Длина хода должна быть целым числом больше 0");
        }

        int[] circArray = new int[n];
        for (int i = 0; i < n; i++)
        {
            circArray[i] = i + 1;
        }

        var path = new List<int>();
        int startIndex = 0;

        do
        {
            path.Add(circArray[startIndex]);
            startIndex = (startIndex + m - 1) % n;
        }
        while (startIndex != 0);

        foreach (int step in path)
        {
            Console.Write(step);
        }
        Console.WriteLine();
    }
}