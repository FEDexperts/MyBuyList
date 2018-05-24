var Menus = function(){
    
    var menus = this;
    
    var selectedMenus;
    var selectedMenusItemTemplate;
    
    var searchedMenus;
    var searchedMenusItemTemplate;
    
    this.Init = function(){
	  
	searchedMenus = $('#m1-Lists-SearchedMenus');
	searchedMenusItemTemplate = searchedMenus.children('li').first();
	searchedMenus.children().remove();
	
	selectedMenus = $('#m1-Lists-SelectedMenus');
	selectedMenusItemTemplate = selectedMenus.children('li').first();
	
	//this.GetSelectedMenus();
		
	$('#m1-Lists-btnSearchMenu').click(function(){

	    var searchValue = $('#m1-Lists-txtSearchedMenu').val();
	    var url = baseUrl + "SearchMenus";
	    var d = '{"userId":' + userId + ',"searchValue":"' + searchValue + '"}';
	    
	    Ajax(url, d, function(data, textStatus, jqXhr){
		if (data != null){
		    
		    searchedMenus.children().remove();
		    
		    var menus = $.parseJSON(data);
		    
		    for(var i = 0; i<menus.length; i++){	
		
			var newListItem = $('<li/>',
				{
				    html : searchedMenusItemTemplate.html(),
				    "id" : 'm1-Lists-listMenuItem' + menus[i].MenuId
				});

			newListItem.css('height','36px')
			
			var oMenuName = newListItem.find('#m1-Lists-txtMenuName2');
			oMenuName.html(menus[i].MenuName);
			
			var check = newListItem.find('#m1-Lists-checkbox4 input');
			check.addClass('select-menu');
			check.attr('data-id',menus[i].MenuId);
			
			searchedMenus.append(newListItem);
		    }
		    
		    $('.select-menu').change(function(){
			var menuId = $(this).attr('data-id');
			var url = baseUrl + "AddMenuToShoppingList";
			var check = $(this).attr('checked') == "checked" ? true : false;
			var d = '{"userId":' + userId + ',"menuId":' + menuId + ',"check":' + check + '}';
			
			Ajax(url, d, function(data, textStatus, jqXhr){
			    
			    menus.GetSelectedMenus();
			});
		    });
		};
	    });
	});
    }
    
    this.GetSelectedMenus = function(){

	var url = baseUrl + "GetMenusInShoppingList";
	var d = '{"userId":' + userId + '}';

	var selectedMenus = $('#m1-Lists-SelectedMenus');
	selectedMenus.children().remove();

	Ajax(url, d, function(data, textStatus, jqXhr){
	    if (data != null){

		selectedMenus.children().remove();

		var menus = $.parseJSON(data);

		for(var i = 0; i<menus.length; i++){	

		    var newListItem = $('<li/>',
			    {
			html : selectedMenusItemTemplate.html(),
			"id" : 'm1-Lists-listSelectedMenuItem' + menus[i].MenuId
			    });

		    newListItem.css('height','36px')

		    var oMenuName = newListItem.find('#m1-Lists-txtMenuName');
		    oMenuName.html(menus[i].MENU_NAME);

		    var oRemoveMenu = newListItem.find('#m1-Lists-btnRemoveMenu');
		    oRemoveMenu.addClass('remove-menu');
		    oRemoveMenu.attr('data-id',menus[i].MenuId);

		    selectedMenus.append(newListItem);
		}

		$('.remove-menu').click(function(){
		    var url = baseUrl + "RemoveMenuFromShoppingList";
		    var menuId = $(this).attr('data-id');
		    var d = '{"userId":' + userId + ',"menuId":' + menuId + '}';

		    Ajax(url, d, function(data, textStatus, jqXhr){
			var li = $('li[id="m1-Lists-listSelectedMenuItem' + menuId + '"]');
			li.hide();
		    });
		});
	    };
	});
    }
    
    this.ClearSearch = function(){
	
	searchedMenus.children().remove();
	$('#m1-Lists-txtSearchedMenu').val('');
    }
}

$(function(){
    
    menus = new Menus();
    menus.Init();
});