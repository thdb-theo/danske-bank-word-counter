// See https://aka.ms/new-console-template for more information

namespace WordCounter;


class WordCounterProgram
{
    static void Main(string[] args) {
        // print hello world
        if (args.Length < 1 || args.Length > 1) {
            System.Console.WriteLine("Usage: WordCounter <path to directory>");
            return;       
        }
        string path = args[0];
    
        Excluder excluder = new Excluder(new string[] {$"{path}\\exclude.txt"});
        DirectoryReader reader = new DirectoryReader(path, excluder);
        Sorter sorter = new Sorter(reader);
        Writer writer = new Writer(sorter, path);

        Counter wordCounter = new Counter(reader, excluder, sorter, writer);

        wordCounter.Count();

    }
}

