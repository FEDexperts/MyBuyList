$(document).ready(function () {
    $('#popUpServingUp').click(function () {
        var hfBaseServings = $('#<%= hfBaseServings.ClientID %>');
        var baseServings = parseInt(hfBaseServings.val());

        var hfExpectedServings = $('#<%= hfExpectedServings.ClientID %>');
        var expectedServings = parseInt(hfExpectedServings.val());

        expectedServings += baseServings;
        $('#<%= hfExpectedServings.ClientID %>').val(expectedServings);

        var txtChooseServings = $('#<%= txtChooseServings.ClientID %>');
        txtChooseServings.val(expectedServings);

        var recipeId = $(this).attr('recipeid');
        var txtServings = $('input[recipeId=' + recipeId + ']');
        txtServings.val(expectedServings);

    });

    $('#servingUp').click(function () {
        var hfBaseServings = $(this).attr('recipeservings');
        var baseServings = parseInt(hfBaseServings);

        var recipeId = $(this).attr('recipeid');
        var txtServings = $('input[recipeId=' + recipeId + ']');
        var expectedServings = parseInt(txtServings.val());

        expectedServings += baseServings;
        txtServings.val(expectedServings);
    });

    $('.linkServingsUp').click(function () {
        var hfBaseServings = $(this).attr('recipeservings');
        var baseServings = parseInt(hfBaseServings);

        var recipeId = $(this).attr('recipeid');
        var txtServings = $('input[recipeId=' + recipeId + ']');
        var expectedServings = parseInt(txtServings.val());

        expectedServings += baseServings;
        txtServings.val(expectedServings);
    });

    $('#popUpServingDown').click(function () {
        var hfBaseServings = $('#<%= hfBaseServings.ClientID %>');
        var baseServings = parseInt(hfBaseServings.val());

        var hfExpectedServings = $('#<%= hfExpectedServings.ClientID %>');
        var expectedServings = parseInt(hfExpectedServings.val());

        var hfBaseExpectedServings = $('#<%= hfBaseExpectedServings.ClientID %>');
        var baseExpectedServings = parseInt(hfBaseExpectedServings.val());

        if (expectedServings > baseExpectedServings)
            expectedServings -= baseServings;
        $('#<%= hfExpectedServings.ClientID %>').val(expectedServings);

        var txtChooseServings = $('#<%= txtChooseServings.ClientID %>');
        txtChooseServings.val(expectedServings);
    });

    $('#servingDown').click(function () {
        var hfBaseServings = $(this).attr('recipeservings');
        var baseServings = parseInt(hfBaseServings);

        var recipeId = $(this).attr('recipeid');
        var txtServings = $('input[recipeId=' + recipeId + ']');
        var expectedServings = parseInt(txtServings.val());

        if (expectedServings > baseServings) {
            expectedServings -= baseServings;
            txtServings.val(expectedServings);
        }
    });

    $('.linkServingsDown').click(function () {
        var hfBaseServings = $(this).attr('recipeservings');
        var baseServings = parseInt(hfBaseServings);

        var recipeId = $(this).attr('recipeid');
        var txtServings = $('input[recipeId=' + recipeId + ']');
        var expectedServings = parseInt(txtServings.val());

        if (expectedServings > baseServings) {
            expectedServings -= baseServings;
            txtServings.val(expectedServings);
        }
    });
});

function refreshMealsDetails() {
    var btnRefresh = document.getElementById('<%= btnRefresh.ClientID %>');
    setDirty();
    btnRefresh.click();
}

function showPopupChooseMeal(sender) {
    var recipeId = $('#' + sender.id).attr('recipeId');
    var recipeName = $('#' + sender.id).attr('recipeName');
    var recipeServings = $('#' + sender.id).attr('recipeServings');
    var recipeExpectedServings = $('#' + sender.id).attr('recipeExpectedServings');

    var hiddenField = document.getElementById('<%= hfRecipeId.ClientID %>');
    hiddenField.value = recipeId;

    var lblRecipeName = document.getElementById('<%= lblChooseMealRecipeName.ClientID %>');
    lblRecipeName.innerHTML = AdjustRecipeNameLength(recipeName, 30);

    var txtChooseServings = document.getElementById('<%= txtChooseServings.ClientID %>');
    txtChooseServings.value = '';
    txtChooseServings.value = recipeExpectedServings;

    var hfBaseServings = document.getElementById('<%= hfBaseServings.ClientID %>');
    hfBaseServings.value = recipeServings;

    var hfExpectedServings = document.getElementById('<%= hfExpectedServings.ClientID %>');
    hfExpectedServings.value = recipeExpectedServings;

    var hfBaseExpectedServings = document.getElementById('<%= hfBaseExpectedServings.ClientID %>');
    hfBaseExpectedServings.value = recipeExpectedServings;

    var hiddenBtn = document.getElementById('<%= hiddenBtn.ClientID %>');
    hiddenBtn.click();
}

function activatePopupPanelOk() {
    var popupPanelOk = document.getElementById('<%= popupPanelOk.ClientID %>');
    setDirty();
    popupPanelOk.click();
}

function activateCategoriesPopupPanelOk() {
    var categoriesPopupOK = document.getElementById('<%= categoriesPopupOK.ClientID %>');
    setDirty();
    categoriesPopupOK.click();
}

function clickHiddenButton2() {
    var hiddenBtn2 = document.getElementById('<%= hiddenBtn2.ClientID %>');
    hiddenBtn2.click();
}

function closeChooseMealDialogBox() {
    var btnCancelHdn = document.getElementById("<%=btnCancelHdn.ClientID %>");
    btnCancelHdn.click();
    return false;
}

function AdjustRecipeNameLength(text, length) {
    if (text.length > length) {
        text = text.substr(0, length - 3) + "...";
    }
    return text;
}