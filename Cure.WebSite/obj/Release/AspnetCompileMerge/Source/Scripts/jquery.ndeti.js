var app = app || {},
    D = $(document),
    W = $(window),
    B,
    HB,
    POPUP,
    mapInitialize, google,
    activeClass = "active",
    preloaderURL = "/content/img/preloader.gif",
    preloader = "<img src='" + preloaderURL + "' />",
    redirectURL = "/Home/Index/";

$.fn.spoilerInit = function(startHeight) {
    var button = $(this),
        startHeight = startHeight || 0;
    button.each(function(index, element) {
        var self = $(this),
            body = self.siblings(".js-spoiler-body"),
            autoHeight = body.css("height", "auto").height();

        if (autoHeight > startHeight) {
            body.height(startHeight);
        } else {
            self.hide();
        }
        self.on("click", function() {
            if (self.hasClass(activeClass)) {
                body.animate({ height: startHeight }, 600);
                self.removeClass(activeClass).text("Читать дальше");
            } else {
                body.animate({ height: autoHeight }, 600);
                self.addClass(activeClass).text("Скрыть");
            }
        });
    });
    return this;
};

$.fn.tabsInit = function() {
    var tabsHead = $(this),
        links = tabsHead.find(".js-tabs-link"),
        tabsBody = tabsHead.siblings(".js-tabs-body"),
        tabs = tabsBody.children(".js-tabs-item");
    links.on("click", function() {
        var self = $(this);
        var tabId = "#" + self.data("tab");
        links.removeClass(activeClass);
        self.addClass(activeClass);
        tabs.removeClass(activeClass);
        $(tabId).addClass(activeClass);
        $(".active .js-tab-slider").slick("setPosition");
    });
    return this;
};

$.fn.scrollToTop = function() {
    var scrollLink = $(this);
    scrollLink.hide();
    if (W.scrollTop() >= "150") {
        scrollLink.fadeIn("slow");
    }
    W.on("scroll", function() {
        if (W.scrollTop() <= "150") {
            scrollLink.fadeOut("slow");
        } else {
            scrollLink.fadeIn("slow");
        }
    });
    $(this).on("click", function() {
        HB.animate({ scrollTop: 0 }, "slow");
    });
};

D.on("ready", function() {
    B = $("body");
    HB = $("html, body");
    POPUP = $(".popup");

    function DeletePicture(id, fileName) {
        $("#pictodelete").val(fileName);
        $("#pictohideuphide").val("");
        $("#pictotop").val((""));
        $('#formGalery').submit();
    }

    function TopPicture(id, fileName) {
        $("#pictotop").val(fileName);
        $("#pictodelete").val("");
        $("#pictohideuphide").val("");
        $('#formGalery').submit();
    }

    function HideShowPicture(id, fileName) {
        $("#pictodelete").val("");
        $("#pictotop").val((""));
        $("#pictohideuphide").val(fileName);
        $('#formGalery').submit();
    }

    function HideShowDocument(id, fileName) {
        $("#doctodelete").val("");
        $("#doctohideuphide").val(fileName);
        $('#formDocuments').submit();
    }

    function DeleteDocument(id, fileName) {
        $("#doctodelete").val(fileName);
        $("#doctohideuphide").val("");
        $('#formDocuments').submit();
    }

    $.validator.addMethod("regex", function (value, element, regexp) {
        var check = false;
        var re = new RegExp(regexp);
        return this.optional(element) || re.test(value);
    }, "Запрещённые символы");

    app.init().onScroll().onResize().onLoad();
});

app.init = function() {
    return this.initScripts().bindActions();
}
 
app.onScroll = function() {
    var self = this;
    return this;
}

app.onResize = function() {
    var self = this;
    W.on("resize", function() {
        // self.doSomething();
    });
    return this;
}
 
app.onLoad = function() {
    var self = this;
    W.on("load", function() {
        self.bindTabs();
        self.bindScrollToTop();
        self.bindSpoiler();
        self.initMainPageStart();
        self.bindHidePreloader();
        self.bindAddCustomClassToMegaMenu();
        setTimeout(function() { self.bindUpdateUnreadMessages(); }, 6000);
        setInterval(function() { self.bindUpdateUnreadMessages(); }, 50000);
        
        //Выставление правильного предзаданного значения в селекте
        var selectorName = selectorName || ".selector";
        $(selectorName).each(function() {
            var self = $(this),
                inputVal = self.children("input").val(),
                selectedText,
                $selectedItem;
            if (inputVal) {
                $selectedItem = self.find('li[data-val="' + inputVal + '"]');
                selectedText = $selectedItem.text();
                $selectedItem.addClass(activeClass).siblings("li").removeClass(activeClass);
            };
            if (selectedText) {
                self.children(".selector-val").text(selectedText);
            }
            //TODO: Нижнее надо оставить только для страницы Наши Дети
            $("#more-count").text($(".js-ajax-for-count").length);
            console.log(selectedText);
        });
    });
    return this;
}

app.initScripts = function() {
    this.initGoogleMap();
    this.initValidate();
    this.initPhoneMask();
    this.initContentSlider();
    this.initDatepicker();
    this.initSlick();
    this.initDotDotDot();
    this.initSwipeBox();
    this.initMainPageAnimation();
    return this;
}

app.bindActions = function() {
    var self = this;
    this.bindScrollTo();
    this.bindMegaMenu();
    this.bindGetScrollWidth();
    this.bindPopup();
    this.bindCopyToClipboard();
    this.bindStickyAfterScroll();
    this.bindHightlightAnchors();
    this.bindAccordeon();
    this.bindMobileMenu();
    this.bindSaveMyPage();
    this.bindLogin();
    this.bindRegistration();
    this.bindRecovery();
    this.bindChangePassword();
    this.bindChangeEmail();
    this.bindChangePhone();
    this.bindSaveOrder();
    this.bindDeferOrder();
    this.bindAddReview();
    this.bindAddFile();
    this.bindRemoveFile();
    this.bindAddUserPic();
    this.bindRemoveUserPic();
    this.bindAddFileToOrder();
    this.bindRemoveFileFromOrder();
    this.bindAddFeedback();
    this.bindAddToRSS();
    this.bindSaveAvatar();
    this.bindFilterReviews();
    this.bindFilterFAQ();
    this.bindAddFileToGallery();
    this.bindAddFileToDocuments();
    this.bindAddPost();
    this.bindRemovePost();
    this.bindEditPost();
    this.bindToggleAssist();
    this.bindShowCharactersLeft();
    this.bindOpenComments();
    this.bindOperationsWithDocuments();
    this.bindOperationsWithGallery();
    this.bindGoBackOneStep();
    this.bindImagesTileToogle();
    this.bindEnableEditMode();
    this.bindEnableEditPost();
    this.bindCancelEditPost();
    this.bindInsertUserNameWhenAnswering();
    this.bindShiftChat();
    this.bindIncrement();
    this.bindDecrement();
    //this.bindDownloadFile();
    this.bindSetCheckedRadiotwix();
    this.bindSelectLogic();
    this.bindShowPassword();

    //Загрузка больше отзывов
    this.bindDataAjaxLoad(".js-load-more-testimonials", "#js-for-load-testimonials", "/Mension/More", "#more-count", function() { 
        return $("#js-for-load-testimonials")[0].childElementCount; 
    }, "#skiprecords", '#mensionform');
    
    //Загрузка больше фото детей и отзывов
    this.bindDataAjaxLoad(".js-load-more-children", "#js-for-load-children", "/Children/More", "#more-count", function() { 
        return $("#js-for-load-children")[0].childElementCount; 
    }, "#skiprecords", '#childrenform');
    
    //Загрузка больше историй
    this.bindDataAjaxLoad(".js-load-more-history", "#js-for-load-history", "ajax/more_history.php");
    
    //Загрузка больше впечатлений
    this.bindDataAjaxLoad(".js-load-more-feelings", "#js-for-load-feelings", "ajax/more_feelings.php");
    
    //Загрузка больше новостей
    this.bindDataAjaxLoad(".js-load-more-news", "#js-for-load-news", "/News/More", "#more-count", function() {
        var calc = $("#js-for-load-news")[0].childElementCount * 4;
        var total = $('#more-all').text() * 1;
        return calc > total ? total : calc;
    }, "#skiprecords", '#newsform');

    /* --- Первоначальная загрузка моих файлов --- */
    $("#formFiles").on("ready", function() {
        var guidId = $("#orderfilesfilter").val();
        self.bindRefreshMyFiles(guidId);
    });

    /*--- Детали Наши Дети - показатели тестов ------------------*/
    B.on("click", ".js-testlevels-start", function(e) {
        alert("gg");
    });

    //  Вертикальная прокрутка в селекторах
    //  $(".selector ul").jScrollPane();

    return this;
}

