var Recipe = function(){
    
    var current = this;
    
    var listRecipes = $('#m1-Lists-listRecipes'); 
    var listItemTemplte = listRecipes.children('li:first'); 
    var listItemHeight = listItemTemplte.css('height');
    listRecipes.children().remove(); 
    
    var listIngredients = $('#m1-Recipe-listIngredients');
    var listIngredientsItemTemplate = listIngredients.children('li:first');
    var listIngredientsItemHeight = listIngredientsItemTemplate.css('height');
    
    var selectedRecipes = $('#m1-Lists-listSelectedRecipes');;
    var selectedItemTemplte = selectedRecipes.children('li:first');;
    var selectedItemHeight = selectedItemTemplte.css('height');
	
    this.Init = function(){
	
	$('#m1-Lists-txtSearchValue').val('');
    }
    
    this.NewSearch = function(){
	listRecipes.children().remove();
	$('#m1-Lists-txtSearchValue').val('');
    };
    
    AddRecipeItem = function(recipe){
	
	var newListItem = $('<li/>', { 
	    html : listItemTemplte .html()
	});

	newListItem.css('height',listItemHeight)

	var item = newListItem.find('#m1-Lists-SearchedRecipeItem'); 
	if (item != null) {
	    item.html(recipe.RecipeName);
	    item.addClass('show-recipe');
	    item.addClass('heb-label');
	    item.attr('data-id',recipe.RecipeId)
	}

	var servings = newListItem.find('#m1-Lists-txtServings');
	servings.addClass('heb-label');
	servings.html(recipe.Servings + ' מנות');
	servings.addClass('show-recipe');
	servings.attr('data-id',recipe.RecipeId)

	var showRecipe = newListItem.find('#m1-Lists-showRecipe');
	showRecipe.addClass('show-recipe');
	showRecipe.attr('data-id',recipe.RecipeId);
	
	var checkRecipe = newListItem.find('#m1-Lists-CheckRecipe');
	checkRecipe.addClass('check-recipe');
	checkRecipe.attr('data-id',recipe.RecipeId);
	
	if (recipe.SHOPPING_LIST == true)
	    checkRecipe.prop('checked', true);

	listRecipes.append(newListItem); 
    };
    
    this.SearchRecipe = function(){
	
	searchVal = $('#m1-Lists-txtSearchValue').val(); 
	var url = baseUrl + "SearchRecipes"; 
	var d = '{"searchValue":"' + searchVal + '"}';

	Ajax(url, d, function(data, textStatus, jqXhr){
	    if (data != null){ 
		listRecipes.children().remove(); 

		var recipes = $.parseJSON(data); 
		
		for (var i=0; i < recipes.length; i++) {
		    
		    AddRecipeItem(recipes[i]);
		}
		
		var scroller = $('#m1-Lists-SearchRecipesPanel-scroller');
	        SetHeight(scroller, listRecipes, listItemHeight);

		$('.show-recipe').click(function(){
		    phoneui.gotoPage('m1-Recipe', "FADE");

		    var recipeId = $(this).attr('data-id');
		    ShowRecipeDetails(recipeId);
		});
	    } 
	})
    }    

    this.GetSelectedRecipes = function() {

	var userId = localStorage.getItem('UserId');
	var url = baseUrl + "GetSelectedRecipes";
	var parameters = '{"userId":' + userId + '}';

	selectedRecipes = $('#m1-Lists-listSelectedRecipes');
	
	CallService(url, parameters, function(data, status, jqXhr) {
	    
	    if (data != null) {

		selectedRecipes.children().remove();
		
		var recipes = $.parseJSON(data);
		for ( var i = 0; i < recipes.length; i++) {

		    var newListItem = $('<li/>',
			    {
				html : selectedItemTemplte.html()
			    });

		    newListItem.css('height',selectedItemHeight)
		    newListItem.attr('data-id',recipes[i].RECIPE_ID)

		    var item = newListItem.find('#m1-Lists-txtRecipeName')
		    if (item != null)
		    {
			item.html(recipes[i].RECIPE_NAME.length >= 30 ? recipes[i].RECIPE_NAME.substr(0,30): recipes[i].RECIPE_NAME);
			item.addClass('show-recipe');
			item.attr('data-id',recipes[i].RECIPE_ID)
			item.attr('title', recipes[i].RECIPE_NAME);
		    }

		    var removeRecipe = newListItem.find('#m1-Lists-RemoveRecipe');
		    removeRecipe.addClass('delete-recipe');
		    removeRecipe.attr('data-id',recipes[i].RECIPE_ID);
		    
		    // Show recipe detailes
		    var btnShowRecipe = newListItem.find('#m1-Lists-backIndicator2');
		    btnShowRecipe.addClass('show-recipe');
		    btnShowRecipe.attr('data-id',recipes[i].RecipeId)

		    selectedRecipes.append(newListItem);
		}
		
		var scroller = $('#m1-Lists-panel4-scroller');
	        SetHeight(scroller, selectedRecipes.children('li').length, selectedItemHeight);

		$('.delete-recipe').click(function() {
		    var recipeId = $(this).attr('data-id');
		    RemoveRecipeFromShoppingList(recipeId);
		});

		$('.show-recipe').click(function(){

		    var recipeId = $(this).attr('data-id');
		    ShowRecipeDetails(recipeId);
		});
	    }
	});
    }
    
    this.AddRecipesToShoppingList = function(){

	$('.check-recipe').each(function(){

	    var recipeId = $(this).attr('data-id');
	    var isChecked = $(this).children('input').is(':checked')

	    if (isChecked == true)
		AddRecipeToShoppingList(recipeId);
	});
    };
    
    AddRecipeToShoppingList = function(recipeId) {

	var url = baseUrl + "AddRecipeToShoppingList";
	var parameters = '{"userId":' + userId + ', "recipeId":' + recipeId + '}';

	CallService(url, parameters, function(data, textStatus, jqXhr) {

	});
    }
    
    RemoveRecipeFromShoppingList = function(recipeId)
    {
        var url = baseUrl + "RemoveRecipeFromShoppingList";
        var parameters = '{"userId":' + userId + ',"recipeId":' + recipeId + '}';

        CallService(url, parameters, function(data, status, jqXhr) {
            
            selectedRecipes.children('li[data-id='+ recipeId+']').remove();
        });
    }
    
    ShowRecipeDetails = function(recipeId){
	
	var url = baseUrl + "GetRecipe";
        var parameters = '{"recipeId":' + recipeId + '}';
	
	CallService(url, parameters, function(data, status, jqXhr) {
            if (data != null){
                
                listIngredients.children('li').remove();
                
        	var recipe = $.parseJSON(data);

        	$('#m1-Recipe-txtRecipeName').html(recipe.RecipeName);

        	var ingredients = recipe.Ingredients;
        	for(var i=0; i<ingredients.length; i++){

        	    var newListItem = $('<li/>',
        		    {
        		html : listIngredientsItemTemplate.html()
        		    });

        	    newListItem.css('height',listIngredientsItemHeight)

        	    var itemName = newListItem.find('#m1-Recipe-txtIngredientName');
        	    itemName.addClass('ingridiantName');
        	    itemName.html(ingredients[i].FOOD_NAME);

        	    var itemQuantity = newListItem.find('#m1-Recipe-txrQuantity');
        	    itemQuantity.html(ingredients[i].Quantity + ' ' + ingredients[i].MEASUREMENT_NAME);
        	    itemQuantity.addClass('heb-label');

        	    listIngredients.append(newListItem);
        	}

		var checkBox = $('#m1-Recipe-checkbox1 input');
		checkBox.addClass('select-recipe');
		checkBox.attr('data-recipe-id',recipe.RecipeId);
		checkBox.removeAttr('checked');
		if (recipe.SHOPPING_LIST){
		    checkBox.prop('checked',true);
		}

        	var prepareMethod = $('#m1-Recipe-txtPrepareMethod');
        	var prepareMethodValue = recipe.PreparationMethod;
        	prepareMethod.html(prepareMethodValue);
        	prepareMethod.addClass('heb-label');
        	
        	$('.select-recipe').change(function(){
        	    var recipeId = $( this).attr('data-recipe-id');
        	    var checked = $( this).attr('checked') != undefined;
        	    
        	    if (checked == true)
        		AddRecipeToShoppingList(recipeId);
        	    else
        		RemoveRecipeFromShoppingList(recipeId);
        	});
        	
        	phoneui.gotoPage('m1-Recipe', "FADE");
            }
            else{
        	alert('No data found');
            }
        });
    }
}

$(function(){
    
    recipe = new Recipe();
    recipe.Init();

    $('#m1-Lists-SearchRecipes').click(function() {
	recipe.SearchRecipe();
    }); 
    
    $('#m1-Lists-text8').click(function(){
	recipe.NewSearch();
    });
    
    $('#m1-Lists-text4').click(function(){
	recipe.AddRecipesToShoppingList();	
    });
})