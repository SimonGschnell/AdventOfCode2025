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
        var accumulatedResult = new List<int>();
        var subarrayForNextNumber = Batteries.Slice(0, Batteries.Count - (N - 1));
        var currentPosition = 0;
        while(true)
        {
            var (greatestNumber, index) = GreatestNumberOfSubArray(subarrayForNextNumber);
            currentPosition += index + 1;
            accumulatedResult.Add(greatestNumber);
            var lengthToEnd = Batteries.Count-(currentPosition);
            if (accumulatedResult.Count == N || N_MoreNumbersToCollect(N, accumulatedResult) == Batteries.Slice(currentPosition,lengthToEnd).Count)
            {
                break;
            }

            
            subarrayForNextNumber =  Batteries.Slice(currentPosition, (Batteries.Count- (currentPosition)) - (N - 1 - accumulatedResult.Count));
        } 

        if (accumulatedResult.Count() != N)
        {
            for (int i = currentPosition; i < Batteries.Count; i++)
            {
                accumulatedResult.Add(Batteries[i].Joltage);
            }
        }

        var res = "";
        foreach (var i in accumulatedResult)
        {
            res += i.ToString();
        }

        return long.Parse(res);

    }

    private static int N_MoreNumbersToCollect(int n, List<int> result)
    {
        return n - result.Count;
    }

    private static (int greatestNumber, int greatestIndex) GreatestNumberOfSubArray(List<Battery> subarray)
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