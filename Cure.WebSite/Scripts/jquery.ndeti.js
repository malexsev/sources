


/*----------- ФУНКЦИИ ПОСЛЕ ГОТОВНОСТИ ---------------------------------------*/

$(document).ready(function () {

    /* --- ФУНКЦИИ: Прокрутка страниц наверх --------------------------------*/
    $.fn.scrollToTop = function () {
        var scrollLink = $(this);
        scrollLink.hide();
        if ($(window).scrollTop() >= "150") scrollLink.fadeIn("slow");
        $(window).scroll(function () {
            if ($(window).scrollTop() <= "150") scrollLink.fadeOut("slow");
            else scrollLink.fadeIn("slow");
        });
        $(this).click(function () {
            $("html, body").animate({ scrollTop: 0 }, "slow");
        });
    };
    //  Инициализация работы прокрутки страниц наверх
    $(".js-scroll-top").scrollToTop();


    /* --- Загрузка больше контента -------------------------------------------*/
    function dataAjaxLoad(loadingBtn, loadingPlace, addr, counter, forCount, showCount) {
        $(loadingBtn).click(function () {
            var $loadingBtn = $(this),
                $loadingPlace = $(loadingPlace),
                $counter = $(counter),
                $showCount = $(showCount),
                $forCount = $(forCount);

            $showCount.val($(counter).text());
            var serializedForm = $('#childrenform').serialize();

            if ($loadingPlace.length) {
                $loadingBtn.addClass("loading");
                $.ajax({
                    type: "POST",
                    url: addr,
                    dataType: "html",
                    data: serializedForm,
                    cache: false,
                    error: function () {
                        console.log("Error loading more");
                    },
                    success: function (poupHtml) {
                        console.log("Success loading more");
                        $loadingPlace.append(poupHtml);
                        setTimeout(function () {
                            $loadingPlace.children(".jast-loaded").removeClass("jast-loaded");
                            $loadingBtn.removeClass("loading");
                            if ($counter.length && $forCount.length) {
                                $counter.text($(forCount).length);
                            };
                        }, 500);
                    }
                });
            };
        });
    };
    //Загрузка больше фото детей и отзывов
    dataAjaxLoad(".js-load-more-children", "#js-for-load-children", "/Children/More",
                 "#more-count", ".js-ajax-for-count", "#skiprecords");
    //Загрузка больше историй
    dataAjaxLoad(".js-load-more-history", "#js-for-load-history", "ajax/more_history.php");
    //Загрузка больше впечатлений
    dataAjaxLoad(".js-load-more-feelings", "#js-for-load-feelings", "ajax/more_feelings.php");

    /*----------- ФУНКЦИИ: Работа табов ---------------------------------------*/
    $.fn.tabsInit = function () {
        var $tabsHead = $(this),
            $links = $tabsHead.find(".js-tabs-link"),
            $tabsBody = $tabsHead.siblings(".js-tabs-body"),
            $tabs = $tabsBody.children(".js-tabs-item");
        $links.click(function () {
            var tabId = "#" + $(this).data("tab");
            $links.removeClass("active");
            $(this).addClass("active");
            $tabs.removeClass("active");
            $(tabId).addClass("active");
            $('.active .js-tab-slider').slick('setPosition');
        });
        return this;
    };
    //  Инициализация работы табов в блоке "Виды работ"
    $(".js-tabs-head").tabsInit();
    $(".js-tabs-userdata").tabsInit();
    $(".js-content-tabs").tabsInit();
    $(".js-order-tabs").tabsInit();
    $(".js-personal-tabs").tabsInit();
    $(".js-tabs-registr").tabsInit();
    $(".js-tabs-citymap").tabsInit();


    /*----------- ФУНКЦИИ: Работа спойлера -----------------------------------*/
    $.fn.spoilerInit = function (startHeight) {
        var $spoilerBtn = $(this),
            $spoilerBody = $spoilerBtn.siblings(".js-spoiler-body"),
            startHeight = startHeight || 0,
            autoHeight = $spoilerBody.css('height', 'auto').height();

        $spoilerBody.height(startHeight);

        $spoilerBtn.click(function () {
            if ($spoilerBtn.hasClass("active")) {
                $spoilerBody.animate({ height: startHeight }, 600);
                $spoilerBtn.removeClass("active").text("Читать дальше");
            } else {
                $spoilerBody.animate({ height: autoHeight }, 600);
                $spoilerBtn.addClass("active").text("Скрыть");
            };
        });
        return this;
    };
    //  Инициализация работы спойлеров (с начальной высотой)
    $(".js-spoiler-clinic").spoilerInit(240);


    /* --- Замена селектов списком со скрытым полем для отправки данных ------*/
    $(document).on('click', ".selector div", function () {
        $(this).closest(".selector").addClass("show-list");
    });
    $(document).on('mouseleave', ".selector div", function () {
        $(this).closest(".selector").removeClass("show-list");
    });
    $(document).on('mouseenter', ".selector ul", function () {
        $(this).closest(".selector").addClass("show-list");
    });
    $(document).on('mouseleave', ".selector ul", function () {
        $(this).closest(".selector").removeClass("show-list");
    });
    $(document).on('click', ".selector li", function () {
        var selDataVal = $(this).data("val");
        var selDataTxt = $(this).text();
        $(this).parents("ul").siblings("div").text(selDataTxt).attr({ "data-val": selDataVal });
        $(this).parents("ul").siblings("input")
                .val(selDataVal)
                .trigger('change'); // Событие изменения в поле
        $(this).closest(".selector").removeClass("show-list");
        $("#skiprecords").val(0);
        $('form#childrenform').submit();
    });
    //Закрытие списка селектора при клике мимо
    $(document).mouseup(function (e) {
        var selectorList = $('.selector.show-list');
        if (e.target !== selectorList[0] && !selectorList.has(e.target).length) {
            selectorList.removeClass("show-list");
        };
    });
    //Выставление правильного предзаданного значения в селекте
    $(window).load(function () {
        $(".selector").each(function () {
            var $that = $(this),
                inputVal = $that.children("input").val(),
                selectedText;
            if (inputVal) selectedText = $that.find('li[data-val="' + inputVal + '"]').text();
            if (selectedText) $that.children(".selector-val").text(selectedText);
            $("#more-count").text($(".js-ajax-for-count").length);
            //console.log( selectedText );
        });
    });
    //  Вертикальная прокрутка в селекторах
    //  $(".selector ul").jScrollPane();

    /* --- Валидация форм -----------------------------------------------------*/
    $(".js-validate").each(function () {
        $(this).validate({
            focusInvalid: false,
            rules: {
                name: {
                    required: true
                },
                mail: {
                    required: true,
                    email: true
                },
                numb: {
                    required: true
                },
                text: {
                    required: true
                }
            },
            messages: {
                name: "",
                mail: "",
                numb: "",
                text: ""
            },
            errorClass: "input-error",
            highlight: function (element, errorClass, validClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
                alert("Submitted!");
                //form.submit();
            }
        });
    });
    //  Маска ввода в поле телефонного номера
    $("[name='numb']").mask("+7 (999) 999-99-99", { placeholder: "_" });

    /* --- Валидация формы регистрации ---------------------------------------*/
    $(".js-registr-validate").each(function () {
        $(this).validate({
            focusInvalid: false,
            rules: {
                regname: { required: true, minlength: 6 },
                regmail: { required: true, email: true },
                regpass: { required: true, minlength: 6 },
                passtwice: { equalTo: "#regpass" }
            },
            messages: {
                regname: {
                    required: "Некорректное имя пользователя",
                    minlength: "Очень короткое имя пользователя"
                },
                regmail: "Введите электронную почту",
                regpass: "Заполните поле<br> пароля",
                passtwice: "Пароли<br> не совпадают"
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
                //form.submit();
            }
        });
    });

    /* --- Валидация формы входа ---------------------------------------------*/
    $(".js-login-validate").each(function () {
        $(this).validate({
            focusInvalid: false,
            rules: {
                loginname: { required: true },
                loginpass: { required: true }
            },
            messages: {
                loginname: "Ошибка в имени пользователя",
                loginpass: "Ошибка<br> в пароле"
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
                //alert("Submitted!");
                //form.submit();
            }
        });
    });

    /* --- Валидация формы восстановления пароля -----------------------------*/
    $(".js-remind-validate").each(function () {
        $(this).validate({
            focusInvalid: false,
            rules: {
                remindinp: { required: true }
            },
            messages: {
                remindinp: "Поле заполнено<br> некорректно"
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
                //$(form).hide().siblings(".js-submit-ok").show();
                //form.submit();
            }
        });
    });

    /*------------ Вход пользователя на сервере ----------------------------*/
    $("#formLogin").submit(function (e) {
        $("#error-login").hide();
        var form = $('#formLogin');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/Login",
                type: "POST",
                data: serializedForm,
                success: function (result) {
                    if (result == "1") {
                        $("#loginname").parent().removeClass("has-error");
                        $("#password").parent().removeClass("has-error");
                        $("#loginname").val("");
                        $("#password").val("");
                        $("#error-login").hide();
                        location.reload();
                    } else {
                        $("#loginname").parent().addClass("has-error");
                        $("#password").parent().addClass("has-error");
                        $("#error-login").show().text("Неверное имя пользователя или пароль");
                    }
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
        }
        e.preventDefault();
    });

    /*------------ Регистрация пользователя на сервере ----------------------------*/
    $("#formRegister").submit(function (e) {
        $("#error-register").hide();
        var form = $('#formRegister');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/Register",
                type: "POST",
                data: serializedForm,
                success: function (result) {
                    if (result == "1") {
                        $("#regname").parent().removeClass("has-error");
                        $("#regname").val("");
                        $("#regpass").val("");
                        $("#regpasswice").val("");
                        $("#error-register").hide();
                        $(form).hide().siblings(".js-submit-ok").show();
                    }
                    else if (result == "0") {
                        $("#loginname").parent().addClass("has-error");
                        $("#error-register").show().text("Данный email уже используется в системе, воспульзуйтесь подсказкой пароля при входе");
                    } else {
                        $("#loginname").parent().addClass("has-error");
                        $("#error-register").show().text("Данный логин уже зарегистрирован, воспульзуйтесь подсказкой пароля при входе");
                    }
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
        }
        e.preventDefault();
    });

    /*------------ Восстановление доступа пользователя на сервере ----------------------------*/
    $("#formRecovery").submit(function (e) {
        $("#error-recovery").hide();
        var form = $('#formRecovery');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/Recovery",
                type: "POST",
                data: serializedForm,
                success: function (result) {
                    if (result == "1") {
                        $("#remindinp").parent().removeClass("has-error");
                        $("#remindinp").val("");
                        $("#error-recovery").hide();
                        $(form).hide().siblings(".js-submit-ok").show();
                    } else {
                        $("#remindinp").parent().addClass("has-error");
                        $("#error-recovery").show().text("Пользователя с таким именем или адресом электронной почты в системе не найдено.");
                    }
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
        }
        e.preventDefault();
    });


    /*----------- Галлерея картинок  -----------------------------------------*/
    $('#big-slider-img').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        fade: true,
        asNavFor: '#big-slider-nav'
    });
    $('#big-slider-nav').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        asNavFor: '#big-slider-img',
        dots: false,
        arrows: false,
        centerMode: true,
        focusOnSelect: true,
        variableWidth: true
    });

    /*----------- Слайдер картинок  ----------------------------------------------*/
    $('.js-allpages-slider').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        dots: true,
        infinite: true,
        speed: 500,
        autoplay: true,
        autoplaySpeed: 5000
    });



    $(".js-toggle-menu").click(function () {
        $(this).toggleClass("active");
        $(".header-top-menu").toggleClass("active");
    });

    $(".js-mob-accordion-btn").click(function () {
        $(this).toggleClass("active");
        $(this).siblings(".js-mob-accordion-body").slideToggle();
    });


    /*---  Ширина скролла -----------------------------------------------------*/
    var scrollWidth = 0;
    getScroll = function () {
        var div = document.createElement('div');

        div.style.overflowY = 'scroll';
        div.style.width = '50px';
        div.style.height = '50px';
        div.style.visibility = 'hidden';

        document.body.appendChild(div);
        scrollWidth = div.offsetWidth - div.clientWidth;
        document.body.removeChild(div);
    };
    getScroll();

    /*--- Всплывающее окно ---------------------------------------------------*/
    $(".js-show-popup").click(function () {
        var popId = "#" + $(this).data("pop"),
            scrollCorr = 0;
        $(".popup").hide();
        $(popId).show();
        $("body").addClass("cutted");
    });

    $(".popup-close, .popup-modal, .js-close-popup").click(function () {
        $(".popup").hide();
        $("body").removeClass("cutted");
    });


    /*--- Кабинет: отключение/включение фото --------------------------------*/
    $(".js-images-tile-toggle").click(function () {
        var $btn = $(this),
            $tileItem = $btn.closest(".images-tile-item");

        if ($tileItem.hasClass("is-enabled")) {
            $btn.removeClass("is-enabled").addClass("is-disabled");
            $tileItem.removeClass("is-enabled").addClass("is-disabled");
        } else {
            $btn.removeClass("is-disabled").addClass("is-enabled");
            $tileItem.removeClass("is-disabled").addClass("is-enabled");
        };
    });

    /*--- Кабинет: показ/скрытие комментариев ------------------------*/
    $(".js-comments-open").click(function () {
        var $btn = $(this),
            $commentsItem = $btn.closest(".post-comments");

        $commentsItem.addClass("is-open");
    });


    /*--- Counter -----------------------------------------------------------*/
    $(".js-counter .btn-dwn").click(function () {
        var $input = $(this).siblings(".counter-rez"),
            count = parseInt($input.val()) - 1;
        count = count < 0 ? 0 : count;
        $input.val(count).change();
    });
    $(".js-counter .btn-upp").click(function () {
        var $input = $(this).siblings(".counter-rez");
        $input.val(parseInt($input.val()) + 1).change();
    });


    /*----------- Слайдер и видео на главной  --------------------------------*/
    function mainPageStart() {
        if ($(".main-page").length) {
            var $slider = $("#sec-intro-slider"),
                $video = $("#sec-intro-video");
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                $slider.children(".slide").each(function () {
                    $(this).attr("style", $(this).data("style"));
                });
                $slider.addClass("active");
                $slider.slick({
                    arrows: false,
                    dots: false,
                    infinite: true,
                    speed: 1000,
                    slidesToShow: 1,
                    autoplay: true,
                    autoplaySpeed: 5000
                });
            } else {
                $video.find("source").each(function () {
                    $(this).attr("src", $(this).data("src"));
                });
                $video.addClass("active");
                var video = $video.children()[0];
                video.load();
                video.play();
            }
        };
    };
    $(window).load(mainPageStart());


    /*----------- Анимация на главной: появление 4-х шагов -------------------*/
    $(".js-step-fadein").addClass("is-invisible");
    $(".js-steps-fadein").viewportChecker({
        offset: 200,
        callbackFunction: function ($elem) {
            var time = 1000,
                $steps = $elem.find(".js-step-fadein");
            $steps.each(function () {
                var $that = $(this);
                setTimeout(function () { $that.addClass("is-visible"); }, time);
                time = time + 1000;
            });
        }
    });

    /*----------- Анимация на главной: мелькание количества ------------------*/
    $(".js-counters").viewportChecker({
        offset: 200,
        callbackFunction: function () {
            var el_1 = document.querySelector(".js-count-one"),
                el_2 = document.querySelector(".js-count-two"),
                od_1 = new Odometer({ el: el_1, format: "( ddd)" }),
                od_2 = new Odometer({ el: el_2, format: "( ddd)" });
            od_1.update(+$(".js-count-one").data("count"));
            od_2.update(+$(".js-count-two").data("count"));
        }
    });

    /*----------- Анимация на главной: появление 4-х шагов -------------------*/
    $(".js-countries-fadein li").addClass("is-invisible");
    $(".js-countries-fadein").viewportChecker({
        offset: 200,
        callbackFunction: function ($elem) {
            var time = 1000,
                $steps = $elem.find("li");
            $steps.each(function () {
                var $that = $(this);
                setTimeout(function () { $that.addClass("is-visible"); }, time);
                time = time + 600;
            });
        }
    });


    /*----------- Календарь в инпутах дат ------------------------------------*/
    /* www.bootstrap-datepicker.readthedocs.io/en/stable
     * Если один инпут:
      $('.js-datepicker').datepicker({
        format: 'dd.mm.yyyy',
        startDate: '+5d',
        language: 'ru'
    }); */
    //Если диапазон:
    $('.js-input-daterange').datepicker({
        format: 'dd.mm.yyyy',
        startDate: '+5d',
        language: 'ru',
        inputs: $('.ico-calendar')
    });






});


/*----------- ФУНКЦИИ ПОСЛЕ ЗАГРУЗКИ -----------------------------------------*/

$(window).load(function () {

    setTimeout(function () {
        $(".preloader").addClass("off");
    }, 600);



});



