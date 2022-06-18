/* Add here all your JS customizations */
var container = document.querySelector('.container-menu');

$('.login-info .profile').click(function (e) {
    e.preventDefault();
    $(this).siblings('.dropdown-content').slideToggle();
    $(this).toggleClass('active');
});

$('section.menu-mobile').css("display", "block");
$('section.search-mobile').removeAttr("style");

$(function () {
    if (($(window).height() - 30 <= $('body').height())) {
        $('#frmSingleForm').height($('#frmSingleForm').height() + 65);
    }
});

$(document).scroll(function (e) {
    if ($(document).scrollTop() <= 0) {
        $("section.navbar .nav").removeClass('fixed');
        $("section.navbar").removeClass('fixed');
    }
    else {
        $("section.navbar .nav").addClass('fixed');
        $("section.navbar").addClass('fixed');
    }
});
