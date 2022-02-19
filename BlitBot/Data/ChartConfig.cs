using System.ComponentModel;
using System.Runtime.CompilerServices;
using BlitBot.Annotations;
using DevExpress.Mvvm.CodeGenerators;

namespace BlitBot.Data;

[GenerateViewModel]
public partial class ChartConfig : INotifyPropertyChanged
{
    [GenerateProperty]
    public string symbol  = "XBTUSD";
    [GenerateProperty]
    public string locale = "en";
    [GenerateProperty]
    public string timeZone = "Etc/UTC";
    [GenerateProperty]
    public string interval = "240";

    public event EventHandler OnChartChanged = 
        (sender, args) => { };

    public void Updated() => 
        OnChartChanged.Invoke(this, new EventArgs());

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        Updated();
    }
}