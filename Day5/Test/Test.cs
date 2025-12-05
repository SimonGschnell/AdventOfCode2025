namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IngredientIsSpoiled()
    {
        FreshIngredientRanges ranges = new FreshIngredientRanges().WithFreshIngredientRange(new IngredientRange(1,3));
        Assert.That(ranges.IsIngredientFresh(new Ingredient(4)), Is.False);
    }
    
    [Test]
    public void IngredientIsFresh()
    {
        FreshIngredientRanges ranges = new FreshIngredientRanges().WithFreshIngredientRange(new IngredientRange(5,10));
        Assert.That(ranges.IsIngredientFresh(new Ingredient(8)), Is.True);
    }
    
    [Test]
    public void AssertThatTheTestSetsOfDay5Pass()
    {
        //3-5
        //10-14
        //16-20
        //12-18
        FreshIngredientRanges ranges = new FreshIngredientRanges()
            .WithFreshIngredientRange(new IngredientRange(3, 5))
            .WithFreshIngredientRange(new IngredientRange(10, 14))
            .WithFreshIngredientRange(new IngredientRange(16, 20))
            .WithFreshIngredientRange(new IngredientRange(12, 18));
        // 1
        // 5
        // 8
        // 11
        // 17
        // 32
        Assert.That(ranges.IsIngredientFresh(new Ingredient(1)), Is.False);
        Assert.That(ranges.IsIngredientFresh(new Ingredient(5)), Is.True);
        Assert.That(ranges.IsIngredientFresh(new Ingredient(8)), Is.False);
        Assert.That(ranges.IsIngredientFresh(new Ingredient(11)), Is.True);
        Assert.That(ranges.IsIngredientFresh(new Ingredient(17)), Is.True);
        Assert.That(ranges.IsIngredientFresh(new Ingredient(32)), Is.False);
    }
    
    [Test]
    public void CheckThatFridgeSystemWorksCorrectly()
    {
        FreshIngredientRanges ranges = new FreshIngredientRanges()
            .WithFreshIngredientRange(new IngredientRange(3, 5))
            .WithFreshIngredientRange(new IngredientRange(10, 14))
            .WithFreshIngredientRange(new IngredientRange(16, 20))
            .WithFreshIngredientRange(new IngredientRange(12, 18));
        FridgeSystem system = new FridgeSystem(ranges);
        system
            .WithIngredient(new Ingredient(1))
            .WithIngredient(new Ingredient(5))
            .WithIngredient(new Ingredient(8))
            .WithIngredient(new Ingredient(11))
            .WithIngredient(new Ingredient(17))
            .WithIngredient(new Ingredient(32));
        Assert.That(system.FreshIngredientCount, Is.EqualTo(3));
    }
    
    [Test]
    public void ConvertsStringToIngredientRange()
    {
        var stringRange = "3-5";
        var range = new IngredientRange(stringRange);
        Assert.That(range.Start , Is.EqualTo(3));
        Assert.That(range.End , Is.EqualTo(5));
    }
    
    [Test]
    public void ConvertsStringToIngredient()
    {
        var stringIngredient = "4";
        var ingredient = new Ingredient(stringIngredient);
        Assert.That(ingredient.Value , Is.EqualTo(4));
    }
    
    [Test]
    public void ReadsRangesAndIngredientsFromFile()
    {
        FridgeSystem fridge = new FridgeSystem();
        FreshIngredientRanges ranges = new FreshIngredientRanges();
        const string testInputFilePath = "Day5TestInput.txt";
        FillFridgeAndRanges(testInputFilePath, fridge, ranges);

        Assert.That(fridge.FreshIngredientCount, Is.EqualTo(3));
    }
    
    [Test]
    public void ComputesDay5Part1Correctly()
    {
        FridgeSystem fridge = new FridgeSystem();
        FreshIngredientRanges ranges = new FreshIngredientRanges();
        const string inputFilePath = "Day5Input.txt";
        FillFridgeAndRanges(inputFilePath, fridge, ranges);

        Assert.That(fridge.FreshIngredientCount, Is.EqualTo(868));
    }
    
    [Test]
    public void ComputesDay5Part2Correctly()
    {
        FreshIngredientRanges ranges = new FreshIngredientRanges();
        const string inputFilePath = "Day5Input.txt";
        var ingredientRanges = File.ReadAllText(inputFilePath).Split("\n").ToList();
        foreach (var range in ingredientRanges)
        {
            if (range == "\r") break; 
            ranges.WithFreshIngredientRange(new IngredientRange(range));
        }

        Assert.That(ranges.CalculateNumber(), Is.EqualTo(354143734113772));
    }

    private static void FillFridgeAndRanges(string testInputFilePath, FridgeSystem fridge, FreshIngredientRanges ranges)
    {
        var splitInput = File.ReadAllText(testInputFilePath).Split("\n").ToList();
        foreach (var line in splitInput)
        {
            if (string.IsNullOrEmpty(line.Replace("\r","ingredients")))
            {
                fridge.Ranges = ranges;
                break;
            }
            ranges.WithFreshIngredientRange(new IngredientRange(line));
        }
        
        var ingredients = splitInput.Slice(splitInput.IndexOf("ingredients"), splitInput.Count - splitInput.IndexOf("ingredients"));
        foreach (var ingredient in ingredients)
        {
            fridge.WithIngredient(new Ingredient(ingredient));
        }
        
    }
}



