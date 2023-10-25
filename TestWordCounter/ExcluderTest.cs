using Xunit;
using WordCounter;
using System.IO;
using System;

namespace TestWordCounter;

public class UnitTestExcluder : IDisposable
{
    // need to use different file names for each test to avoid IO errors
    readonly static string name = "UnitTestExclude";
    readonly static string excludeFileName = $"exclude-{name}.txt";
    readonly static string filesDirectoryName = $"files-{name}";
    public UnitTestExcluder() {
        File.WriteAllText(excludeFileName, "a b b c");
        Directory.CreateDirectory(filesDirectoryName);
        File.WriteAllText($"{filesDirectoryName}/file1.txt", "a b c d e f");
    }
    [Fact]
    public void TestExcluderInit()
    {
         // test excluded words
        Excluder excluder = new Excluder(new string[] { excludeFileName });
        Assert.Equal(new string[] { "a", "b", "c" }, excluder.ExcludedWords);
    }

    [Fact]
    public void TestExcluderExclude() {

        Excluder excluder = new Excluder(new string[] { excludeFileName });
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);

        excluder.Exclude(reader);
        Assert.Equal(reader.Words, new string[] { "d", "e", "f" });

    }

    [Fact]
    public void TestExcluderResult() {
        Excluder excluder = new Excluder(new string[] { excludeFileName });
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);

        excluder.Exclude(reader);
        excluder.SaveResult();
        Assert.Equal("3", File.ReadAllText("result.txt"));
    }

    public void Dispose() {
        File.Delete(excludeFileName);
        File.Delete("result.txt");
        Directory.Delete(filesDirectoryName, true);
    }
}