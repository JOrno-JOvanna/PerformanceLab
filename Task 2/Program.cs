class Task2
{
    static void Main(string[] args)
    {
        string userDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string circleFilePath = Path.Combine(userDesktopPath, args[0]);
        string pointsFilePath = Path.Combine(userDesktopPath, args[1]);

        if (!File.Exists(circleFilePath) || !File.Exists(pointsFilePath))
        {
            Console.WriteLine("Файл не найден");
            return;
        }

        string[] circleData = File.ReadAllLines(circleFilePath);
        if (circleData.Length < 2)
        {
            Console.WriteLine("Недостаточно данных в файле окружности");
            return;
        }

        string[] circleCenter = circleData[0].Split();
        if (circleCenter.Length < 2 || !TryParseCoordinate(circleCenter[0], out double circleX) || !TryParseCoordinate(circleCenter[1], out double circleY))
        {
            Console.WriteLine("Ошибка в координатах центра окружности");
            return;
        }

        if (!double.TryParse(circleData[1], out double radius) || radius == 0)
        {
            Console.WriteLine("Некорректный радиус окружности");
            return;
        }

        string[] pointsData = File.ReadAllLines(pointsFilePath);
        if (pointsData.Length < 1)
        {
            Console.WriteLine("Недостаточно точек");
            return;
        }
        else if(pointsData.Length > 100)
        {
            Console.WriteLine("Слишком много точек");
            return;
        }

        foreach (var point in pointsData)
        {
            string[] coordinates = point.Split();
            if (coordinates.Length < 2 || !TryParseCoordinate(coordinates[0], out double pointX) || !TryParseCoordinate(coordinates[1], out double pointY))
            {
                Console.WriteLine("Ошибка в координатах точки");
                continue;
            }

            double distanceSquared = (pointX - circleX) * (pointX - circleX) + (pointY - circleY) * (pointY - circleY);
            double radiusSquared = radius * radius;

            if (distanceSquared < radiusSquared)
            {
                Console.WriteLine(1);
            }
            else if (distanceSquared == radiusSquared)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(2);
            }
        }
    }

    static bool TryParseCoordinate(string value, out double result)
    {
        value = value.Trim();

        if (string.IsNullOrEmpty(value))
        {
            result = 0;
            return false;
        }

        if (double.TryParse(value, out result))
        {
            if(result >= Math.Pow(10, -38) && result <= Math.Pow(10, 38))
            {
                return true;
            }
            else
            {
                result = 0;
                return true;
            }
        }

        return false;
    }
}