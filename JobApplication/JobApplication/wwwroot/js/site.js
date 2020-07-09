// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".button1").click(function () {
        if ($(this).hasClass('disabled')) {

        }
        else {
            HideCompany();
            ToggleCurrMenuItem(this, ".button2");
        }
    });
    $(".button2").click(function () {
        if ($(this).hasClass('disabled')) {

        }
        else {
            ShowCompany();
            ToggleCurrMenuItem(this, ".button1");
        }
    });
    $('.menu-item').click(function () {
        var $thisnav = $('.current-menu-item').offset().left;
        var $left = $(this).offset().left - $thisnav;
        var $width = $(this).outerWidth();
        $('.wee').css({ 'left': $left, 'width': $width });
    });

});

function ShowCompany() {
    $(".firma").show("fast");
}

function HideCompany() {
    $(".firma").hide("fast");
    $("#NazwaFirmy").val("");
}

function ToggleCurrMenuItem(button1, button2) {
    $(button1).addClass('disabled');
    $(button2).removeClass('disabled');
}

$(window).on('load resize', function () {
    var $thisnav = $('.current-menu-item').offset().left;
    var $left = $(this).offset().left - $thisnav;
    var $width = $(this).outerWidth();
    $('.wee').css({ 'left': $left, 'width': $width });
});
