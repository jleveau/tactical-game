using System;

public interface IStatistic
{
    int Value { get; }
    void UpdateTurnChange();
}

[Serializable]
public class Statistic
{
    public int value;
    public string name;

    public Statistic(string name, int value)
    {
        this.name = name;
        this.value = value;
    }

    public virtual void UpdateTurnChange()
    {
    }
}
[Serializable]
public class ResetEachTurnStatistic : Statistic
{
    public int reset_value;

    public ResetEachTurnStatistic(string name, int value, int reset_value) : base(name, value)
    {
        this.reset_value = reset_value;
    }


    public override void UpdateTurnChange()
    {
        base.UpdateTurnChange();
        value = reset_value;
    }
}