window.charting = {
    show: function(symbol, locale, timezone, interval, showDetails, indicators) {
        new TradingView.widget(
            {
                "autosize": true,
                "symbol": symbol,
                "interval": interval,
                "timezone": timezone,
                "theme": "dark",
                "style": "1",
                "locale": locale,
                "toolbar_bg": "#f1f3f6",
                "enable_publishing": false,
                "hide_top_toolbar": true,
                "allow_symbol_change": true,
                "save_image": false,
                "details": showDetails,
                "container_id": "tradingview_515f8",
                "studies": indicators,
            }
        )
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

window.pageLoader = function (sourceUrl) {
    window.location.href = sourceUrl;
}