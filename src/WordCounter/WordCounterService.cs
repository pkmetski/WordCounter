using System.Collections.Concurrent;

namespace WordCounter;

public class WordCounterService
{
    public async Task<IDictionary<string, int>> CountWords(string[] filePaths)
    {
        var wordCounts = new ConcurrentDictionary<string, int>();
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        await Parallel.ForEachAsync(filePaths, parallelOptions, async (filePath, _) =>
        {
            var lines = File.ReadLines(filePath);

            await Parallel.ForEachAsync(lines, parallelOptions, async (line, _) =>
            {
                var words = line.Split(" ");

                await Parallel.ForEachAsync(words, parallelOptions, (word, _) =>
                {
                    var sanitizedWord = SanitizeWord(word);
                    wordCounts.AddOrUpdate(sanitizedWord, 1, (_, oldValue) => oldValue + 1);

                    return ValueTask.CompletedTask;
                });
            });
        });

        return wordCounts;
    }

    /// <summary>
    /// Removes punctuation characters from the string provided
    /// </summary>
    private string SanitizeWord(string word)
    {
        return new string(word.Where(c => !char.IsPunctuation(c)).ToArray());
    }
}