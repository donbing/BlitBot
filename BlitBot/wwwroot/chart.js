window.charting = {
    show: function(symbol) {
        new TradingView.widget(
            {
                "autosize": true,
                "symbol": symbol,
                "interval": "D",
                "timezone": "Etc/UTC",
                "theme": "dark",
                "style": "1",
                "locale": "en",
                "toolbar_bg": "#f1f3f6",
                "enable_publishing": false,
                "hide_top_toolbar": true,
                "allow_symbol_change": true,
                "save_image": false,
                "container_id": "tradingview_515f8"
            }
        )
    }
}   