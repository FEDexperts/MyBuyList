var shoppingList;

var ShoppingList = function() {

    var alfabetList = $('#m1-Lists-AlfabetShoppingList');
    var alfabetItem = alfabetList.children('li:first');
    var categoriesList = $('#m1-Lists-ItemsList');
    var categoryHeader = categoriesList.children('li:first');
    var categoryItem = $(categoriesList.children('li')[1]);
    var categories = new Array();
    
    Array.prototype.FindCategory = function(categoryId) {

	for ( var f = 0; f < this.length; f++) {
	    if (this[f].Id == categoryId)
		return f;
	}
	return -1;
    };

    Array.prototype.FindAllByCategory = function(categoryId) {

	var result = new Array();

	for ( var i = 0; i < this.length; i++) {
	    if (this[i].CATEGORY_ID == categoryId) {
		result.push(this[i]);
	    }
	}

	return result;
    };

    Array.prototype.Clear = function() {
	while (this.length > 0) {
	    this.pop();
	}
    };

    this.Init = function() {
	this.GetShoppingList();
    }

    this.GetShoppingList = function() {

	var url = baseUrl + "ShoppingList";
	var userId = localStorage.getItem('UserId');
	var parameters = '{ "userId": ' + userId + '}';
	
        	CallService(url, parameters, function(data) {
        
        	    if (data != '') {
        
        		var list = $.parseJSON(data);
        
        		GetByAlfabet(list);
        		GetByCategory(list);
        	    }
        	    else{
        		ClearLists();
        	    }
        	})
    }
    
    ClearLists = function(){
	
	var list;
	
	list = $('#m1-Lists-ItemsList');
	list.children('li').remove();
	
	list = $('#m1-Lists-AlfabetShoppingList');
	list.children('li').remove();
    }

    GetByCategory = function(list) {
	
	var headerHeight = categoryHeader.css('height');
	var height = categoryItem.css('height');
	categoriesList.children('li').remove();
	categories.Clear();
		
	// Create categories
	for ( var i = 0; i < list.length; i++) {
	    
	    var f = categories.FindCategory(list[i].CATEGORY_ID);
	    
	    if (f == -1) {
		var headerItem = $(categoryHeader.prop('outerHTML'));
		
		headerItem.addClass('category-header');

		var headerName = headerItem.find('#m1-Lists-CategoryTitle-label');
		headerName.html(list[i].CATEGORY_NAME);

		categoriesList.append(headerItem);

		var category = new Object();
		category.Id = list[i].CATEGORY_ID;
		category.Name = list[i].CATEGORY_NAME;
		categories.push(category);

		var items = list.FindAllByCategory(category.Id);

		// Create Category Items
		for ( var a = 0; a < items.length; a++) {

		    var newItem = $('<li/>', {
			html : categoryItem.html()
		    });
		    
		    newItem.addClass('category-item');

		    var checkBox = newItem.find('#m1-Lists-Check input');
		    checkBox.addClass('shopping-list');
		    checkBox.attr('data-id', items[a].FOOD_ID);
		    if (items[a].ACTIVE == true)
			checkBox.attr('checked', 'checked');
		    else
			checkBox.removeAttr('checked');

		    var oFood = newItem.find('#m1-Lists-Name');
		    oFood.addClass('heb-label');
		    oFood.html(items[a].FOOD_NAME);
		    
		    if (items[a].IMAGE != null)
		    {
	        	    var foodImage = newItem.find('#m1-Lists-Image');
	        	    foodImage.attr('src', items[a].IMAGE_SRC);
		    }

		    var quantity = newItem.find('#m1-Lists-Quantity');
		    quantity.addClass('heb-label');
		    quantity.html(items[a].FriendlyQuantity + ' ' + items[a].MEASUREMENT_NAME);

		    categoriesList.append(newItem);
		}
	    }
	}

	$('input[class="shopping-list"]').click(function() {
	    var checked = $(this).attr('checked') == "checked";
	    var foodId = $(this).attr('data-id');
	    CheckItem(checked, foodId);
	});
    }

    GetByAlfabet = function(list) {

	var height = alfabetItem.css('height');
	alfabetList.children('li').remove();

	for ( var i = 0; i < list.length; i++) {
	    
	    var newListItem = $('<li/>', {
		html : alfabetItem.html()
	    });

	    newListItem.addClass('alfabet-item');

	    var foodNameElement = newListItem.find('#m1-Lists-text3');
	    foodNameElement.addClass('heb-label');
	    foodNameElement.html(list[i].FOOD_NAME);
	    
	    if (list[i].IMAGE != null)
	    {
        	    var foodImage = newListItem.find('#m1-Lists-image1');
        	    foodImage.attr('src', list[i].IMAGE_SRC);
	    }

	    var checkBox = newListItem.find('#m1-Lists-checkbox1 input');
	    checkBox.addClass('shopping-list');
	    checkBox.attr('data-id', list[i].FOOD_ID);
	    if (list[i].ACTIVE == true)
		checkBox.attr('checked', 'checked');
	    else
		checkBox.removeAttr('checked');

	    var quantity = newListItem.find('#m1-Lists-txtQuntity');
	    quantity.addClass('heb-label');
	    quantity.html(list[i].FriendlyQuantity + ' ' + list[i].MEASUREMENT_NAME);

	    alfabetList.append(newListItem);
	};

	$('input[class="shopping-list"]').click(function() {
	    var checked = $(this).attr('checked') == "checked";
	    var foodId = $(this).attr('data-id');
	    CheckItem(checked, foodId);
	});
    }

    CheckItem = function(checked, foodId) {
	var url = baseUrl + "CheckShoppingListItem";
	var userId = localStorage.getItem('UserId');
	var d = '{"userId":' + userId + ', "foodId":' + foodId + ', "active":'
		+ checked + '}';

	Ajax(url, d, function(data, textStatus, jqXh) {
	});
    }
}

$(function() {

    shoppingList = new ShoppingList();
    shoppingList.Init();
    
    var alfabetList = $('#m1-Lists-AlfabetShoppingList');
    
    $('#m1-Lists-tabBar3-pageCategories-img').css('display','none');
})