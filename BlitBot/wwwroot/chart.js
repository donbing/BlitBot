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
    },
    analysis: function(symbol) {

    }
};

window.scriptLoader = function(sourceUrl, targetElement, innerHtml) {
    if (sourceUrl.Length == 0) {
        console.error("Invalid source URL");
        return;
    }

    var tag = document.createElement('script');
    tag.src = sourceUrl;
    tag.type = "text/javascript";

    tag.onload = function () {
        console.log("Script loaded successfully");
    }

    tag.onerror = function () {
        console.error("Failed to load script");
    }
    tag.innerHTML = innerHtml
    const target = document.querySelector(targetElement);
    target.appendChild(tag);
};