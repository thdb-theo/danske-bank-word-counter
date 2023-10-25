namespace WordCounter;


public class Writer {
    readonly Sorter Sorter;
    private readonly string Path;
    public Writer(Sorter sorter, string path) {
        Sorter = sorter;
        Path = path;
    }

    public void Write() {
        Dictionary<char, string> result = Sorter.GetResult();

        // create output directory if it doesn't exist
        if (!Directory.Exists($"{Path}/output")) {
            Directory.CreateDirectory($"{Path}/output");
        }
        foreach (var letter in result) {
            string fileName = $"{Path}/output/FILE_{letter.Key.ToString().ToUpper()}.txt";
            File.WriteAllText(fileName, letter.Value.ToString().ToUpper());
        }
    }
}