//Menu Meals - Client functionality (Drag & Drop)
//=================================================
var mouseState = 'up';
var clone = null; 
var target = null;
var deleteItem = null;
var dinersItem = null;

var mealTypePattern = 'mealTypeCell';
var titlePattern = ".+_lblTitle$"

var uniqueNumber = 1; 

function IsFireFox() 
{
    if(navigator.appName == 'Netscape') 
     return true; 
     else return false; 
}


function DeleteItem(e) 
{        
    var evt = e || window.event;
    var evtTarget = evt.target || evt.srcElement;
    
    deleteItem = evtTarget;    
    var mealId = deleteItem.getAttribute('mealId');
    var recipeId = deleteItem.getAttribute('recipeId');
    
    PageMethods.RemoveMealRecipe(mealId, recipeId, OnRemoveRecipeSucceeded, OnActionFailed);  
    
}

function OnRemoveRecipeSucceeded(results)
{
    //deleteItem.parentNode.parentNode.removeChild(deleteItem.parentNode);
    var container = deleteItem.parentNode.parentNode.parentNode.parentNode;
    
    container.parentNode.removeChild(container);   
}

function Servings_Change(e)
{
    var evt = e || window.event;
    var evtTarget = evt.target || evt.srcElement;
    
    var mealId = evtTarget.getAttribute('mealId');
    var recipeId = evtTarget.getAttribute('recipeId');

    dinersItem = evtTarget;
    PageMethods.SaveServings(evtTarget.value, mealId, recipeId, OnSaveDinersSucceeded, OnActionFailed); 
}

function DinersChange(element, menuId, dayIndex, mealTypeId)
{
    dinersItem = element;
    PageMethods.SaveDiners(element.value, menuId, dayIndex, mealTypeId, OnSaveDinersSucceeded, OnActionFailed); 
}

function Diners_Change(element, menuId, courseTypeId)
{
    dinersItem = element;
    PageMethods.Save_Diners(element.value, menuId, courseTypeId, OnSaveDinersSucceeded, OnActionFailed); 
}
function OnSaveDinersSucceeded(results)
{
    if(results[0] == false)
    {
        alert(results[1]);
        dinersItem.value = results[2]; 
    }
}

function OnSaveSucceeded(results)
{
}

function OnActionFailed(results) {
    alert(results);
}



function ResetColor(e) 
{
    var evt = e || window.event;
    var evtTarget = evt.target || evt.srcElement;    

    var mainDiv = document.getElementById("main");
    var tableCellElements = mainDiv.getElementsByTagName("td");    
    // iterate through the array and find it the id exists 
    for(i = 0; i < tableCellElements.length; i++) 
    {
        if(tableCellElements[i].id.indexOf(mealTypePattern) != -1) 
        {
            var dZone = tableCellElements[i];
            tableCellElements[i].className = 'DefaultDropZoneColor'; 
        }
     }           
}

function IsInDropZone(evtTarget) 
{     
    if(evtTarget == null || evtTarget.parentNode.id == null)
    {
        return false;
    }
    
    var result = false;  
//    var mainDiv = document.getElementById("main");
//    var tableCellElements = mainDiv.getElementsByTagName("td");    
//    // iterate through the array and find it the id exists 
//    for(i = 0; i < tableCellElements.length; i++) 
//    {
//        if((evtTarget.id.indexOf(mealTypePattern) != -1 &&
//           evtTarget.id == tableCellElements[i].id) || 
//           (evtTarget.parentNode.id.indexOf(mealTypePattern) != -1 &&
//           evtTarget.parentNode.id == tableCellElements[i].id)) 
//        {
//            result = true; 
//            break; 
//        }
//    }
    var mealTypePatternCell = $(evtTarget).parents('td[id$=mealTypeCell]');
    
    result = (mealTypePatternCell != null && mealTypePatternCell[0] != null);

    if (!result && evtTarget.id.indexOf(mealTypePattern) > 0) {
        var mealTypePatternCell = evtTarget;
        result = true;
    }

    if (result) {
        var i;
        i++;
    }
    return result;
}

