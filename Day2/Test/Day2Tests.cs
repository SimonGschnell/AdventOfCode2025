using NUnit.Framework.Constraints;

namespace Day2;


public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CanAccessStartAndEndFromProductRangeCorrectly()
    {
        ProductIdRange range = new ProductIdRange("12-15");
        Assert.That(range.Start, Is.EqualTo(12));
        Assert.That(range.End, Is.EqualTo(15));
    }
    
    [Test]
    public void RangeContainsInvalidIDWith2Digits()
    {
        ProductIdRange range = new ProductIdRange("12-22");
        Assert.True(range.ContainsInvalidId());
    }
    
    [Test]
    public void RangeContainsInvalidIDWith3Digits()
    {
        ProductIdRange range = new ProductIdRange("999-1011");
        Assert.True(range.ContainsInvalidId());
    }
    
    [Test]
    public void CountTimesOfInvalidIDCorrectly()
    {
        ProductIdRange range = new ProductIdRange("10-34");
        Assert.That(range.InvalidIdCount, Is.EqualTo(3));
    }
    
    [Test]
    public void SumOfInvalidIdsIsCorrect()
    {
        ProductIdRange range = new ProductIdRange("10-34");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(11+22+33));
    }
    
    [Test]
    public void SumOfInvalidIdsForMultipleProductIdRanges()
    {
        ProductIdRanges ranges = new ProductIdRanges([new ProductIdRange("10-34"), new ProductIdRange("95-115")]);
        Assert.That(ranges.GetSumOfInvalidIds(), Is.EqualTo(11+22+33+99));
    }
    
    [Test]
    public void ReadsProductRangeFromFileCorrectly()
    {
        const string testInputFilePath = "Day2TestInput.txt";
        var content =File.ReadAllText(testInputFilePath);
        List<ProductIdRange> ranges = [];
        foreach (var productRange in content.Split(","))
        {
            ranges.Add(new ProductIdRange(productRange));
        }
        ProductIdRanges productRanges = new ProductIdRanges(ranges);
        Assert.That(productRanges.GetSumOfInvalidIds(), Is.EqualTo(11+22+33+99));
    }
    
    [Test]
    public void CalculatesDay2Part1SolutionCorrectly()
    {
        const string inputFilePath = "Day2Input.txt";
        var content =File.ReadAllText(inputFilePath);
        List<ProductIdRange> ranges = [];
        foreach (var productRange in content.Split(","))
        {
            ranges.Add(new ProductIdRange(productRange));
        }
        ProductIdRanges productRanges = new ProductIdRanges(ranges);
        Assert.That(productRanges.GetSumOfInvalidIds(), Is.EqualTo(54234399924));
    }
}


public class ProductIdRanges(List<ProductIdRange> ranges)
{
    private List<ProductIdRange> Ranges { get; } = ranges;

    public long GetSumOfInvalidIds()
    {
        return Ranges.Sum(range => range.GetSumOfInvalidIds());
    }
}

public class ProductIdRange
{
    public long End { get; }

    public long Start { get; }
    
    public int InvalidIdCount { get; set; }
    public List<long> InvalidIds { get; set; } = [];
    public ProductIdRange(string productRange)
    {
        var splitRange = productRange.Split("-");
        Start = long.Parse(splitRange[0]);
        End = long.Parse(splitRange[1]);
        UpdateInvalidIdCount();
    }


    public bool ContainsInvalidId()
    {
        return InvalidIdCount > 0;
    }

    private void UpdateInvalidIdCount()
    {
        for (var number = Start; number <= End; number++)
        {
            var numberString = number.ToString();
            if (numberString.Length % 2 == 0)
            {
                var firstPart = numberString.Substring(0, numberString.Length / 2);
                var secondPart = numberString.Substring(numberString.Length / 2);
                if (firstPart == secondPart)
                {
                    InvalidIds.Add(number);
                    InvalidIdCount++;
                }
            }
        }
    }

    public long GetSumOfInvalidIds()
    {
        return InvalidIds.Sum();
    }
}