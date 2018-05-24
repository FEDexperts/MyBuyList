var Item = function(){
    
    this.Name;
    this.Quantity;
};

var SavedListItem = function(){
    
    this.Id;
    this.Name;
    this.ShoppingList;
}


var SavedList = function()
{
    var current = this;
    var ListId = -1;
    var SeletedListId = -1;
    
    var savedLists = new Array();
    var items = new Array();
    var item;
    
    var select;
    var selectOptionTemplate;
    
    var newSavedList = $('#m1-Lists-NewSavedList');
    var newSavedListItemTemplate = newSavedList.children('li:first');
    var newSavedListItemHeight = newSavedListItemTemplate.css('height');
    
    var mySavedList = $('#m1-Lists-MySavedList');
    var mySavedListTemplate = mySavedList.children('li:first');
    var mySavedListItemHeight = mySavedListTemplate.css('height');
    
    this.Init = function(){
	
	select = $('#m1-Lists-hidden-select-comboboxSavedLists');
	selectOptionTemplate = select.children('option:first');
	
	mySavedList.children().remove();
	newSavedList.children().remove();
    }
    
    AddListOptionItem = function(item){

	var newOption = $('<option/>', {
	    html : selectOptionTemplate.html(),
	    value: item.ID
	});

	newOption.removeAttr('selected');
	newOption.html(item.NAME);

	select.append(newOption);

	savedListItem = new SavedListItem();
	savedListItem.Id = item.ID;
	savedListItem.Name = item.NAME;
	savedListItem.ShoppingList = item.SHOPPING_LIST;
	savedLists.push(savedListItem);
    }
    
    this.GetLists = function(){

	select = $('#m1-Lists-hidden-select-comboboxSavedLists');
	select.children().remove();
	
	var userId = localStorage.getItem('UserId');
	var url = baseUrl + "GetSavedLists";
	var d = '{"userId":' + userId + '}';

	Ajax(url, d, function(data, textStatus, jqXhr) {
	    if (data != null){
		
		var savedListItems = $.parseJSON(data);
		
		if(savedListItems.length == 0){
		    mySavedList.children().remove();
		    return;
		}
		
		savedLists.Clear();
		
		for (var i=0; i<savedListItems.length; i++){
		    
		    AddListOptionItem(savedListItems[i]);
		}
		
		select.children('option:first').attr('selected','');
		
		SeletedListId = select.val();
		GetMySavedListItems();
		
		// Shopping list check box
		var checkShoppingList = $('#m1-Lists-checkbox5 input');
		checkShoppingList.addClass('add-saved-list-to-shopping-list');
		checkShoppingList.removeAttr('checked');
		if (savedLists[0].ShoppingList == true)
		    checkShoppingList.attr('checked','checked');
		
		$('.add-saved-list-to-shopping-list').change(function(){
		    
		    var checked = $(this).attr('checked') == 'checked';
		    var url = baseUrl + "UpdateSavedList";
		    var d = '{"listId":' + SeletedListId + ',"shoppingList":' + checked + '}';
		    
		    Ajax(url, d, function(data, textStatus, jqXhr) {
		    });
		});
		
		SetListsChangeEvent();
	    }
	})
    }
    
 // Saved list selection changed
    SetListsChangeEvent = function(){
	
	select = $('#m1-Lists-hidden-select-comboboxSavedLists');
	
	select.unbind('change');
	select.change(function(){

	    SeletedListId = $(this).val();

	    var indx = savedLists.FindById(SeletedListId);
	    var checkShoppingList = $('#m1-Lists-checkbox5 input');
	    if (savedLists[indx].ShoppingList == true)
		checkShoppingList.attr('checked','checked');
	    else
		checkShoppingList.removeAttr('checked');

	    GetMySavedListItems(SeletedListId);
	});
    };
    
    this.AddSearchItems = function(){

	var items = searchItems.GetSelectedItems();
	
	switch(subPage){
	case 'NewSavedList':
	    if (ListId == -1){
		var listName = $('#m1-Lists-txtSavedListName').val();
		var url = baseUrl + "AddSavedList";
		var d = '{"userId":' + userId + ',"listName":"' + listName + '"}';

		Ajax(url, d, function(data, textStatus, jqXhr) {

		    var newList = $.parseJSON(data);

		    ListId = newList.ID;
		    SeletedListId = newList.ID;
		    AddListOptionItem(newList);
		    mySavedList.children('li').remove();
		    AddSavedItems(items);
		    
		    SetListsChangeEvent();
		});
	    }
	    else{
		AddSavedItems(items);
	    }
	    break;
	case 'MySavedList':
	    AddSavedItems(items);
	    break;
	}
    };
    
    AddSavedItems =  function(items){

	for (var i=0; i<items.length; i++){
	    AddSavedItem(items,i);
	};
    }
    
    AddSavedItem = function(items,i){
	
	var url = baseUrl + "AddSavedListItem";
	
	switch(subPage){
	case 'NewSavedList':
	    d = '{"listId":' + ListId + ',"itemName":"' + items[i].ItemName + '","quantity":' + items[i].ItemQuantity + '}';
	    break;
	case 'MySavedList':
	    d = '{"listId":' + SeletedListId + ',"itemName":"' + items[i].ItemName + '","quantity":' + items[i].ItemQuantity + '}';
	    break;
	}
	
	Ajax(url, d, function(data, textStatus, jqXhr){
	    
	    if (data != null){
		
		var savedItem = $.parseJSON(data);

		switch(subPage){
		case 'NewSavedList':
		    AddNewSavedItem(savedItem);
		    AddMySavedItem(savedItem);
		    break;
		case 'MySavedList':
		    AddMySavedItem(savedItem);
		    break;
		}
		
		if (i+1 == items.length){

		    $('.DeleteNewSavedItem').click(function(){

			var itemId = $(this).attr('data-id');
			DeleteItem(itemId);
			newSavedList.children('li[data-id="'+itemId+'"]').remove();

			if (newSavedList.children('li').length == 0){
			    $('#m1-Lists-txtSavedListName').val('');
			    ListId = -1;
			}
		    });

		    $('.DeleteMySavedItem').click(function(){

			var itemId = $(this).attr('data-id');
			DeleteItem(itemId);
			mySavedList.children('li[data-id="'+itemId+'"]').remove();

			if (mySavedList.children('li').length == 0){
			    this.GetLists();
			}
		    });

		    $('.SavedItemQuantity').keyup(function(){

			var id = $(this).attr('data-id');
			var quantity = $(this).val();
			savedList.UpdateItem(id, quantity);
		    });
		}
	    }
	});
    };
    
    AddNewSavedItem = function(item){

	var newListItem = $('<li/>', {
	    html : newSavedListItemTemplate.html()
	});

	newListItem.css('height',newSavedListItemHeight)
	newListItem.attr('data-id',item.ID);

	var foodName = newListItem.find('#m1-Lists-NewSavedItemName');
	foodName.html(item.FOOD_NAME);

	var quantity = newListItem.find('#m1-Lists-NewSavedItemQuantity');
	quantity.html(item.QUANTITY);

	var btnDelete = newListItem.find('#m1-Lists-btnRemoveNewSavedItem');
	btnDelete.addClass('DeleteNewSavedItem');
	btnDelete.attr('data-id',item.ID);

	newSavedList.append(newListItem);
    }
    
    AddMySavedItem = function(item){

	var newListItem = $('<li/>', {
	    html : mySavedListTemplate.html()
	});

	newListItem.css('height',mySavedListItemHeight)
	newListItem.attr('data-id',item.ID);

	var foodName = newListItem.find('#m1-Lists-txtSavedItem');
	foodName.html(item.FOOD_NAME);

	var quantity = newListItem.find('#m1-Lists-SavedItemQuantity');
	quantity.attr('data-id',item.ID)
	quantity.addClass('SavedItemQuantity');
	quantity.val(item.QUANTITY);

	var unit = newListItem.find('#m1-Lists-MySavedListItemUnit');
	unit.html(item.UNIT_NAME);

	var btnDelete = newListItem.find('#m1-Lists-btnDeleteSavedItem');
	btnDelete.addClass('DeleteMySavedItem');
	btnDelete.attr('data-id',item.ID);

	mySavedList.append(newListItem);
    }
    
    this.Clear = function(){
	
	$('#m1-Lists-txtSavedListName').val('');
	$('#m1-Lists-txtSavedListItemName').val('');
	$('#m1-Lists-txtSavedListItemQuantity').val('');
	items.Clear();
	newSavedList.children().remove();
	ListId = -1;
    }
    
    GetMySavedListItems = function(){
	
	var url = baseUrl + "GetSavedListItems";
	var d = '{"listId":' + SeletedListId + '}';
	
	mySavedList = $('#m1-Lists-MySavedList');

	Ajax(url, d, function(data, textStatus, jqXhr) {
	    if (data != null){
		
		var mySavedListItems = $.parseJSON(data);
		mySavedList.children('li').remove();
		
		for (var i=0; i<mySavedListItems.length; i++){
		    
		    AddMySavedItem(mySavedListItems[i]);
		}
		
		$('.DeleteMySavedItem').click(function(){
		    
		    var itemId = $(this).attr('data-id');
		    DeleteItem(itemId);
		});
		    
		$('.SavedItemQuantity').keyup(function(){

		    var id = $(this).attr('data-id');
		    var quantity = $(this).val();
		    savedList.UpdateItem(id, quantity);
		});
	    }
	});
    }
    
    this.DeleteList = function(listId){
	
	var url = baseUrl + "DeleteSavedList";
	var d = '{"listId":' + SeletedListId + '}';
	
	Ajax(url, d, function(data, textStatus, jqXhr) {
	    
	    SeletedListId = -1;
	    savedList.GetLists();
	});
    }
    
    DeleteItem = function(itemId){
	
	var url = baseUrl + "DeleteSavedListItem";
	var d = '{"itemId":' + itemId + '}';
	
	Ajax(url, d, function(data, textStatus, jqXhr) {
	    
	    mySavedList.children('li[data-id="'+itemId+'"]').remove();
	    
	    if (mySavedList.children('li').length == 0){
		select = $('#m1-Lists-hidden-select-comboboxSavedLists');
	    }
	 	    
	});
    }
    
    this.Update = function(){
	
	var url = baseUrl + "UpdateSavedListItem";
	
	mySavedList.children('li').each(function(){

	    var id = $(this).attr('data-id');
	    var quantity = $(this).find('#m1-Lists-SavedItemQuantity');
	    var d = '{"id":' + id + ',"quantity":' + quantity.val() + '}';
	    
	    Ajax(url, d, function(data, textStatus, jqXhr) {
	    });
	});
    };
    
    this.UpdateItem = function(id, quantity){
	
	var url = baseUrl + "UpdateSavedListItem";
	var d = '{"id":' + id + ',"quantity":' + quantity + '}';

	Ajax(url, d, function(){
	});
    };
}

