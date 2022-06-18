if (typeof jQuery === 'undefined') { console.error('base script requires jQuery support') }

// html
$.fn.outerHtml = function () {

    // IE, Chrome & Safari will comply with the non-standard outerHTML, all others (FF) will have a fall-back for cloning
    return (!this.length) ? this : (this[0].outerHTML || (
        function (el) {
            var div = document.createElement('div');
            div.appendChild(el.cloneNode(true));
            var contents = div.innerHTML;
            div = null;
            return contents;
        })(this[0]));
}

// date
Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

// extends string
String.prototype.formatUnicorn = String.prototype.formatUnicorn ||
function () {
    "use strict";
    var str = this.toString();
    if (arguments.length) {
        var t = typeof arguments[0];
        var key;
        var args = ("string" === t || "number" === t) ?
            Array.prototype.slice.call(arguments)
            : arguments[0];

        for (key in args) {
            str = str.replace(new RegExp("\\{" + key + "\\}", "gi"), args[key]);
        }
    }

    return str;
};

$.QueryString = (function (a) {
    if (a == "") return {};
    var b = {};
    for (var i = 0; i < a.length; ++i) {
        var p = a[i].split('=');
        if (p.length != 2) continue;
        b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
    }
    return b;
})(window.location.search.substr(1).split('&'))

$(function () {
    // $("body").html($("body").html().autoLink({ target: "_blank"}));
    $('*[data-url="true"]').each(function () {
        $(this).html(Linkify($(this).html()));
    });
});


// translation
var currentLanguage = $('html').attr('lang');
if (currentLanguage === 'en') {

    var lang = {
        'datatable': {
            'sProcessing': 'Processing...',
            'sLengthMenu': 'Rows per page: _MENU_',
            'sZeroRecords': 'No matching records found',
            'sInfo': 'Showing _START_ to _END_ of _TOTAL_ entries',
            'sInfoEmpty': 'Show 0 of 0 from 0 rows',
            'sInfoFiltered': '(filtered from _MAX_ total entries)',
            'sInfoPostFix': '',
            'sSearch': 'Search:',
            'sUrl': '',
            'sEmptyTable': 'No data available in table',
            "oPaginate": {
                "sFirst": "<i class='fa fa-angle-double-left'></i>",
                "sPrevious": "<i class='fa fa-angle-left'></i>",
                "sNext": "<i class='fa fa-angle-right'></i>",
                "sLast": "<i class='fa fa-angle-double-right'></i>"
            }
        },
        'uploader': {
            'browse': 'Browse',
            'browseUrl': 'Insert Url',
            'browseRemark': 'Support only documents in {0} Filesize must not over ',
            'browseMaxSize': 'Filesize must not over ',
            'browseOld': 'Browse uploaded',
            'browseOld_Name': 'File name',
            'browseOld_UsedIn': 'Used in',
            'browseOld_Type': 'Type',
            'browseOld_Date': 'Upload date',
            'browseOld_SelectFile': 'Select',
            'msg_RequestTokenFailed': 'Request upload token failed, Please try again later',
            'msg_RequestSignedFileInfoFailed': 'Signd document failed, Please try again later',
            'msg_invalidFileType': 'Support only documents in {0}',
            'msg_uploadSuccess': 'Success'

        },
        'swal': {
            close: "Close",
            ok: "OK",
            cancel: "Cancel"
        }
    };
}
else {
    var lang = {
        'datatable': {
            'sProcessing': 'กำลังดำเนินการ...',
            'sLengthMenu': 'จำนวน _MENU_',
            'sZeroRecords': 'ไม่พบข้อมูล',
            'sInfo': '_START_ - _END_ จาก _TOTAL_',
            'sInfoEmpty': 'แสดง 0 ถึง 0 จาก 0 แถว',
            'sInfoFiltered': '(กรองข้อมูล _MAX_ ทุกแถว)',
            'sInfoPostFix': '',
            'sSearch': 'ค้นหา:',
            'sUrl': '',
            'sEmptyTable': 'ไม่พบข้อมูล',
            "oPaginate": {
                "sFirst": "<i class='fa fa-angle-double-left'></i>",
                "sPrevious": "<i class='fa fa-angle-left'></i>",
                "sNext": "<i class='fa fa-angle-right'></i>",
                "sLast": "<i class='fa fa-angle-double-right'></i>"
            }
        },
        'uploader': {
            'browse': 'เลือกเอกสาร',
            'browseUrl': 'ใส่ Url เอกสาร',
            'browseRemark': 'รองรับเอกสารชนิด {0} ที่มีขนาดไม่เกิน ',
            'browseMaxSize': 'ไม่สามารถแนบไฟล์ที่มีขนาดเกินกว่า ',
            'browseOld': 'เลือกเอกสารที่เคยอัปโหลด',
            'browseOld_Name': 'ชื่อเอกสาร',
            'browseOld_UsedIn': 'ใช้ในการยื่นขอใช้บริการ',
            'browseOld_Type': 'ประเภทเอกสาร',
            'browseOld_Date': 'วันที่อัปโหลด',
            'browseOld_SelectFile': 'เลือก',
            'msg_RequestTokenFailed': 'ไม่สามารถขออัปโหลด token ได้กรุณาลองใหม่อีกครั้ง',
            'msg_RequestSignedFileInfoFailed': 'ไม่สามารถเข้ารหัสข้อมูลเอกสารได้กรุณาลองใหม่อีกครั้ง',
            'msg_invalidFileType': 'รองรับเอกสารชนิด {0} เท่านั้น',
            'msg_uploadSuccess': 'เพิ่มเอกสารสำเร็จ'
        },
        'swal': {
            close: "ปิด",
            ok: "ตกลง",
            cancel: "ยกเลิก"
        }
    };
}

