$(document).ready(function () {

    var searchValue;
    var searchText;
    var searchCategory;

    var searchValueBox = $('#header div.search-box input[type=text]');
    searchValueBox.addClass('search-value');

    var select = $('#header div.search-box select');
    select.addClass('search-value');

    $('#header div.search-box input[type=text]').autocomplete({
        source: function(request, response){
            var data = { method: 'SearchValues', category: searchCategory, term: request.term };
            $.post('Handler.ashx', data, function (result) {
                if (result != '') {
                    var filteredData = $.parseJSON(result);
                    response(filteredData);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            searchValue = ui.item.value;
            searchText = ui.item.label;

            var value = searchValue.split('|')[0];
            var listId = searchValue.split('|')[1];

            ui.item.value = searchText;

            $.post('Handler.aspx', { method: 'SetSearchParameters', searchIn: listId, searchFor: searchText }, function () {

            });

            switch (searchCategory) {
                //case '0':
                //    if (category == 0)
                //        window.location = 'RecipeDetails.aspx?RecipeId=' + value;
                //    if (category == 1)
                //        window.location = 'MenuDetails.aspx?menuId=' + value;
                //    break;
                case '1':
                case '2':
                case '3':
                    window.location = 'RecipeDetails.aspx?RecipeId=' + value;
                    break;
                case '4':
                case '5':
                case '6':
                    window.location = 'MenuDetails.aspx?menuId=' + value;
                    break;
            }
        },
        search: function(event, ui){
            searchCategory = select.val();
        },
        open: function () {
            searchCategory = select.val();
            var text = $(this).val();
            $('ul.ui-autocomplete li a').each(function () {
                var listText = $(this).html();
                var newListText = listText.replace(text, '<span style="color: orange"><b>' + text + '</b></span>');
                $(this).html(newListText);
            });
        }
    });

    $('.search-button').click(function () {

        searchCategory = select.val();
        var searchValue = $('#header div.search-box input[type=text]').val();

        switch (searchCategory) {
            case '0':
                //if (category == 0)
                //    window.location = 'RecipeDetails.aspx?RecipeId=' + value;
                //if (category == 1)
                //    window.location = 'MenuDetails.aspx?menuId=' + value;
                break;
            case '1':
                if (searchValue != '')
                    window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term=' + searchValue + '&category=' + searchCategory;
                else
                    window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=All';
                break;
            case '2':
                window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=MyRecipes&term=' + searchValue + '&category=' + searchCategory;
                break;
            case '3':
                window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes&term=' + searchValue + '&category=' + searchCategory;
                break;
            case '4':
                if (searchValue != '')
                    window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term=' + searchValue + '&category=' + searchCategory;
                else
                    window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=All';
                break;
            case '5':
                window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=MyRecipes&term=' + searchValue + '&category=' + searchCategory;
                break;
            case '6':
                window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes&term=' + searchValue + '&category=' + searchCategory;
                break;
        }
    });

    //SetSearchOptions(options);

    select.change(function () {
        searchCategory = $(this).val();
        var option = $('#header div.search-box select option:selected');
        var requireLogin = option.hasClass('requireLogin');
        if (requireLogin) {
            var data = { method: 'IsLoggedIn' };
            $.post('Handler.ashx', data, function (data) {
                if (data == '') {
                    OpenLoginDialog();
                }
            });
        }

        $('#header div.search-box input[type=text]').val('');
    });
})

function SetSearchOptions(options)
{
    var select = $('#header div.search-box select');
    select.html('');

    var options =
        [
            { "value": 1, "text": "מתכונים" },
            { "value": 2, "text": "המתכונים שלי", "class": "requireLogin" },
            { "value": 3, "text": "המתכונים המועדפים שלי", "class": "requireLogin" },
            { "value": 4, "text": "תפריטים" },
            { "value": 5, "text": "התפריטים שלי", "class": "requireLogin" },
            { "value": 6, "text": "התפריטים המועדפים שלי", "class": "requireLogin" }
        ];

    for (var i = 0; i < options.length; i++) {
        select
            .append($('<option/>')
            .text(options[i].text)
            .val(options[i].value)
            .addClass(options[i].class));
    }
}

function SetSearchOption(selected) {
    $('#header div.search-box select').val(selected);
}

function ResetSearch()
{
    SetSearchOption(1);
    $('#header div.search-box input[type=text]').val('');
}