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

function OpenNavbarItem(evt, itemNumber) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(itemNumber).style.display = "block";
    evt.currentTarget.className += " active";

    if (itemNumber == "Employer1") {
        changeurl('Panel');
    }
    else if (itemNumber == "Employer2") {
        changeurl('DodajOferte');
    }
    else if (itemNumber == "Employer3") {
        changeurl('MojeOferty');
    }
    else if (itemNumber == "Employer4") {
        changeurl('OfertyAplikacyjne');
    }
    else if (itemNumber == "Employer5") {
        changeurl('PakietyOfert');
    }
    else if (itemNumber == "Employer6") {
        changeurl('PakietyKandydatow');
    }
    else if (itemNumber == "Employer7") {
        changeurl('PrzegladaneZyciorysy');
    }
    else {
        changeurl('Panel');
    }
}

//var navbar = document.getElementById("navbar-scroll");
//window.onscroll = function () { scrollFunction() };

//function scrollFunction() {
//    if (document.body.scrollTop > 100 || document.documentElement.scrollTop >100) {
//        $(".navbar").addClass("fixed-top");
//    } else {
//        $(".navbar").removeClass("fixed-top");
//    }
//}