namespace Logic;

public class BatteryBank(List<Battery> batteries)
{
    public List<Battery> Batteries { get; } = batteries;

    public long GetJoltage(int numberOfBatteriesToTurnOn=2)
    {
        List<int> accumulatedResult = [];
        var currentPosition = 0;
        var subarrayForNextNumber = GetSubArrayForNextNumber(numberOfBatteriesToTurnOn, currentPosition, accumulatedResult);
        while(accumulatedResult.Count != numberOfBatteriesToTurnOn)
        {
            var (greatestNumber, index) = GreatestNumberOfSubArray(subarrayForNextNumber);
            currentPosition += index + 1;
            accumulatedResult.Add(greatestNumber);
            if (accumulatedResult.Count == numberOfBatteriesToTurnOn) continue;
            if (numberOfBatteriesToTurnOn - accumulatedResult.Count == Batteries.Slice(currentPosition,BatteriesLeftToTurn(currentPosition)).Count)
            {
                for (int i = currentPosition; i < Batteries.Count; i++)
                {
                    accumulatedResult.Add(Batteries[i].Joltage);
                }
                continue;
            }

            subarrayForNextNumber =  GetSubArrayForNextNumber(numberOfBatteriesToTurnOn, currentPosition, accumulatedResult);
        } 
        return ConcatenatedResult(accumulatedResult);
    }

    private List<Battery> GetSubArrayForNextNumber(int numberOfBatteriesToTurnOn, int currentPosition, List<int> accumulatedResult)
    {
        return Batteries.Slice(currentPosition, BatteriesLeftToTurn(currentPosition) - RangeToFindNextNumber(numberOfBatteriesToTurnOn, accumulatedResult));
    }

    private int BatteriesLeftToTurn(int currentPosition)
    {
        return Batteries.Count-currentPosition;
    }

    private static int RangeToFindNextNumber(int numberOfBatteriesToTurnOn, List<int> accumulatedResult)
    {
        return numberOfBatteriesToTurnOn - 1 - accumulatedResult.Count;
    }

    private static long ConcatenatedResult(List<int> accumulatedResult)
    {
        var result = "";
        foreach (var number in accumulatedResult)
        {
            result += number.ToString();
        }
        return long.Parse(result);
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