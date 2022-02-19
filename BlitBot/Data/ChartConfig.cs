using System.ComponentModel;
using DevExpress.Mvvm.CodeGenerators;

namespace BlitBot.Data;

[GenerateViewModel]
public partial class ChartConfig : INotifyPropertyChanged
{
    [GenerateProperty]
    ExchangeSymbol symbol = new ExchangeSymbol { Symbol = "XBTUSD" };
    [GenerateProperty]
    string locale = "en";
    [GenerateProperty]
    string timeZone = "Etc/UTC";
    [GenerateProperty]
    string interval = "240";

    public event EventHandler OnChartChanged = 
        (sender, args) => { };

    public void Updated() => 
        OnChartChanged.Invoke(this, new EventArgs());

    public event PropertyChangedEventHandler? PropertyChanged;
}
