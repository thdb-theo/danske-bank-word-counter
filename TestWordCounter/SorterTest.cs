using Xunit;
using WordCounter;
using System.IO;
using System;

namespace TestWordCounter;

public class UnitTestSorter : IDisposable
{
    // need to use different file names for each test to avoid IO errors
    readonly static string name = "UnitTestSorter";
    readonly static string filesDirectoryName = $"files-{name}";
    public UnitTestSorter() {
        Directory.CreateDirectory(filesDirectoryName);
        File.WriteAllText($"{filesDirectoryName}/file1.txt", "alice bob alice andy");
    }
    [Fact]
    public void TestSorterCountWords() {

        Excluder excluder = new Excluder(new string[] {});
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);
    
        Sorter sorter = new Sorter(reader);
        sorter.CountWords();
        Assert.Equal(2, sorter.WordCount["alice"]);
        Assert.Equal(1, sorter.WordCount["bob"]);
        Assert.Equal(1, sorter.WordCount["andy"]);
        Assert.Equal(3, sorter.WordCount.Count);
    }

    [Fact]
    public void TestSorterSortByLetter() {
        Excluder excluder = new Excluder(new string[] {});
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);
    
        Sorter sorter = new Sorter(reader);
        sorter.CountWords();
        sorter.SortByLetter();
    
        Assert.Equal(new string[] { "alice", "andy" }, sorter.WordsByLetter['a']);
        Assert.Equal(new string[] { "bob" }, sorter.WordsByLetter['b']);
    }

    [Fact]
    public void TestSorterGetResult() {
        Excluder excluder = new Excluder(new string[] {});
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);
    
        Sorter sorter = new Sorter(reader);
        sorter.CountWords();
        sorter.SortByLetter();
        var result = sorter.GetResult();
        Assert.Equal("alice 2\nandy 1", result['a']);
        Assert.Equal("bob 1", result['b']);

    }
    public void Dispose() {
        File.Delete("result.txt");
        Directory.Delete(filesDirectoryName, true);
    }
}