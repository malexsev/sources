


/*----------- ФУНКЦИИ ПОСЛЕ ГОТОВНОСТИ ---------------------------------------*/

$(document).ready(function() {
  
  /* --- ФУНКЦИИ: Прокрутка страниц наверх --------------------------------*/ 
  $.fn.scrollToTop = function() {
    var scrollLink = $(this);
    scrollLink.hide();
    if ($(window).scrollTop() >= "150") scrollLink.fadeIn("slow");
    $(window).scroll(function() {
        if ($(window).scrollTop() <= "150") scrollLink.fadeOut("slow");
        else scrollLink.fadeIn("slow");
    });
    $(this).click(function() {
        $("html, body").animate({scrollTop: 0}, "slow");
    });
  };
//  Инициализация работы прокрутки страниц наверх
    $(".js-scroll-top").scrollToTop();
 

/* --- Загрузка больше контента -------------------------------------------*/
 function dataAjaxLoad(loadingBtn, loadingPlace, addr, counter, forCount){
  $( loadingBtn ).click(function(){
    var $loadingBtn = $(this),
        $loadingPlace = $(loadingPlace),
        $counter = $(counter),
        $forCount = $(forCount);
        
    if ( $loadingPlace.length ){
      $loadingBtn.addClass("loading");
      $.ajax({
        type: "POST",
        url: addr,
        dataType: "html",
        cache: false,
        error: function(){
          console.log("Error loading more");
        },
        success: function(poupHtml){
          console.log("Success loading more");
          $loadingPlace.append(poupHtml);
          setTimeout(function(){
            $loadingPlace.children(".jast-loaded").removeClass("jast-loaded");
            $loadingBtn.removeClass("loading");
            if ( $counter.length && $forCount.length ){
              $counter.text( $(forCount).length );
            };
          }, 500);
        }
      });
    };
  });
 };
//Загрузка больше фото детей и отзывов
 dataAjaxLoad(".js-load-more-children", "#js-for-load-children", "ajax/more_children.php",
              "#more-count", ".js-ajax-for-count");
//Загрузка больше историй
 dataAjaxLoad(".js-load-more-history", "#js-for-load-history", "ajax/more_history.php");
//Загрузка больше впечатлений
 dataAjaxLoad(".js-load-more-feelings", "#js-for-load-feelings", "ajax/more_feelings.php");

/*----------- ФУНКЦИИ: Работа табов ---------------------------------------*/
  $.fn.tabsInit = function(){
    var $tabsHead = $(this),
        $links = $tabsHead.find(".js-tabs-link"),
        $tabsBody = $tabsHead.siblings(".js-tabs-body"),
        $tabs = $tabsBody.find(".js-tabs-item");
    $links.click(function(){
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


/* --- Замена селектов списком со скрытым полем для отправки данных ------*/
  $(document).on('click', ".selector div", function(){
    $(this).closest(".selector").addClass("show-list");
  });
  $(document).on('mouseleave', ".selector div", function(){
    $(this).closest(".selector").removeClass("show-list");
  });
  $(document).on('mouseenter', ".selector ul", function(){
    $(this).closest(".selector").addClass("show-list");
  });
  $(document).on('mouseleave', ".selector ul", function(){
    $(this).closest(".selector").removeClass("show-list");
  });
  $(document).on('click', ".selector li", function(){
    var selDataVal = $(this).data("val");
    var selDataTxt = $(this).text();
    $(this).parents("ul").siblings("div").text(selDataTxt).attr({"data-val": selDataVal});
    $(this).parents("ul").siblings("input")
            .val(selDataVal)
            .trigger('change'); // Событие изменения в поле
    $(this).closest(".selector").removeClass("show-list");
  });
//Закрытие списка селектора при клике мимо
  $(document).mouseup(function(e){
    var selectorList = $('.selector.show-list');
    if( e.target !== selectorList[0] && !selectorList.has(e.target).length ){
      selectorList.removeClass("show-list");
    };
  });
//  Вертикальная прокрутка в селекторах
//  $(".selector ul").jScrollPane();

/* --- Валидация форм -----------------------------------------------------*/
  $(".js-validate").each(function(){
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
      highlight: function(element, errorClass, validClass) {
        $(element).parent().addClass(errorClass);
      },
      unhighlight: function(element, errorClass, validClass) {
        $(element).parent().removeClass(errorClass);
      },
      submitHandler: function(form) { 
        alert("Submitted!"); 
        //form.submit();
      }
    });
  });
//  Маска ввода в поле телефонного номера
    $("[name='numb']").mask("+7 (999) 999-99-99",{placeholder:"_"});

/*----------- Галлерея картинок  ---------------------------------------------*/
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
 
 
 
  $(".js-toggle-menu").click(function(){
    $(this).toggleClass("active");
    $(".header-top-menu").toggleClass("active");
  });

  $(".js-mob-accordion-btn").click(function(){
    $(this).toggleClass("active");
    $(this).siblings(".js-mob-accordion-body").slideToggle();
  });
 

/*---  Ширина скролла -----------------------------------------------------*/
  var scrollWidth = 0;
  getScroll = function(){
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
  $(".js-show-popup").click(function(){
    var popId = "#" + $(this).data("pop"),
        scrollCorr = 0;
    
    $(popId).show();
    $("body").addClass("cutted");
  });

  $(".popup-close, .popup-modal").click(function(){
    $(".popup").hide();
    $("body").removeClass("cutted");
  });


/*--- Кабинет: отключение/включение фото --------------------------------*/
  $(".js-images-tile-toggle").click(function(){
    var $btn = $(this),
        $tileItem = $btn.closest(".images-tile-item");
    
    if ( $tileItem.hasClass("is-enabled") ){
      $btn.removeClass("is-enabled").addClass("is-disabled");
      $tileItem.removeClass("is-enabled").addClass("is-disabled");
    } else {
      $btn.removeClass("is-disabled").addClass("is-enabled");
      $tileItem.removeClass("is-disabled").addClass("is-enabled");
    };
  });

/*--- Кабинет: показ/скрытие комментариев ------------------------*/
  $(".js-comments-open").click(function(){
    var $btn = $(this),
        $commentsItem = $btn.closest(".post-comments");
        
    $commentsItem.addClass("is-open");
  });

 
 
 
 
 
 
 
 
 

});


/*----------- ФУНКЦИИ ПОСЛЕ ЗАГРУЗКИ -----------------------------------------*/

$(window).load(function(){
	
  setTimeout(function(){
    $(".preloader").addClass("off");
  }, 600);
  

  
});



