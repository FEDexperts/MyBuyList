$(function () {
    $('#' + lnkNewRecipe).click(function () {
        var data = { method: 'IsLoggedIn' };
        $.post('Handler.ashx', data, function (data) {
            if (data == '') {
                OpenLoginDialog(function () {
                    window.location = 'RecipeEdit.aspx';
                });
            }
            else
                window.location = 'RecipeEdit.aspx';
        });
    })
})