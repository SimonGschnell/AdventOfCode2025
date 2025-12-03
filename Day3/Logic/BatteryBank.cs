namespace Logic;

public class BatteryBank(List<Battery> batteries)
{
    public List<Battery> Batteries { get; } = batteries;

    public long GetJoltage(int numberOfBatteriesToTurnOn=2)
    {
        var accumulatedResult = new List<int>();
        var subarrayForNextNumber = Batteries.Slice(0, Batteries.Count - (numberOfBatteriesToTurnOn - 1));
        var currentPosition = 0;
        while(accumulatedResult.Count != numberOfBatteriesToTurnOn)
        {
            var (greatestNumber, index) = GreatestNumberOfSubArray(subarrayForNextNumber);
            currentPosition += index + 1;
            accumulatedResult.Add(greatestNumber);
            var lengthToEnd = Batteries.Count-(currentPosition);

            if (accumulatedResult.Count == numberOfBatteriesToTurnOn) continue;
            if (numberOfBatteriesToTurnOn - accumulatedResult.Count == Batteries.Slice(currentPosition,lengthToEnd).Count)
            {
                for (int i = currentPosition; i < Batteries.Count; i++)
                {
                    accumulatedResult.Add(Batteries[i].Joltage);
                }
                continue;
            }
            subarrayForNextNumber =  Batteries.Slice(currentPosition, (Batteries.Count- (currentPosition)) - (numberOfBatteriesToTurnOn - 1 - accumulatedResult.Count));


        } 
        
        return ConcatenatedResult(accumulatedResult);
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