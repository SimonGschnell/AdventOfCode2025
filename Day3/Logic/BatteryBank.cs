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
        var greatesBatteryCombination = 0;
        foreach (var firstBattery in Batteries)
        {
            greatesBatteryCombination = GetGreatestBatteryCombination(firstBattery);
        }
        return greatesBatteryCombination;
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