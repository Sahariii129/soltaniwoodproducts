/**
 * jQuery Captcha Basic
 *
 * @fileoverview  Plugin object
 * @link          https://github.com/pemre/jquery-captcha-basic/
 * @author        Emre Piskin (http://rencs.com/)
 * @requires      jQuery 1.12.4+
 */

(function($) {
    "use strict";

    $.fn.captcha = function (param) {
       

        // DEFAULT VARIABLES
        var params = $.extend({
            idCaptchaText: 'captchaText',   // The ID for the captcha text. Default is 'captchaText'.
            idCaptchaInput: 'captchaInput', // The ID for the captcha input. Default is 'captchaInput'.
            class: ''                       // Class name for the submit button toggle. Default is ''.
        }, param);

        // Find and disable the submit button
        var submit = $(this).find('input[type=submit]');
        submit.attr('disabled', 'disabled');
        var captchid = $('#captcha')
        var htmlcaptcha = '<label class="control-label col-md-4" id="' + params.idCaptchaText + '"></label>'
        htmlcaptcha += '<div class="col-md-8">'
        htmlcaptcha += '<input class="control-label" id="' + params.idCaptchaInput + '" aria-label="Captcha Input" type="text" required>'
        htmlcaptcha += '</div>'
       

        // Insert captcha text and input before the submit button with the given ID's
        //$('<label class="control-label" id="' + params.idCaptchaText + '"></label>').insertBefore(submit);
        //$('<input class="control-label" id="' + params.idCaptchaInput + '" aria-label="Captcha Input" type="text" required>').insertBefore(submit);
        captchid.html(htmlcaptcha)
        // Select text and input elements to fill
        var label = this.find('#' + params.idCaptchaText);
        var input = this.find('#' + params.idCaptchaInput);

        // Generate random numbers and the sum of them
        var rndmNr1 = Math.floor(Math.random() * 10),
            rndmNr2 = Math.floor(Math.random() * 10),
            totalNr = rndmNr1 + rndmNr2;

        // Print the numbers to screen
        $(label).text(rndmNr1 + ' + ' + rndmNr2 + ' =');

        // Check the input value, enable it if the sum is correct
        $(input).keyup(function () {
            if ($(this).val() == totalNr)
                submit.removeAttr('disabled').addClass(params.class);
            else
                submit.attr('disabled', 'disabled').removeClass(params.class);
        });

        // Don't breake jQuery chain!
        return this;
    }
})(jQuery);
