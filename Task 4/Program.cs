class Task4
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Указан неверный файл");
            return;
        }

        string userDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(userDesktopPath, args[0]);
        int[] nums;

        try
        {
            nums = File.ReadAllLines(filePath).Select(int.Parse).ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
            return;
        }

        if (nums.Length == 0)
        {
            Console.WriteLine("Файл пуст");
            return;
        }

        Array.Sort(nums);
        int median = nums[nums.Length / 2];

        int moves = nums.Sum(num => Math.Abs(num - median));

        Console.WriteLine(moves);
    }
}
