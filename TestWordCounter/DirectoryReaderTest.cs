using Xunit;
using WordCounter;
using System.IO;
using System;

namespace TestWordCounter;

public class UnitTestDirectoryReader : IDisposable
{
    // need to use different file names for each test to avoid IO errors
    readonly static string name = "UnitTestDirectoryReader";
    readonly static string excludeFileName = $"exclude-{name}.txt";
    readonly static string filesDirectoryName = $"files-{name}";

    public UnitTestDirectoryReader() {
        File.WriteAllText(excludeFileName, "");
        Directory.CreateDirectory(filesDirectoryName);
        File.WriteAllText($"{filesDirectoryName}/file1.txt", "a b c");
    }

    [Fact]
    public void TestDirectoryReaderInit()
    {
        Excluder excluder = new Excluder(new string[] { excludeFileName });
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);
        Assert.Equal(new string[] { "a", "b", "c"}, reader.Words);
    }

    [Fact]
    public void TestDirectoryReaderIgnoresExcludeFile() {
        File.WriteAllText($"{filesDirectoryName}/{excludeFileName}.txt", "d e");
        Excluder excluder = new Excluder(new string[] { $"{filesDirectoryName}\\{excludeFileName}.txt" });
        DirectoryReader reader = new DirectoryReader(filesDirectoryName, excluder);
        Assert.DoesNotContain($"d", reader.Words);
    }

    public void Dispose() {
        Console.WriteLine("Disposing");
        File.Delete(excludeFileName);
        File.Delete("result.txt");
        Directory.Delete(filesDirectoryName, true);
    }
}