$(function(){
    
    savedList = new SavedList();
    savedList.Init();
    
    $('#m1-Lists-UpdateSavedListItem').click(function(){
	
	savedList.Update();
    });
    
    $('#m1-Lists-btnNewSavedList').click(function(){
	
	savedList.Clear();
    });
    
    $('#m1-Lists-DeleteMySaveList').click(function(){
	
	$('#deleteSavedListDialog').dialog('open');
    });

    $('#m1-Lists-SavedListAddItems').click(function(){

	if ($('#m1-Lists-txtSavedListName').val() != ''){
	    subPage = 'NewSavedList';
	    searchItems.ClearSearchBox();
	    phoneui.gotoPage('m1-AddFood', 'SLIDE_UP');
	}
	else{
	    $('#message').html('לא נבחר שם לרשימה');
	    $('#message').dialog('open');
	}
    });

    $('#m1-Lists-MySavedListAddItems').click(function(){
	subPage = 'MySavedList';
	searchItems.ClearSearchBox();
	phoneui.gotoPage('m1-AddFood', 'SLIDE_UP');
    })
	
    Array.prototype.FindById = function(val){

	for(var f=0; f<this.length; f++){
	    if (this[f].Id == val)
		return f;
	}
	return -1;
    };

    Array.prototype.Find = function(val){

	for(var f=0; f<this.length; f++){
	    if (this[f].Name == val)
		return f;
	}
	return -1;
    };

    Array.prototype.Clear = function(){

	this.length = 0;
    };
});