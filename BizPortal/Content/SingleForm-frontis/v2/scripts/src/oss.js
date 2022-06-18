import $ from 'jquery';
import _ from 'lodash';
import 'slick-carousel';
import ScrollMagic from 'scrollmagic';
import 'scrollmagic/scrollmagic/uncompressed/plugins/animation.gsap';
import 'scrollmagic/scrollmagic/uncompressed/plugins/debug.addIndicators.js';
import {TweenMax, TimelineMax, Power2, Power1, Linear} from 'gsap';


if($('section.about-oss').length > 0) {
  var controller = new ScrollMagic.Controller({vertical: true});
  var tween1 = new TimelineMax ()
    .add([
    TweenMax.from(".oss-1 p.t1", 0.3, {alpha: 0, ease: Linear.easeNone}),
    TweenMax.from(".oss-1 p.t2", 0.3, {alpha: 0, ease: Linear.easeNone}),
    TweenMax.staggerFrom(".oss-1 .blog-item-mobile", 0.5, {y: 30, alpha: 0, ease: Linear.easeNone}, 0.3),
    TweenMax.staggerFrom(".oss-1 .blog-item", 0.5, {y: 30, alpha: 0, ease: Linear.easeNone}, 0.3),
  ]);  

  var tween2 = new TimelineMax ()
    .add([
    TweenMax.from(".oss-2 .item-header", 0.3, {scaleX: 0.8, scaleY: 0.8, alpha: 0, ease: Linear.easeOut}),
    TweenMax.staggerFrom(".oss-2 .item-info", 0.3, {scaleX: 0.8, scaleY: 0.8, alpha: 0, ease: Linear.easeOut}, 0.2),
    TweenMax.from(".oss-2 .main-img", 0.3, {delay: 0.4, alpha: 0, ease: Linear.easeOut}),
  ]); 

  var tween3 = new TimelineMax ()
    .add([
    TweenMax.from(".oss-3 p.head", 0.3, {y: 30, alpha: 0, ease: Linear.easeOut}),
    TweenMax.from(".oss-3 p.sub", 0.3, {y: 30, alpha: 0, ease: Linear.easeOut}),
    TweenMax.staggerFrom(".oss-3 .link-item", 0.3, {y: 30, alpha: 0, ease: Linear.easeOut}, 0.2),
  ]);  

  var tween4 = new TimelineMax ()
    .add([
    TweenMax.from(".oss-4 p.head", 0.3, {y: 30, alpha: 0, ease: Linear.easeOut}),
    TweenMax.from(".oss-4 p.sub", 0.3, {y: 30, alpha: 0, ease: Linear.easeOut}),
    TweenMax.staggerFrom(".oss-4 .blog-item", 0.3, {y: 50, alpha: 0, ease: Linear.easeOut}, -0.3),
  ]);  

  var tween3s = new TimelineMax ()
    .add([
    TweenMax.to(".oss-3 .link-1", 0.3, {rotation: 90, /* transformOrigin:"left top", */ ease: Power1.easeIn}),
    //TweenMax.to(".oss-3 .link-2", 0.3, {rotation: 260, ease: Power1.easeIn}),
    TweenMax.to(".oss-3 .link-3", 0.3, {rotation: -60, ease: Power1.easeIn}),
  ]);  
  new ScrollMagic.Scene({triggerElement: ".oss-1" /*, duration: $("section.banner").height() */})
      .setTween(tween1)
      // .addIndicators()
      .addTo(controller);
  new ScrollMagic.Scene({triggerElement: ".oss-2" /*, duration: $("section.banner").height() */})
      .setTween(tween2)
      // .addIndicators()
      .addTo(controller);
  new ScrollMagic.Scene({triggerElement: ".oss-3" /*, duration: $("section.banner").height() */})
      .setTween(tween3)
      // .addIndicators()
      .addTo(controller);
  new ScrollMagic.Scene({triggerElement: ".oss-3" , duration: $(".oss-3").height() })
      .setTween(tween3s)
      // .addIndicators()
      .addTo(controller);
  new ScrollMagic.Scene({triggerElement: ".oss-4"})
      .setTween(tween4)
      // .addIndicators()
      .addTo(controller);
}