/* js cookie */
function setCookie(name, value, days) {
    var expires = "";

    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }

    document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function updateCookie(name, value) {
    setCookie(name, value, 1);
}

function delCookie(name) {
    setCookie(name, "", -1);
}

/* set sidebar active menu */
function activeMenu(id) {
    if (id === 'navAppRequest' || id === 'navAppManage') {
        if (!$("#navApps").hasClass("active")) {
            $("#navApps").addClass("active")
        }
    }

    //changelayout(id);
    if (!$("#" + id).hasClass("active")) {
        $("#" + id).addClass("active");
    }
}

/* get date now in thai language */
function thDateNow() {
    var now = new Date();
    var thmonth = new Array("มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม");
    return (now.getDate() + " " + thmonth[now.getMonth()] + " " + Number(now.getFullYear() + 543));
}

/* set delay */
var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

/* html5 single file dropzone */
var $cropperModal = $("#modal-cropper");
var $image = $cropperModal.find(".modal-cropper-img img");
var originalData = {};
var cropData = "";
var cropImg = function () {
    cropData = $image.cropper("getDataURL");
    $(dropElement).addClass('dropped');
    $(dropElement).children('img').remove();
    $cropperModal.modal('hide');

    var $img = $('<img />').attr('src', cropData).fadeIn();
    $(linkData).val(cropData);
    $(dropElement).children('div').html($img);
};

function dropzone(dropElement, linkData) {
    $(function () {
        var initImage = $(linkData).val();
        if ((initImage != "") && (initImage != null) && (initImage != undefined)) {
            $(dropElement).children('img').remove();
            $img = $('<img />').attr('src', initImage)
            $(dropElement).children('div').html($img);
            $('.dropzone-btn-edit').text('คลิก เพื่อแก้ไข');
        }
        else {
            $('.dropzone-btn-edit').text('คลิก เพื่อเพิ่มรูปภาพ');
        }
    });
    $('.dropzone-btn-edit').on('click', function () {
        $(dropElement).children('input[type=file]').click();
    });
    $(dropElement).on('dragover', function () {
        //$(this).addClass('hover');
    });
    $(dropElement).on('dragleave', function () {
        //$(this).removeClass('hover');
    });
    $(dropElement).on('change', 'input[type=file]', function (e) {
        var file = this.files[0];
        if (this.accept && $.inArray(file.type, this.accept.split(/, ?/)) == -1) {
            return alert('ขออภัยไม่สามารถใช้ไฟล์นี้เพื่อเป็นรูปโปรไฟล์ได้ค่ะ');
        }

        if ((/^image\/(gif|png|jpeg)$/i).test(file.type)) {
            var reader = new FileReader(file);
            reader.readAsDataURL(file);
            reader.onload = function (e) {
                var data = e.target.result;
                $image.attr('src', data);
            };
            reader.onloadend = function (e) {
                $cropperModal.modal('show');
            };
        } else {
            var ext = file.name.split('.').pop();
            $(dropElement).children('div').html(ext);
        }

    });
    $cropperModal.on("shown.bs.modal", function () {
        $image.cropper({
            aspectRatio: 1 / 1,
            zoomable: false,
            rotatable: false,
            data: originalData
        });
    }).on("hidden.bs.modal", function () {
        cropData = "";
        $(dropElement).children('input[type=file]').val('');
        $image.cropper("destroy");
    });
}

/* convert date to buddihist year */
function convertToBuddhistYear(date) {
    var result = date.split("/");
    var buddhistYear = parseInt(result[2]) + 543;
    return result[0] + "/" + result[1] + "/" + buddhistYear;
}

function convertToChristianYear(date) {
    var result = date.split("/");
    var buddhistYear = parseInt(result[2]) - 543;
    return result[0] + "/" + result[1] + "/" + buddhistYear;
}

/* notify msg */
//toastr.options = {
//    escapeHtml: true,
//    closeButton: false,
//    newestOnTop: false,
//    positionClass: "toast-top-full-width",
//    preventDuplicates: true,
//    onclick: null,
//    showDuration: "300",
//    hideDuration: "1000",
//    defaultTimeOut: "5000",
//    extendedTimeOut: "0",
//    showEasing: "swing",
//    hideEasing: "linear",
//    showMethod: "fadeIn",
//    hideMethod: "fadeOut"
//}

function calert(cname) {

    var cvalue = getCookie(cname);
    if (cvalue != undefined && cvalue != "") {
        delCookie(cname);
        var obj = JSON.parse(cvalue);
        notify(obj.type, obj.msg, false, obj.returnUrl);
    }
    else {
        // calert not found
    }
}

function notify(type, msg, isStatic, returnUrl) {
    if (returnUrl) {
        swal({ title: '', text: decodeURIComponent(msg), type: type, html: true, confirmButtonText: lang.swal.ok, customClass: 'swal-wide' }, function (isConfirm) {
            if (window.location.href == returnUrl) {
                location.reload();
            }
            else {
                window.location = returnUrl;
            }

        });
    }
    else {
        swal({ title: '', text: decodeURIComponent(msg), type: type, html: true, confirmButtonText: lang.swal.ok }); //customClass: 'swal-wide'
    }
}

