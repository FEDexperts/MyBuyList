/**
 * Notification that the UI is about to transition to a new screen.
 * Perform custom prescreen-transition logic here.
 * @param {String} currentScreenId 
 * @param {String} targetScreenId 
 * @returns {boolean} true to continue transtion; false to halt transition
 */
phoneui.prePageTransition = function(currentScreenId, targetScreenId) {
    // add custom pre-transition code here
    // return false to terminate transition
    
    
    
    return true;
}

/**
 * Notification that the UI has transitioned to a new screen.
 * 
 * @param {String} newScreenId 
 */
phoneui.postPageTransition = function(newScreenId) {
    
    switch(newScreenId)
    {
    case '#m1-AddFood':
	searchItems.Clear();
	break;
    case '#m1-Lists':

	switch (page) {
	case 'ShoppingList':
	    break;
	case 'MissingsList':
	    missingList.AddSearchItems();
		
	    var list = $('#m1-Lists-listMissing');
	    var listItemHeight = list.css('height');
	    var scroller = $('#m1-Lists-panel3-scroller');
	    SetHeight(scroller, list.children('li').length, listItemHeight);
	    break;
	case 'SavedList':
	    savedList.AddSearchItems();
	    break;
	case 'Menus':
	    if (menus != null) menus.GetSelectedMenus();
	    break;
	case 'Recipes':
	    if (recipe != null) recipe.GetSelectedRecipes();
	    break;
	default:
		if (shoppingList != null) shoppingList.GetShoppingList();
	    	if (menus != null) menus.GetSelectedMenus();
		if (savedList != null) savedList.GetLists();
		if (recipe != null) recipe.GetSelectedRecipes();
		if (missingList != null) missingList.RetrieveList();

		break;
	}

	$('#m1-Lists-WelcomeName').html('שלום ' + localStorage.getItem('DisplayName') + ', <a id="m1-Lists-Logout">התנתק</a>');
	
	$('#m1-Lists-Logout').click(function(){
		localStorage.removeItem('UserId');
		localStorage.removeItem('RememberMe');
		localStorage.removeItem('DisplayName');
		phoneui.gotoPage('m1-Login', "FADE");
		
		$('#m1-Login-txtUserName').val('');
		$('#m1-Login-txtPassword').val('');
		$('#m1-Lists-WelcomeName').val('שלום אורח');
		page = null;
	    });
	
	break;
    }
    
}

/**
 * Notification that the page's HTML/CSS/JS is about to be loaded.
 * Perform custom logic here, f.e. you can cancel request to the server.
 * @param {String} targetScreenId 
 * @returns {boolean} true to continue loading; false to halt loading
 */
phoneui.prePageLoad = function(targetScreenId) {
    // add custom pre-load code here
    // return false to terminate page loading, this cancels transition to page as well
    return true;
}

/**
 * Notification that device orientation has changed. 
 * 
 * @param {String} newOrientation 
 */
phoneui.postOrientationChange = function(newOrientation) {
    //orientation = newOrientation;
}

/**
 * Called when document is loaded.
 */
phoneui.documentReadyHandler = function() {
    
    $('#m1-Login-text4').html('');
    
    $('#m1-Lists-tabBar1 div').addClass('pointer-cursor');
    
    // Login
    $('#m1-Login-btnLogin').click(function() {

	var UN = $('#m1-Login-txtUserName').val();
	var PWD = $('#m1-Login-txtPassword').val();

	var url = baseUrl + "CheckCredencials";
	var parameters = '{ "userName":"' + UN + '", "password":"' + PWD + '"}';
	
	CallService(url, parameters, function(data, textStatus, jqXhr){
	   
	    if (data != '') {
		var user = $.parseJSON(data);
		
		if (user != null){
		    var rememberMe = $('#m1-Login-checkBoxRememberMe input').is(':checked');
		    localStorage.setItem('RememberMe', rememberMe);
		    localStorage.setItem('UserId', user.UserId);
		    localStorage.setItem('DisplayName', user.DisplayName);
		    userId = user.UserId
		    
		    while(dataMgr.ItemsLoaded() != true)
			$('#m1-Login-text4').html('טוען פריטים...');
		    
		    phoneui.gotoPage('m1-Lists', "FADE");
		}
		else{
		    $('#message').html('משתמש לא קיים');
		    $('#message').dialog('open');
		}
	    }
	});

    });
    
    var rememberMe = localStorage.getItem('RememberMe');
    if (rememberMe == 'true'){
	
	 //while(dataMgr.ItemsLoaded() != true)
	 //    $('#m1-MyBuyList-text1').html('טוען פריטים...');
	
	phoneui.gotoPage('m1-Lists', "FADE");
    }
    else{
	phoneui.gotoPage('m1-Login', "FADE");
    }
	
}
