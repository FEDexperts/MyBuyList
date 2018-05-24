var baseUrl = 'http://www.mybuylist.com/MyBuyListService/WebMobileAccessService.svc/';
var userId = localStorage.getItem('UserId');

var recipe = null;
var missingList = null;
var menus = null;
var savedList = null;
var shoppingList = null;
var searchItems = null;
var page = null;
var subPage = null;
var orientation = null;

$(function(){
    
    $('<div id="message" class="error-message"></div>').appendTo('body');
    
    $('#message').dialog({
	autoOpen: false,
	modal: true,
	show: 'Fade',
	hide: 'fade',
	title: 'שגיאה',
	open: function(){
		$('.ui-dialog-title').addClass('heb-label');
    	}
    });
    
    $('<div id="deleteMissingListDialog" class="error-message">האם למחוק את רשימת החוסרים?</div>').appendTo('body');
    
    $('#deleteMissingListDialog').dialog({
	autoOpen: false,
	modal: true,
	show: 'fade',
	hide: 'fade',
	title: 'רשימת חוסרים',
	open: function(){
		$('.ui-dialog-title').addClass('heb-label');
        },
        buttons: 
        	{ מחק: function(){missingList.DeleteList(); $(this).dialog('close')},
        	  בטל: function(){$(this).dialog('close');}}
    });
    
    $('<div id="deleteSavedListDialog" class="error-message">האם למחוק את הרשימה הקבועה?</div>').appendTo('body');
    
    $('#deleteSavedListDialog').dialog({
	autoOpen: false,
	modal: true,
	show: 'fade',
	hide: 'fade',
	title: 'רשימה קבועה',
	open: function(){
		$('.ui-dialog-title').addClass('heb-label');
        },
        buttons: 
        	{ מחק: function(){
            		select = $('#m1-Lists-hidden-select-comboboxSavedLists');
        		savedList.DeleteList(select.val());
            		$(this).dialog('close')},
        	  בטל: function(){$(this).dialog('close');}}
    });
});

function Ajax(url, parameters, callback)
{
    $.ajax( {
	type : 'POST',
	contentType : 'application/json; charset=UTF-8',
	data : parameters,
	url : url,
	dataType : 'json',
	success : function(data, textStatus, jqXhr) {
		callback(data.d, textStatus, jqXhr)
	},
	error: function(jqXHR, textStatus, errorThrown ){
	    callback(jqXHR.responseText, textStatus, jqXHR)
	}
    });
}

function Get(url, parameters, callback)
{
    $.ajax( {
	type : 'GET',
	contentType : 'application/json; charset=UTF-8',
	data : parameters,
	url : url,
	dataType : 'json',
	success : function(data, textStatus, jqXhr) {
		callback(data.d, textStatus, jqXhr)
	},
	error: function(jqXHR, textStatus, errorThrown ){
	    callback(jqXHR.responseText, textStatus, jqXHR)
	}
    });
}

function CallService(url, parameters, callback)
{
    $.ajax( {
	type : 'POST',
	contentType : 'application/json; charset=UTF-8',
	data : parameters,
	url : url,
	dataType : 'json',
	success : function(data, textStatus, jqXhr) {
		callback(data.d, textStatus, jqXhr)
	},
	error: function(jqXHR, textStatus, errorThrown ){
	    callback(jqXHR.responseText, textStatus, jqXHR)
	}
    });
}

function Post(url, parameters, callback)
{
    $.ajax( {
	type : 'POST',
	contentType : 'application/json; charset=UTF-8',
	data : parameters,
	url : url,
	dataType : 'json',
	success : function(data, textStatus, jqXhr) {
		callback(data.d, textStatus, jqXhr)
	},
	error: function(jqXHR, textStatus, errorThrown ){
	    callback(jqXHR.responseText, textStatus, jqXHR)
	}
    });
}

function SyncAjax(url, d, callback)
{
    $.ajax( {
	type : 'POST',
	contentType : 'application/json; charset=UTF-8',
	data : d,
	url : url,
	dataType : 'json',
	async: false,
	success : function(data, textStatus, jqXhr) {
		callback(data.d, textStatus, jqXhr)
        },
        error: function(jqXHR, textStatus, errorThrown ){
    		callback(jqXHR.responseText, textStatus, jqXHR)
        }
    });
}

function SetHeight(obj, length, height){
    
    height = parseInt(height.replace('px',''));
    var totalHeight = length * height;
    obj.css('height', totalHeight + 'px !important');
}