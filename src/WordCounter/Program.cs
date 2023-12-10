//TODO:
//2. handle large files - memory
//3. Profiling
//4. Refactor in different classes

using System.Collections.Concurrent;
using System.Diagnostics;

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

var wordCounts = new ConcurrentDictionary<string, int>();

var sw = Stopwatch.StartNew();
await CountWords(filePaths, wordCounts);
sw.Stop();
PrintWords(wordCounts);
Console.WriteLine(sw.ElapsedMilliseconds);

static async Task CountWords(string[] filePaths, ConcurrentDictionary<string, int> wordCounts)
{
    var processorCount = Environment.ProcessorCount;
    await Parallel.ForEachAsync(filePaths, new ParallelOptions { MaxDegreeOfParallelism = processorCount }, async (filePath, _) =>
    {
        var lines = File.ReadLines(filePath);

        await Parallel.ForEachAsync(lines, new ParallelOptions { MaxDegreeOfParallelism = processorCount }, async (line, _) =>
        {
            var words = line.Split(" ");

            await Parallel.ForEachAsync(words, new ParallelOptions { MaxDegreeOfParallelism = processorCount }, (word, _) =>
            {
                var sanitizedWord = SanitizeWord(word);
                
                wordCounts.AddOrUpdate(sanitizedWord, 1, (_, oldValue) => oldValue + 1);
                return ValueTask.CompletedTask;
            });
        });
    });
}

static string SanitizeWord(string word)
{
   return new string(word.Where(c => !char.IsPunctuation(c)).ToArray());
}

static void PrintWords(IDictionary<string, int> wordCounts)
{
    foreach (var wordCount in wordCounts)
    {
        Console.WriteLine($"{wordCount.Value}: {wordCount.Key}");
    }
}