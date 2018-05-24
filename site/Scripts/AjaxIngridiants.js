var ingridiantsContainer;

var IngridiantsContainer = function () {
    
    var ingridiantsArr = new Array();
    var fractures = new Array()

    this.SelectedIngrediant;

    this.Length = function(){
        return ingridiantsArr.length;
    }

    this.Exists = function (ingridiant) {
        for (var i = 0; i < ingridiantsArr.length; i++) {
            if (ingridiantsArr[i].Id == ingridiant.Id)
                return true;
        }
        return false;
    }

    this.IndexOf = function (id) {
        for (var i = 0; i < ingridiantsArr.length; i++) {
            if (ingridiantsArr[i].Id == id)
                return i;
        }
        return -1;
    }

    this.Add = function (ingridiant) {

        ingridiant.Id = ingridiantsArr.length - 1;
        ingridiantsArr.push(ingridiant);
    }

    this.Update = function (ingridiant) {

        var indx = ingridiant.Id;
        ingridiantsArr[indx].FoodId = ingridiant.FoodId;
        ingridiantsArr[indx].IntQuantity = ingridiant.IntQuantity;
        ingridiantsArr[indx].FractionValue = ingridiant.FractionValue;
        ingridiantsArr[indx].FractionDisplay = ingridiant.FractionDisplay;
        ingridiantsArr[indx].MeasurementUnitId = ingridiant.MeasurementUnitId;
        ingridiantsArr[indx].MeasurementUnitName = ingridiant.MeasurementUnitName;
        ingridiantsArr[indx].Remarks = ingridiant.Remarks;
        ingridiantsArr[indx].FoodName = ingridiant.FoodName;
        ingridiantsArr[indx].DisplayIngredient = ingridiant.DisplayIngredient;
    }

    this.Delete = function (id) {

        ingridiantsArr.splice(id, 1);
    }

    this.GetItem = function (id) {

        return ingridiantsArr[id];
    }

    this.GetList = function () {
        return ingridiantsArr;
    }

    this.SetList = function (arr) {
        for (var i = 0; i < arr.length; i++) {

            var ingrediant = new Ingridiant();

            ingrediant.Id = i;
            ingrediant.FoodId = arr[i].FoodId;
            ingrediant.FoodName = arr[i].FoodName;
            ingrediant.MeasurementUnitId = arr[i].MeasurementUnitId;
            ingrediant.MeasurementUnitName = arr[i].MeasurementUnitName;
            ingrediant.IntQuantity = arr[i].CompleteValue != null ? arr[i].CompleteValue : 0;
            ingrediant.FractionValue = arr[i].FractionValue != null ? arr[i].FractionValue : 0;
            ingrediant.FractionDisplay = arr[i].FractionDisplay;
            ingrediant.Remarks = arr[i].Remarks;
            ingrediant.DisplayIngredient = arr[i].DisplayIngredient;
            this.Add(ingrediant);
        }
    }
}

var Ingridiant = function () {

    this.Id;
    this.RecipeId;
    this.FoodId;
    this.FoodName;
    this.MeasurementUnitId;
    this.MeasurementUnitName;
    this.Quantity;
    this.IntQuantity;
    this.Remarks;
    this.CompleteValue;
    this.FractionValue;
    this.FractionDisplay;
    this.DisplayIngredient;
    this.DecimalSeperator;

    this.IsValid = function () {

        return (this.IntQuantity != '' || this.FractionValue != '') && this.FoodId != '';
    }
}

