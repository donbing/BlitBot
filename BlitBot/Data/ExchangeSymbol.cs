namespace BlitBot.Data;

public record ExchangeSymbol
{
    public string Id => $"{Exchange}:{Symbol}";
    public string Symbol { get; init; }
    public string Description { get; init; }
    public string Type { get; init; }
    public string Exchange { get; init; }
    public string Provider_id { get; init; }
}