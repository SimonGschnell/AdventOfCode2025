namespace Logic;

public class BatteryBank(List<Battery> batteries)
{
    private List<int> AccumulatedResult { get; } = [];
    public List<Battery> Batteries { get; } = batteries;

    public long GetJoltage(int numberOfBatteriesToTurnOn=2)
    {
        var currentPosition = 0;
        while(AccumulatedResult.Count != numberOfBatteriesToTurnOn)
        {
            var (greatestNumber, indexToAdvance) = GreatestNumberOfSubArray(GetSubArrayForNextNumber(numberOfBatteriesToTurnOn, currentPosition));
            AccumulatedResult.Add(greatestNumber);
            currentPosition += indexToAdvance + 1;
            if (GetRemainingBatteriesToTurnOn(numberOfBatteriesToTurnOn) == BatteriesLeftToTurn(currentPosition))
            {
                foreach (var battery in Batteries.Skip(currentPosition))
                {
                    AccumulatedResult.Add(battery.Joltage);
                }
            }
        } 
        
        return ConcatenatedResult();
    }

    private long ConcatenatedResult()
    {
        var result = "";
        foreach (var number in AccumulatedResult)
        {
            result += number.ToString();
        }
        return long.Parse(result);
    }

    private List<Battery> GetSubArrayForNextNumber(int numberOfBatteriesToTurnOn, int currentPosition)
    {
        return Batteries.Slice(currentPosition, BatteriesLeftToTurn(currentPosition) - RangeToFindNextNumber(numberOfBatteriesToTurnOn, AccumulatedResult));
    }

    private int GetRemainingBatteriesToTurnOn(int numberOfBatteriesToTurnOn)
    {
        return numberOfBatteriesToTurnOn - AccumulatedResult.Count;
    }

    private static int GetRemainingBatteriesToTurnOn(int numberOfBatteriesToTurnOn, List<int> accumulatedResult)
    {
        return numberOfBatteriesToTurnOn - accumulatedResult.Count;
    }

    private int BatteriesLeftToTurn(int currentPosition)
    {
        return Batteries.Count-currentPosition;
    }

    private static int RangeToFindNextNumber(int numberOfBatteriesToTurnOn, List<int> accumulatedResult)
    {
        return GetRemainingBatteriesToTurnOn(numberOfBatteriesToTurnOn, accumulatedResult) - 1;
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