$(document).ready(function () {

    $('#updateIngridiant').hide();

    $('#addIngridiant').mousedown(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_down.png")');
    })

    $('#addIngridiant').mouseup(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_over.png")');
    })

    $('#addIngridiant').mouseout(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_up.png")');
    })

    $('#addIngridiant').mouseover(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_over.png")');
    })

    $('#addIngridiant, #updateIngridiant').click(function () {

        var ingridiant = new Ingridiant();

        var intQuantity = $('#' + quantityClientId).val();

        ingridiant.Id = $('#' + hfSelectedIngridiantClientId).val();
        ingridiant.FoodId = $('#' + hfSelectedIngridiantClientId).val();
        ingridiant.IntQuantity = intQuantity != '' ? intQuantity : 0;
        ingridiant.FractionDisplay = $('#' + fractionClientId + ' option:selected').text();
        ingridiant.FractionValue = $('#' + fractionClientId).val();
        ingridiant.MeasurementUnitName = $('#' + unitClientId + ' option:selected').text();
        ingridiant.MeasurementUnitId = $('#' + unitClientId).val();
        ingridiant.FoodName = $('#' + foodNameClientId).val();
        ingridiant.Remarks = $('#' + foodRemarkClientId).val();
        ingridiant.DisplayIngredient = ingridiant.FractionDisplay + (intQuantity != '' ? intQuantity : '') + ' ' + ingridiant.MeasurementUnitName + ' ' + ingridiant.FoodName;

        if (!ingridiant.IsValid()) return;

        if (!ingridiantsContainer.Exists(ingridiant)) {

            ingridiantsContainer.Add(ingridiant);
        }
        else {
            ingridiantsContainer.Update(ingridiant);
        }

        ShowList();

        $('#' + quantityClientId).val('');
        $('#' + fractionClientId ).val('');
        $('#' + foodNameClientId).val('');
        $('#' + foodRemarkClientId).val('');

        $('#addIngridiant').show();
        $('#updateIngridiant').hide();
    })

    $('#updateIngridiant').mousedown(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_down.png")');
    })
    $('#updateIngridiant').mouseup(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_over.png")');
    })
    $('#updateIngridiant').mouseout(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_up.png")');
    })
    $('#updateIngridiant').mouseover(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_over.png")');
    })
})

function ShowList() {

    var ingredients = $('#' + ingredientsClientId);
    ingredients.html('');

    for (var i = 0; i < ingridiantsContainer.Length() ; i++) {

        var ingridiant = ingridiantsContainer.GetItem(i);
        ingridiant.Id = i;

        var div = $('<div/>').addClass('row').attr('data-id', ingridiant.Id);
        var spanEdit = $('<a />').html('עדכן').addClass('listBtn').addClass('updateBtn').addClass('modifyBtn').addClass('col').attr('data-id', ingridiant.Id);
        div.append(spanEdit);
        var spanDelete = $('<a />').html('מחק').addClass('listBtn').addClass('deleteBtn').addClass('deleteBtn').addClass('col').attr('data-id', ingridiant.Id);
        div.append(spanDelete);
        var spanDisplayIngredient = $('<span />').html(ingridiant.DisplayIngredient).addClass('floatRight').addClass('col');
        div.append(spanDisplayIngredient);
        var spanFoodRemark = $('<span />').html(ingridiant.Remarks).addClass('floatRight');
        div.append(spanFoodRemark);

        ingredients.append(div);
    }

    var listAsJSON = JSON.stringify(ingridiantsContainer.GetList());
    $('#' + hfIngridiantsClientId).val(listAsJSON);
    initEvents();
}

function InitAjaxIngediantsControl() {

    ingridiantsContainer = new IngridiantsContainer();
}

function ShowSavedListIngridiant(arr) {

    InitAjaxIngediantsControl();
    ingridiantsContainer.SetList(arr);
    ShowList()
}

function initEvents() {

    $('.modifyBtn').click(function () {

        var id = $(this).attr('data-id');
        var ingridiant = ingridiantsContainer.GetItem(id);

        $('#' + quantityClientId).val(ingridiant.IntQuantity != 0 ? ingridiant.IntQuantity : '');
        $('#' + fractionClientId).val(ingridiant.FractionValue);
        $('#' + hfFoodIdClientId).val(ingridiant.FoodId);
        $('#' + foodNameClientId).val(ingridiant.FoodName);
        $('#' + foodRemarkClientId).val(ingridiant.Remarks);
        $('#' + unitClientId).val(ingridiant.MeasurementUnitId);
        $('#' + hfSelectedIngridiantClientId).val(id);

        $('#addIngridiant').hide();
        $('#updateIngridiant').show();
    })

    $('.deleteBtn').click(function () {
        var id = $(this).attr('data-id');
        ingridiantsContainer.Delete(id);
        $('.row[data-id=' + id + ']').hide();

        var listAsJSON = JSON.stringify(ingridiantsContainer.GetList());
        $('#' + hfIngridiantsClientId).val(listAsJSON);
    })
}

function OnClientItemSelected(behaviour, args) {
    var value = args._value.Second;
    $('#' + hfSelectedIngridiantClientId).val(value);
}

function OnClientShowing(behaviour, e) {
    var ResultsDiv = behaviour.get_completionList();

    for (var i = 0; i < ResultsDiv.childNodes.length; i++) {

        var item = ResultsDiv.childNodes[i];
        var text = item._value.First;
        item.innerText = text;
    }
}