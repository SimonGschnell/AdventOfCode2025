using NUnit.Framework.Constraints;

namespace Day2;
public class Day2TestsPart2
{
    [Test]
    public void OddNumbersAreCalculatedCorrectly()
    {
        ProductIdRange2 range = new ProductIdRange2("554-558");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(555));
    }
    
    [Test]
    public void RangesWith2PartsAreCalculatedCorrectly()
    {
        ProductIdRange2 range = new ProductIdRange2("2221-2226");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(2222));
    }
    
    [Test]
    public void RangesWith3PartsAreCalculatedCorrectly()
    {
        ProductIdRange2 range = new ProductIdRange2("123123122-123123127");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(123123123));
    }
    
    [Test]
    public void RangesWith4PartsAreCalculatedCorrectly()
    {
        ProductIdRange2 range = new ProductIdRange2("12121211-12121215");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(12121212));
    }
    
    [Test]
    public void RangesWith5PartsAreCalculatedCorrectly()
    {
        ProductIdRange2 range = new ProductIdRange2("1212121211-1212121215");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(1212121212));
    }
    
    [Test]
    public void CalculatesSingleDigitsCorrectly()
    {
        ProductIdRange2 range = new ProductIdRange2("1-21");
        Assert.That(range.GetSumOfInvalidIds(), Is.EqualTo(11));
    }
    
    [Test]
    public void AllTestsFromDay2ChallengePass()
    {
        Assert.That(new ProductIdRange2("11-22").GetSumOfInvalidIds(), Is.EqualTo(11+22));
        Assert.That(new ProductIdRange2("95-115").GetSumOfInvalidIds(), Is.EqualTo(99+111));
        Assert.That(new ProductIdRange2("998-1012").GetSumOfInvalidIds(), Is.EqualTo(999+1010));
        Assert.That(new ProductIdRange2("1188511880-1188511890").GetSumOfInvalidIds(), Is.EqualTo(1188511885));
        Assert.That(new ProductIdRange2("222220-222224").GetSumOfInvalidIds(), Is.EqualTo(222222));
        Assert.That(new ProductIdRange2("1698522-1698528").GetSumOfInvalidIds(), Is.EqualTo(0));
        Assert.That(new ProductIdRange2("446443-446449").GetSumOfInvalidIds(), Is.EqualTo(446446));
        Assert.That(new ProductIdRange2("38593856-38593862").GetSumOfInvalidIds(), Is.EqualTo(38593859));
        Assert.That(new ProductIdRange2("565653-565659").GetSumOfInvalidIds(), Is.EqualTo(565656));
        Assert.That(new ProductIdRange2("824824821-824824827").GetSumOfInvalidIds(), Is.EqualTo(824824824));
        Assert.That(new ProductIdRange2("2121212118-2121212124").GetSumOfInvalidIds(), Is.EqualTo(2121212121));
    }
    
    [Test]
    public void SumOfRangesFromDay2ChallengePass()
    {
        ProductIdRanges2 productRanges = new ProductIdRanges2([
            new ProductIdRange2("11-22"),
            new ProductIdRange2("95-115"),
            new ProductIdRange2("998-1012"),
            new ProductIdRange2("1188511880-1188511890"),
            new ProductIdRange2("222220-222224"),
            new ProductIdRange2("1698522-1698528"),
            new ProductIdRange2("446443-446449"),
            new ProductIdRange2("38593856-38593862"),
            new ProductIdRange2("565653-565659"),
            new ProductIdRange2("824824821-824824827"),
            new ProductIdRange2("2121212118-2121212124")]);
        Assert.That(productRanges.GetSumOfInvalidIds(), Is.EqualTo(4174379265));
    }
    
    [Test]
    public void CalculatesDay2Part2SolutionCorrectly()
    {
        const string inputFilePath = "Day2Input.txt";
        var content =File.ReadAllText(inputFilePath);
        List<ProductIdRange2> ranges = [];
        foreach (var productRange in content.Split(","))
        {
            ranges.Add(new ProductIdRange2(productRange));
        }
        ProductIdRanges2 productRanges = new ProductIdRanges2(ranges);
        Assert.That(productRanges.GetSumOfInvalidIds(), Is.EqualTo(70187097315));
    }
}

public class ProductIdRanges2(List<ProductIdRange2> ranges)
{
    private List<ProductIdRange2> Ranges { get; } = ranges;

    public long GetSumOfInvalidIds()
    {
        return Ranges.Sum(range => range.GetSumOfInvalidIds());
    }
}

public class ProductIdRange2
{
    public long End { get; }

    public long Start { get; }
    
    public int InvalidIdCount { get; set; }
    public List<long> InvalidIds { get; set; } = [];
    public ProductIdRange2(string productRange)
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
            for (int lengthToUse = 1; lengthToUse <= numberString.Length / 2; lengthToUse++)
            {
                if ((numberString.Length % lengthToUse) != 0)
                {
                    continue;
                }

                List<string> partsList = []; 
                for (int index = 0; index < numberString.Length; index+=lengthToUse)
                {
                    partsList.Add(numberString.Substring(index, lengthToUse));
                }
                if (partsList.Count > 1 && AllItemsAreEqual(partsList))
                {
                    TrackInvalidId(number);
                    break;
                }
                
            }
        }
    }

    private void TrackInvalidId(long number)
    {
        InvalidIds.Add(number);
        InvalidIdCount++;
    }

    private static bool AllItemsAreEqual(List<string> eachDigit)
    {
        var digitToCompareTo = eachDigit[0];
        var allDigitsAreEqual = true;
        foreach (var digit in eachDigit)
        {
            if (digit != digitToCompareTo)
            {
                allDigitsAreEqual = false;
            }
        }

        return allDigitsAreEqual;
    }

    public long GetSumOfInvalidIds()
    {
        return InvalidIds.Sum();
    }
}