function notify2(type, title, msg, returnUrl) {
    if (title) {
        title = decodeURIComponent(title);
    }
    if (msg) {
        msg = decodeURIComponent(msg);
    }

    if (returnUrl) {
        swal({ title: title, text: msg, type: type, showConfirmButton: false });
        if (window.location.href == returnUrl) {
            location.reload();
        }
        else {
            window.location = returnUrl;
        }
    } else {
        swal({ title: title, text: msg, type: type });
    }
}

function notifyPrint(type, msg, trackingUrl, printUrl, statusName, printName) {

    if (trackingUrl) {
        swal({
            title: '',
            text: decodeURIComponent(msg),
            type: type,
            showCancelButton: true,
            confirmButtonText: statusName,
            cancelButtonText: printName,
        }, function (isConfirm) {
            if (isConfirm)
                window.location = trackingUrl;
            else {
                window.location = trackingUrl;
                window.open(printUrl);
            }
        });
    }
    else
        swal({ title: '', text: decodeURIComponent(msg), type: type });
}

function notifyMsg(type, msg, delayTime) {

    if (delayTime != undefined) {
        toastr.options.timeOut = delayTime;

        if (delayTime == 0) {
            toastr.options.closeButton = true;
        }
        else {
            toastr.options.closeButton = false;
        }
    }
    else {
        toastr.options.timeOut = toastr.options.timeOut = toastr.options.defaultTimeOut;
        toastr.options.closeButton = false;
    }

    switch (type) {
        case 'info':
            toastr.info(msg);
            break;
        case 'success':
            toastr.success(msg);
            break;
        case 'warning':
            toastr.warning(msg);
            break;
        case 'error':
            toastr.error(msg);
            break;
        default:
            toastr.info(msg);
            break;
    }
}


/* modal msg*/
function cmodal(cname) {
    var cvalue = getCookie(cname);
    if (cvalue != undefined && cvalue != "") {
        delCookie(cname);
        var obj = JSON.parse(cvalue);
        modalMsg(obj.title, obj.htmlBody, obj.htmlFooter, obj.isStatic);
    }
    else {
        // cmodal not found
    }
}

function modalMsg(title, body, footer, isStatic) {

    $('#messageModal').on('show.bs.modal', function (e) {
        $('#messageModalTitle').html(decodeURIComponent(title));
        $('#messageModalBody').append(decodeURIComponent(body));
        $('#messageModalFooter').append(decodeURIComponent(footer));
    });

    $('#messageModal').on('hidden.bs.modal', function (e) {
        $('#messageModalTitle').empty();
        $('#messageModalBody').empty();
        $('#messageModalFooter').empty();

    });

    if (Boolean(isStatic) == true) {
        $('#messageModal').modal({ backdrop: 'static' });
    }
    else {
        $('#messageModal').modal();
    }
}

