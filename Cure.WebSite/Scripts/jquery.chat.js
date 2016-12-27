jQuery.fn.extend({
    insertAtCaret: function (myValue) {
        return this.each(function(i) {
            if (document.selection) {
                // Для браузеров типа Internet Explorer
                this.focus();
                var sel = document.selection.createRange();
                sel.text = myValue;
                this.focus();
            } else if (this.selectionStart || this.selectionStart == '0') {
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
        });
    }
});

/* --- Кастомный скролл в чате -------------------------------------------*/
function chatScrollerInitUsers() {
    $(".js-chat-scroller-users").nanoScroller({
        paneClass: 'nano-pane',
        sliderClass: 'nano-slider',
        contentClass: 'nano-content'
    });
}
function chatScrollerInitLog() {
    $(".js-chat-scroller-log").nanoScroller({
        paneClass: 'nano-pane',
        sliderClass: 'nano-slider',
        contentClass: 'nano-content',
        scrollTo: $('#chat-bottom')
    });
}

/*----------- ФУНКЦИИ ПОСЛЕ ГОТОВНОСТИ ---------------------------------------*/

$(document).ready(function () {

    /* --- Увеличение чата на весь экран -------------------------------------*/
    $(".js-chat-toggle").click(function () {
        $(this).closest(".chat-wrap").toggleClass("is-full-screen");
        $("body").toggleClass("cutted");
        chatScrollerInitUsers();
        chatScrollerInitLog();
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
        RemoveConversation($(this));
    });
    
    $(document).on('click', ".js-chat-filter-clear", function () {
        $("._search-input").val("");
        UpdateContacts();
    });

    $(document).on('click', ".js-chat-filter-start", function () {
        UpdateContacts();
    });

    $(document).on('input', "._search-input", function () {
        UpdateContacts();
        $("._search-input").focus();
    });
});

/*----------- ФУНКЦИИ ПОСЛЕ ЗАГРУЗКИ -----------------------------------------*/

$(window).load(function () {
    UpdateContacts();
});

function SwitchUser(link) {
    $('#active-contact-id').val($(link).data("userid"));
    $('#active-contact-userpic').val($(link).data("userpic"));
    $('.chat-user').removeClass("active");
    $(link).closest('.chat-user').addClass('active');

    $('.chat-head-name').text($(link).data("userdisplay"));
    $('.chat-head-stat').text($(link).data("useronline"));
    UpdateMessages();
}

/*--- Устанавливает значение выбранного пользователя по умолчанию ---*/
function Reset() {
    $('#active-contact-id').val("");
    $('#active-contact-userpic').val("");
    $('#chat-messages').html("");
    $('.chat-head-name').text("");
    $('.chat-head-stat').text("");
}

function SetDefaults() {
    var contact = getUrlVars()["contact"];
    var elem;
    if (contact != null) {
        elem = $('[data-userid=' + contact + ']');
    } else {
        elem = $('[data-userid]')[0];
    }
    if (elem) {
        SwitchUser($(elem));
    }
}

function RemoveConversation(link) {
    var userId = $(link).data("userid");

    var data = new FormData();
    data.append("contact", userId);

    $.ajax({
        url: "/Cabinet/RemoveMessages",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                UpdateContacts();
            }
        },
        error: function (result) {
            alert(result.responseText);
        }
    });

    UpdateMessages();
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
                chatScrollerInitLog();
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
    Reset();
    var control = $('#chat-contacts-list');
    var filter = $("._search-input").val();
    if (control != null) {
        var contact = getUrlVars()["contact"];
        var data = new FormData();
        data.append("contact", contact);
        data.append("filter", filter);

        $.ajax({
            url: "/Cabinet/GetContacts",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $(control).html("<img src='/content/img/preloader.gif' />");
                chatScrollerInitUsers();
            },
            success: function (result) {
                $(control).html(result);
                SetDefaults();
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