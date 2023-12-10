//TODO:
//2. handle large files - memory

using System.Diagnostics;
using WordCounter;

var filePaths = new[]
{
    "/Users/plamen.kmetski/file1.txt",
    "/Users/plamen.kmetski/file2.txt",
    "/Users/plamen.kmetski/big.txt",
    "/Users/plamen.kmetski/moby_dick.txt",
    "/Users/plamen.kmetski/little_women.txt",
    "/Users/plamen.kmetski/frankenstein.txt",
    "/Users/plamen.kmetski/dracula.txt",
};


var sw = Stopwatch.StartNew();
var wordCounts = await new WordCounterService().CountWords(filePaths);
sw.Stop();
PrintWords(wordCounts);

Console.WriteLine(sw.ElapsedMilliseconds);

static void PrintWords(IDictionary<string, int> wordCounts)
{
    foreach (var wordCount in wordCounts)
    {
        Console.WriteLine($"{wordCount.Value}: {wordCount.Key}");
    }
}