using System.Collections;
using System.IO;

namespace WordCounter;

public class DirectoryReader {
    public List<string>? Words;
    private readonly Excluder Excluder;
    private readonly string Path;
    public DirectoryReader(string path, Excluder excluder) {
        if (!Directory.Exists(path)) {
            throw new DirectoryNotFoundException("Directory does not exist");
        }
        string[] fileNames = Directory.GetFiles(path);
        Excluder = excluder;
        Path = path;

        Words = ReadFiles(fileNames);
    }

    /* Get list of all words in the files (with duplicates) */
    private List<string> ReadFiles(string[] files) {
        var allWords = new List<string>();
        foreach (string file in files) {
            // TODO: remove OS specific path separator
            if (Excluder.ExcludedFiles.Contains($"{file}")) {
                continue;
            }
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines) {
                string[] words = line.Split(' ');
                for (int i = 0; i < words.Length; i++) {
                    words[i] = words[i].ToLower();
                    // maybe remove punctuation here
                }
                allWords.AddRange(words);
            }
        }
        return allWords;
    }
}