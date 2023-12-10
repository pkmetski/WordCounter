using System.Diagnostics;
using WordCounter;

var files = new[]
{
    "file1.txt",
    "file2.txt",
};


var sw = Stopwatch.StartNew();
var wordCounts = await new WordCounterService().CountWords(files);
sw.Stop();
PrintWords(wordCounts);

Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} (ms)");

static void PrintWords(IDictionary<string, int> wordCounts)
{
    foreach (var wordCount in wordCounts)
    {
        Console.WriteLine($"{wordCount.Value}: {wordCount.Key}");
    }
}