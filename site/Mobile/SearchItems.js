var SearchItems = function(){
    
    var current = this;
    var items = new Array();
    var resultList = $('#m1-AddFood-Results');
    var resultListItem = resultList.children('li:first');
    var resultListItemHeight = resultList.css('height');
    
    this.Add = function(name, quantity){
	
	var newItem = {ItemName: name, ItemQuantity: quantity};
	items.push(newItem);
    };
    
    this.AddToResult = function(foodName, image){
	
	var newListItem = $('<li/>', {
	    html : resultListItem.html()
	});

	newListItem.css('height',resultListItemHeight);

	var itemName = newListItem.find('#m1-AddFood-ItemName');
	itemName.addClass('heb-label');
	itemName.html(foodName);
	
	var foodImage = newListItem.find('#m1-AddFood-ItemImage');
	foodImage.attr('src', image);
	
	resultList.append(newListItem);
    };
    
    this.GetList = function(){
	
	return resultList;
    };
    
    this.GetSelectedItems = function(){
	
	return items;
    };
    
    this.Clear = function(){
	items.length = 0;
	resultList.children().remove();
    };
    
    this.ClearSearchBox = function(){
	$('#m1-AddFood-txtSearchedItem').val('');
    }
}

$(function(){
    
    searchItems = new SearchItems();
    searchItems.Clear();
    
    $('#m1-AddFood-txtSearchedItem').keyup(function(){

	var searchVal = $('#m1-AddFood-txtSearchedItem').val();
	
	if(searchVal == '' || searchVal.length < 3) {
	    searchItems.Clear();
	    return;
	}
	
	/*var parameters = '{ "prefix":"' + searchVal +'"}';
	var url = baseUrl + "GetFoodList";*/
	
	searchItems.Clear();
	var items = dataMgr.GetFoods(searchVal);
	if (items.length>0){
	    for(var i=0; i<items.length; i++){

		searchItems.AddToResult(items[i].FoodName, items[i].IMAGE_SRC);
	    }
	}
	else{
	    searchItems.Clear();
	    searchItems.AddToResult(searchVal);
	}

	/*CallService(url, parameters, function(data, textStatus, jqXhr){
	    if (data != null){
		searchItems.Clear();
		
		var items = $.parseJSON(data);
		
		if (items.length>0){
		    for(var i=0; i<items.length; i++){

			searchItems.AddToResult(items[i].FoodName, items[i].IMAGE_SRC);
		    }
		}
		else{
		    searchItems.Clear();
		    searchItems.AddToResult(searchVal);
		}
	    }
	});*/
    });
    
    $('#m1-AddFood-btnAddItems').click(function(){
	
	var results = searchItems.GetList();
	var itemsFound = results.children('li').length
	
	if (itemsFound > 0){
	    results.children('li').each(function(){

		var quantity = $(this).find('#m1-AddFood-ItemQuantity');

		if (quantity.val() != ''){
		    var name = $(this).find('#m1-AddFood-ItemName');
		    searchItems.Add(name.html(), quantity.val());
		}
	    });
	}
	
	phoneui.back();
    });
})