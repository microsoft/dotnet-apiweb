function InitializePopovers() {
    $(".popup-marker").popover();
    $(".popup-marker").on('click', function (e) { e.preventDefault(); return true; });

    $('.popup-marker').popover();

    $('body').on('click', function (e) {
        $('.popup-marker').each(function () {
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover-marker').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });
}

$(document).ready(InitializePopovers);