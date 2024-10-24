function JAlert(title, message, cssclass, AutoHide) { cssclass = cssclass == null || cssclass == 'error' ? cssclass = 'danger' : cssclass; var _msg = "<div id='JAlert' style='width: auto!important;margin-left: 27%;position: fixed;top: 2.5%;min-width: 50%;z-index: 1100;' class='alert alert-" + cssclass + " alert-dismissable'><i class='fa fa-ban'></i><button type='button' onclick='$(\"#JAlert\").remove();' class='close' data-dismiss='alert' aria-hidden='true'>×</button><b>" + title + "</b> <br>" + message + "</div>"; $('#JAlert').remove(); $("body").append(_msg); setTimeout(function () { $('#JAlert').remove(); }, 5000); }
function ShowValidationMessage(ControlName, ErrorMessage, CssClassName, isShow) { if (isShow) { if ($('#' + ControlName).parent().find('label').find('span').length == 0) { $('#' + ControlName).parent().find('label').append('<span>&nbsp;&nbsp;<i class="fa fa-' + CssClassName + '"></i>&nbsp;' + ErrorMessage + '</span>'); $('#' + ControlName).parent().addClass('has-' + CssClassName); } $('#' + ControlName).focus(); } else { $('#' + ControlName).parent().find('label').children('span').remove(); $('#' + ControlName).parent().removeClass('has-' + CssClassName); } }
function ValidateControl(ControlName, Type, ErrorMessage, CssClassName) {
    var status = false;
    switch (Type) {
        case 'Textbox': if ($('#' + ControlName).val() == '') { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, true); status = false; } else { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, false); status = true; } break;
        case 'File': if ($('#' + ControlName).val() == '') { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, true); status = false; } else { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, false); status = true; } break;
        case 'Dropdownlist': if ($('#' + ControlName).val() == '0') { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, true); status = false; } else { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, false); status = true; } break;
        case 'Password': if ($('#' + ControlName).val().trim() == '') { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, true); status = false; } else { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, false); status = true; } break;
        case 'Email':
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (!expr.test($('#' + ControlName).val())) { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, true); JAlert('Alert', ErrorMessage); status = false; }
            else { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, false); status = true; } break;
        case 'Numbers':
            var expr = /^[0-9-+]+$/;
            if (!expr.test($('#' + ControlName).val())) { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, true); JAlert('Alert', ErrorMessage); status = false; }
            else { ShowValidationMessage(ControlName, ErrorMessage, CssClassName, false); status = true; } break;

            break;
    }
    return status;
}
function HideModelPopUp() { if (window.parent != null && window.parent.$('#btnPopupCloseButton') != null) window.parent.$('#btnPopupCloseButton').click(); else $('#btnPopupCloseButton').click(); }
// Call Method 
// showPopUp(' fa-cloud-download', 'Hello', 'http://125.18.77.8:8080/hrportal/', 600, 500,'Scrolling');
function showPopUp(icon, title, url, width, height, scrolling) {
    var _scrolling = scrolling == null ? "yes" : "no";
    if ($(window).width() < width)
        width = $(window).width() - 10;
    $('#compose-modal').remove();
    var _html = " <div class='modal fade' id='compose-modal' tabindex='-1' role='dialog' aria-hidden='true'>" +
                    " <div class='modal-dialog' style='width: " + width + "px; height:" + height + "px'>" +
                    " <div class='modal-content'>" +
                    " <div class='modal-header'>" +
                    " <button type='button' id='btnPopupCloseButton' title='Close' class='close' data-dismiss='modal' onclick='setTimeout(function () { $('#compose-modal').remove(); $('.modal-backdrop').remove(); }, 1000);'" +
                    "    aria-hidden='true'>" +
                    " &times;</button>" +
                    " <h4 class='modal-title' style='color:black!important'>" +
                    " <i class='fa fa-" + icon + "'></i>&nbsp;" + title + "</h4>" +
                    " </div>" +
                    " <div class='loading' style='margin-left: 46%'>" +
                    " </div>" +
                    " <iframe id='frmPopupWindow' src='#' width='" + (parseInt(width) - 2) + "px' height='" + height + "px' style='border: 0px;background-color:white!important' scrolling='" + _scrolling + "' onload='$(\".loading\").remove();'></iframe>" +
                    " </div>" +
                    " </div>" +
                    " <a style='display: none' id='btnPopModel' class='btn btn-block btn-primary' data-toggle='modal'" +
                    " data-target='#compose-modal'><i class='fa fa-pencil'></i>Compose Message</a>" +
                    " </div>" +
                     "<script type='text/javascript'>" +
                     "$('#btnPopupCloseButton').click(function() {" +
                     " setTimeout(function () { $('#compose-modal').remove(); $('.modal-backdrop').remove(); }, 500);" +
                     "});   " +
                    "</script>";
    $("body").prepend(_html);
    setTimeout(function () { $('#btnPopModel').click(); $("#frmPopupWindow").attr("src", url); }, 100);
    return false;
}
$('form').attr("autocomplete", "off");