/* dismiss popover */
function enableAutoDismissPopover() {
    $('body').on('click', function (e) {
        $('[data-toggle="popover"]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });
    $('.scrollable').scroll(function (e) {
        $('[data-toggle="popover"]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });
}

function menuToggle() {
    if (getCookie("toggle") == "true") {
        setCookie("toggle", "false", 1);
        $("#pnlUser").show();
    }
    else {
        $("#pnlUser").hide();
        setCookie("toggle", "true", 1);
    }
}

function checkLength() {
    var message = Math.round(parseFloat($("#txtSMSMessage").val().length / 70));
    $("#lblLength").html($("#txtSMSMessage").val().length + ' ตัวอักษร ');
    if (message > 0)
        $("#lblLength").html($("#lblLength").html() + "(" + message + " ข้อความ)");
}

function modalMessage(subject, content) {
    $("#msgModalHeader").html(subject);
    $("#msgModalContent").html(content);
    $('#msgModal').modal('show');
}

function isGUID(objGuid) {
    var str = (objGuid);
    var reEmail = /^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/
    var bresults

    if (objGuid == "") {
        alert("please Enter Guid Value");
        bresults = false;

    }
    else {
        if (reEmail.test(str)) {
            bresults = true;
        }
        else {
            bresults = false;
        }
    }
    return bresults;
}

function bytesToSize(bytes) {
    try {
        var sizes = ['ไบต์', 'กิโลไบต์', 'เมกกะไบต์', 'กิ๊กกะไบต์', 'เทระไบต์'];
        if (!bytes) return '';

        bytes = parseFloat(bytes);
        var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
        return accounting.formatMoney((bytes / Math.pow(1024, i)).toFixed(2), '', 2) + ' ' + sizes[i];
    }
    catch (e) {
        return bytes + "ไบต์";
    }
}

function Linkify(inputText) {
    var replacedText = "";

    //URLs starting with http://, https://, or ftp://
    var replacePattern1 = /(\b(https?|ftp):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/gim;
    replacedText = inputText.replace(replacePattern1, '<a href="$1" target="_blank">$1</a>');

    //URLs starting with www. (without // before it, or it'd re-link the ones done above)
    var replacePattern2 = /(^|[^\/])(www\.[\S]+(\b|$))/gim;
    replacedText = replacedText.replace(replacePattern2, '$1<a href="http://$2" target="_blank">$2</a>');

    //Change email addresses to mailto:: links
    var replacePattern3 = /(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})/gim;
    replacedText = replacedText.replace(replacePattern3, '<a href="mailto:$1">$1</a>');

    return replacedText;
}

function PreventEnterSubmited(event) {
    if (event.keyCode === 13) {
        return false;
    }
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function isInt(n) {
    return Number(n) === n && n % 1 === 0;
}

function isFloat(n) {
    return n === Number(n) && n % 1 !== 0
}

/* loader */
var isLoading = false;
function showLoader(isShow) {
    isLoading = isShow;
    if (isShow) {
        var loader = '<div class="sk-spinner sk-spinner-double-bounce page-loader"><div class="sk-double-bounce1"></div><div class="sk-double-bounce2"></div></div>';
        $('body').append('<div class="page-loading">' + loader + '</div>');
        $('div.page-loading').css("opacity", "0.5");
        bodyScroll(false);
    }
    else {
        $('div.page-loading').remove();
        bodyScroll(true);
    }
}

function bodyScroll(isShow) {
    if (isShow == true) {
        $('html').removeClass('modal-open');
    }
    else {
        $('html').addClass('modal-open');
    }
}

function loader(state) {
    $loaderModal = $('#loaderModal');
    if (state == true) {
        $loaderModal.modal({
            backdrop: 'static',
            keyboard: false
        });
    }
    else {
        $loaderModal.modal('hide');
    }
}

function changelayout(page) {
    if (page === "navHome") {
        $("#left_panel").removeClass("col-md-1");
        $("#left_panel").addClass("col-md-2");
        $("#right_panel").removeClass("col-md-1");
        $("#right_panel").addClass("col-md-2");


        $("#left_panel").removeClass("col-sm-2");
        $("#left_panel").addClass("col-sm-1");
        $("#right_panel").removeClass("col-sm-2");
        $("#right_panel").addClass("col-sm-3");

        $("#main_panel").removeClass("col-md-10");
        $("#main_panel").addClass("col-md-8");


    }
    else if (page == "nav_About") {
        $("#right_panel").remove();
        $("#main_content").removeClass();
        $("#main_panel").removeClass();
        $("#main_panel").children('div').removeClass('container-fluid');
    }
    else if (page == "nav_Contact") {
        $("#right_panel").removeClass("col-md-1");
        $("#right_panel").addClass("col-md-3");
        $("#main_panel").removeClass("col-md-10");
        $("#main_panel").addClass("col-md-8");
        $("#main_panel").children('div').removeClass('container-fluid');
    }
}

/* scroll to top */
function scrollToTop(delayTime, callback) {
    $('body, html').animate({
        scrollTop: 0
    }, (delayTime || 500), callback);
}

/* select2 */
function select2Spinner(id, disabled) {
    $('#s2id_' + id + ' .select2-arrow b').hide();
    $('#s2id_' + id + ' .select2-arrow').append('<i class="fa fa-spin fa-spinner"></i>');

    if (disabled)
        $("#" + id).prop("disabled", disabled);
}

function select2Reset(id) {
    $('#s2id_' + id + ' .select2-arrow b').show();
    $('#s2id_' + id + ' .select2-arrow i').remove();

    $("#" + id).prop("disabled", false);
}

function buttonSpinner(selector) {
    var $btn = $(selector);
    var $i = $btn.find('i.fa');
    if ($i.length) {
        $btn.data('old-icon', $i.prop('class'));
        $i.remove();
    }
    $btn.prepend('<i class="fa fa-spin fa-spinner"></i>');
    $btn.prop('disabled', true);
}

function buttonReset(selector) {
    var $btn = $(selector);
    var $i = $btn.find('i.fa');
    if ($i.length) {
        $i.remove();
    }
    if ($btn.data('old-icon')) {
        $btn.prepend('<i class="' + $btn.data('old-icon') + '"></i>');
        $btn.removeData('old-icon');
    }
    $btn.prop('disabled', false);
}



// datatable
$.extend(true, $.fn.dataTable.defaults, {
    pagingType: 'full_numbers',
    dom: "<'row'<'col-sm-6'l>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-5'i><'col-sm-7'p>>",
    language: lang.datatable,
    initComplete: function (e) {

    },
    drawCallback: function (e) {
        if (e.oInit.indexColoumn === true) {
            /* Need to redo the counters if filtered or sorted */
            for (var i = 0, iLen = e.aiDisplay.length ; i < iLen ; i++) {
                $('td:eq(0)', e.aoData[e.aiDisplay[i]].nTr).html(e._iDisplayStart + i + 1);
            }

            $('.dataTables_length').each(function () {
                var $row = $('<div class="row"></div>')
                var $col1 = $('<div class="col-xs-2 col-sm-2 col-md-2 col-lg-2"></div>');
                var $col2 = $('<div class="col-xs-4 col-sm-3 col-md-3 col-lg-2"></div>');
                $row.append($col1);
                $row.append($col2);

                $col2.append($(this).find('select').addClass('form-control'));
                $col1.append($(this).find('label'));
                $(this).append($row);
                $(this).removeClass('dataTables_length').addClass('form-group');
            });
        }

        if (e.oInit.checkerColumn == true) {
            $('.tableCheckAll').prop('checked', false);
            $('.tableCheckAll').checkAll({ container: $('.tableCheckAll').closest('table'), showIndeterminate: false });
        }
    }
});

// file upload wrapper
(function ($) {
    var fileOldTable;

    $.fn.uploader = function (options) {

        var browsers = {};
        var url = options.url;
        var events = options.events;
        var extras = options.extras;
        var style = options.style;
        var styleClass = options.class;
        var $modal = $(browseOldModal);

        $.each(this, function (index, el) {
            var browseId = $(el).attr('id');
            var fileOwners = $(el).attr('owners');
            var fileMaxSize = $(el).attr('maxsize');
            var fileTypeFilter = $(el).attr('filter');
            var isUrlBrowse = $(el).attr('isenableurl') == 'True' || $(el).attr('isenableurl') == 'true' || $(el).attr('isenableurl') == true;
            var filePlUploadOpts = {
                fileConsumerKey: url.fileConsumerKey,
                fileServiceUrl: url.fileServiceUrl,
                fileSignedInfoUrl: url.fileSignedInfoUrl,
                fileUploadTokenUrl: url.fileUploadTokenUrl,
                fileDownloadTokenUrl: url.fileDownloadTokenUrl,
                fileMaxSize:(fileMaxSize !== undefined && fileMaxSize !== 'undefined' && fileMaxSize !== null && fileMaxSize !== '') ? fileMaxSize : '5mb',
                fileTypeFilter: (fileTypeFilter !== undefined && fileTypeFilter !== 'undefined' && fileTypeFilter !== null && fileTypeFilter !== '') ? fileTypeFilter : 'jpg,png,pdf,zip',
                fileOwner: fileOwners,
                events: events
            };

            var fileOldTableOpts = {
                fileOldUploadUrl: url.fileOldUploadUrl,
                events: events
            };

            var $browseHelper = $(el);
            var $browseRemark = $('#' + browseId + '-remark');
            var $browseNew = $('<button type="button" id="' + browseId + '_browseNew" class="btn btn-default ' + styleClass + '" style="' + style + '"><i class="fa fa-folder-open-o"></i> ' + lang.uploader.browse + '</button>');
            var $browseUrl = $('<button type="button" id="' + browseId + '_browseNew" class="btn btn-default ' + styleClass + '" style="' + style + ' ;margin-left:5px;"><i class="fa fa-link"></i> ' + lang.uploader.browseUrl + '</button>');
            var $browseOld = $('<button type="button" id="' + browseId + '_browseOld" class="btn btn-default"><i class="fa fa-folder-open-o"></i> ' + lang.uploader.browseOld + '</button>');
            var $browseContainer = $(browseTemplate);
            var isOldfileBrowse = $browseHelper.data('oldfile-browse') || options.oldfileBrowse;

            delete $browseHelper.data()['uploader'];
            delete $browseHelper.data()['oldfile-browse'];
            delete $browseHelper.data()['required'];
            delete $browseHelper.data()['msg-required'];
            var fileExtras = $.extend({}, $browseHelper.data(), extras);

            $browseContainer.attr('id', browseId + '_container');
            $browseContainer.children().children('span.input-button').append($browseNew);

            if (isUrlBrowse) {
                $browseContainer.children().children('span.input-button').append($browseUrl);
            }

            if (isOldfileBrowse) {
                $browseContainer.children().children('span.input-button').append($browseOld);
            }

            $browseHelper.addClass('invisibility');
            $browseHelper.before($browseContainer);

            if (isOldfileBrowse) {
                initBrowseOldFile($browseOld, fileOldTableOpts, fileExtras, $browseHelper, $modal);
            }

            $browseRemark.html(lang.uploader.browseRemark.replace('{0}', filePlUploadOpts.fileTypeFilter) + filePlUploadOpts.fileMaxSize.replace('mb', ' MB'));

            initBrowseFile($browseNew, filePlUploadOpts, fileExtras, $browseHelper, $browseContainer);
            initBrowseUrl($browseUrl, filePlUploadOpts, fileExtras, $browseHelper, $browseContainer);
            browsers[browseId] = $browseHelper;
        });

        $('body').append($modal);
        return browsers;
    };

    function initBrowseFile(el, options, extras, $browseHelper, $browseContainer) {

        var uploadParams = {};
        var uploader = new plupload.Uploader({
            runtimes: 'html5,flash,silverlight,html4',
            browse_button: $(el).attr('id'),
            url: options.fileServiceUrl,
            chunk_size: '1.5mb',
            headers: { 'Accept': 'application/json' },
            filters: {
                max_file_size: options.fileMaxSize,
                mime_types: [
                    { title: "Document files", extensions: options.fileTypeFilter }
                ]
            },
            multi_selection: false,
            flash_swf_url: '/plupload/js/Moxie.swf',
            silverlight_xap_url: '/plupload/js/Moxie.xap',
            init: {
                Browse: function () {
                    setTimeout(function () {
                        $browseHelper.val('');
                        $browseContainer.children('.url-container').children('.input-url').removeAttr('required');
                        $browseContainer.children('.url-container').children('.input-url').removeAttr('contain');
                        $browseContainer.children('.url-container').children('.input-url').val('');
                        $browseContainer.children('.url-container').hide();
                    }, 200);
                },
                BeforeUpload: function (up, file) {
                    up.settings.multipart_params = uploadParams;
                },
                FilesAdded: function (up, files) {
                    $browseHelper.trigger('fileadded','');
                    $(el).attr('disabled', true);
                    $(el).children('i').removeClass('fa fa-folder-open-o').addClass('fa fa-spinner fa-spin');

                    $.each(files, function (i, data) {

                        var fileMetaData = { Name: data.name, ContentType: data.type, Size: data.size, IsPublic: false };
                        var fileOwners = options.fileOwner ? options.fileOwner.split(',') : [];

                        if (fileOwners.length > 0) {
                            fileMetaData.Consumers = fileOwners;
                        }

                        var fileInfo = JSON.stringify(fileMetaData);

                        uploadParams = { FileInfo: fileInfo };
                        uploader.start();
                    });
                },
                UploadProgress: function (up, file) {
                    $('#' + $browseHelper.attr('id') + '_container .file-desc').text('กำลังดำเนินการ...' + file.percent + ' %');
                },
                FileUploaded: function (uploader, file, res) {

                    var data = JSON.parse(res.response);
                    var filename = data.Name;
                    var uploadedData = {
                        UploaderID: $browseHelper.attr('id'),
                        UploaderTypeName: extras.uploadertype,
                        FileID: data.FileId,
                        FileName: data.Name,
                        ContentType: data.ContentType,
                        FileSize: data.FileSize,
                        IsPublic: data.IsPublic,
                        FileTypeCode: extras.filetype,
                        FileTypeName: extras.filetypename,
                        Extras: extras
                    };

                    setTimeout(function () {
                        $(el).removeAttr('disabled');
                        $(el).children('i').removeClass('fa fa-spinner fa-spin').addClass('fa fa-folder-open-o');
                        $('#' + $browseHelper.attr('id') + '_container .file-desc').text('');
                    }, 500);

                    uploadParams = {};
                    $browseHelper.val($browseHelper.val() ? $browseHelper.val() + ',' + uploadedData.FileID : uploadedData.FileID);
                    $browseHelper.trigger('fileselected', [uploadedData]);
                    notifyMsg('success', lang.uploader.msg_uploadSuccess, 1000);

                    if (options.events && options.events.onFileSelected) {
                        options.events.onFileSelected(uploadedData);
                    }
                },
                Error: function (up, err) {

                    if (err.message === "ไม่สามารถแนบไฟล์ที่มีขนาดเกินกว่าที่กำหนดได้" || err.message == "File size error.")
                    {
                        err.message = lang.uploader.browseMaxSize + options.fileMaxSize.replace('mb', 'MB');
                    }

                    swal('', err.message, 'error');

                    $(el).removeAttr('disabled');
                    $(el).children('i').removeClass('fa fa-spinner fa-spin').addClass('fa fa-folder-open-o');
                }
            }
        });
        uploader.init();

        plupload.addFileFilter('mime_types', function (filterType, file, cb) {
            var filterTypes = filterType[0].extensions.split(',');
            var ispassfilter = false;

            for (var i in filterTypes)
            {
                if (filterTypes[i] == 'pdf' && file.type == 'application/pdf') {
                    ispassfilter = true;
                    break;
                }
                else if ((filterTypes[i] == 'jpeg' || filterTypes[i] == 'jpg') && file.type == 'image/jpeg') {
                    ispassfilter = true;
                    break;
                }
                else if (filterTypes[i] == 'png' && file.type == 'image/png') {
                    ispassfilter = true;
                    break;
                }
                else if (filterTypes[i] == 'zip' && (file.type == 'application/zip' || file.type == 'application/x-zip-compressed')) {
                    ispassfilter = true;
                    break;
                }
                else if (filterTypes[i] == 'rar' && (file.type == 'application/rar' || file.type == 'application/x-rar-compressed')) {
                    ispassfilter = true;
                    break;
                }
                else if (file.type == 'application/octet-stream') {
                    ispassfilter = true;
                    break;
                }
            }

            if (ispassfilter) {
                cb(true);
            }
            else {

                notify('error', lang.uploader.msg_invalidFileType.replace('{0}', filterType[0].extensions), false);
                cb(false);
            }
        });
    }

    function initBrowseUrl(el, options, extras, $browseHelper, $browseContainer) {

        var filterUrl = 'drive.google|dropbox';
        var $browseUrl = $browseContainer.children('.url-container');
        var $inputUrl = $browseUrl.children('.input-url');
        var $btnUrl = $browseUrl.children().children('.btn-url');

        $inputUrl.attr('id', $(el).attr('id') + '_url');

        $inputUrl.on('keydown', function (e) {
            if (e.keyCode === 13) {

                if ($inputUrl.valid()) {
                    var url = $inputUrl.val().indexOf('http') > -1 ? $inputUrl.val() : 'http://' + $inputUrl.val();
                    var uploadedData = {
                        UploaderID: $browseHelper.attr('id'),
                        UploaderTypeName: extras.uploadertype,
                        FileID: guid(),
                        FileName: url,
                        ContentType: 'file/url',
                        FileSize: 0,
                        IsPublic: true,
                        FileTypeCode: extras.filetype,
                        FileTypeName: extras.filetypename,
                        FileURL: url,
                        Extras: extras
                    };

                    if (options.events && options.events.onFileSelected) {
                        options.events.onFileSelected(uploadedData);
                    }

                    $inputUrl.removeAttr('required');
                    $inputUrl.removeAttr('contain');
                    $browseHelper.val($inputUrl.val());
                }

                e.preventDefault();
                return false;
            }
        });

        $btnUrl.on('click', function (e) {

            if ($inputUrl.valid()) {
                var url = $inputUrl.val().indexOf('http') > -1 ? $inputUrl.val() : 'http://' + $inputUrl.val();
                var uploadedData = {
                    UploaderID: $browseHelper.attr('id'),
                    UploaderTypeName: extras.uploadertype,
                    FileID: guid(),
                    FileName: url,
                    ContentType: 'file/url',
                    FileSize: 0,
                    IsPublic: true,
                    FileTypeCode: extras.filetype,
                    FileTypeName: extras.filetypename,
                    FileURL: url,
                    Extras: extras
                };

                if (options.events && options.events.onFileSelected) {
                    options.events.onFileSelected(uploadedData);
                }

                $inputUrl.removeAttr('required');
                $inputUrl.removeAttr('contain');
                $browseHelper.val($inputUrl.val());
            }
            
        });

        el.on('click', function (e) {
            $inputUrl.attr('required', true);
            $inputUrl.attr('contain', filterUrl);
            $browseUrl.show();
        });
    }

    function initBrowseOldFile(el, options, extras, $browseHelper, $modal) {
        $(el).click(function () {
            initTableOldFile(options, extras, $browseHelper, $modal);
            $('#' + $modal.attr('id') + ' .file-container').empty();
            $modal.modal('show');
        });
    }

    function initTableOldFile(options, extras, $browseHelper, $modal) {
        if (fileOldTable) {
            fileOldTable.api().destroy();
        }

        fileOldTable = $('#' + $modal.attr('id') + ' table').dataTable({
            processing: true,
            serverSide: true,
            destroy: true,
            ajax: {
                url: options.fileOldUploadUrl,
                type: 'POST',
                contentType: 'application/json',
                data: function (params) {
                    return JSON.stringify(params);
                }
            },
            columns: [
                 {
                     data: 'FileName',
                     render: function (data, type, row) {
                         if (data && data.length > 15) {
                             data = data.substring(15, 0) + "..." + data.substring(data.length, data.length - 4);
                         }

                         if (row.ContentType && row.ContentType.indexOf('image') > -1) {
                             return '<i class="fa fa-file-image-o"></i> ' + data;
                         }
                         else {
                             return '<i class="fa fa-file-text-o"></i> ' + data;
                         }
                     },
                     sortable: false
                 },
                 {
                     data: 'ApplicationName',
                     sortable: false
                 },
                 {
                     data: 'FileTypeName',
                     sortable: false
                 },
                 {
                     data: 'UpdatedDateTxt',
                     sortable: false
                 },
                 {
                     data: 'ApplicationID',
                     render: function (data, type, row) {
                         return '<button class="btn btn-xs btn-default select">' + lang.uploader.browseOld_SelectFile + '</button>';
                     },
                     sortable: false,
                     'class': 'text-center'
                 }
            ],
            fnRowCallback: function (el, row) {
                $(el).click(function () {
                    var uploadedData = {
                        UploaderID: $browseHelper.attr('id'),
                        UploaderTypeName: extras.uploadertype,
                        FileID: row.FileID,
                        FileName: row.FileName,
                        ContentType: row.ContentType,
                        FileSize: row.FileSize,
                        IsPublic: row.IsPublic,
                        FileTypeCode: row.FileTypeCode,
                        FileTypeName: row.FileTypeName,
                        Extras: extras
                    };

                    $browseHelper.val(uploadedData.FileID);
                    $browseHelper.trigger('fileselected', [uploadedData]);
                    notifyMsg('success', lang.uploader.msg_uploadSuccess, 1000);

                    if (options.events && options.events.onFileSelected) {
                        options.events.onFileSelected(uploadedData);
                    }
                });
            },
            fnDrawCallback: function () {
                $('#' + $modal.attr('id') + ' .table-loader').hide();
                $('#' + $modal.attr('id') + ' .table-container').removeClass('invisibility');
            }
        });
    }

    function requestAccessToken(consumerKey, serviceUploadUrl, fileInfo, signedFileInfo, callback) {
        $.ajax({
            type: "POST",
            url: serviceUploadUrl,
            dataType: 'JSON',
            data: { 'Consumer-Key': consumerKey, FileInfo: fileInfo, SignedFileInfo: signedFileInfo },
            async: false,
            success: function (data) {
                callback(data.accessToken);
            },
            error: function (data) {
                callback(null);
            }
        });
    }

    function signFileInfo(serviceUrl, fileInfo, callback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            dataType: 'JSON',
            data: { SereializeData: fileInfo },
            success: function (data) {
                callback(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                callback(null);
            }
        });
    }

    var browseTemplate = '<div class="browse-container">' +
                         '  <div class="input-group url-container" style="display:none;">' +
                         '     <input type="text" class="form-control input-url" style="margin-bottom:5px;" placeholder="ใส่ link เอกสารจาก Google Drive หรือ Dropbox"  data-msg-required="กรุณากรอก link เอกสารจาก Google Drive หรือ Dropbox" data-msg-contain="กรุณากรอก link เอกสารจาก Google Drive หรือ Dropbox"></input>' +
                         '     <span class="input-group-btn" style="vertical-align: top;">' +
                         '        <input type="button" class="btn-url btn btn-primary" style="margin-left: 5px; margin-bottom: 5px; font-size:20px;" value="ตกลง"/>' +
                         '     </span>' +
                         '  </div>' +
                         '  <div class="input-group">' +
                         '      <span class="input-group-btn input-button"></span>' +
                         '      <span class="file-desc" style="padding-left:10px; vertical-align: sub;"></span>' +
                         '  </div>' +
                         '</div>';

    var browseOldModal = '<div class="modal fade" id="modalBrowseOldFile" role="dialog" aria-labelledby="modalBrowseOldFileLabel">' +
                         '   <div class="modal-dialog modal-lg" role="document">' +
                         '       <div class="modal-content">' +
                         '           <div class="modal-header">' +
                         '               <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                         '               <h4 class="modal-title" id="modalBrowseOldFileLabel">เลือกเอกสาร</h4>' +
                         '           </div>' +
                         '           <div class="modal-body m-xs" style="min-height: 200px;max-height: 500px; overflow-x: auto;">' +
                         '               <div class="table-loader text-center"><i class="fa fa-spinner fa-spin"></i> กำลังดำเนินการ</div>' +
                         '               <div class="table-container invisibility" style="word-break: break-word;">' +
                         '                   <table id="tableOldFile" class="table table-responsive table-bordered">' +
                         '                       <thead>' +
                         '                           <tr>' +
                         '                               <th style="vertical-align: top; min-width:75px;" >' + lang.uploader.browseOld_Name + '</th>' +
                         '                               <th style="vertical-align: top; min-width:75px;" >' + lang.uploader.browseOld_UsedIn + '</th>' +
                         '                               <th style="vertical-align: top;" >' + lang.uploader.browseOld_Type + '</th>' +
                         '                               <th style="vertical-align: top; min-width:75px;" >' + lang.uploader.browseOld_Date + '</th>' +
                         '                               <th style="vertical-align: top;" ></th>' +
                         '                           </tr>' +
                         '                       </thead>' +
                         '                   </table>' +
                         '               </div>' +
                         '           </div>' +
                         '       </div>' +
                         '   </div>' +
                         '</div>';

}(jQuery));

function number_format(number, decimals, decPoint, thousandsSep) {
    decimals = decimals || 0;
    number = parseFloat(number);

    if (!decPoint || !thousandsSep) {
        decPoint = '.';
        thousandsSep = ',';
    }

    var roundedNumber = Math.round(Math.abs(number) * ('1e' + decimals)) + '';
    var numbersString = decimals ? roundedNumber.slice(0, decimals * -1) : roundedNumber;
    var decimalsString = decimals ? roundedNumber.slice(decimals * -1) : '';
    var formattedNumber = "";

    while (numbersString.length > 3) {
        formattedNumber += thousandsSep + numbersString.slice(-3)
        numbersString = numbersString.slice(0, -3);
    }

    return (number < 0 ? '-' : '') + numbersString + formattedNumber + (decimalsString ? (decPoint + decimalsString) : '');
}

function stringToDate(dateStr, era) {
    var dateParts = dateStr.split("/");
    if (era == "th") {
        return new Date(dateParts[2] - 543, dateParts[1] - 1, dateParts[0]);
    }
    return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
}

function stringToDate(dateStr, era, year) {
    var dateParts = dateStr.split("/");
    if (year == undefined) {
        year = 0;
    }
    if (era == "th") {
        return new Date((dateParts[2] - 543) + year, dateParts[1] - 1, dateParts[0]);
    }
    return new Date(dateParts[2] + year, dateParts[1] - 1, dateParts[0]);
}

function dateToString(date, format, era) {
    var month = '' + (date.getMonth() + 1),
        day = '' + date.getDate(),
        year = era == "th" ? date.getFullYear() + 543 : date.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    if (!format.trim() && format.indexOf("DD") != -1 && format.indexOf("MM") != -1 && format.indexOf("YYYY") != -1) {
        format = "YYYY-MM-DD"; // Default ISO date format
    }
    return format.replace("DD", day).replace("MM", month).replace("YYYY", year);
}

function datepickerBEtoCE(dateStr, format) {
    if (dateStr == null || dateStr == undefined) {
        return "";
    }
    var split = dateStr.split('/');
    var day = split[0];
    var month = split[1];
    var year = split[2];

    var CE = parseInt(year) - 543;

    if ((format == null || format == undefined) || !format.trim() && format.indexOf("DD") != -1 && format.indexOf("MM") != -1 && format.indexOf("YYYY") != -1) {
        format = "YYYY-MM-DD"; // Default ISO date format
    }

    return format.replace("DD", day).replace("MM", month).replace("YYYY", CE);
}

function display_ignore(elem, show) {
    if (show) {
        $(elem).each(function (i, e) {
            $(e).removeClass("ignore");
        });
    } else {
        $(elem).each(function (i, e) {
            var type = e.type;
            $(e).addClass("ignore");
            if (type == "checkbox" || type == "radio") {
                $(e).prop("checked", false);
            } else if (type == "text") {
                $(e).val("");
            }
        });
    }
}

function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}

String.prototype.includes = function (str) {
    var returnValue = false;

    if (this.indexOf(str) !== -1) {
        returnValue = true;
    }

    return returnValue;
}

function notifyConfirm(title, callback) {
    swal({
        title: title,
        type: 'info',
        showCancelButton: true,
        confirmButtonText: 'ตกลง',
        cancelButtonText: 'ยกเลิก',
    }, function (isConfirm) {
        if (isConfirm)
            callback();
    });
}