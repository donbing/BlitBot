namespace BlitBot.Data;

public record ExchangeSymbol
{
    public string Id => $"{Exchange}:{Symbol}";
    public string Symbol { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Exchange { get; set; }
    public string Provider_id { get; set; }
}