// custom funcitons
jQuery.fn.OnlyNumeric = function () { return this.each(function () { $(this).keydown(function (e) { var key = e.charCode || e.keyCode || 0; return (key == 8 || key == 9 || key == 13 || key == 109 || key == 46 || key == 110 || key == 190 || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105)); }); }); };
jQuery.fn.OnlyVarchar = function () { return this.each(function () { $(this).keydown(function (e) { var key = e.charCode || e.keyCode || 0; return (key == 8 || key == 9 || key == 13 || key == 32 || key == 110 || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 48 && key <= 57)); }); }); };
jQuery.fn.OnlyDate = function () { return this.each(function () { $(this).keydown(function (e) { var key = e.charCode || e.keyCode || 0; return (key == 8 || key == 9 || key == 13 || key == 46 || key == 110 || key == 190 || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key = 92) || (key >= 96 && key <= 105)); }); }); };
function GetFormattedDate(todayTime) { return todayTime.getDate() + "/" + (todayTime.getMonth() + 1) + "/" + todayTime.getFullYear(); }
function ShowLoading(ID) {
    if ($('body').find('#' + ID) != null)
        $("#" + ID).prepend('<div class="overlay" style="background:transparent!important"></div><div class="loading-img loading" style="top:45%!important;left:45%!important"></div>');
    else
        $("." + ID).prepend('<div class="overlay" style="background:transparent!important"></div><div class="loading-img loading" style="top:45%!important;left:45%!important"></div>');
}
function RemoveLoadingDiv(ParentID) { $('#' + ParentID).find('.overlay').remove(); $('#' + ParentID).find('.loading-img').remove(); }
// Global Variables
var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
var _LoadingDiv = '<div class="overlay"></div><div class="loading-img"></div>';
var _LoadingDivWithoutOverLay = '<div class="overlay" style="background:transparent!important"></div><div class="loading-img"></div>';
var colours = ["#54C571", "#CD7F32", "#00FFFF", "#7FFFD4", "#F0FFFF", "#F5F5DC", "#FFE4C4", "#000000", "#FFEBCD", "#0000FF", "#8A2BE2", "#A52A2A", "#DEB887", "#5F9EA0", "#7FFF00", "#D2691E", "#FF7F50", "#6495ED", "#FFF8DC", "#DC143C", "#00FFFF", "#00008B", "#008B8B", "#B8860B", "#A9A9A9", "#006400", "#BDB76B", "#8B008B", "#556B2F", "#FF8C00", "#9932CC", "#8B0000", "#E9967A", "#8FBC8F", "#483D8B", "#2F4F4F", "#00CED1", "#9400D3", "#FF1493", "#00BFFF", "#696969", "#1E90FF", "#B22222", "#FFFAF0", "#228B22", "#FF00FF", "#DCDCDC"];
// Start For Printing Table or Div TO excel
var ExportToExcel = (function () {
 
    var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        downloadWithName(uri + base64(format(template, ctx)), name + '.xls');
    }
})()

