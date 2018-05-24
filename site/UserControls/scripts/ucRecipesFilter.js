$(function () {

    $('.recipes-search-option').click(function () {

        var redirectTo = $(this).attr('data-href');

        var data = { method: 'IsLoggedIn' };
        $.post('Handler.ashx', data, function (data) {
            if (data == '') {
                OpenLoginDialog(function () {
                    window.location = redirectTo;
                });
            }
            else
                window.location = redirectTo;
        });
    })
})