

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
        var smileNum = $(this).data("type");
        $("#form-smiles").removeClass("active");
        alert(smileNum);
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
            else
            {
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
        $('#active-contact-id').val($(this).data("username"));
        $('#active-contact-userpic').val($(this).data("userpic"));
        $('.chat-user').removeClass("active");
        $(this).closest().addClass('active');
        UpdateMessages();
    });
    
    $(document).on('click', ".js-chat-user-close", function () {
        alert("Видимо нужно что-то удалить или закрыть");
    });
});

/*----------- ФУНКЦИИ ПОСЛЕ ЗАГРУЗКИ -----------------------------------------*/

$(window).load(function () {
	UpdateContacts();

	//setInterval(function () {
	//	UpdateContacts();
	//}, 30000);
});

/*--- Обновить сообщения ---*/
function UpdateMessages() {
    var username = $("#active-contact-id").val();
    var incoming_userpic = $("#active-contact-userpic").val();
    var my_userpic = $("#my-userpic").attr('src');
    var control = $("#chat-messages");
    if (control != null && username != null) {
        var data = new FormData();
        data.append("contact", username);

        $.ajax({
            url: "/Cabinet/GetMessages",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                $(control).html(result);
                $(".incoming-userpic").attr('src', incoming_userpic);
                $(".my-userpic").attr('src', my_userpic);
            },
            error: function (result) {
                alert(result.responseText);
            }
        });
    }
}

/*--- Обновление списка контактов ---*/
function UpdateContacts() {
	var control = $("#chat-contacts-list");
	if (control != null) {
	    var new_contact = getUrlVars()["contact"];
		var data = new FormData();
		data.append("contact", new_contact);

		$.ajax({
			url: "/Cabinet/GetContacts",
			type: "POST",
			data: data,
			contentType: false,
			processData: false,
			success: function (result) {
			    $(control).html(result);
			},
			error: function (result) {
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