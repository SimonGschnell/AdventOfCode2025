namespace Logic;

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