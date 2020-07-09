// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".button1").click(function () {
        if ($(this).hasClass('.disabled')) {

        }
        else {
            $(".firma").hide("fast");
            $(this).addClass('disabled');
            $(".button2").removeClass('disabled');
            $(".button1").toggleClass("current-menu-item");
            $(".button2").toggleClass("current-menu-item");
            $("#NazwaFirmy").val("");
        }
    });
    $(".button2").click(function () {
        if ($(this).hasClass('disabled')) {

        }
        else {
            $(".firma").show("fast");
            $(this).addClass('disabled');
            $(".button1").removeClass('disabled');
            $(".button1").toggleClass("current-menu-item");
            $(".button2").toggleClass("current-menu-item");
        }
    });
});

$(document).ready(function () {
    var $thisnav = $('.current-menu-item').offset().left;
    $('.menu-item').click(function () {
        var $left = $(this).offset().left - $thisnav;
        var $width = $(this).outerWidth();
        $('.wee').css({ 'left': $left, 'width': $width });
    });
});