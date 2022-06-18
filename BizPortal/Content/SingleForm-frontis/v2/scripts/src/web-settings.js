"use strict";
import $ from 'jquery';
import _ from 'lodash';
import 'slick-carousel';
import ScrollMagic from 'scrollmagic';
import 'scrollmagic/scrollmagic/uncompressed/plugins/animation.gsap';
import 'scrollmagic/scrollmagic/uncompressed/plugins/debug.addIndicators.js';
import {TweenMax, TimelineMax, Power2, Power1, Linear} from 'gsap';
var Masonry = require('masonry-layout');
var GoogleMapsLoader = require('google-maps');


$(document).ready(function(){
  var container = document.querySelector('.container-menu');

  // var msnry = new Masonry( '.container-menu .wrap', {
  //   itemSelector: '.row-main',
  //   // columnWidth: 250
  // });

  $('section.menu-mobile').css("display", "block");
  $('section.search-mobile').removeAttr("style");

  //$('.btn-group-sticky').sticky({topSpacing:100});

  var uluru = {lat: 13.7645578, lng: 100.5103048};

    if (document.getElementById('map')) {

      var mapsGoogle;
      GoogleMapsLoader.KEY = 'AIzaSyAAJafSBGcivEpgCA1aJfvC-oQ1KtcPJG0';
      GoogleMapsLoader.load(function(google) {
         new google.maps.Map(document.getElementById('map'), {
          zoom: 16,
          center: new google.maps.LatLng(13.7645578, 100.5103048),
          mapTypeId: 'roadmap',
          //zoomControl: true,
          // zoomControlOptions: {
          //     position: google.maps.ControlPosition.LEFT_CENTER
          // },
          scaleControl: true,
          //streetViewControl: true,
          // streetViewControlOptions: {
          //     position: google.maps.ControlPosition.LEFT_TOP
          // },
          //fullscreenControl: true
        });
      });

      GoogleMapsLoader.onLoad(function(google) {
        console.log('I just loaded google maps api', google);
        google.maps.event.addListener(GoogleMapsLoader,'bounds_changed', updateMapBounds);
        var features = [
          {
            position: new google.maps.LatLng(13.7645578, 100.5103048),
            type: 'info'
          }
        ];
        features.forEach(function(feature) {
          var marker = new google.maps.Marker({
            position: feature.position,
            //icon: iconBase,
            map: GoogleMapsLoader
          });
        });
      });


      function updateMapBounds(){
        console.log('MMA');
        GoogleMapsLoader.setCenter(uluru);
      }
    }



  var maxLevel = 2;
  $(document).scroll(function() {
    if ($(document).scrollTop() > 30){
      $("section.navbar .nav").addClass('fixed');
      $("section.navbar").addClass('fixed');
      $("section.menu").addClass('fixed');
    } else {
      $("section.navbar .nav").removeClass('fixed');
      $("section.navbar").removeClass('fixed');
      $("section.menu").removeClass('fixed');
    }
  });
  $('.business-info .item-info').click(function(e){
    //e.preventDefault();
    console.log('MAMA');
    $(this).toggleClass('active');
  });
  $('.item-info a.button.primary').click(function(e){
    console.log('PRI');
    e.stopPropagation();
  });

  $('.company-slider').slick({
    speed: 500,
    slidesToShow: 4,
    appendArrows: '.carousel-arrows-company',
    prevArrow: '<div class="carousel-arrow carousel-prev"><i class="fa fa-angle-left"></i></div>',
    nextArrow: '<div class="carousel-arrow carousel-next"><i class="fa fa-angle-right"></i> </div>',
    responsive: [
      {
        breakpoint: 968,
        settings: {
          slidesToShow: 3,
        }
      },
      {
        breakpoint: 568,
        settings: {
          slidesToShow: 2,
        }
      },
    ]
  });

  $('.b-slider').slick({
    //centerMode: true,
    speed: 500,
    slidesToShow: 4,
    appendArrows: '.carousel-arrows',
    prevArrow: '<div class="carousel-arrow carousel-prev"><i class="fa fa-angle-left"></i></div>',
    nextArrow: '<div class="carousel-arrow carousel-next"><i class="fa fa-angle-right"></i> </div>',
    responsive: [
      {
        breakpoint: 968,
        settings: {
          slidesToShow: 3,
        }
      },
      {
        breakpoint: 568,
        settings: {
          slidesToShow: 1,
        }
      },
    ]
  });
  
  $("ul.navbar > li > a").click(function(e){
    e.preventDefault();
    $("ul.navbar > li > a").removeClass('active');
    $(this).addClass('active');
    $("section.menu").removeClass('active');
    //menu list special
    $("section.menu-list").removeClass('active');
    $(`section.menu[data-id=${$(this).attr('data-id')}]`).addClass('active');
    //menu list special
    $(`section.menu-list[data-id=${$(this).attr('data-id')}]`).addClass('active');
  });

  $('.login-info .profile').click(function(e){
    e.preventDefault();
    $(this).siblings('.dropdown-content').slideToggle();
    $(this).toggleClass('active');
  });

  // $("ul.navbar > li > a").hover(function(e){
  //     e.preventDefault();

  //     $("ul.navbar > li > a").removeClass('active');
  //     $(this).addClass('active');
  //     $("section.menu").removeClass('active');
  //     $(`section.menu[data-id=${$(this).attr('data-id')}]`).addClass('active');
  // });

  /////////////test animation when click form///////////////////
     $(`input[type="radio"][name="s5"]`).change(function(){
      console.log($(this).val());
        if ($(this).val() == '1') {
          console.log('fafafaf');
          $('.wrap-form.tab.hide').slideToggle();
        }
        else {
          $('.wrap-form.tab.hide').slideUp();
        }
        // if($(this).prop("checked") == true){

        //     //alert("Checkbox is checked.");
        // }
        // else if($(this).prop("checked") == false){
        //     //alert("Checkbox is unchecked.");
        // }
     });

    $(`input[type="radio"][name="quiz-retail-3"]`).change(function(){
      console.log($(this).val());
        if ($(this).val() == 'others') {

          $(`.wrap-form.tab[id="others"]`).slideToggle();
        }
        else {
          $(`.wrap-form.tab[id="others"]`).slideUp();
        }
        // if($(this).prop("checked") == true){

        //     //alert("Checkbox is checked.");
        // }
        // else if($(this).prop("checked") == false){
        //     //alert("Checkbox is unchecked.");
        // }
     });

     $(`input[type="radio"][name="choice"]`).change(function(){
        console.log($(this).val());
        var val = $(this).val();
        // $(`.choice-form[data-id="${val}"]`).slideToggle();
        $(`.choice-form`).slideUp();
        $(`.choice-form[data-id=${val}]`).slideDown();
      });

     $('.form-info').click(function(e){
       e.preventDefault();
       const id = $(this).attr('data-target');

       $(`.wrap-form.info[id=${id}]`).slideToggle();
     });

    $('.wrap-form .close').click(function(e){
       e.preventDefault();
       $(this).parent().slideUp();
      //$('.wrap-form.info').slideToggle();
    });


     $('section.accept-form .item-list.clickable').click(function(e){
       e.preventDefault();
       $(this).toggleClass('active');
     });
  ///////////test animation when click form///////////////////

  ///click dashboard row
  $('.dash-recent .item .row-list.select .select').click(function(e){
    e.preventDefault();
    $(this).siblings('.row-content').slideToggle();
    $(this).parent().toggleClass('active');
  });

  $('.dash-progress .item .head').click(function(e){
    e.preventDefault();
    $(this).siblings('.body').slideToggle();
    $(this).toggleClass('active');
  });

  ///////////////////////

  $("section.menu").mouseleave(function(){
    const data = $(this);
    setTimeout(function(){
        if(!data.is(":hover")){
          data.removeClass('active');
          $(`ul.navbar > li > a[data-id=${data.attr('data-id')}]`).removeClass('active');
        }
     }/*,700*/);
  });

  //menu list special
  $("section.menu-list").mouseleave(function(){
    const data = $(this);
    setTimeout(function(){
        if(!data.is(":hover")){
          data.removeClass('active');
          $(`ul.navbar > li > a[data-id=${data.attr('data-id')}]`).removeClass('active');
        }
     }/*,700*/);
    resetMenulist();
  });

  $("body").mouseup(function(evt){ 
    $("section.menu").removeClass('active');
    if (!isMenuListItem(evt)) {
      $("ul.navbar > li > a").removeClass('active');
    }
  });

  $("body").on('touchend', function(evt){ 
    $("ul.navbar > li > a").removeClass('active');
    $("section.menu").removeClass('active');
  });


  ////search mobile
  $(".search-mobile-button").click(function(e){ ///onclick for display searchbar
    e.preventDefault();
    $("section.mask").addClass("active");
    $("section.search-mobile").addClass("active");
    $("body").addClass("disable-scroll");
  });
  $(".wrap-search a").click(function(e){  ///onclick search button
    e.preventDefault();
    $("section.mask").removeClass("active");
    $("body").removeClass("disable-scroll");
    $("section.search-mobile").removeClass("active");
  });
  ////burger
  $("section.navbar .nav > .burger > a").click(function(e){
    e.preventDefault();
    $("section.mask").addClass("active");
    $("section.menu-mobile").addClass("active");
    $("body").addClass("disable-scroll");
  });
  ///close mobile menu
  $("section.mask").click(function(e){
    e.preventDefault();
    $("section.mask").removeClass("active");
    $("body").removeClass("disable-scroll");
    $("section.menu-mobile").removeClass("active");
    $("section.search-mobile").removeClass("active");
    $("section.menu-mobile > .content > .main").removeClass('disactive');
    $("section.menu-mobile > .content > .menu").removeClass('active');
  });

  ///mobile menu click main menu
  $("section.menu-mobile > .content ul.main > li > a").click(function(e){
    e.preventDefault();
    console.log($(this).attr('data-id'));
    $(`section.menu-mobile > .content > .menu[data-id=${$(this).attr('data-id')}]`).addClass('active');
    $("section.menu-mobile > .content > .main").addClass('disactive');
  });

  ///sub menu click back
  $("section.menu-mobile > .content > .menu >.back > a").click(function(e){
    e.preventDefault();
    $("section.menu-mobile > .content > .main").removeClass('disactive');
    $("section.menu-mobile > .content > .menu").removeClass('active');
  });


  ///progress menu type script
  $("section.menu-list .menu ul > li > a").click(function(e){
    e.preventDefault();
    const currentLevel = $(this).attr('level');
    const targetId = $(this).attr('menu-id');
    $(`section.menu-list .menu[level=${currentLevel}] ul > li > a`).removeClass('active');
    removePreviousLevel(Number(currentLevel), maxLevel);
    $(this).addClass('active');
    if(targetId !== '') {
      $(`section.menu-list .menu[menu-id=${targetId}]`).addClass('active');
    }
  });

  const resetMenulist = () => {
    $(`section.menu-list .menu[level=0] ul > li > a`).removeClass('active');
    removePreviousLevel(0, maxLevel);
  }

  const removePreviousLevel = (level, max) => {
    for (var i = level+1; i <= max; i++) {
      $(`section.menu-list .menu[level=${i}]`).removeClass('active');
      $(`section.menu-list .menu[level=${i}] ul > li > a`).removeClass('active');
    }
  }

  const isMenuListItem = (evt) => {
    const className = $(evt.target).attr('class');
    if (className === 'menu-list-item' || className === 'menu-list active' || className === 'container-list') {
      return true; 
    }
    return false;
  }



  /////////scroll magic
  if ($('section.owner').length) {
    var controller = new ScrollMagic.Controller({vertical: true});
    var tweenOwner = new TimelineMax ()
    .add([
        TweenMax.from("section.owner", 0.2, {alpha: 0, y: 100, ease: Linear.easeNone}),
    ]);
    var tweenStart = new TimelineMax ()
    .add([
        TweenMax.from("section.start", 0.2, {alpha: 0, y: 100, ease: Linear.easeNone}),
    ]);
    var tweenSecurity = new TimelineMax ()
    .add([
        TweenMax.from("section.security", 0.2, {alpha: 0, y: 100, ease: Linear.easeNone}),
    ]);

    var scene1 = new ScrollMagic.Scene({triggerElement: "section.owner"})
      .setTween(tweenOwner)
      //.addIndicators()
      .addTo(controller);

    var scene2 = new ScrollMagic.Scene({triggerElement: "section.start"})
      .setTween(tweenStart)
      //.addIndicators()
      .addTo(controller);

    var scene3 = new ScrollMagic.Scene({triggerElement: "section.security"})
      .setTween(tweenSecurity)
      //.addIndicators()
      .addTo(controller);
  }
});
