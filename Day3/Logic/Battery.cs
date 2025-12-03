namespace Logic;

public class Battery
{
    public Battery(int joltage)
    {
        if (joltage is <= 0 or >= 10) throw new ArgumentException("Joltage value must be 1-9");
        Joltage = joltage;
    }

    public int Joltage { get; }
}