jQuery.fn.extend({
    insertAtCaret: function (myValue) {
        return this.each(function (i) {
            if (document.selection) {
                // Для браузеров типа Internet Explorer
                this.focus();
                var sel = document.selection.createRange();
                sel.text = myValue;
                this.focus();
            }
            else if (this.selectionStart || this.selectionStart == '0') {
                // Для браузеров типа Firefox и других Webkit-ов
                var startPos = this.selectionStart;
                var endPos = this.selectionEnd;
                var scrollTop = this.scrollTop;
                this.value = this.value.substring(0, startPos) + myValue + this.value.substring(endPos, this.value.length);
                this.focus();
                this.selectionStart = startPos + myValue.length;
                this.selectionEnd = startPos + myValue.length;
                this.scrollTop = scrollTop;
            } else {
                this.value += myValue;
                this.focus();
            }
        })
    }
});

/*----------- ФУНКЦИИ ПОСЛЕ ГОТОВНОСТИ ---------------------------------------*/

$(document).ready(function () {
    /* --- Кастомный скролл в чате -------------------------------------------*/
    function chatScrollerInit() {
        $(".js-chat-scroller").nanoScroller({
            paneClass: 'nano-pane',
            sliderClass: 'nano-slider',
            contentClass: 'nano-content'
        });
    }
    chatScrollerInit();

    /* --- Увеличение чата на весь экран -------------------------------------*/
    $(".js-chat-toggle").click(function () {
        $(this).closest(".chat-wrap").toggleClass("is-full-screen");
        $("body").toggleClass("cutted");
        chatScrollerInit();
    });


    /* --- Работа смайликов в чате  ------------------------------------------*/
    $(".js-form-smiles-toggle").click(function () {
        $("#form-smiles").toggleClass("active");
    });

    $("#form-smiles li").click(function () {
        var code = $(this).data("code");
        $("#form-smiles").removeClass("active");
        $(".chat-form-input").insertAtCaret(code);
    });

    $(".chat-form-send-btn").click(function () {
        var text = $(".chat-form-input").val();
        var contact = $('#active-contact-id').val();
        var data = new FormData();
        data.append("text", text);
        data.append("contact", contact);
        $(".chat-form-input").val("");

        if (!text) {
            alert("Введите сообщение.");
        } else {
            if (!contact) {
                alert("Не выбран собеседник.");
            }
            else {
                $.ajax({
                    url: "/Cabinet/SendMessage",
                    type: "POST",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function () {
                        UpdateMessages();
                    },
                    error: function (result) {
                        alert(result.responseText);
                    }
                });
            }
        }
    });

    /* --- Клик по кнопкам  юзера чата ---------------------------------------*/
    $(document).on('click', ".js-chat-user-show", function () {
        SwitchUser($(this));
    });

    $(document).on('click', ".js-chat-user-close", function () {
        alert("Видимо нужно что-то удалить или закрыть");
        SetDefaults();
    });
});

/*----------- ФУНКЦИИ ПОСЛЕ ЗАГРУЗКИ -----------------------------------------*/

$(window).load(function () {
    UpdateContacts();

    setTimeout(function () {
        SetDefaults();
    }, 600);

    //setInterval(function () {
    //	UpdateContacts();
    //}, 30000);
});

function SwitchUser(link) {
    $('#active-contact-id').val($(link).data("username"));
    $('#active-contact-userpic').val($(link).data("userpic"));
    $('.chat-user').removeClass("active");
    $(link).closest('.chat-user').addClass('active');

    $('.chat-head-name').text($(link).data("userdisplay"));
    $('.chat-head-stat').text($(link).data("useronline"));
    UpdateMessages();
}

/*--- Устанавливает значение выбранного пользователя по умолчанию ---*/
function SetDefaults() {
    var contact = getUrlVars()["contact"];
    var elem;
    if (contact != null) {
        elem = $('[data-userdisplay][data-username=' + decodeURIComponent(contact) + ']');
    } else {
        elem = $('[data-userdisplay]')[0];
    }
    if (elem) {
        SwitchUser($(elem));
    }
}

/*--- Обновить сообщения ---*/
function UpdateMessages() {
    var username = $('#active-contact-id').val();
    var incoming_userpic = $('#active-contact-userpic').val();
    var my_userpic = $('#my-userpic').attr('src');


    var control = $('#chat-messages');
    if (control != null && username != null) {
        var data = new FormData();
        data.append("contact", username);

        $.ajax({
            url: "/Cabinet/GetMessages",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $(control).html("<img src='/content/img/preloader.gif' />");
            },
            success: function (result) {
                $(control).html(result);
                $(".incoming-userpic").attr('src', incoming_userpic);
                $(".my-userpic").attr('src', my_userpic);
            },
            error: function (result) {
                $(control).html("");
                alert(result.responseText);
            }
        });
    }
}

/*--- Обновление списка контактов ---*/
function UpdateContacts() {
    var control = $('#chat-contacts-list');
    if (control != null) {
        var contact = getUrlVars()["contact"];
        var data = new FormData();
        data.append("contact", decodeURIComponent(contact));

        $.ajax({
            url: "/Cabinet/GetContacts",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $(control).html("<img src='/content/img/preloader.gif' />");
            },
            success: function (result) {
                $(control).html(result);
            },
            error: function (result) {
                $(control).html("");
                alert(result.responseText);
            }
        });
    }
}

function getUrlVars() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
        vars[key] = value;
    });
    return vars;
}