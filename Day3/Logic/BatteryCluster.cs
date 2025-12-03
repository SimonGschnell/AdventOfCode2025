namespace Logic;

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