public class FridgeSystem
{

    public int FreshIngredientCount { get; set; } = 0;

    public FridgeSystem(FreshIngredientRanges ranges)
    {
        Ranges = ranges;
    }

    public FridgeSystem()
    {
        
    }

    public FreshIngredientRanges Ranges { get; set; }

    public FridgeSystem WithIngredient(Ingredient ingredient)
    {
        ListOfIngredients.Add(ingredient);
        if (Ranges.IsIngredientFresh(ingredient))
        {
            FreshIngredientCount++;
        }
        return this;
    }

    public List<Ingredient> ListOfIngredients { get; set; } = [];
}

public class Ingredient
{
    public long Value { get; set; }

    public Ingredient(long value)
    {
        Value = value;
    }

    public Ingredient(string stringIngredient)
    {
        Value = long.Parse(stringIngredient);
    }
}

public class FreshIngredientRanges
{

    public long ValidFreshIngredientValues { get; set; } = 0;

    public FreshIngredientRanges WithFreshIngredientRange(IngredientRange range)
    {
        
        Ranges.Add(range);
        return this;
    }
    
    public long CalculateNumber()
    {
        var ListOfMergedRanges = new List<IngredientRange>();
        var sortedRanges = Ranges.OrderBy(r => r.Start).ToList();
        var currentRange = sortedRanges[0];
        foreach (var range in sortedRanges)
        {
            if (range.Start > currentRange.End)
            {
                ListOfMergedRanges.Add(currentRange);
                currentRange = range;
                continue;
            }
            if (range.Start >= currentRange.Start && range.Start <= currentRange.End)
            {
                if (range.End > currentRange.End)
                    currentRange = new IngredientRange(currentRange.Start, range.End);
            }
        }
        ListOfMergedRanges.Add(currentRange);

        long result = 0;
        foreach (var range in ListOfMergedRanges)
        {
            result += range.End - range.Start + 1;
        }

        return result;
    }

    public List<IngredientRange> Ranges { get; set; } = [];

    public bool IsIngredientFresh(Ingredient ingredient)
    {
        var ingredientIsSpoiled = true;
        foreach (var range in Ranges)
        {
            if (ingredient.Value >= range.Start && ingredient.Value <= range.End)
            {
                ingredientIsSpoiled = false;
            }
        }

        return !ingredientIsSpoiled;
    }
}

public class IngredientRange
{
    public IngredientRange(long start, long end)
    {
        Start = start;
        End = end;
    }

    public IngredientRange(string stringRange)
    {
        var splitRange = stringRange.Split("-");
        Start = long.Parse(splitRange[0]);
        End = long.Parse(splitRange[1]);
    }

    public long Start { get; set; }
    public long End { get; set; }
}


