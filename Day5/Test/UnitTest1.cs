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
        var splitInput = File.ReadAllText("Day5Input.txt").Split("\n").ToList();
        var startOfIngredients = false;
        FridgeSystem fridge = new FridgeSystem();
        FreshIngredientRanges ranges = new FreshIngredientRanges();
        foreach (var line in splitInput)
        {
            if (string.IsNullOrEmpty(line))
            {
                startOfIngredients = true;
                fridge.Ranges = ranges;
                continue;
            }
            if (!startOfIngredients)
            {
                ranges.WithFreshIngredientRange(new IngredientRange(line));
            }
            else
            {
                fridge.WithIngredient(new Ingredient(line));
            }
        }
        
        Assert.That(fridge.FreshIngredientCount, Is.EqualTo(3));
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
    public int Value { get; set; }

    public Ingredient(int value)
    {
        Value = value;
    }

    public Ingredient(string stringIngredient)
    {
        Value = int.Parse(stringIngredient);
    }
}

public class FreshIngredientRanges
{
    public FreshIngredientRanges()
    {
    }

    public FreshIngredientRanges WithFreshIngredientRange(IngredientRange range)
    {
        Ranges.Add(range);
        return this;
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
    public IngredientRange(int start, int end)
    {
        Start = start;
        End = end;
    }

    public IngredientRange(string stringRange)
    {
        var splitRange = stringRange.Split("-");
        Start = int.Parse(splitRange[0]);
        End = int.Parse(splitRange[1]);
    }

    public int Start { get; set; }
    public int End { get; set; }
}