function MakeElementDraggable(obj) 
{    
    var startX = 0; 
    var startY = 0; 
        
function InitiateDrag(e) 
{    
    mouseState = 'down';
    
    var evt = e || window.event;
    
    startX = parseInt(evt.clientX); 
    startY = parseInt(evt.clientY);    
    
    clone = obj.cloneNode(true);
           
    clone.style.position = 'absolute';   
    clone.style.top = parseInt(startY) + 'px';
    clone.style.left = parseInt(startX) + 'px'; 
    clone.className = 'dragElement';
    
    document.body.appendChild(clone);     
    
    document.onmousemove = Drag;
    document.onmouseup = Drop; 
    
    return false;             
}

function Drop(e) 
{
       
       var evt = e || window.event;
       var evtTarget = evt.target || evt.srcElement;
       
       if(IsInDropZone(evtTarget)) {
           
           if(evtTarget.id.indexOf(mealTypePattern) != -1)
           {
               target = evtTarget; 
           }
           else
           {
               target = $(evtTarget).parents('td[id$=mealTypeCell]')[0];
           }

           var recipeId = clone.getAttribute('recipeId');
           
           var courseTypeId = target.getAttribute('courseTypeId');
           if(courseTypeId != null)
           {
                PageMethods.AddMeal_Recipe(courseTypeId, recipeId, OnAddRecipeSucceeded, OnActionFailed); 
           } 
           else
           {
                var dayIndex = target.getAttribute('dayIndex');
                var mealTypeId = target.getAttribute('mealTypeId');
                PageMethods.AddMealRecipe(dayIndex, mealTypeId, recipeId, OnAddRecipeSucceeded, OnActionFailed);
            }
            
           ResetColor(e);     
       }
                                           
        document.onmouseup = null; 
        document.onmousemove = null;               
       
        document.body.removeChild(clone); 
        mouseState = 'up';
               
}

function OnAddRecipeSucceeded(results)
{
    if(results != null)
    {
        var mealId = results[0];
        var servings = results[1];
        var recipeServings = results[2];
        AddRecipe(mealId, servings, recipeServings); 
    }
}

function AddRecipe(mealId, servings, recipeServings) 
{   

    var recipeId = clone.getAttribute('recipeId');
    var dayIndex = target.getAttribute('dayIndex');
       
    var dZone = document.getElementById(target.id); 
    
    var divElement = document.createElement('div');
    divElement.id = 'itemDiv' + uniqueNumber; 
//    divElement.style.height='25px';
    dZone.appendChild(divElement);

    var tableElement = document.createElement('table');
    //tableElement.style.height = '100%';
    divElement.appendChild(tableElement);

    var rowElement = tableElement.insertRow(-1);

    var cellElement1 = document.createElement('td');
    rowElement.appendChild(cellElement1);

    var cellElement2 = document.createElement('td');
    rowElement.appendChild(cellElement2);

    var cellElement3 = document.createElement('td');
    rowElement.appendChild(cellElement3);
       
     // create the textServings
    var txtServings = document.createElement('input');
    txtServings.type='text';
    txtServings.style.width='25px';
    txtServings.onchange =  Servings_Change;
    txtServings.setAttribute('mealId', mealId);
    txtServings.setAttribute('recipeId', recipeId);
    //divElement.appendChild(txtServings);
    cellElement1.appendChild(txtServings);
    
    //divElement.innerHTML += '&nbsp;' + GetRecipeName() + '&nbsp;';
    cellElement2.innerHTML = GetRecipeName();
    
    // create the delete button   
    var deleteButton = document.createElement('img');
    deleteButton.src = "Images/x_normal.gif";
    deleteButton.align = 'middle';
    deleteButton.style.borderWidth ='0px';
    deleteButton.style.cursor ='pointer';
    deleteButton.onclick = DeleteItem;
    deleteButton.setAttribute('mealId', mealId);
    deleteButton.setAttribute('recipeId', recipeId);
    //divElement.appendChild(deleteButton);
    cellElement3.appendChild(deleteButton);
     
    var txtMealDiners =  null;
    if(IsFireFox()) 
    {
    
        txtMealDiners = dZone.parentNode.childNodes[1].getElementsByTagName("input")[0];
    }
    else
    {
        txtMealDiners = dZone.parentNode.childNodes[0].getElementsByTagName("input")[0];
    }
    
    if(txtMealDiners != null)
    {
        txtMealDiners.value = servings;
    }
    
    divElement.getElementsByTagName("input")[0].value = recipeServings;
    
    // increment
    uniqueNumber++; 
    
}

function GetRecipeName() 
{          
    var title = '';      
    if(IsFireFox()) 
    {
        title = clone.childNodes[1].innerHTML;    
    }    
    else 
    { 
        title = clone.childNodes[0].innerHTML;  
    }    
    return title; 
}

function Drag(e) 
{ 
    //only drag when the mouse is down  
    var evt = e || window.event;
    if( IsFireFox() )
    {
        e.preventDefault();
    } 
    else 
    {
        window.event.returnValue = false;
    }
    
    if(mouseState == 'down')
    {
           
        var evtTarget = evt.target || evt.srcElement;    
    
        clone.style.top = evt.clientY + 'px'; 
        clone.style.left = evt.clientX + 'px';
            
        ResetColor(e); 
    
        // Check if we are in the drop Zone 
        if(IsInDropZone(evtTarget)) 
        {          
//            if(evtTarget.id.indexOf(mealTypePattern) != -1)
//            {
//                evtTarget.className = 'highlightDropZone';   
//            }
//            else
//            {
//                evtTarget.parentNode.className = 'highlightDropZone';   
//            }

            if (evtTarget.id.indexOf(mealTypePattern) != -1) {
                target = evtTarget;
            }
            else {
                target = $(evtTarget).parents('td[id$=mealTypeCell]')[0]
            }
            
            target.className = 'highlightDropZone';   

        } 
        
        clone.style.cursor = 'move';  
    }     
}

obj.onmousedown = InitiateDrag;


}

function IsMatch(id, pattern) 
{
    var regularExpresssion = new RegExp(pattern);
    if(id.match(regularExpresssion)) return true;
    else return false;
}