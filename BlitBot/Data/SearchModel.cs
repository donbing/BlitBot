using System.ComponentModel;
using DevExpress.Mvvm.CodeGenerators;

namespace BlitBot.Data;

[GenerateViewModel]
public partial class SymbolSearchForm : INotifyPropertyChanged
{
    [GenerateProperty]
    string symbolType = "all";

    [GenerateProperty]
    string exchange = "coinbase";

    readonly HttpClient httpClient;
    public event PropertyChangedEventHandler? PropertyChanged;

    public SymbolSearchForm(HttpClient httpClient) => 
        this.httpClient = httpClient;

    public async Task<IEnumerable<ExchangeSymbol>> SearchSymbols(string searchText)
    {
        var searchType = symbolType == "all" ? null : symbolType;
        var requestUri = $"https://symbol-search.tradingview.com/symbol_search/?text={searchText}&type={searchType}&exchange={exchange}";
        var matchedSymbols = await httpClient.GetFromJsonAsync<List<ExchangeSymbol>>(requestUri);
        return matchedSymbols ?? Enumerable.Empty<ExchangeSymbol>();
    }
}
