using Xunit;
using WordCounter;
using System.IO;
using System;

namespace TestWordCounter;

public class UnitTestWriter : IDisposable
{
    // need to use different file names for each test to avoid IO errors
    readonly static string name = "UnitTestWriter";
    readonly static string filesDirectoryName = $"files-{name}";
    public UnitTestWriter() {
        Directory.CreateDirectory(filesDirectoryName);
        File.WriteAllText($"{filesDirectoryName}/file1.txt", "alice bob alice andy");

    }
    [Fact]
    public void TestWriterWrite() {
        Excluder excluder = new Excluder(new string[] {});
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);
        Sorter sorter = new Sorter(reader);
        sorter.CountWords();
        sorter.SortByLetter();

        Writer writer = new Writer(sorter, filesDirectoryName);
        writer.Write();

        Assert.Equal("ALICE 2\nANDY 1", File.ReadAllText($"{filesDirectoryName}/output/FILE_A.txt"));
        Assert.Equal("BOB 1", File.ReadAllText($"{filesDirectoryName}/output/FILE_B.txt"));
        Assert.Equal("", File.ReadAllText($"{filesDirectoryName}/output/FILE_C.txt"));

    }

    public void Dispose() {
        Directory.Delete(filesDirectoryName, true);
    }
}