app.initMainPageStart = function() {
    if ($(".main-page").length) {
        var slider = $("#sec-intro-slider"),
            video = $("#sec-intro-video");
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            slider.children(".slide").each(function() {
                $(this).attr("style", $(this).data("style"));
            });
            slider.addClass(activeClass);
            slider.slick({
                arrows: false,
                dots: false,
                infinite: true,
                speed: 1000,
                slidesToShow: 1,
                autoplay: true,
                autoplaySpeed: 5000
            });
        } else {
            video.find("source").each(function() {
                $(this).attr("src", $(this).data("src"));
            });
            video.addClass(activeClass);
            var player = video.children()[0];
            player.load();
            player.play();
        }
    };
}

app.initGoogleMap = function() {
    var mapWrap = $("#map-wrap");
    mapInitialize = function() {
        console.log("Карта стартовала");
        var myMapPlace = document.getElementById("map-wrap"),
          myLatlng = new google.maps.LatLng(mapWrap.data("lat"), mapWrap.data("lng"));

        var myOptions = {
            scrollwheel: false,
            disableDefaultUI: true,
            zoom: 15,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(myMapPlace, myOptions);

        var companyLogo = new google.maps.MarkerImage('content/img/icon_map.png',
          new google.maps.Size(28, 37),
          new google.maps.Point(0, 0),
          new google.maps.Point(14, 37)
        );
        var company = myLatlng;
        var companyMarker = new google.maps.Marker({
            position: company,
            map: map,
            icon: companyLogo,
            title: "Санкт-Петербург, проспект Энгельса, д. 33, корп. 1, лит. А, офис 610"
        });
        var styles = [{ "stylers": [{ "saturation": -100 }] }, {}];
        map.setOptions({ styles: styles });
    };
    if (mapWrap.length) {
        $.getScript(
            "http://maps.google.com/maps/api/js?key=AIzaSyAKBqR85f1-xQBsBP54b5X5yEXDRJM1etQ&sensor=false&callback=mapInitialize",
            function() {
                console.log("Кaрта загружена");
            }
        );
    }
}

app.initValidate = function() {
    /* --- Валидация форм --- */
    $(".js-validate").each(function() {
        $(this).validate({
            focusInvalid: false,
            rules: {
                birthday: {
                    required: true
                },
                childname: {
                    required: true
                },
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
                name: "Введите имя",
                mail: "Введите корректный адрес",
                numb: "",
                text: "Введите сообщение",
                birthday: "Дата рождения обязательна",
                childname: "Введите имя ребёнка"
            },
            errorClass: "input-error",
            highlight: function (element, errorClass, validClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
                //alert("Submitted!");
                //form.submit();
                //$(form).hide().siblings(".js-submit-ok").show();
            }
        });
    });

    /* --- Валидация формы регистрации --- */
    $(".js-registr-validate").each(function() {
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

    /* --- Валидация добавления в рассылку --- */
    $(".js-newsletter-validate").each(function() {
        $(this).validate({
            focusInvalid: false,
            rules: {
                email: { required: true, email: true }
            },
            messages: {
                email: "Не верный адрес"
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

    /* --- Валидация формы входа --- */
    $(".js-login-validate").each(function() {
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

    /* --- Валидация смены пароля --- */
    $(".js-password-change").each(function() {
        $(this).validate({
            focusInvalid: false,
            rules: {
                currentpass: { required: true, minlength: 6 },
                newpass: { required: true, minlength: 6 },
                newpasstwice: { equalTo: "#newpass" },
            },
            messages: {
                currentpass: "Ошибка в текущем<br> пароле",
                newpass: "Слишком короткий<br> пароль",
                newpasstwice: "Пароли<br> не совпадают"
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
            }
        });
    });

    /* --- Валидация смены телефона --- */
    $(".js-phone-change").each(function() {
        $(this).validate({
            focusInvalid: false,
            rules: {
                phone: { maxlength: 12, minlength: 7 },
            },
            messages: {
                phone: "Проверьте введённый телефон"
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
            }
        });
    });

    /* --- Валидация смены email --- */
    $(".js-email-change").each(function() {
        $(this).validate({
            focusInvalid: false,
            rules: {
                email: { email: true },
            },
            messages: {
                email: "Введите корректный e-mail адрес"
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
            }
        });
    });

    /* --- Валидация добавления отзыва --- */
    $(".js-mension-add").each(function() {
        $(this).validate({
            focusInvalid: false,
            onkeyup: true,
            onclick: true,
            rules: {
                subject: { number: true, range: [-1, 100] },
                text: {
                    required: true,
                    minlength: 5,
                    maxlength: 3000,
                    regex: /^[^<>]+$/
                }
            },
            messages: {
                subject: "Выберите тему отзыва",
                text: {
                    required: "Отсутствует текст отзыва",
                    minlength: "Текст должен превышать 5 символов",
                    maxlength: "Текст не должен превышать 3000 символов",
                    regex: "Текст содержит запрещённые символы \"<\" или \">\""
                }
            },
            errorClass: "has-error",
            highlight: function (element, errorClass) {
                $(element).parent().addClass(errorClass);
            },
            unhighlight: function (element, errorClass) {
                $(element).parent().removeClass(errorClass);
            },
            submitHandler: function (form) {
            }
        });
    });

    /* --- Валидация формы восстановления пароля --- */
    $(".js-remind-validate").each(function() {
        $(this).validate({
            focusInvalid: false,
            rules: {
                remindinp: { required: true }
            },
            messages: {
                remindinp: "Поле заполнено <br/> некорректно"
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
}

app.initPhoneMask = function() {
    var item = $("[name='numb']");
    if (item.length) {
        item.mask("+7 (999) 999-99-99", { placeholder: "_" });
    }
}

app.initContentSlider = function() {
    var item = $(".js-content-slider");
    if (item.length) {
        item.each(function(index) {
            var self = $(this),
                totalSlides = self.children().length;
            self.siblings(".js-slide-count").find("._slide-all").text(totalSlides);
            self.slick({
                arrows: true,
                dots: false,
                autoplay: false,
                infinite: false,
                slidesToShow: 1
            });
        });
    };
    item.on("beforeChange", function (event, slick, currentSlide, nextSlide) {
        var self = $(this);
        self.siblings(".js-slide-count").find("._slide-now").text(nextSlide + 1);
    });
}

app.initDatepicker = function() {
    /* www.bootstrap-datepicker.readthedocs.io/en/stable */
    var daterange = $(".js-input-daterange"),
        datepicker = $(".js-datepicker");
    if (daterange.length) {
        daterange.datepicker({
            dateFormat: "dd.mm.yyyy",
            startDate: "+5d",
            language: "ru",
            autoclose: true,
            inputs: daterange.find(".ico-calendar")
        });
    }
    if (datepicker.length) {
        datepicker.datepicker({
            dateFormat: "dd.mm.yyyy",
            language: "ru",
            autoclose: true,
            multidate: false
        });
    }
}

app.initSlick = function() {
    /* --- Галерея картинок --- */
    var block1 = $("#big-slider-img");
    if (block1.length) {
        block1.slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: true,
            fade: true,
            asNavFor: "#big-slider-nav",
            infinite: true,
            speed: 100,
            autoplay: true,
            autoplaySpeed: 5000
        });
    }

    var block2 = $("#big-slider-nav");
    if (block2.length) {
        block2.slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            asNavFor: "#big-slider-img",
            dots: false,
            arrows: false,
            centerMode: true,
            focusOnSelect: true,
            variableWidth: true,
            speed: 500,
            autoplay: true,
            autoplaySpeed: 5000
        });
    }

    /* --- Слайдер картинок --- */
    var block3 = $(".js-allpages-slider");
    if (block3.length) {
        block3.slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: true,
            dots: true,
            infinite: true,
            speed: 500,
            autoplay: true,
            autoplaySpeed: 5000
        });
    }


    /* --- Слайдер докторов --- */
    var block4 = $(".js-doctors-slider");
    if (block4.length) {
        block4.slick({
            dots: false,
            arrows: true,
            infinite: false,
            speed: 600,
            variableWidth: true,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 5000
        });
    }

    /* --- Слайдер секции блога/новостей --- */
    var block5 = $(".js-blog-slider");
    if (block5.length) {
        block5.slick({
            dots: false,
            arrows: true,
            infinite: false,
            speed: 600,
            slidesToScroll: 1,
            slidesToShow: 4,
            autoplay: false,
            autoplaySpeed: 5000,
            responsive: [
                {
                  breakpoint: 992,
                  settings: {
                    slidesToShow: 3
                  }
                },
                {
                  breakpoint: 767,
                  settings: {
                    slidesToShow: 2
                  }
                },
                {
                  breakpoint: 519,
                  settings: {
                    slidesToShow: 1
                  }
                }
            ]
        });
    }

    /* --- Слайдер новостей с фоном --- */
    var block6 = $(".js-news-slider");
    if (block6.length) {
        block6.slick({
            dots: true,
            arrows: false,
            infinite: true,
            speed: 600,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 5000
        });
    }
}

app.initDotDotDot = function() {
    var element = $(".js-dots");
    if (element.length) {
        element.dotdotdot({
            watch: 'window',
            wrap: 'letter',
            fallbackToLetter: 'letter',
            windowResizeFix: true
        });
    }
}

app.initSwipeBox = function() {
    /* --- Галлерея в всплывающем окне --- */
    var gallery = $(".js-popup-gallery");
    if (gallery.length) {
        gallery.swipebox();
    }
}

app.initMainPageAnimation = function() {
    /*----------- Анимация на главной: появление 4-х шагов -------------------*/
    $(".js-step-fadein").addClass("is-invisible");
    $(".js-steps-fadein").viewportChecker({
        offset: 200,
        callbackFunction: function(element) {
            var time = 1000,
                steps = element.find(".js-step-fadein");
            steps.each(function() {
                var self = $(this);
                setTimeout(function() { 
                    self.addClass("is-visible"); 
                }, time);
                time = time + 1000;
            });
        }
    });

    /*----------- Анимация на главной: мелькание количества ------------------*/
    $(".js-counters").viewportChecker({
        offset: 200,
        callbackFunction: function() {
            var el_1 = document.querySelector(".js-count-one"),
                el_2 = document.querySelector(".js-count-two"),
                od_1 = new Odometer({ el: el_1, format: "( ddd)" }),
                od_2 = new Odometer({ el: el_2, format: "( ddd)" });
            od_1.update(+$(".js-count-one").data("count"));
            od_2.update(+$(".js-count-two").data("count"));
        }
    });

    /*----------- Анимация на главной: появление 4-х шагов -------------------*/
    //$(".js-countries-fadein li").addClass("is-invisible");
    $(".js-countries-fadein").viewportChecker({
        offset: 200,
        callbackFunction: function(element) {
            var time = 500,
                steps = element.find("li");
            steps.each(function() {
                var self = $(this);
                setTimeout(function() { 
                    self.addClass("is-visible"); 
                }, time);
                time = time + 250;
            });
        }
    });
}

app.bindDataAjaxLoad = function(button, appendContainer, url, showedLabel, getCount, skiprecords, form) {
    $(button).on("click", function() {
        var $button = $(this),
            $appendContainer = $(appendContainer),
            $showedLabel = $(showedLabel),
            $skiprecords = $(skiprecords),
            //$showedNumber = $(showedNumber),
            $form = $(form);

        $skiprecords.val($showedLabel.text());
        var serializedForm = $form.serialize();

        if ($appendContainer.length) {
            $button.addClass("loading");
            $.ajax({
                type: "POST",
                url: url,
                dataType: "html",
                data: serializedForm,
                cache: false,
                error: function() {
                    console.log("Error loading more");
                },
                success: function(poupHtml) {
                    console.log("Success loading more");
                    $appendContainer.append(poupHtml);
                    setTimeout(function() {
                        $appendContainer.children(".jast-loaded").removeClass("jast-loaded");
                        $button.removeClass("loading");
                        if ($showedLabel.length && getCount) {
                            $showedLabel.text(getCount());
                        };
                    }, 500);
                }
            });
        };
    });
}

app.bindHightlightAnchors = function() {
    var page = $(".hosp-page"),
        block = $(".js-for-menu"),
        nav = $(".hosp-page-nav"),
        link = nav.find("a[data-target]");
    if (page.length && block.length && nav.length) {
        W.on("scroll", function() {
            $.each(link, function() {
                var self = $(this),
                    target = self.data("target");
                if (W.scrollTop() >= $(target).offset().top - $(".header").outerHeight(true) - $(".hosp-page-menu").outerHeight(true) - 1) {
                    link.removeClass(activeClass);
                    self.addClass(activeClass);
                }
            });
        });
    }
}

app.bindUpdateUnreadMessages = function() {
    var control = $("#unread-messages-count");
    if (control != null) {
        $.ajax({
            url: "/Cabinet/GetUnreadCount",
            type: "POST",
            contentType: false,
            processData: false,
            success: function(result) {
                if (result == 0) {
                    control.hide();
                } else {
                    control.text(result);
                }
            }
        });
    }
}

app.bindScrollTo = function() {
    B.on("click", ".js-scroll-to", function(e) {
        e.preventDefault();
        var self = $(this),
            target = $(self.attr("href") || self.data("target")),
            targetTop;
        if (target.length) {
            targetTop = target.offset().top - $(".header").outerHeight(true) - $(".hosp-page-menu").outerHeight(true) - $(".account-header").outerHeight(true) + 1;
            HB.animate({scrollTop: targetTop}, 1000);
        };
    });
}

app.bindMegaMenu = function() {
    B.on("click", ".js-megamenu-open", function(e) {
        e.preventDefault();
        e.stopPropagation();
        var self = $(this),
            menuId = "#" + self.data("menu");
        $(".megamenu, .js-toggle-menu, .header-menu").removeClass(activeClass);
        $(menuId).addClass(activeClass).children(".megamenu-back").height(D.height());
    });
    B.on("click", ".js-megamenu-close", function(e) {
        e.stopPropagation();
        $(".megamenu").removeClass(activeClass);
    });
}

app.bindMobileMenu = function() {
    B.on("click", ".js-toggle-menu", function() {
        var self = $(this),
            header = $(".header"),
            menu = $(".header-menu");
        if (self.hasClass(activeClass)) {
            self.removeClass(activeClass);
            menu.removeClass(activeClass);
            header.removeClass("z-index");
        } else {
            self.addClass(activeClass);
            menu.addClass(activeClass);
            header.addClass("z-index");
        }
    });
}

app.bindGetScrollWidth = function() {
    var scrollWidth = 0,
        div = document.createElement("div");

    div.style.overflowY = "scroll";
    div.style.width = "50px";
    div.style.height = "50px";
    div.style.visibility = "hidden";

    document.body.appendChild(div);
    scrollWidth = div.offsetWidth - div.clientWidth;
    document.body.removeChild(div);
}

app.bindPopup = function() {
    B.on("click", ".js-show-popup", function() {
        var popId = "#" + $(this).data("pop"),
            scrollCorr = 0;
        POPUP.hide();
        $(popId).show();
        B.addClass("cutted");
    });

    B.on("click", ".popup-close, .popup-modal, .js-close-popup", function() {
        POPUP.hide();
        B.removeClass("cutted");
        var isReload = $(this).data("reload");
        if (isReload == true) {
            location.reload();
        }
    });
}

app.bindCopyToClipboard = function() {
    B.on("click", ".js-copy-clipboard", function() {
        var text = $(this).data("text");
        window.prompt("Для копирования в буфер обмена нажмите Ctrl+C, Enter", text);
    });
}

app.bindStickyAfterScroll = function() {
    W.on("scroll", function(){
        B.toggleClass("scrolled", W.scrollTop() > 0);
    });
}

app.bindAccordeon = function() {
    B.on("click", ".js-accordion-toggle", function(e) {
        e.preventDefault();
        var self = $(this);
        self.closest(".accordion-section").toggleClass(activeClass);
        self.siblings(".accordion-body").slideToggle(300);
    });
    B.on("click", ".js-mob-accordion-btn", function(e) {
        e.preventDefault();
        var self = $(this);
        self.toggleClass(activeClass);
        self.siblings(".js-mob-accordion-body").slideToggle();
    });
}

app.bindTabs = function() {
    $(".js-tabs-head").tabsInit();
    $(".js-tabs-userdata").tabsInit();
    $(".js-content-tabs").tabsInit();
    //$(".js-order-tabs").tabsInit();
    $(".js-personal-tabs").tabsInit();
    $(".js-tabs-registr").tabsInit();
    $(".js-tabs-citymap").tabsInit();
    $(".js-tabs-pre-registr").tabsInit();
}

app.bindScrollToTop = function() {
    $(".js-scroll-top").scrollToTop();
}

app.bindSpoiler = function() {
    $(".js-spoiler-clinic").spoilerInit(240);
    $(".js-spoiler-testimonials").spoilerInit(72);
}

app.bindRefreshMyFiles = function() {
    $.ajax({
        url: "/Cabinet/GetFiles",
        type: "POST",
        contentType: false,
        processData: false,
        beforeSend: function() {
            $("#user-files-list").html(preloader);
        },
        success: function(result) {
            $("#user-files-list").html(result);
        },
        error: function(result) {
            $("#user-files-list").html(result.responseText);
        }
    });
}

app.bindHidePreloader = function() {
    var preloader = $(".preloader");
    if (preloader.length) {
        setTimeout(function() {
            preloader.addClass("off");
        }, 300);
    }
}

app.bindAddCustomClassToMegaMenu = function() {
    var hosp = $(".hosp-page-menu"),
        menu = $(".megamenu");
    if (hosp.length) {
        menu.addClass("custom");
    }
}

app.bindSaveMyPage = function() {
    /*------------ Сохранение Моя страница Таб1 ----------------------------*/
    $("#formChildTab1").on("submit", function(e) {
        e.preventDefault();
        $("#error-tab1").hide();
        var form = $('#formChildTab1');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveTab1",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $('#uploadprogress').html(preloader);
                },
                success: function(result) {
                    if (result == "1") {
                        $("#error-tab1").removeClass("form-errors");
                        $("#error-tab1").show().text("Сохранено.");
                    } else {
                        $("#error-tab1").addClass("form-errors");
                        $("#error-tab1").show().text("Ошибка при сохранении данных, проверьте данные и попробуйте снова.");
                    }
                    $('#uploadprogress').html("");
                },
                error: function(result) {
                    $('#uploadprogress').html("");
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Моя страница Таб2 ----------------------------*/
    $("#formChildTab2").on("submit", function(e) {
        e.preventDefault();
        $("#error-tab2").hide();
        var form = $("#formChildTab2");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveTab2",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#uploadprogress2").html(preloader);
                },
                success: function(result) {
                    if (result == "1") {
                        $("#error-tab2").removeClass("form-errors");
                        $("#error-tab2").show().text("Данные сохранены.");
                    } else {
                        $("#error-tab2").addClass("form-errors");
                        $("#error-tab2").show().text("Ошибка при сохранении данных, проверьте данные и попробуйте снова.");
                    }
                    $("#uploadprogress2").html("");
                },
                error: function(result) {
                    $("#uploadprogress2").html("");
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Моя страница Таб3 ----------------------------*/
    $("#formChildTab3").on("submit", function(e) {
        e.preventDefault();
        $("#error-tab3").hide();
        var form = $('#formChildTab3');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveTab3",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $('#uploadprogress3').html(preloader);
                },
                success: function(result) {
                    if (result == "1") {
                        $("#error-tab3").removeClass("form-errors");
                        $("#error-tab3").show().text("Сохранено.");
                    } else {
                        $("#error-tab3").addClass("form-errors");
                        $("#error-tab3").show().text("Ошибка при сохранении данных, проверьте данные и попробуйте снова.");
                    }
                    $('#uploadprogress3').html("");
                },
                error: function(result) {
                    $('#uploadprogress3').html("");
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Моя страница Таб4 ----------------------------*/
    $("#formChildTab4").on("submit", function(e) {
        e.preventDefault();
        $("#error-tab4").hide();
        var form = $("#formChildTab4");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveTab4",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#uploadprogress4").html(preloader);
                },
                success: function(result) {
                    if (result == "1") {
                        $("#error-tab4").removeClass("form-errors");
                        $("#error-tab4").show().text("Сохранено.");
                    } else {
                        $("#error-tab4").addClass("form-errors");
                        $("#error-tab4").show().text("Ошибка при сохранении данных, проверьте данные и попробуйте снова.");
                    }
                    $('#uploadprogress4').html("");
                },
                error: function(result) {
                    $("#uploadprogress4").html("");
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindLogin = function() {
    $("#formLogin").on("submit", function(e) {
        e.preventDefault();
        $("#error-login").hide();
        var form = $('#formLogin');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/Login",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#loginprogress").html(preloader);
                },
                success: function(result) {
                    if (result == "1") {
                        $("#loginname").parent().removeClass("has-error");
                        $("#password").parent().removeClass("has-error");
                        $("#loginname").val("");
                        $("#password").val("");
                        $("#error-login").hide();
                        location.href = $("#RedirectTo").val();
                    } else {
                        $("#loginname").parent().addClass("has-error");
                        $("#password").parent().addClass("has-error");
                        $("#loginprogress").html("");
                        if (result == "-1") {
                            $("#error-login").show().text("Ваш логин временно блокирован. Было превышено максимальное число попыток входа с неверным паролем. Это сделано в целях исключения подбора Вашего пароля. Администратор уже осведомлён о данном факте и в ближайшее время проблема будет решена.");
                        } else if (result == "-2") {
                            $("#error-login").show().text("Вами не подтверждена электронная почта. Пройдите на свой электронный ящик для подтверждение email-адреса");
                        } else {
                            $("#error-login").show().text("Неверное имя пользователя или пароль.");
                        }
                        
                    }
                },
                error: function(result) {
                    $("#loginprogress").html("");
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindRegistration = function() {
    $("#formRegister").on("submit", function(e) {
        e.preventDefault();
        var ageeCheck = $("#agreecheck");
        if (ageeCheck) {
            if (!ageeCheck.is(":checked")) {
                $("#loginname").parent().addClass("has-error");
                $("#error-register").show().text("Уважаемый посетитель, будьте внимательны. Регистрация не может быть продолжена без получения Вашего согласия на обработку Ваших персональных данных.");
                return;
            }
        }
        $("#error-register").hide();
        var form = $("#formRegister");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/Register",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#registerprogress").html(preloader);
                },
                success: function(result) {
                    $("#registerprogress").html("");
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
                        $("#error-register").show().text("Данный email уже используется в системе, воспульзуйтесь восстановлением пароля.");
                    } else if (result == "-2") {
                        $("#loginname").parent().addClass("has-error");
                        $("#error-register").show().text("Не подтвержден email, проверьте почту и пройдите по ссылке, указанной в письме о регистрации, затем повторите попытку входа.");
                    } else {
                        $("#loginname").parent().addClass("has-error");
                        $("#error-register").show().text("Данный логин уже зарегистрирован, воспульзуйтесь восстановлением пароля");
                    }
                },
                error: function(result) {
                    $("#registerprogress").html("");
                    alert(result.responseText);
                }
            });
        }
        
    });
}

app.bindRecovery = function() {
    $("#formRecovery").on("submit", function(e) {
        e.preventDefault();
        $("#error-recovery").hide();
        var form = $("#formRecovery");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/Recovery",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#remindinp").parent().removeClass("has-error");
                        $("#remindinp").val("");
                        $("#error-recovery").hide();
                        $(form).hide().siblings(".js-submit-ok").show();
                    } else if (result == "-1") {
                        $("#remindinp").parent().addClass("has-error");
                        $("#error-recovery").show().text("Вам уже отправлено письмо с паролем. Повторная отправка возможна через минуту.");
                    } else {
                        $("#remindinp").parent().addClass("has-error");
                        $("#error-recovery").show().text("Пользователя с таким именем или адресом электронной почты в системе не найдено.");
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindChangePassword = function() {
    $("#formChangePass").on("submit", function(e) {
        e.preventDefault();
        $("#error-passchange").hide();
        var form = $("#formChangePass");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Login/ChangePass",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#currentpass").parent().removeClass("has-error");
                        $("#regpass").parent().removeClass("has-error");
                        $("#passtwice").parent().removeClass("has-error");
                        $("#currentpass").val("");
                        $("#regpass").val("");
                        $("#passtwice").val("");
                        $("#error-passchange").hide();
                        $("#error-passchange").show().text("Пароль успешно изменён. Окно закроется автоматически.");
                        setTimeout(function() { POPUP.hide(); }, 3000);
                    } else {
                        $("#currentpass").parent().addClass("has-error");
                        $("#error-passchange").show().text("Неверный пароль");
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindChangeEmail = function() {
    $("#formChangeEmail").on("submit", function(e) {
        e.preventDefault();
        $("#error-emailchange").hide();
        var form = $("#formChangeEmail");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/ChangeEmail",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#email").parent().removeClass("has-error");
                        $("#email").val("");
                        $("#error-emailchange").hide();
                        $("#error-emailchange").show().text("Email успешно изменён. Окно закроется автоматически.");
                        setTimeout(function() {
                            POPUP.hide();
                            location.reload();
                        }, 3000);
                    } else {
                        $("#currentpass").parent().addClass("has-error");
                        $("#error-emailchange").show().text("Ошибка сохранения email.");
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindChangePhone = function() {
    $("#formChangePhone").on("submit", function(e) {
        e.preventDefault();
        $("#error-phonechange").hide();
        var form = $("#formChangePhone");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/ChangePhone",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#phone").parent().removeClass("has-error");
                        $("#phone").val("");
                        $("#error-phonechange").hide();
                        $("#error-phonechange").show().text("Телефон успешно изменён. Окно закроется автоматически.");
                        setTimeout(function() {
                            POPUP.hide();
                            location.reload();
                        }, 3000);
                    } else {
                        $("#phone").parent().addClass("has-error");
                        $("#error-phonechange").show().text("Ошибка сохранения телефона.");
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindSaveOrder = function() {
    /*------------ Сохранение Заявки Шаг1 ----------------------------*/
    B.on("click", "#orderstep1next", function(e) {
        e.preventDefault();
        $("#error-step1").hide();
        var form = $("#formOrderStep1");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveStep1",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#error-step1").removeClass("form-errors");
                        $("#error-step1").show().text("Сохранено.");
                        $("#order-step-2").load('/Cabinet/OrderStep2Partial');
                        $("#order-step-3").load('/Cabinet/OrderStep3Partial');
                        $("#order-step-4").load('/Cabinet/OrderStep4Partial');
                        app.bindSetOrderStep(2);
                        $(".forms-radiotwix > .forms-radio input:checked").each(function() {
                            $(this).parent(".forms-radio").addClass(activeClass).siblings(".forms-radio").removeClass(activeClass);
                        });
                        $(".forms-radiotwix > .forms-radio").on("click", function() {
                            $(this).addClass(activeClass).siblings(".forms-radio").removeClass(activeClass);
                        });
                        setTimeout(function() {
                            // Если один инпут:
                            $(".js-datepicker").datepicker({
                                dateFormat: "dd.mm.yyyy",
                                language: "ru",
                                autoclose: true,
                                multidate: false
                            });
                        }, 2000);
                    } else if (result == "0") {
                        window.location.href = redirectURL;
                    } else {
                        $("#error-step1").addClass("form-errors");
                        $("#error-step1").show().text(result);
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Заявки Шаг2 ----------------------------*/
    B.on("click", "#orderstep2next", function(e) {
        e.preventDefault();
        $("#error-step2").hide();
        var form = $('#formOrderStep2');
        $("#formOrderStep2").data("validator", null);
        $.validator.unobtrusive.parse('#formOrderStep2');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveStep2",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#error-step2").removeClass("form-errors");
                        $("#error-step2").show().text("Сохранено.");
                        $("#order-step-3").load('/Cabinet/OrderStep3Partial');
                        app.bindSetOrderStep(3);
                        $(".forms-radiotwix > .forms-radio input:checked").each(function() {
                            $(this).parent(".forms-radio").addClass(activeClass).siblings(".forms-radio").removeClass(activeClass);
                        });
                        $(".forms-radiotwix > .forms-radio").on("click", function() {
                            $(this).addClass(activeClass).siblings(".forms-radio").removeClass(activeClass);
                        });
                    } else if (result == "0") {
                        window.location.href = redirectURL;
                    } else {
                        $("#error-step2").addClass("form-errors");
                        $("#error-step2").show().text(result);
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Заявки Шаг3 ----------------------------*/
    B.on("click", "#orderstep3next", function(e) {
        e.preventDefault();
        $("#error-step3").hide();
        var form = $("#formOrderStep3");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveStep3",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#error-step3").removeClass("form-errors");
                        $("#error-step3").show().text("Сохранено.");
                        $("#order-step-4").load('/Cabinet/OrderStep4Partial');
                        app.bindSetOrderStep(4);
                    } else if (result == "0") {
                        window.location.href = redirectURL;
                    } else {
                        $("#error-step3").addClass("form-errors");
                        $("#error-step3").show().text(result);
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Заявки Шаг4 ----------------------------*/
    B.on("click", "#orderstep4next", function(e) {
        e.preventDefault();
        $("#error-step4").hide();
        var form = $('#formOrderStep4');
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Cabinet/SaveStep4",
                type: "POST",
                data: serializedForm,
                success: function(result) {
                    if (result == "1") {
                        $("#error-step4").removeClass("form-errors");
                        $("#error-step4").show().text("Заявка отправлена.");
                        app.bindSetOrderStep(5);
                    } else if (result == "0") {
                        window.location.href = redirectURL;
                    } else {
                        $("#error-step4").addClass("form-errors");
                        $("#error-step4").show().text(result);
                    }
                },
                error: function(result) {
                    alert(result.responseText);
                }
            });
        }
    });

    /*------------ Сохранение Заявки Шаг5 ----------------------------*/
    B.on("click", "#orderstep5next", function(e) {
        e.preventDefault();
        $("#error-step5").hide();
        $("#orderstep5next").hide();
        $("#orderstep5back").hide();
        $("#orderstep5save").hide();
        $.ajax({
            url: "/Cabinet/SaveStep5",
            type: "POST",
            success: function(result) {
                if (result == "1") {
                    $("#order-current-tab").load('/Cabinet/OrdersPartial');
                    $("#error-step5").removeClass("form-errors");
                    $("#error-step5").show().text("Заявка успешно отправлена.");
                    setTimeout(function() {
                        app.bindSetOrderTab("order-current");
                        $(".js-tabs-link").hide();
                    }, 3000);
                } else if (result == "0") {
                    window.location.href = redirectURL;
                } else {
                    $("#error-step5").addClass("form-errors");
                    $("#error-step5").show().text(result);
                }
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });
}

app.bindDeferOrder = function() {
    /*------------ Отложить заполнение Заявки Шаг1 ----------------------------*/
    B.on("click", "#orderstep1save", function(e) {
        e.preventDefault();
        $("#error-step1").hide();
        var form = $("#formOrderStep1");
        var serializedForm = form.serialize();
        $.ajax({
            url: "/Cabinet/PendStep1",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                if (result == "1") {
                    $("#error-step1").removeClass("form-errors");
                    $("#error-step1").show().text("Данные сохранены. Вы можете вернуться к заполнению в любое время.");
                } else if (result == "0") {
                    window.location.href = redirectURL;
                } else {
                    $("#error-step1").addClass("form-errors");
                    $("#error-step1").show().text(result);
                }
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });

    /*------------ Отложить заполнение Заявки Шаг2 ----------------------------*/
    B.on("click", "#orderstep2save", function(e) {
        e.preventDefault();
        $("#error-step2").hide();
        var form = $("#formOrderStep2");
        var serializedForm = form.serialize();
        $.ajax({
            url: "/Cabinet/PendStep2",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                if (result == "1") {
                    $("#error-step2").removeClass("form-errors");
                    $("#error-step2").show().text("Данные сохранены. Вы можете вернуться к заполнению в любое время.");
                } else if (result == "0") {
                    window.location.href = redirectURL;
                } else {
                    $("#error-step2").addClass("form-errors");
                    $("#error-step2").show().text(result);
                }
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });

    /*------------ Отложить заполнение Заявки Шаг3 ----------------------------*/
    B.on("click", "#orderstep3save", function(e) {
        e.preventDefault();
        $("#error-step3").hide();
        var form = $("#formOrderStep3");
        var serializedForm = form.serialize();
        $.ajax({
            url: "/Cabinet/PendStep3",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                if (result == "1") {
                    $("#error-step3").removeClass("form-errors");
                    $("#error-step3").show().text("Данные сохранены. Вы можете вернуться к заполнению в любое время.");
                } else if (result == "0") {
                    window.location.href = redirectURL;
                } else {
                    $("#error-step3").addClass("form-errors");
                    $("#error-step3").show().text(result);
                }
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });

    /*------------ Отложить заполнение Заявки Шаг4 ----------------------------*/
    B.on("click", "#orderstep4save", function(e) {
        e.preventDefault();
        $("#error-step4").hide();
        var form = $("#formOrderStep4");
        var serializedForm = form.serialize();
        $.ajax({
            url: "/Cabinet/PendStep4",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                if (result == "1") {
                    $("#error-step4").removeClass("form-errors");
                    $("#error-step4").show().text("Данные сохранены. Вы можете вернуться к заполнению в любое время.");
                } else if (result == "0") {
                    window.location.href = redirectURL;
                } else {
                    $("#error-step4").addClass("form-errors");
                    $("#error-step4").show().text(result);
                }
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });

    /*------------ Отложить заполнение Заявки Шаг5 ----------------------------*/
    B.on("click", "#orderstep5save", function(e) {
        e.preventDefault();
        $("#error-step5").hide();
        $.ajax({
            url: "/Cabinet/PendStep5",
            type: "POST",
            success: function(result) {
                if (result == "1") {
                    $("#error-step5").removeClass("form-errors");
                    $("#error-step5").show().text("Данные сохранены. Вы можете вернуться к заполнению в любое время.");
                } else if (result == "0") {
                    window.location.href = redirectURL;
                } else {
                    $("#error-step5").addClass("form-errors");
                    $("#error-step5").show().text(result);
                }
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });
}

app.bindAddReview = function() {
    $("#formMensionAdd").on("submit", function(e) {
        e.preventDefault();
        $("#error-mensionadd").hide();
        var form = $("#formMensionAdd");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Mension/AddNew",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#addmensionprogress").html(preloader);
                },
                success: function(result) {
                    if (result == "1") {
                        $("#error-mensionadd").hide();
                        $(form).hide().siblings(".js-submit-ok").show();
                        setTimeout(function() {
                            $("#text").val("");
                            $("#rest-symbols").text("0");
                            $(form).show().siblings(".js-submit-ok").hide();
                        }, 3000);
                    } else {
                        $("#error-mensionadd").show().text("Ошибка сохранения отзыва.");
                    }
                    $("#addmensionprogress").html("");
                },
                error: function(result) {
                    $("#addmensionprogress").html("");
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindAddFile = function() {
    B.on("change", "#fileFilesUpload", function (e) {
        e.preventDefault();
        var files = e.target.files;
        if (files.length) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: "/Cabinet/UploadMyFile",
                    contentType: false,
                    processData: false,
                    data: data,
                    beforeSend: function() {
                        $("#filesfileprogress").html(preloader);
                    },
                    success: function(result) {
                        console.log("Upload success.");
                        $("#filesfileprogress").html("Файл загружен.");
                        RefreshMyFiles();
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                        $("#filesfileprogress").html(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });
}

app.bindRemoveFile = function() {
    B.on("click", ".js-delete-myfile", function(e) {
        e.preventDefault();
        var filename = $(this).data("filename"),
            data = new FormData();
        data.append("filename", filename);
        $.ajax({
            url: "/Cabinet/DeleteMyFile",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function() {
                $("#user-files-list").html(preloader);
            },
            success: function(result) {
                $("#user-files-list").html(result);
            },
            error: function(result) {
                $("#user-files-list").html(result.responseText);
            }
        });
    });
}

app.bindAddUserPic = function() {
    B.on("change", "#fileUserpicUpload", function(e) {
        e.preventDefault();
        var files = e.target.files;
        if (files.length) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: '/Cabinet/UploadUserpic',
                    contentType: false,
                    processData: false,
                    data: data,
                    beforeSend: function() {
                        $("#uploadprogress").html(preloader);
                    },
                    success: function(result) {
                        $("#error-settings").removeClass("form-errors");
                        console.log(result);
                        $("#uploadprogress").html("");
                        location.reload();
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                        $("#uploadprogress").html("");
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });
}

app.bindRemoveUserPic = function() {
    B.on("click", ".userpic-del", function() {
        $.ajax({
            type: "POST",
            url: "/Cabinet/RemoveUserpic",
            datatype: "text",
            success: function(result) {
                console.log(result);
                location.reload();
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });
    });
}

app.bindAddFileToOrder = function() {
    B.on("change", "#fileOrderUpload", function(e) {
        e.preventDefault();
        var files = e.target.files;
        if (files.length) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: "/Cabinet/UploadOrderFile",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function(result) {
                        $(".order-files-list").html(result);
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });
}

app.bindRemoveFileFromOrder = function() {
    B.on("click", ".file-del", function(e) {
        e.preventDefault();
        var filename = $(this).data("file");
        $.ajax({
            type: "POST",
            url: "/Cabinet/DeleteOrderFile",
            datatype: "text",
            data: { "filename": filename },
            success: function(result) {
                $(".order-files-list").html(result);
                console.log(result);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });
    });
}

app.bindAddFeedback = function() {
    $("#formFeedback").on("submit", function(e) {
        e.preventDefault();
        var ageeCheck = $("#feedbackagreecheck");
        if (ageeCheck) {
            if (!ageeCheck.is(":checked")) {
                $("#error-feedback").show().text("Уважаемый посетитель, будьте внимательны. Сообщение не может быть отправлено без получения Вашего согласия на обработку Ваших персональных данных.");
                return;
            }
        }
        $("#error-feedback").hide();
        var form = $("#formFeedback");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Feedback/AddFeedback",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#feedbackprogress").html(preloader);
                },
                success: function(result) {
                    $("#feedbackprogress").html("");
                    if (result == "1") {
                        $("#error-feedback").removeClass("form-errors");
                        $("#error-feedback").show().text("Сообщение отправлено.");
                        $("#name").val("");
                        $("#mail").val("");
                        $("#phone").val("");
                        $("#text").val("");
                    } else {
                        $("#error-feedback").addClass("form-errors");
                        $("#error-feedback").show().text("Ошибка сохранения сообщения.");
                    }
                },
                error: function(result) {
                    $("#feedbackprogress").html("");
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindAddToRSS = function() {
    /*------------ Добавление в список рассылки ----------------------------*/
    $("#formNewsletter").on("submit", function(e) {
        e.preventDefault();
        $("#error-newsletter").hide();
        var form = $("#formNewsletter");
        if (form.valid()) {
            var serializedForm = form.serialize();
            $.ajax({
                url: "/Newsletter/AddNew",
                type: "POST",
                data: serializedForm,
                beforeSend: function() {
                    $("#newsletterprogress").html(preloader);
                },
                success: function(result) {
                    $("#newsletterprogress").html("");
                    if (result == "1") {
                        $("#error-newsletter").removeClass("form-errors");
                        $("#error-newsletter").show().text("Рассылка подключена.");
                        $("#email").val("");
                    } else if (result == "-1") {
                        $("#error-newsletter").addClass("form-errors");
                        $("#error-newsletter").show().text("Данный e-mail уже подписан. Спасибо.");
                    } else {
                        $("#error-newsletter").addClass("form-errors");
                        $("#error-newsletter").show().text("Ошибка подключения.");
                    }
                },
                error: function(result) {
                    $("#newsletterprogress").html("");
                    alert(result.responseText);
                }
            });
        }
    });
}

app.bindSaveAvatar = function() {
    B.on("change", "#fileUpload", function (e) {
        e.preventDefault();
        var files = e.target.files;
        if (files.length) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: "/Cabinet/UploadAva",
                    contentType: false,
                    processData: false,
                    data: data,
                    beforeSend: function() {
                        $("#uploadprogress").html(preloader);
                    },
                    success: function(result) {
                        $("#error-tab1").removeClass("form-errors");
                        $("#error-tab1").show().text("Файл загружен.");
                        console.log("Upload success.");
                        $("#uploadprogress").html("");
                        var img = $("#mainphoto");
                        img.attr("src", result);
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                        $('#uploadprogress').html("");
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });
}

app.bindFilterReviews = function() {
    B.on("change", "#mensionfilter", function(e) {
        e.preventDefault();
        $("#skiprecords").val(0);
        var form = $("#mensionform");
        var serializedForm = form.serialize();
        $.ajax({
            url: "/Mension/More",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                $("#js-for-load-testimonials").html(result);
                $(".js-spoiler-testimonials").spoilerInit(72);
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });
}

app.bindFilterFAQ = function() {
    B.on("change", "#faqsubject", function() {
        var self = $(this),
            filter = self.val();
        $(".accordion-section").hide();
        if (filter == 0) {
            $(".accordion-section").show();
        } else {
            var answers = $(".accordion-section[data-val=" + filter + "]");
            answers.show();
        }
    });
}

app.bindAddFileToGallery = function() {
    B.on("change", "#fileGaleryUpload", function (e) {
        e.preventDefault();
        var files = e.target.files;
        if (files.length) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: "/Cabinet/UploadPhoto",
                    contentType: false,
                    processData: false,
                    data: data,
                    beforeSend: function() {
                        $("#uploadgaleryprogress").html(preloader);
                    },
                    success: function(result) {
                        $("#galerytales").html(result);
                        $("#uploadgaleryprogress").html("");
                        console.log(result);
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                        $("#uploadgaleryprogress").html("");
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });
}

app.bindAddFileToDocuments = function() {
    B.on("change", "#fileDocumentsUpload", function(e) {
        e.preventDefault();
        var files = e.target.files;
        if (files.length) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: "/Cabinet/UploadDoc",
                    contentType: false,
                    processData: false,
                    data: data,
                    beforeSend: function() {
                        $("#uploaddocprogress").html(preloader);
                    },
                    success: function(result) {
                        $("#documentstales").html(result);
                        $("#uploaddocprogress").html("");
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                        $("#uploaddocprogress").html("");
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });
}

app.bindAddPost = function() {
    B.on("click", ".post-add", function(e) {
        e.preventDefault();
        $("#error-postadd").hide();
        var self = $(this),
            answerForPost = self.data("postid"),
            text = self.parent().children('#newtext'),
            data = new FormData();
        data.append("answerForPost", answerForPost);
        data.append("text", text.val());
        $.ajax({
            url: "/Post/NewPost",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function() {
                $("#addpostprogress").html(preloader);
            },
            success: function(result) {
                $("#error-postadd").hide();
                $("#addpostprogress").html("");
                $("#js-for-load-feelings").html(result);
                text.val('');
            },
            error: function(result) {
                $("#addpostprogress").html("");
                alert(result.responseText);
            }
        });
    });
}

app.bindRemovePost = function() {
    B.on("click", ".post-btn-delete", function(e) {
        e.preventDefault();
        var self = $(this),
            post = self.data("postid"),
            data = new FormData();
        data.append("post", post);
        $.ajax({
            url: "/Post/DeletePost",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function() {
                $("#postprogress-" + post).html(preloader);
            },
            success: function(result) {
                $("#postprogress-" + post).html("");
                $("#js-for-load-feelings").html(result);
            },
            error: function(result) {
                $("#postprogress-" + post).html("");
                alert(result.responseText);
            }
        });
    });
}

app.bindEditPost = function() {
    B.on("click", ".post-edit", function(e) {
        e.preventDefault();
        var self = $(this),
            post = self.data("postid"),
            memotext = $("#editmemo-" + post),
            data = new FormData();
        data.append("text", memotext.val());
        data.append("postid", post);
        $.ajax({
            url: "/Post/UpdatePost",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function() {
                $("#postprogress-" + post).html(preloader);
            },
            success: function(result) {
                $("#postprogress-" + post).html("");
                $("#js-for-load-feelings").html(result);
            },
            error: function(result) {
                $("#postprogress-" + post).html("");
                alert(result.responseText);
            }
        });
    });
}

app.bindToggleAssist = function() {
    B.on("click", ".js-assist-show", function(e) {
        e.preventDefault();
        var self = $(this);
        self.siblings(".assist-drop").toggleClass(activeClass);
    });
    B.on("click", ".js-assist-close", function(e) {
        e.preventDefault();
        var self = $(this);
        self.closest(".assist-drop").toggleClass(activeClass);
    });
}

app.bindShowCharactersLeft = function() {
    B.on("keyup", ".js-symbols-couneter", function(e) {
        e.preventDefault();
        var self = $(this),
            value = self.val(),
            entered = $("#rest-symbols");
        entered.text(value.length);
    });
}

app.bindOpenComments = function() {
    B.on("click", ".js-comments-open", function() {
        var self = $(this),
            commentsItem = self.closest(".post-comments");
        $("#text").val("");
        commentsItem.addClass("is-open");
    });
}

app.bindOperationsWithDocuments = function() {
    $("#formDocuments").on("submit", function(e) {
        e.preventDefault();
        var form = $("#formDocuments"),
            serializedForm = form.serialize();
        $.ajax({
            url: "/Cabinet/DocumentsAction",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                $("#doctodelete").val("");
                $("#doctohideuphide").val("");
                $("#documentstales").html(result);
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });
}

app.bindOperationsWithGallery = function() {
    $("#formGalery").on("submit", function(e) {
        e.preventDefault();
        var form = $("#formGalery"),
            serializedForm = form.serialize();
        $.ajax({
            url: "/Cabinet/GaleryAction",
            type: "POST",
            data: serializedForm,
            success: function(result) {
                $("#pictodelete").val("");
                $("#pictotop").val("");
                $("#pictohideuphide").val("");
                $("#galerytales").html(result);
            },
            error: function(result) {
                alert(result.responseText);
            }
        });
    });
}

app.bindGoBackOneStep = function() {
    B.on("click", ".btn-back", function(e) {
        e.preventDefault();
        var self = $(this),
            stepId = self.data("move-to");
        if (stepId > 0) {
            $("#error-step" + stepId).hide();
            app.bindSetOrderStep(stepId);
        }
    });
}

app.bindImagesTileToogle = function() {
    B.on("click", ".js-images-tile-toggle", function(e) {
        e.preventDefault();
        var self = $(this),
            tileItem = self.closest(".images-tile-item");
        if (tileItem.hasClass("is-enabled")) {
            self.removeClass("is-enabled").addClass("is-disabled");
            tileItem.removeClass("is-enabled").addClass("is-disabled");
        } else {
            self.removeClass("is-disabled").addClass("is-enabled");
            tileItem.removeClass("is-disabled").addClass("is-enabled");
        };
    });
}

app.bindEnableEditMode = function() {
    B.on("click", ".js-enable-edit", function(e) {
        e.preventDefault();
        var self = $(this),
            popId = "#" + self.data("pop"),
            scrollCorr = 0;
        alert(popId);
    });
}

app.bindEnableEditPost = function() {
    B.on("click", ".post-btn-edit", function(e) {
        e.preventDefault();
        var self = $(this),
            post = self.data("postid"),
            editdiv = $("#editblock-" + post),
            textpost = $("#textpost-" + post),
            memotext = $("#editmemo-" + post);
        memotext.val(textpost.html());
        textpost.slideUp();
        editdiv.slideDown();
    });
}

app.bindCancelEditPost = function() {
    B.on("click", ".post-cancel", function(e) {
        e.preventDefault();
        var self = $(this),
            post = self.data("postid"),
            editdiv = $("#editblock-" + post),
            textpost = $("#textpost-" + post);
        textpost.slideDown();
        editdiv.slideUp();
    });
}

app.bindInsertUserNameWhenAnswering = function() {
    B.on("click", ".comments-reply", function(e) {
        e.preventDefault();
        var self = $(this),
            username = self.data("username");
        self.parents(".post-comments-body").find("#newtext").val(username + ", ").focus();
    });
}

app.bindShiftChat = function() {
    B.on("click", ".js-chat-shift", function(e) {
        e.preventDefault();
        $(".chat-wrap").toggleClass("is-shifted");
    });
}

app.bindIncrement = function() {
    B.on("click", ".js-counter .btn-upp", function(e) {
        e.preventDefault();
        var self = $(this),
            input = self.siblings(".counter-rez");
        //input.val(parseInt(input.val()) + 1).change();
        counterMax = parseInt(self.parent(".js-counter").data("max")) || 99,
        inputVal = parseInt(input.val());
        if (counterMax > 0 && inputVal < counterMax) {
            input.val(inputVal + 1).change();
        };
    });
}

app.bindDecrement = function() {
    B.on("click", ".js-counter .btn-dwn", function(e) {
        e.preventDefault();
        var self = $(this),
            input = self.siblings(".counter-rez"),
            count = parseInt(input.val()) - 1;
        count = count < 1 ? 1 : count;
        input.val(count).change();
    });
}

app.bindDownloadFile = function() {
    B.on("click", ".js-download-myfile", function(e) {
        e.preventDefault();
        var self = $(this),
            fullname = self.data("filename"),
            mimetype = self.data("filetype"),
            filename = self.text(),
            guidId = $("#orderfilesfilter").val(),
            data = new FormData();
       data.append("filename", fullname);
       data.append("guidId", guidId);
       $.ajax({
           url: "/Cabinet/DownloadFile",
           type: "POST",
           data: data,
           contentType: false,
           processData: false,
           success: function(result) {
               var file = new File([result], filename, { type: mimetype });
               saveAs(file, filename);
           }
       });
    });
}

app.bindSetCheckedRadiotwix = function() {
    $(".forms-radiotwix > .forms-radio input:checked").each(function() {
        var self = $(this);
        self.parent(".forms-radio").addClass(activeClass).siblings(".forms-radio").removeClass(activeClass);
    });
    B.on("click", ".forms-radiotwix > .forms-radio", function() {
        var self = $(this);
        //$(".forms-radiotwix > .forms-radio").on("click", function() {
        self.addClass(activeClass).siblings(".forms-radio").removeClass(activeClass);
    });
}

app.bindSelectLogic = function() {
    B.on("click", ".selector div", function(e) {
        e.preventDefault();
        var self = $(this);
        self.closest(".selector").addClass("show-list");
    });
    B.on("mouseleave", ".selector div", function(e) {
        e.preventDefault();
        var self = $(this);
        self.closest(".selector").removeClass("show-list");
    });
    B.on("mouseenter", ".selector ul", function(e) {
        e.preventDefault();
        var self = $(this);
        self.closest(".selector").addClass("show-list");
    });
    B.on("mouseleave", ".selector ul", function(e) {
        e.preventDefault();
        var self = $(this);
        self.closest(".selector").removeClass("show-list");
    });
    B.on("click", ".selector li", function(e) {
        e.preventDefault();
        var self = $(this),
            selDataVal = self.data("val"),
            selDataTxt = self.text();
        self.parents("ul").siblings("div").text(selDataTxt).attr({ "data-val": selDataVal });
        self.parents("ul").siblings("input")
                .val(selDataVal)
                .trigger("change");
        self.closest(".selector").removeClass("show-list");
        //TODO: Нижнее надо только селекторам формы 'form#childrenform'
        $("#skiprecords").val(0);
        $("form#childrenform").submit();
    });

    // Закрытие списка селектора при клике мимо
    B.on("mouseup", function(e) {
        var selectorList = $(".selector.show-list");
        if (e.target !== selectorList[0] && !selectorList.has(e.target).length) {
            selectorList.removeClass("show-list");
        };
    });
}

app.bindShowPassword = function() {  
    B.on("click", ".js-password-trigger", function(e) {
        e.preventDefault();
        var self = $(this),
            password = self.parent().find("input");
        if (self.hasClass(activeClass)) {
            self.attr("title", "Показать пароль");
            self.removeClass(activeClass);
            password.prop("type", "password").focus();
        } else {
            self.addClass(activeClass);
            self.attr("title", "Скрыть пароль");
            password.prop("type", "text").focus();
        }
    });
}

app.bindSetOrderStep = function(index) {
    var tabsHead = $(".js-order-tabs"),
        links = tabsHead.find(".js-tabs-link"),
        tabsBody = tabsHead.siblings(".js-tabs-body"),
        tabs = tabsBody.find(".js-tabs-item");

    links.removeClass(activeClass);
    $("#steps-link-" + index).addClass(activeClass);
    tabs.removeClass(activeClass);
    $("#order-step-" + index).addClass(activeClass);
}

app.bindSetOrderTab = function(index) {
    var tabsHead = $(".js-content-tabs"),
        links = tabsHead.find(".js-tabs-link"),
        tabsBody = tabsHead.siblings(".js-tabs-body"),
        tabs = tabsBody.find(".js-tabs-item");

    links.removeClass(activeClass);
    tabs.removeClass(activeClass);
    $("#" + index + "-tab").addClass(activeClass);
    $("#" + index + "-link").addClass(activeClass);
}
