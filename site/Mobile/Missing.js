var MissingList = function(){
	
    var missingList = this;
    
    var list;
    var listItemTemplte;
    var listItemHeight;
    var storedData;
    
    this.Init = function(){
	
	list = $('#m1-Lists-listMissing');
	listItemTemplte = list.children('li:first');
	listItemHeight = listItemTemplte.css('height');
	storedData = '';
    }
    
    this.AddSearchItems = function(){
	
	var items = searchItems.GetSelectedItems();
	var url = baseUrl + "AddToMissingList";
	
	for (var i=0; i<items.length; i++){
	    
	    $('#m1-Lists-text2').show();

	    var item = items[i].ItemName;
	    var quantity = items[i].ItemQuantity; 
	    var parameters = '{ "userId": ' + userId + ', "item": "' + item + '", "quantity": ' + quantity + '}';
	    
	    CallService(url, parameters, function(data, textStatus, jqXhr){
		
		var item = $.parseJSON(data);
		AddSingleItem(item);

		$('.delete-item').click(function() {
		    var id = $(this).attr('data-id');
		    DeleteFromMissingList(id);
		});

		$('.MissingQuantity').keyup(function(){

		    var id = $(this).attr('data-id');
		    var quantity = $(this).val();
		    missingList.UpdateItem(id, quantity);
		});
	    })
	};
    }
    
    AddSingleItem = function(listItem){
	
	var newListItem = $('<li/>', {
	    html : listItemTemplte.html()
	});

	newListItem.css('height', listItemHeight);
	newListItem.attr('data-id',listItem.ID);

	var item = newListItem.find('#m1-Lists-txtMissingItem');
	item.addClass('heb-label');
	item.html(listItem.FOOD_NAME);

	var txtQuantity = newListItem.find('#m1-Lists-MissingQuantity');
	txtQuantity.attr('data-id',listItem.ID);
	txtQuantity.addClass('MissingQuantity');
	txtQuantity.val(listItem.QUANTITY);

	var unit = newListItem.find('#m1-Lists-MissingItemUnit');
	unit.html(listItem.UNIT_NAME);
	
	var image = newListItem.find('#m1-Lists-image2');
	image.attr('src',listItem.IMAGE_SRC);

	var btnDelete = newListItem.find('#m1-Lists-btnDelete');
	btnDelete.addClass('delete-item pointer-cursor');
	btnDelete.attr('data-id', listItem.ID);

	list.append(newListItem);
    }
    
    this.RetrieveList = function() {
	
	list.children().remove();

	var url = baseUrl + "GetMissingListDetails";
	var userId = localStorage.getItem('UserId');
	var parameters = '{"userId":' + userId + '}';

	CallService(url, parameters, function(data, textStatus, jqXhr){

	    if (data != null) {
		
		storedData = data;
		
		var oList = $.parseJSON(data);
		if (oList != null){
		    for ( var i = 0; i < oList.length; i++) {
			
			AddSingleItem(oList[i]);
		    }
		    
		    if (list.children('li').length != 0)
			    $('#m1-Lists-text2').show();

		    $('.delete-item').click(function() {
			var id = $(this).attr('data-id');
			DeleteFromMissingList(id);
		    });
		    
		    $('.MissingQuantity').keyup(function(){
			
			var id = $(this).attr('data-id');
			var quantity = $(this).val();
			missingList.UpdateItem(id, quantity);
		    });
		}
	    }
	});
    };
    
    DeleteFromMissingList = function(id) {

	var url = baseUrl + "DeleteFromMissingList";
	var userId = localStorage.getItem('UserId');
	var d = '{"id":' + id + '}';
	
	CallService(url, d, function(){
	});
	
	list.children('li[data-id=' + id + ']').remove();
	
	if (list.children('li').length == 0)
	    $('#m1-Lists-text2').hide();
    };
    
    this.DeleteList = function(){
	
	list.children('li').each(function(){

	    var id = $(this).attr('data-id');
	    DeleteFromMissingList(id);
	});
    };
    
    this.Update = function(){
	
	list.children('li').each(function(){

	    var id = $(this).attr('data-id');
	    var url = baseUrl + "UpdateMissingListItem";
	    var quantity = $(this).find('#m1-Lists-MissingQuantity');
	    var d = '{"id":' + id + ',"quantity":' + quantity.val() + '}';

	    Ajax(url, d, function(){
	    });
	});
    };
    
    this.UpdateItem = function(id, quantity){
	
	var url = baseUrl + "UpdateMissingListItem";
	var d = '{"id":' + id + ',"quantity":' + quantity + '}';

	Ajax(url, d, function(){
	});
    };
}

$(function() {

    missingList = new MissingList();
    missingList.Init();

    $('#m1-Lists-SaveMissinChanges').click(function() {
	missingList.Update();
    });

    $('#m1-Lists-text1').click(function() {
	page = 'MissingsList';
	searchItems.ClearSearchBox();
	phoneui.gotoPage('m1-AddFood', 'SLIDE_UP');
    })

    $('#m1-Lists-text2').hide();
    $('#m1-Lists-text2').click(function() {

	$('#deleteMissingListDialog').dialog('open');
    });
})