//Menu Edit - Client functionality (Drag & Drop)
//=================================================
var mouseState = 'up';
var clone = null;
var target = null;

function IsFireFox() {
    if (navigator.appName == 'Netscape')
        return true;
    else return false;
}

function ResetColor(e) {
    var evt = e || window.event;
    var evtTarget = evt.target || evt.srcElement;

    var mainDiv = document.getElementById("menu_details");
    var tableCellElements = mainDiv.getElementsByTagName("td");
    // iterate through the array and find it the id exists 
    for (i = 0; i < tableCellElements.length; i++) {
        if (tableCellElements[i].id.indexOf('tdMealRecipes') != -1) {
            var dZone = tableCellElements[i];
            tableCellElements[i].className = 'DefaultDropZoneColor';
        }
    }
}

function IsInDropZone(evtTarget) {
    if (evtTarget == null || evtTarget.parentNode.id == null) {
        return false;
    }

    var result = false;

    var mealTypePatternCell = $(evtTarget).parents('td[id$=tdMealRecipes]');

    result = (mealTypePatternCell != null && mealTypePatternCell[0] != null);
    
    if (!result && evtTarget.id.indexOf('tdMealRecipes') > 0) {
        var mealTypePatternCell = evtTarget;
        result = true;
    }

    if (result) {
        var i;
        i++;
    }
    return result;
}

function MakeElementDraggable(obj) {
    var startX = 0;
    var startY = 0;

    function InitiateDrag(e) {
        mouseState = 'down';

        var evt = e || window.event;

        startX = parseInt(evt.clientX) + 5;
        startY = parseInt(evt.clientY) - 5;

        clone = obj.cloneNode(true);

        clone.style.position = 'fixed';
        clone.style.top = parseInt(startY) + 'px';
        clone.style.left = parseInt(startX) + 'px';
        clone.className = 'dragElement';

        document.body.appendChild(clone);

        document.onmousemove = Drag;
        document.onmouseup = Drop;

        return false;
    }

    function Drop(e) {

        var evt = e || window.event;

        if (IsFireFox()) {
            e.preventDefault();
        }
        else {
            window.event.returnValue = false;
        }

        var evtTarget = evt.target || evt.srcElement;

        if (IsInDropZone(evtTarget)) {

            if (evtTarget.id.indexOf('tdMealRecipes') != -1) {
                target = evtTarget;
            }
            else {
                target = $(evtTarget).parents('td[id$=tdMealRecipes]')[0];
            }

            var recipeId = clone.getAttribute('recipeId');
            var mealSignature = target.getAttribute('meal_signature');

            ResetColor(e);
            
            PageMethods.AddToMenu_DnD(recipeId, mealSignature, OnComplete);
        }

        function OnComplete() {            
            refreshMealsDetails();
        }

        document.onmouseup = null;
        document.onmousemove = null;

        document.body.removeChild(clone);
        mouseState = 'up';

    }

    function Drag(e) {
        //only drag when the mouse is down  
        var evt = e || window.event;
        if (IsFireFox()) {
            e.preventDefault();
        }
        else {
            window.event.returnValue = false;
        }

        if (mouseState == 'down') {

            var evtTarget = evt.target || evt.srcElement;

            clone.style.top = (evt.clientY + 5) + 'px';
            clone.style.left = (evt.clientX - 5) + 'px';

            ResetColor(e);
            
            //check if mouse is over a DropZone and highlight zone if yes.
            if (IsInDropZone(evtTarget)) {
                if (evtTarget.id.indexOf('tdMealRecipes') != -1) {
                    target = evtTarget;
                }
                else {
                    target = $(evtTarget).parents('td[id$=tdMealRecipes]')[0];
                }
                
                target.className = 'highlightDropZone';

            }

            clone.style.cursor = 'move';
        }
    }

    obj.onmousedown = InitiateDrag;

}

function IsMatch(id, pattern) {
    var regularExpresssion = new RegExp(pattern);
    if (id.match(regularExpresssion)) return true;
    else return false;
}
