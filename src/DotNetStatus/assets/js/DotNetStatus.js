function RefreshChart(contentPlaceHolder, route) {

    contentPlaceHolder.hide();
    $.get('/Data/' + route, function (data) {
        contentPlaceHolder.html(data);
        contentPlaceHolder.fadeIn(1000);
    });
}