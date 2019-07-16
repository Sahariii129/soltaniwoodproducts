
// Register in site
$('body').on('click', '#Btn_Register', function (e) {
    e.preventDefault();
    //alert('hi')
    $("form").validate()
    if ($("form").valid()) {
        var mydata = new FormData()
        mydata.append('username', $('.RegisterForm #username').val())
        mydata.append('password', $('.RegisterForm #password').val())
        mydata.append('confirmpass', $('.RegisterForm #confirmpass').val())
        mydata.append('email', $('.RegisterForm #email').val())
        mydata.append('cellphone', $('.RegisterForm #cellphone').val())


        $.ajax({
            type: 'POST',
            url: '/Account/registerAction',
            data: mydata,
            datatype: 'json',
            processData: false,
            contentType: false,
        }).done(function (result) {
            //alert(JSON.stringify(result))
            //values = JSON.parse(result)
            ////alert('hi')
            //alert(result.jsonstatus)
            if (result.jsonstatus == 'SuccessRegisterEmail') {
                $(alertregister).removeClass('alert-danger')
                $(alertregister).addClass('alert-success')
                $(alertregister).html(result.jsonmessage)
                $(alertregister).css('display', 'block')
                $('input').val('')
            } else {

                $(alertregister).html(result.message)
                $(alertregister).css('display', 'block')
            }
        })




    } else {
        return;
    }


})




$('body').on('click', '#btnLogInNewUser', function (e) {

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
                    $('#modalLogin').modal('hide');

                    $('.modal-backdrop').remove();

                    var html = ''


                    html += '<div class="UserMenudiv mx-2">'
                    html += '<a href="" class="btn UserMenu d-none d-lg-inline" data-target="#collapseProfile" data-toggle="collapse"><i class="fas fa-bars mx-2"></i><span username="' + data.username + '">' + data.username + '</span>، خوش آمدید</a>'
                    html += '<a href="" class="d-inline mx-5 d-lg-none UserMenu " data-target="#collapseProfile" data-toggle="collapse"><i class="fas fa-user"></i></a>'
                    html += '</div>'
                    $('.toptwobuttons').html(html)






                } else if (data.status == 'NoUser') {

                    $(alertlogin).css('display', 'block')

                    $(alertlogin).html(data.message)
                } else if (data.status == 'notactive') {
                    $(alertlogin).css('display', 'block')
                    $(alertlogin).html(data.message)
                } else if (data.status == 'validationerror') {
                    var html = '<ul>'
                    $.each(data.message, function (idx, p) {
                        html += '<li>'
                        html += p
                        html += 'li'


                    })
                    html += '</ul>'
                    $('.alertmessage').css('display', 'block')
                    $(alertlogin).html(data.message)
                } else {
                    $(alertlogin).css('display', 'block')
                    $(alertlogin).html('خطایی غیر قابل پیش بینی رخ داده است');
                }

            }
        })
    } else {
        return;
    }


})



// open usermenu
$('body').on('click', '.UserMenu', function () {
    var contain = $('.usermeupan').html();

    if (contain.length == 0) {

        $.post('/admin/UserPanel', {}, function (e) {
           
            $('.usermeupan').html(e)
            return

        })
       
    }
    else {
        
       $('.usermeupan').collapse('show')

    }



})




AOS.init({
    once: true,
    duration: 1300
})

$(document).ready(function () {

    $('.dropdown').on('show.bs.dropdown', function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
    });
    $('.dropdown').on('hide.bs.dropdown', function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
    });

    var navbarTogglerIcon = $('.navbar-toggler i');
    $('#menu').on('shown.bs.collapse', function (e) {
        if (e.target == this) {
            navbarTogglerIcon.removeClass('fa-bars');
            navbarTogglerIcon.addClass('fa-times');
        }
    })
    $('#menu').on('hidden.bs.collapse', function (e) {
        if (e.target == this) {
            navbarTogglerIcon.removeClass('fa-times');
            navbarTogglerIcon.addClass('fa-bars');
        }
    });
    $(window).scroll(function () {
        $('.scrolltop').css('display', 'block');
    });
    $('.scrolltop a').on('click', function (e) {
        var target = $(this.hash);
        if (target.lenght != 0) {
            e.preventDefault();
            $('html,body').animate({
                scrollTop: target.offset().top
            }, 1000)
        }
    });

    $('.chatCollapseHeader .close').on('click', function (e) {
        e.preventDefault();
        $('#collapseLogin').collapse('hide');
    })
    $('.chatCollapseHeader .close').on('click', function (e) {
        e.preventDefault();
        $('#collapseChat').collapse('hide');
    })
    $('.btn-loginChat').on('click', function () {
        $('#collapseLogin').collapse('hide');
        $('#collapseChat').collapse('show');
    })
});

