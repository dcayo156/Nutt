
$.fn.menumaker = function (options) {

    var cssmenu = $(this), settings = $.extend({
        title: "Menu",
        format: "dropdown",
        sticky: false
    }, options);

    return this.each(function () {
        cssmenu.prepend('<div id="menu-button"><a href="/inicio" class="logo"><img alt="" src="/css/img/logo.png" style="margin-left: 3px; margin-top: 3px" /></a></div>');
        $(this).find("#menu-button").on('click', function () {
            $(this).toggleClass('menu-opened');
            var mainmenu = $(this).next('ul');
            if (mainmenu.hasClass('open')) {
                mainmenu.hide().removeClass('open');
            }
            else {
                mainmenu.show().addClass('open');
                if (settings.format === "dropdown") {
                    mainmenu.find('ul').show();
                }
            }
        });

        cssmenu.find('li ul').parent().addClass('has-sub');

        multiTg = function () {
            cssmenu.find(".has-sub").prepend('<span class="submenu-button"></span>');
            cssmenu.find('.submenu-button').on('click', function () {
                $(this).toggleClass('submenu-opened');
                if ($(this).siblings('ul').hasClass('open')) {
                    $(this).siblings('ul').removeClass('open').hide();
                }
                else {
                    $(this).siblings('ul').addClass('open').show();
                }
            });
        };

        if (settings.format === 'multitoggle') multiTg();
        else cssmenu.addClass('dropdown');

        if (settings.sticky === true) cssmenu.css('position', 'fixed');

        var anteriorVisibilidad = "xxx";
        resizeFix = function () {
            //if ($(window).width() > 1000) {
            var nuevaVisibilidad = $("#parametro1000").is(":visible");
            if (nuevaVisibilidad !== anteriorVisibilidad) {
                if (!nuevaVisibilidad) {
                    cssmenu.find('ul').show();
                }

                //if ($(window).width() <= 1000) {
                if (nuevaVisibilidad) {
                    cssmenu.find('ul').hide().removeClass('open');
                }

                anteriorVisibilidad = nuevaVisibilidad;
            }

        };
        resizeFix();
        return $(window).on('resize', resizeFix);

    });
};