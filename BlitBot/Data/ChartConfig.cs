namespace BlitBot.Data;

public class ChartConfig
{
    public string Symbol { get; set; } = "XBTUSD";

    public event EventHandler OnChartChanged = 
        (sender, args) => { };

    public void Updated() => 
        OnChartChanged.Invoke(this, new EventArgs());
}