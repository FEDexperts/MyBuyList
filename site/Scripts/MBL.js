$(document).ready(function ()
{
    
})

var isIE = document.all ? true : false;
var isDirty = false;
function setDirty() {
    isDirty = true;
}

function clearDirty() {
    isDirty = false;
}

function allowLeave(msg) {
    var leave = true;
    if (isDirty) {
        if (!msg)
            msg = "<%= Resources.MyGlobalResources.NotSaved %>";

        msg = msg.replace("<br/>", " ");

        if (confirm(msg)) {
            clearDirty();
        } else {
            leave = false;
        }
    }

    return leave;
}

var suspendLeaveConfirm = false;
function SuspendLeaveConfirmation() {
    suspendLeaveConfirm = true;
}

function allowUnload() {
    if (isDirty) {
        if (isIE && suspendLeaveConfirm) {
            suspendLeaveConfirm = false;
            return;
        }

        return "<%= Resources.MyGlobalResources.NotSaved %>";
    }
}

window.onbeforeunload = allowUnload;
/*
if (Sys.WebForms != null) {
    function initializeRequest(sender, args) {
        // checks the PageRequestManager if there is already a postback being processed
        // http://muath-ismail.blogspot.com/2008/07/sysinvalidoperationexception.html
        if (prm.get_isInAsyncPostBack()) {
            args.set_cancel(true);
        }
    }

    // http://encosia.com/2007/07/18/how-to-improve-aspnet-ajax-error-handling/
    function endRequest(sender, args) {
        // Check to see if there's an error on this request.
        var error = args.get_error();
        if (error) {
            var msg = String.format("<p class='ajaxError'><B>Message:</B> {0}<BR/><BR/><B>Stack trace:</B> {1}</p>",
                    error.message,
                    error.stack
        );

            //alert(msg);
        }
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(initializeRequest);
    prm.add_endRequest(endRequest);
}
*/

//startDnD();

//function startDnD() {

//    var dragElementPattern = ".+_pnlItem$";
//    var divElements = document.getElementsByTagName("div");
//    var currentPage = "<%= this.Page.AppRelativeVirtualPath %>";

//    if (currentPage == '~/MenuMeals.aspx') {
//        for (i = 0; i < divElements.length; i++) {

//            if (IsMatch(divElements[i].id, dragElementPattern)) {
//                MakeElementDraggable(divElements[i]);
//            }
//        }
//    }

//    if (currentPage == '~/MenuEdit.aspx') {
//        for (i = 0; i < divElements.length; i++) {

//            if (IsMatch(divElements[i].id, dragElementPattern)) {
//                MakeElementDraggable(divElements[i]);
//            }
//        }
//    }
//}