namespace WordCounter;

class Counter {
    readonly Excluder Excluder;
    readonly DirectoryReader Reader;

    readonly Sorter Sorter;

    readonly Writer Writer;

    public Counter(DirectoryReader reader, Excluder excluder, Sorter sorter, Writer writer) {
        Excluder = excluder;
        Reader = reader;
        Sorter = sorter;
        Writer = writer;

    }

    public void Count() {
        Excluder.Exclude(Reader);
        Excluder.SaveResult();

        Sorter.SortByLetter();
        Sorter.CountWords();

        Writer.Write();
    }
}