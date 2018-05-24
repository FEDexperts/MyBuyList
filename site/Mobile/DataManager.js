var dataMgr;

var FoodMngr = function(){
    
    
}

var DataManager = function(){
    
    var foods;
    var itemsLoaded= false;
    
    this.StoreFoods = function(){
	
	var url = baseUrl + "GetAllFoodList";
	
	CallService(url, null, function(data){

	   if (data != null) {
    		foods = $.parseJSON(data);
    		localStorage.setItem('foods', foods);
    		itemsLoaded = true;
    		alert('Items Loaded');
	    }
	})
    }
    
    this.ItemsLoaded = function()
    {
	return itemsLoaded;
    }
    
    this.GetFoods = function(prefix){
	
	var result = $.grep(foods, function(n,i){
	    var pos = foods[i].FoodName.search(prefix);
	    if (pos != -1)
		return true;
	}, false);
	
	return result;
    }
}

$(function(){
    
    dataMgr = new DataManager();
    dataMgr.StoreFoods();
})