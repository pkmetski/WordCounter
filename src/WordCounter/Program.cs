var filePaths = new[]
{
    "/Users/plamen.kmetski/file1.txt",
    "/Users/plamen.kmetski/file2.txt"
};

var wordCounts = new Dictionary<string, int>();

CountWords(filePaths, wordCounts);
PrintCounted(wordCounts);

static void CountWords(string[] filePaths, IDictionary<string, int> wordCounts)
{
    foreach (var filePath in filePaths)
    {
        var lines = File.ReadLines(filePath);
        foreach (var line in lines)
        {
            var words = line.Split(" ");
            foreach (var word in words)
            {
                if (!wordCounts.ContainsKey(word))
                {
                    wordCounts.Add(word, 0);
                }
                wordCounts[word]++;
            }
        }
    }
}

static void PrintCounted(IDictionary<string, int> wordCounts)
{
    foreach (var wordCount in wordCounts)
    {
        Console.WriteLine($"{wordCount.Value}: {wordCount.Key}");
    }
}