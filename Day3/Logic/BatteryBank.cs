namespace Logic;

public class BatteryBank
{
    public List<Battery> Batteries { get; }

    public BatteryBank(List<Battery> batteries)
    {
        Batteries = batteries;
    }

    public long GetJoltage(int N=2)
    {
        //818181911112111
        var greatesBatteryCombination = 0;
        
        
        var result = new List<int>();

        var numbersToCollectLeft = 0;
        var rangeWhereToFindNextNumber = Batteries.Slice(0, Batteries.Count - (N - 1));
        var lastIndex = 0;
        var nextIndex = 0;
        while(true)
        {
            var (greatestNumber, greatestIndex) = GreatestNumber(rangeWhereToFindNextNumber);
            nextIndex = nextIndex + greatestIndex + 1;
            result.Add(greatestNumber);
            numbersToCollectLeft = N - result.Count;
            var lengthToEnd = Batteries.Count-(nextIndex);
            lastIndex = nextIndex-1;
            if (result.Count == N || numbersToCollectLeft == Batteries.Slice(nextIndex,lengthToEnd).Count)
            {
                break;
            }

            
            rangeWhereToFindNextNumber =  Batteries.Slice(nextIndex, (Batteries.Count- (nextIndex)) - (N - 1 - result.Count));
        } 

        if (result.Count() != N)
        {
            for (int i = lastIndex+1; i < Batteries.Count; i++)
            {
                result.Add(Batteries[i].Joltage);
            }
        }

        var res = "";
        foreach (var i in result)
        {
            res += i.ToString();
        }

        return long.Parse(res);

    }

    private static (int greatestNumber, int greatestIndex) GreatestNumber(List<Battery> subarray)
    {
        var greatestNumber = 0;
        var greatestIndex = 0;
        for(var index =0; index < subarray.Count; index++)
        {
            if (subarray[index].Joltage > greatestNumber)
            {
                greatestNumber = subarray[index].Joltage;
                greatestIndex = index;
            }
        }

        return (greatestNumber, greatestIndex);
    }

    private int GetGreatestBatteryCombination(Battery firstBattery)
    {
        var greatesBatteryCombination = 0;
        foreach (var secondBattery in Batteries)
        {
            if (secondBattery == firstBattery || PositionOfFirstBatteryIsGreaterThenSecondBattery(firstBattery, secondBattery))
            {
                continue;
            }

            var batteriesJoltage = GetBatteryJoltageFor2Batteries(firstBattery, secondBattery);
            if (batteriesJoltage > greatesBatteryCombination)
            {
                greatesBatteryCombination = batteriesJoltage;
            }
        }

        return greatesBatteryCombination;
    }

    private static int GetBatteryJoltageFor2Batteries(Battery firstBattery, Battery secondBattery)
    {
        return int.Parse($"{firstBattery.Joltage}{secondBattery.Joltage}");
    }

    private bool PositionOfFirstBatteryIsGreaterThenSecondBattery(Battery firstBattery, Battery secondBattery)
    {
        return Batteries.IndexOf(firstBattery) > Batteries.IndexOf(secondBattery);
    }

    public static BatteryBank Create(string batteryBankString)
    {
        List<Battery> batteries = [];
        foreach (var batteryCharacter in batteryBankString)
        {
            batteries.Add(new Battery(int.Parse(batteryCharacter.ToString())));
        }
        return new BatteryBank(batteries);
    }
}