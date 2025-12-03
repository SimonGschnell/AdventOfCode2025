using NUnit.Framework.Constraints;

namespace Test;

public class Tests
{

    [Test]
    public void BatteryIsCorrectlyCreated()
    {
        Battery battery = new Battery(8);
        Assert.That(battery.Joltage, Is.EqualTo(8));
    }
    
    [Test]
    public void BatteryJoltageCanNotBeGeaterThan9()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Battery battery = new Battery(10);
        });
        Assert.Throws<ArgumentException>(() =>
        {
            Battery battery = new Battery(99);
        });
        Assert.Throws<ArgumentException>(() =>
        {
            Battery battery = new Battery(111);
        });
    }
    
    [Test]
    public void BatteryBankIsCreatedCorrectly()
    {
        var batteryBank = new BatteryBank([
        new Battery(1),
        new Battery(2),
        new Battery(3),
        ]);
        Assert.That(batteryBank.Batteries, Has.Count.EqualTo(3));
    }
    
    [Test]
    public void JoltedProducedByBankIsCorrectlyCalculated()
    {
        var batteryBank = new BatteryBank([
            new Battery(8),
            new Battery(1),
            new Battery(9),
        ]);
        Assert.That(batteryBank.GetJoltage(), Is.EqualTo(89));
    }
    
    [Test]
    public void CorrectlyConvertsStringIntoBatteryBank()
    {
        var batteryBankString = "987654321111111";
        var batteryBank = BatteryBank.Create(batteryBankString);
        Assert.That(batteryBank.GetJoltage(), Is.EqualTo(98));
        Assert.That(batteryBank.Batteries, Has.Count.EqualTo(15));
    }

    [Test]
    public void CorrectlyCreatesBatteryCluster()
    {
        BatteryCluster cluster = new BatteryCluster([
            BatteryBank.Create("987654321111111"),
            BatteryBank.Create("987654321111111"),
            BatteryBank.Create("987654321111111")
        ]);
        Assert.That(cluster.GetJoltage(), Is.EqualTo(98+98+98));
        Assert.That(cluster.BatteryBanks, Has.Count.EqualTo(3));
    }
    
    [Test]
    public void CorrectlyCalculatesTheChallengeTestSets()
    {
        Assert.That(BatteryBank.Create("987654321111111").GetJoltage(), Is.EqualTo(98));
        Assert.That(BatteryBank.Create("811111111111119").GetJoltage(), Is.EqualTo(89));
        Assert.That(BatteryBank.Create("234234234234278").GetJoltage(), Is.EqualTo(78));
        Assert.That(BatteryBank.Create("818181911112111").GetJoltage(), Is.EqualTo(92));
    }
    
    [Test]
    public void CorrectlyCalculatesTheResultsOfDay3ChallengePart1()
    {
        BatteryCluster cluster = new BatteryCluster([]);
        foreach (var batteries in File.ReadAllLines("Day3Input.txt"))
        {
            cluster.BatteryBanks.Add(BatteryBank.Create(batteries));
        }

        Assert.That(cluster.GetJoltage(), Is.EqualTo(17158));
    }
}

public class BatteryCluster
{
    public List<BatteryBank> BatteryBanks { get; }

    public BatteryCluster(List<BatteryBank> batteryBanks)
    {
        BatteryBanks = batteryBanks;
    }

    public int GetJoltage()
    {
        return BatteryBanks.Sum(batteryBank => batteryBank.GetJoltage());
    }
}

public class BatteryBank
{
    public List<Battery> Batteries { get; }

    public BatteryBank(List<Battery> batteries)
    {
        Batteries = batteries;
    }

    public int GetJoltage()
    {
        int greatesBatteryCombination = 0;
        foreach (var firstBattery in Batteries)
        {
            foreach (var secondBattery in Batteries)
            {
                if (secondBattery == firstBattery || Batteries.IndexOf(firstBattery) > Batteries.IndexOf(secondBattery))
                {
                    continue;
                }

                var batteriesJoltage = int.Parse($"{firstBattery.Joltage}{secondBattery.Joltage}");
                if (batteriesJoltage > greatesBatteryCombination)
                {
                    greatesBatteryCombination = batteriesJoltage;
                }
            }
        }

        return greatesBatteryCombination;
    }

    private int GetBatteryJoltage(int position)
    {
        return Batteries[position].Joltage;
    }

    public static BatteryBank Create(string batteryBankString)
    {
        BatteryBank batteryBank = new BatteryBank([]);
        foreach (var batteryCharacter in batteryBankString)
        {
            batteryBank.Batteries.Add(new Battery(int.Parse(batteryCharacter.ToString())));
        }

        return batteryBank;
    }
}

public class Battery
{
    public Battery(int joltage)
    {
        if (joltage is <= 0 or >= 10) throw new ArgumentException("Joltage value must be 1-9");
        Joltage = joltage;
    }

    public int Joltage { get; }
}