// to hide usermenu by clicking
$(document).click(function () {
    $('header #collapseProfile').collapse('hide'); 
});
$(".usermeupan").click(function (e) {
    e.stopPropagation(); 
});

//click on product in dropdown-menu 
$('header .part2 .dropdown-menu').on('click', function (e) {
    e.stopPropagation();  
    var objClicked = e.target.tagName.toLowerCase()
    if (objClicked === 'h6') {
        var thisObj = e.target.parentElement
        if (thisObj.querySelector('.sectionclass').classList.contains('hidden') == false) {
            $('.sectionclass').addClass('hidden')
        } else {
            $('.sectionclass').addClass('hidden')
            thisObj.querySelector('.sectionclass').classList.remove('hidden')
        }  
    }
})

$(function () {
    // Initializes and creates emoji set from sprite sheet
    window.emojiPicker = new EmojiPicker({
        emojiable_selector: '[data-emojiable=true]',
        assetsPath: '/soltanistatic/lib/img/',
        popupButtonClasses: 'fa fa-smile-o'
    });
    // Finds all elements with `emojiable_selector` and converts them to rich emoji input fields
    // You may want to delay this step if you have dynamically created input fields that appear later in the loading process
    // It can be called as many times as necessary; previously converted input fields will not be converted again
    window.emojiPicker.discover();
});

// Google Analytics
(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
ga('create', 'UA-49610253-3', 'auto');
ga('send', 'pageview');
// to add product to card

$('body').on('click', '.btn-addToCart', function (e) {

    e.preventDefault();
    
    var p_id = $(this).parents('[productid]').attr('productid')
   

    jQuery.post('/purchasecart/addnewitem', { product_id: p_id }, function (result) {
        if (result.preselected == '1') {
            $(messagenotokbody).html(' این کالا در سبد خرید شما قبلاً اضافه شده است. ')
            $(messagenotok).modal()
            return
        }

        if (result.userid == '0') {
            var html = '<div class="row">'
            html += ' <div class="col-md-12">'
            html += '<div class="col-md-6">'
            html += ' <center>'
            html += '<p> عضو سایت می باشید؟</p>'
            html += '<p style="font-size:8pt;"> برای تکمیل فرآیند خرید خود وارد شوید</p>'
            html += '<a href="/home/login" class="btn btn-success">ورود به سایت</a>'
            html += ' </center>'
            html += ' </div>'
            html += ' <div class="col-md-6">'
            html += ' <center>'
            html += ' <p>  هنوز عضو سایت نشده اید؟</p>'
            html += ' <p style="font-size:8pt;"> برای تکمیل فرآیند خرید خود ثبت نام کنید</p>'
            html += ' <a href="/home/register" class="btn btn-primary">ثبت نام در سایت</a>'
            html += ' </center>'
            html += '</div>'
            html += ' </div>'
            html += ' </div>'
            $(messagenotokbody).html(html)
            $(messagenotokheader).html('عدم ورود به سایت')
            $(messagenotok).modal()

        } else {


            if (result.numberitemexist <= 0) {
                $(messagenotokbody).html(' این کالا در حال حاضر در فروشگاه موجود نمی باشد ')
                $(messagenotok).modal()

            } else {

                $('.badge-pill').html(result.number)

                $(messageokbody).html('کالای مورد نظر به سبد کالای شما افزوده شد')
                $(messageok).modal()
            }

        }

    })

})