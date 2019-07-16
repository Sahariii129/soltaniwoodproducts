


//Get the Modal
var modal = document.getElementById("myModal")
// show login form
$('.headerband').on('click', '#LogIn', function () {

    $.post('/Account/LogIn', {}, function (e) {
        $('.Mymodal-content').html(e)

        modal.style.display = "block";
    })


})

// Get the <span> element that closes the modal
$('.modal').on('click', '.close', function () {
    modal.style.display = "none";
})

// show Register Form
$('.headerband').on('click', '#Register', function () {

    $.post('/Account/Register', {}, function (e) {
        $('.Mymodal-content').html(e)

        modal.style.display = "block";
    })


})

// exit user
$('.headerband').on('click', '#LogOut', function (e) {
    e.preventDefault();

    $.post('/Account/LogOut', {}, function (data) {
        if (data == true) {
            var html = ''
            html += '<div class="ContactBTNholder">'
            html += '<p>'
            html += '<span>'
            html += '<a href="#" id="LogIn">ورود</a></span>|'
            html += '<span>'
            html += '<a href="#" id="Register">ثبت نام</a></span>'
            html += '</p>'
            html += '</div >'
            $('.TopBand1').html(html)
            setTimeout(location.reload(), 2000)


        }



    })
})
// login to site
$('#myModal').on('click', '#btnLogInNewUser', function (e) {

    e.preventDefault();
    $('.alertmessage').css('display', 'none')
    $("form").validate()
    if ($("form").valid()) {
        var username = $('#username').val();
        var password = $('#password').val();
        var rememberMe = document.querySelector('input[type="checkbox"]').checked;
        var data = { username: username, password: password, RememberMe: rememberMe };
        var datainjson = JSON.stringify(data)
        $.ajax({
            url: '/Account/LogInAction',
            type: 'Post',
            data: datainjson,
            dataType: 'json',
            contentType: 'application/json',
            success: function (data, textStatus, jqXHR) {
                if (data.status == 'success') {
                    var html = ''
                    html += '<div class="UserMenudiv">'
                    html += '<p>'
                    html += '<span>'
                    html += '<i class="fas fa-bars UserMenu"></i>'
                    html += '</span>'
                    html += '<span>'
                    html += data.username + ', خوش آمدید...'
                    html += '</span>|'
                    html += '<span>'
                    html += '<a href="#" id="LogOut">خروج</a>'
                    html += '</span>'
                    html += '</p>'
                    html += '</div>'
                    $('.TopBand1').html(html)
                    modal.style.display = "none";
                    setTimeout(location.reload(),2000);

                } else if (data.status == 'NoUser') {
                    $('.alertmessage').css('display', 'block')
                    $('.alertmessage').html(data.message)
                } else if (data.status == 'notactive') {
                    $('.alertmessage').css('display', 'block')
                    $('.alertmessage').html(data.message)
                } else if (data.status == 'validationerror') {
                    var html = '<ul>'
                    $.each(data.message, function (idx, p) {
                        html += '<li>'
                        html += p
                        html += 'li'


                    })
                    html += '</ul>'
                    $('.alertmessage').css('display', 'block')
                    $('.alertmessage').html(data.message)
                } else {
                    $('.alertmessage').css('display', 'block')
                    $('.alertmessage').html('خطایی غیر قابل پیش بینی رخ داده است');
                }

            }
        })
    } else {
        return;
    }


})




// Register in site
$('#myModal').on('click', '#Btn_Register', function (e) {
    e.preventDefault();
    $("form").validate()
    if ($("form").valid()) {
        var mydata = new FormData()
        mydata.append('username', $('.RegisterForm #username').val())
        mydata.append('password', $('.RegisterForm #password').val())
        mydata.append('confirmpass', $('.RegisterForm #confirmpass').val())
        mydata.append('email', $('.RegisterForm #email').val())
        mydata.append('cellphone', $('.RegisterForm #cellphone').val())
        mydata.append('image', $('.RegisterForm #image_file')[0].files[0])
        
        $.ajax({
            type: 'POST',
            url: '/Account/registerAction',
            data: mydata,
            datatype: 'json',
            processData: false,
            contentType: false,
        }).done(function (result) {
            //values = JSON.parse(msg)
            ////alert('hi')
            //alert(result.jsonstatus)
            if (result.jsonstatus == 'SuccessRegisterEmail') {
                $('.alertmessage').removeClass('alert-danger')
                $('.alertmessage').addClass('alert-success')
                $('.alertmessage').html(result.jsonmessage)
                $('.alertmessage').css('display', 'block')
            } else {

                $('.alertmessage').html(result.jsonmessage)
            $('.alertmessage').css('display', 'block')
            }
        })




    } else {
        return;
    }


})




// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}



var screenwidth = screen.width
// to open modal for usermenu
$('.headerband').on('click', '.UserMenu', function () {

    $.post('/admin/UserPanel', {}, function (e) {
      

        $('.Mymodal-content').html(e)
        //$('.modal-content').css('backgrouncolor':'')

        modal.style.display = "block";
        //$(myModal).slideToggle(200);

    })



})




$('#openmenu').click(function (e) {

    e.preventDefault();
    var x = $(myTopnav)
    if (x.hasClass("responsive")) {
        x.removeClass("responsive")
        //$('.subnavbtn2').css('float', '')

    } else {
        x.addClass("responsive")
        //$('.subnavbtn2').css('float','right')
        //var d = document.getElementsByClassName('subnavbtn2')

    }

})


$('.subnavbtn').click(function () {

    if ($(this).hasClass('active')) {
        $('.subnavbtn').removeClass('active')
        $('.subnav-content').hide()
        return
    }
    $('.subnavbtn').removeClass('active')
    $('.subnav-content').hide()
    $(this).addClass('active')
    $(this).parent().find('.subnav-content').toggle(100)


})

$(document).click((e) => {

    var container = $('.topnav');
    var subnav = $('.subnav-content')
    var subnav2 = $('.subnav2-content')

    if (!container.is(e.target) && container.has(e.target).length === 0) {
        subnav2.hide();
        subnav.hide();
        $('.subnavbtn').removeClass('active')
        $('.subnav2').find('.fa-caret-up').removeClass('fa-caret-up').addClass('fa-caret-down')
        //subnav2.fadeOut('fast')
        //subnav.fadeOut('slow')
    }

})

$('.subnavbtn2').click(function () {
    

    if ($(this).hasClass('activebtn')) {
        $('.subnavbtn2').removeClass('activebtn')
        $('.subnav2-content').hide()
        $(this).find('.fa-caret-up').removeClass('fa-caret-up').addClass('fa-caret-down')
        return
    }
    if ($(this).parent().parent().parent().find('.fa-caret-up')) {
        //alert('passed')
        $(this).parent().parent().parent().find('.fa-caret-up').removeClass('fa-caret-up').addClass('fa-caret-down')
    }



    $('.subnavbtn2').removeClass('activebtn')
    $('.subnav2-content').hide()
    $(this).addClass('activebtn')
    var secid = $(this).attr('sectionid')

    var thiscontent = $(this).parent().parent()
    
    $(this).find('.fa-caret-down').removeClass('fa-caret-down').addClass('fa-caret-up')
    thiscontent.find('.subnav2-content[sectionid="' + secid + '"]').toggle(300)

})


// slide show script
var mySlides = $('.mySlides')
var myDots = $('.dot')
var slideIndex = 1;
myshowSlides(slideIndex);

function plusSlides(n) {

    slideIndex = slideIndex + n
    myshowSlides(slideIndex);
}

function currentSlide(n) {
    slideIndex = n
    myshowSlides(n);

}

function myshowSlides(n) {
    mySlides.hide()
    myDots.removeClass('active')
    if (n > mySlides.length) {
        slideIndex = 1
    }
    if (n < 1) {
        slideIndex = mySlides.length
    }

    mySlides.eq(slideIndex - 1).css('display', 'block')
    //alert(mySlides.eq(slideIndex - 1).html())
    mySlides.eq(slideIndex - 1).show()
    myDots.eq(slideIndex - 1).addClass('active')
}


var slideInterval = setInterval(function () {
    slideIndex += 1
    myshowSlides(slideIndex)
}, 5000)

function ResumeTimer() {
    slideInterval = setInterval(function () {
        slideIndex += 1
        myshowSlides(slideIndex)
    }, 5000)
}


$('.dotcontainer .dot').mouseenter(function () {
    clearInterval(slideInterval)
})
$('.dotcontainer .dot').mouseleave(function () {
    ResumeTimer()
})
