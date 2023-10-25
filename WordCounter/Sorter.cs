namespace WordCounter;


public class Sorter {
    private readonly List<string> Words;
    readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    public Dictionary<char, HashSet<string>>? WordsByLetter;
    public Dictionary<string, int>? WordCount;

    public Sorter(DirectoryReader reader) {
        if (reader.Words == null) {
            throw new Exception("No words to sort");
        }
        Words = reader.Words;
        // concatenate all words from all files
    }

    /* Sort the words into a hashmap with a key for each letter in the alphabet
    the value is a hashset of the words beginning with that letter */
    public void SortByLetter() {
        // We avoid N^2 complexity by sorting the words first
        // We can then iterate through the words once
        var SortedWords = Words.OrderBy(word => word).ToList();
        SortedWords.RemoveAll(word => word == "");

        WordsByLetter = new Dictionary<char, HashSet<string>>();
        int WordIdx = 0;
        foreach (char letter in Alphabet) {
            WordsByLetter.Add(letter, new HashSet<string>());
            
            // while the first letter of the word is the same as the current letter
            // and there are still words left to iterate through
            while (WordIdx < SortedWords.Count && SortedWords[WordIdx][0] == letter) {
                string word = SortedWords[WordIdx];
                WordsByLetter[letter].Add(word);
                WordIdx++;
            }
        }
    }

    /* Contruct a Dictionary with how many times each word occurs */
    public void CountWords() {
        if (Words == null) {
            throw new Exception("No words to count");
        }
        WordCount = new Dictionary<string, int>();
        foreach (string word in Words) {
            if (WordCount.ContainsKey(word)) {
                WordCount[word]++;
            } else {
                WordCount.Add(word, 1);
            }
        }
    }

    /* Get the words in the desired output format */
    public Dictionary<char, string> GetResult() {
        if (WordsByLetter == null || WordCount == null) {
            throw new Exception("No words to get result from. have you called CountWords() and SortByLetter()?");
        }
        var result = new Dictionary<char, string>();
        foreach (var letter in WordsByLetter) {
            var letterResult = new List<string>();
            foreach (var word in letter.Value) {
                if (WordCount.ContainsKey(word)) {
                    letterResult = letterResult.Append($"{word} {WordCount[word]}").ToList();
                } else {
                    throw new Exception("Word not found");
                }
            }
            result.Add(letter.Key, string.Join("\n", letterResult));
        }
        return result;
    }
}