namespace Logic;

public class BatteryCluster
{
    public List<BatteryBank> BatteryBanks { get; }

    public BatteryCluster(List<BatteryBank> batteryBanks)
    {
        BatteryBanks = batteryBanks;
    }

    public long GetJoltage(int N =2)
    {
        return BatteryBanks.Sum(batteryBank => batteryBank.GetJoltage(N));
    }
}