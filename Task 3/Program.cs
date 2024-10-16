using System.Text.Json;
class Task3
{
    public class Test
    {
        public int TestId { get; set; }
        public string? Title { get; set; }
        public string? Value { get; set; }
        public List<Test>? Values { get; set; }
    }

    public class ValueResult
    {
        public int ValueId { get; set; }
        public string? Value { get; set; }
    }

    public class ValueCollection
    {
        public List<ValueResult>? Values { get; set; }
    }

    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Ошибка: необходимо 3 аргумента");
            return;
        }

        string userDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string valuesPath = Path.Combine(userDesktopPath, args[0]);
        string testsPath = Path.Combine(userDesktopPath, args[1]);
        string reportPath = Path.Combine(userDesktopPath, args[2]);

        ValueCollection? valueCollection = ReadValues(valuesPath);

        if (valueCollection == null)
        {
            Console.WriteLine("Не удалось прочитать файл values.json");
            return;
        }

        List<Test>? tests = ReadTests(testsPath);

        if (tests == null)
        {
            Console.WriteLine("Не удалось прочитать файл tests.json");
            return;
        }

        FillTestValues(tests, valueCollection);

        CreateReportFile(tests, reportPath);

        Console.WriteLine("Файл report.json успешно создан");
    }

    static ValueCollection? ReadValues(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<ValueCollection>(json);
    }

    static List<Test>? ReadTests(string path)
    {
        string json = File.ReadAllText(path);
        var document = JsonSerializer.Deserialize<Dictionary<string, List<Test>>>(json);

        if (document == null || !document.ContainsKey("tests"))
        {
            return null;
        }

        return document["tests"];
    }

    static void FillTestValues(List<Test> tests, ValueCollection valueCollection)
    {
        foreach (var test in tests)
        {
            var result = valueCollection.Values?.Find(v => v.ValueId == test.TestId);
            if (result != null)
            {
                test.Value = result.Value;
            }

            if (result != null)
            {
                test.Value = result.Value;
            }

            if (test.Values != null && test.Values.Count > 0)
            {
                FillTestValues(test.Values, valueCollection);
            }
        }
    }

    static void CreateReportFile(List<Test> tests, string path)
    {
        var reportData = new Dictionary<string, List<Test>> { { "tests", tests } };

        string json = JsonSerializer.Serialize(reportData, new JsonSerializerOptions { WriteIndented = true });

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        File.WriteAllText(path, json);
    }
}
