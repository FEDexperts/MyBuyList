var user;

var User = function(){
    
    this.Add = function(){
	
	var url = baseUrl + "SaveUser";
	var userName = $('#m1-NewUser-UserName').val();
	var password = $('#m1-NewUser-Password').val();
	var email = $('#m1-NewUser-Email').val();
	var recieveUpdates = $('#m1-NewUser-RecieveUpdates input').is(':checked');
	var firstName = $('#m1-NewUser-FirstName').val();
	var lastName = $('#m1-NewUser-LastName').val();
	var displayName = $('#m1-NewUser-DisplayName').val();
	
	if (userName == '') {
	    alert('שם משתמש ריק');
	    return;
	};
	
	if (password == '') {
	    alert('סיסמה ריקה');
	    return;
	};
	
	if (email == '') {
	    alert('דוא"ל ריק');
	    return;
	};
	
	if (firstName == '') {
	    alert('שם פרטי ריק');
	    return;
	};
	
	if (lastName == '') {
	    alert('שם משפחה ריק');
	    return;
	};
	
	if (displayName == '') {
	    alert('שם תצוגה ריק');
	    return;
	};
	
	var params = '{"userName":"' + userName + '","password":"' + password + '","email":"' + email + '","recieveUpdates":' + recieveUpdates + ',"firstName":"' + firstName + '","lastName":"' + lastName + '","displayName":"' + displayName + '"}';
	
	Ajax(url, params, function(data){
	    
	    if (data == true){

		$('#m1-NewUser-UserName').val('');
		$('#m1-NewUser-Password').val('');
		$('#m1-NewUser-Email').val('');
		$('#m1-NewUser-FirstName').val('');
		$('#m1-NewUser-LastName').val('');
		$('#m1-NewUser-DisplayName').val('');

		phoneui.gotoPage('m1-MyBuyList', "FADE");
	    }
	    else{
		alert('חלה גיאה בשמירת משתמש. ייתכן שהמשתמש קיים.')
	    }
	});
	
    }
}

$(function(){
    
    user = new User();
    
    $('#m1-NewUser-Save').click(function(){
	
	user.Add();
    });
});