function downloadWithName(uri, name) {
    function eventFire(el, etype) {
        if (el.fireEvent) {
            (el.fireEvent('on' + etype));
        } else {
            var evObj = document.createEvent('Events');
            evObj.initEvent(etype, true, false);
            el.dispatchEvent(evObj);
        }
    }
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
    eventFire(link, "click");
}
// this can be use to get query stirng paramter in view page.
function getQueryStringValue(name) { var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search); return match && decodeURIComponent(match[1].replace(/\+/g, ' ')); }
function goBack() { window.history.back(); }
function showwidgetFilter(basepath) { showPopUp(' fa-fw fa-filter', 'Widget Filter', basepath + "/widget/WidgetFilter", 400, 330, 'no'); }
function ReplaceForwardslashbyBackwordSlash(str) { return str.replace(new RegExp(/\\/g), '/'); }
function UpdateEmployeePhotoOnPage(photourl) { $('.img-circle').attr('src', photourl); }
function validateMultipleEmailsCommaSeparated(_this) {
    var value = $(_this).val();
    if (value == null || value == '') return true;
    var result = value.split(",");
    for (var i = 0; i < result.length; i++)
        if (!validateEmail(result[i])) {
            if (result[i] == '')
                JAlert('Alert', 'Please remove extra commas', 'error', true)
            else
                JAlert('Alert', 'Invalid Email Address [' + result[i] + ']', 'error', true)

            $(_this).focus();
            return false;
        }
    return true;
}
function validateEmail(field) {
    var regex = /\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b/i;
    return (regex.test(field)) ? true : false;
}
function getDateFromJSONDate(_date) {
    var parsedDate = new Date(parseInt(_date.substr(6)));
    var jsDate = new Date(parsedDate); //Date object
    return jsDate.getDate() + "/" + (jsDate.getMonth() + 1) + "/" + jsDate.getFullYear();
}
$.fn.ScrollingContent = function () {
    var parentControl = this;
    setInterval(function () {
        var child = $(parentControl).find('li:first');
        $(parentControl).find('li:first').clone(true).appendTo($(parentControl));
        $(parentControl).find('li:first').hide('slow');
        $(parentControl).find('li:first').remove();
    }, 5000);
};

function formatAMPM(mydate) {
    var date = mydate != null && mydate != '' ? mydate : new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}
/*------------- Custom Confirmbox ------------*/

function CustomConfirm(message, YesHandler) {
    var confirm = '<div id="confirmMessagebox" class="col-md-4" style="position: absolute;top: 15%;left: 33%;width: 33%;"> <div class="overlay" style="position: fixed;top: 0px;left: 0px;width: 100%;height: 100%;background-color: black;opacity: 0.5;z-index: 9999;"></div>'
    confirm += '<div class="box box-solid" style="border: 3px solid gray;z-index:9999">'
    confirm += '<div class="box-body"> <table  style="width:100%"><tr><td style="font-size: 16px;">' + message + '</td></tr><tr><td>&nbsp;</td></tr><tr><td> <div class="box-tools pull-right"><input type="button" id="confirmYes" class="btn btn-success" value="Yes" onclick=" $(\'#confirmMessagebox\').remove(); ' + YesHandler + '" /> &nbsp;&nbsp;<input type="button" id="confirmNo" class="btn btn-default" value="No" onclick="$(\'#confirmMessagebox\').remove()" /></div></td></tr></table>' 
    confirm += '</div></div>'
    confirm += '</div>'
    $('body').append(confirm);
    return false;
}

/*------------- Custom ConfirmYesNo ------------*/

function CustomConfirmYesNo(message, YesHandler, NoHandler) {
    var confirm = '<div id="confirmMessagebox" class="col-md-4" style="position: absolute;top: 15%;left: 33%;width: 33%;"> <div class="overlay" style="position: fixed;top: 0px;left: 0px;width: 100%;height: 100%;background-color: black;opacity: 0.5;z-index: 9999;"></div>'
    confirm += '<div class="box box-solid" style="border: 3px solid gray;z-index:9999">'
    confirm += '<div class="box-body"> <table  style="width:100%"><tr><td style="font-size: 16px;">' + message + '</td></tr><tr><td>&nbsp;</td></tr><tr><td> <div class="box-tools pull-right"><input type="button" id="confirmYes" class="btn btn-success" value="Yes" onclick=" $(\'#confirmMessagebox\').remove(); ' + YesHandler + '" /> &nbsp;&nbsp;<input type="button" id="confirmNo" class="btn btn-default" value="No" onclick="$(\'#confirmMessagebox\').remove(); ' + NoHandler + '" /></div></td></tr></table>'
    confirm += '</div></div>'
    confirm += '</div>'
    $('body').append(confirm);
    return false;
}

/*------------------------------------------------*/
/*------------- Custom Confirmbox ------------*/
function CusstomPrint(id) {
    var textToPrint = $("#"+id).html();
    print(textToPrint);
}
/*------------------------------------------------*/

/*----------Disable right click on froms ---------------*/
$(document).bind("contextmenu", function (e) {
    e.preventDefault();
    JAlert('Alert', 'Right Click is Disabled for this Web Application ! ','info');
});