$(document).ready(function () {
    baseUrl = window.mblRestHost;
    InitShoppingList();
})

var userId = null;

function getRecipes()
{
    var url = baseUrl + 'ShoppingList/Recipes';
    $.get(url, { userId: userId }, function (response)
    {
        console.log('Recipes => ', response);

        var tabs2 = $('li#tabs2 a span');
        tabs2.text(response.length);

        var recipesElement = $('#recipes');
        recipesElement.html('');
        $.each(response, function (index, recipe)
        {
            var recipeDiv = $('<div>', {
                id: 'recipe_' + recipe.RECIPE_ID,
                text: recipe.RECIPE_NAME
            });
            recipeDiv.addClass('item');

            var imgRemove = $('<img>');
            imgRemove.addClass('remove').addClass('remove-recipe');
            imgRemove.attr('src', 'images/green-cross-icon.png');
            imgRemove.on('click', { recipeId: recipe.RECIPE_ID }, removeRecipe);
            recipeDiv.append(imgRemove);

            recipesElement.append(recipeDiv);
        });

    }, 'json');
}

function removeFood(event)
{
    var url = baseUrl + 'ShoppingList/remove';

    var categoryId = event.data.categoryId;
    var foodId = event.data.foodId;
    var data = {
        "userId": userId,
        "foodId": parseInt(foodId)
    }

    $.ajax({
        url: url,
        method: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (data, status)
        {
            var foodDiv = $('#' + foodId);
            foodDiv.remove();

            var categoryDiv = $('#' + categoryId);
            var items = $('.item[categoryid="' + categoryId + '"]');
            if (items.length === 0) {
                categoryDiv.remove();
            }
        },
        error: function (jqXHR, status, text)
        {
        }
    });
}

function addRecipe(event)
{
    var url = baseUrl + 'ShoppingList/addRecipe';
    var recipeId = event.data.recipeId;
    var data = {
        "userId": userId,
        "recipeId": recipeId
    }

    $.ajax({
        url: url,
        method: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (data, status)
        {
            getShoppingList();
            getRecipes();
        },
        error: function (jqXHR, status, text)
        {
            console.log(text);
        }
    })
}

function removeRecipe(event)
{
    var url = baseUrl + 'ShoppingList/removeRecipe';
    var recipeId = event.data.recipeId;
    var data = {
        "userId": userId,
        "recipeId": recipeId
    }

    $.ajax({
        url: url,
        method: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (data, status)
        {
            getShoppingList();
            getRecipes();
        },
        error: function (jqXHR, status, text)
        {
        }
    });
}

function getShoppingList()
{
    
    var url = baseUrl + 'ShoppingList/ShoppingList';

    $.get(url, { userId: userId }, function (response)
    {
        var categoriesElement = $('.categoriesList');
        categoriesElement.text('');
        $.each(response.categories, function (index, category)
        {
            var categoryDiv = $('<div>', {
                id: category.id,
                text: category.name
            });
            categoryDiv.addClass('category');
            categoriesElement.append(categoryDiv);

            $.each(category.foodItems, function (index, food)
            {
                var foodDiv = $('<div>', {
                    id: food.id,
                    categoryId: category.id,
                    text: food.quantity + ' ' + food.measure + ' ' +   food.name
                });
                foodDiv.addClass('item');

                if (food.canDelete) {
                    var deleteImg = $('<img>');
                    deleteImg.addClass('remove');
                    deleteImg.attr('src', 'Images/green-cross-icon.png');
                    deleteImg.on('click', {categoryId: category.id, foodId: food.id}, removeFood);
                    foodDiv.append(deleteImg);
                }
                
                categoriesElement.append(foodDiv);
            });
        })
    }, 'json');
}

function getMeasureUnits()
{
    var url = baseUrl + 'ShoppingList/MeasureUnits';

    $.get(url, {}, function (response)
    {
        var select = $('#measureUnits');
        $.each(response.measureUnits, function (index, item)
        {
            select.append($("<option />").val(item.unitId).text(item.unitName));
        });
    },'json');
}

function InitShoppingList()
{
    console.log('InitShoppingList');

    isLoggedIn(function (id)
    {
        if (id) {
            userId = id;
            $('#ShoppingList').show();
            getMeasureUnits();
            getShoppingList();
            getRecipes();
        }
    });

    $("#tabs").tabs({
        activate: function (event, ui)
        {
            var selectedTab = $("#tabs").tabs('option', 'active');
            $('#' + hfSelectedTabClientID).val(selectedTab);
        }
    });

    var selectedTab = $('#' + hfSelectedTabClientID).val();
    if (selectedTab != '')
        $("#tabs").tabs({ active: selectedTab });

    $('#menusListPopUp').dialog({
        autoOpen: false,
        title: 'בחר מתפריט קיים',
        height: 300,
        width: 500,
        modal: true

    })

    $('#addAdditional').click(function ()
    {
        var url = baseUrl + 'ShoppingList/Add';

        var data = {
            "userId": userId,
            "foodName": $('#foodName').val(),
            "measureUnit": $('#measureUnits').val(),
            "quantity": $('#quantity').val()
        }

        $.ajax({
            url: url,
            method: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            success: function (data, status)
            {
                getShoppingList();
            },
            error: function (jqXHR, status, text)
            {
            }
        })
    })

    $('#btnAddToExistingMenu').click(function ()
    {

        $('#menusListPopUp').dialog('open');
    })
}