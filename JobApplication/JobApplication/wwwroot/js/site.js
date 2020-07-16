// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var pageURL = $(location).attr("href");
    var edited = pageURL.substr(pageURL.indexOf("?") + 1);
    $(".button1").click(function () {
        $("#Role").val("Candidate");
        if ($(this).hasClass('disabled')) {
        }
        else {
            HideCompany();
            ToggleCurrMenuItem(this, ".button2");
        }
    });
    $(".button2").click(function () {
        $("#Role").val("Employer");
        if ($(this).hasClass('disabled')) {
            
        }
        else {
            ShowCompany();
            ToggleCurrMenuItem(this, ".button1");
        }
    });
    $('.menu-item').click(function () {
        var $thisnav = $('.current-menu-item').offset().left;
        var $left = $(this).offset().left - $thisnav + 2;
        var $width = $(this).outerWidth() - 4;
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

//$(window).on('load resize', function () {
//    var $thisnav = $('.current-menu-item').offset().left;
//    var $left = $(this).offset().left - $thisnav + 2;
//    var $width = $(this).outerWidth() - 4;
//    $('.wee').css({ 'left': $left, 'width': $width });
//});

$("#NazwaFirmy").change(function () {
    if ($('#NazwaFirmy').val() == '')
    {
        document.getElementById("companyNameRequired").innerHTML = "Nazwa firmy jest wymagana";
    } else
    {
        document.getElementById("companyNameRequired").innerHTML = "";  
    }

   
});

$(window).on("load", function () {
    $(".lds-ellipsis-wrapper").fadeOut("slow");
});

//$(document).ready(function () {


//    var pageURL = $(location).attr("href");
//    var edited = pageURL.substr(pageURL.indexOf("?") + 1);
//    alert(edited);



//});
function ShowPreview(input) {
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#impPrev').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}