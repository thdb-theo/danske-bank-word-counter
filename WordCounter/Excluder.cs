using System.IO;

namespace WordCounter;

public class Excluder {
    public HashSet<string> ExcludedWords;
    public string[] ExcludedFiles;
    string? Result;
    public Excluder(string[] excludeFiles) {
        var allText = new HashSet<string>();
        foreach (string fileName in excludeFiles) {
            if (!File.Exists(fileName)) {
                throw new FileNotFoundException("File does not exist");
            }
            string text = File.ReadAllText(fileName);
            string[] words = text.Split(' ');
            foreach (string word in words) {
                allText.Add(word);
            }
        }
        ExcludedFiles = excludeFiles;
        ExcludedWords = allText;
    }

    /* Remove the exluded words from the readers list of words */
    public void Exclude(DirectoryReader reader) {
        if (reader.Words == null) {
            throw new Exception("No files to exclude");
        }
        int originalLength = reader.Words.Count;
 
        reader.Words.RemoveAll(word => ExcludedWords.Contains(word));

        int NumberOfWordsExcluded = originalLength - reader.Words.Count;
        Result = $"{NumberOfWordsExcluded}";
    }

    /* Save the result to a file */
    public void SaveResult() {
        if (Result == null) {
            throw new Exception("No result to save");
        }
        File.WriteAllText("result.txt", Result);
    }
}