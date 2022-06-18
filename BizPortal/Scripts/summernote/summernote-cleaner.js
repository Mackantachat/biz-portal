(function(factory){
  if(typeof define==='function'&&define.amd){
    define(['jquery'],factory);
  }else if(typeof module==='object'&&module.exports){
    module.exports=factory(require('jquery'));
  }else{
    factory(window.jQuery);
  }
}
(function($){
  $.extend(true,$.summernote.lang,{
    'en-US':{
      cleaner:{
        tooltip:'Cleaner',
        not:'Text has been Cleaned!!!'
      }
    }
  });
  $.extend($.summernote.options,{
    cleaner:{
      notTime:2400, // Time to display Notifications.
      action:'both', // both|button|paste 'button' only cleans via toolbar button, 'paste' only clean when pasting content, both does both options.
      newline:'<br>', // Summernote's default is to use '<p><br></p>'
      notStyle:'position:absolute;top:0;left:0;right:0',
      icon: '<i class="fa fa-eraser"></i>',
      keepHtml: true, //Remove all Html formats
      keepClasses: false, //Remove Classes
      badTags:['style','script','applet','embed','noframes','noscript','html', 'p'], //Remove full tags with contents
      badAttributes:['style','start', 'face'] //Remove attributes from remaining tags
    }
  });
  $.extend($.summernote.plugins,{
    'cleaner':function(context){
      var self=this;
      var ui=$.summernote.ui;
      var $note=context.layoutInfo.note;
      var $editor=context.layoutInfo.editor;
      var options=context.options;
      var lang=options.langInfo;
      var cleanText = function (txt, nlO) {
        if(options.cleaner.keepHtml){
          if(!options.cleaner.keepClasses){
            var sS=/(\n|\r| class=(")?Mso[a-zA-Z]+(")?)/g;
            var out=txt.replace(sS,' ');
          }
          var nL=/(\n)+/g;
          out=out.replace(nL,nlO);
          var cS=new RegExp('<!--(.*?)-->','gi');
          out=out.replace(cS,'');
          var tS=new RegExp('<(/)*(meta|link|span|\\?xml:|st1:|o:|font)(.*?)>','gi');
          out=out.replace(tS,'');
          var bT=options.cleaner.badTags;
          for(var i=0;i<bT.length;i++){
            tS=new RegExp('<'+bT[i]+'.*?'+bT[i]+'(.*?)>','gi');
            out=out.replace(tS,'');
          }
          var bA=options.cleaner.badAttributes;
          for(var ii=0;ii<bA.length;ii++){
            //var aS=new RegExp(' ('+bA[ii]+'="(.*?)")|('+bA[ii]+'=\'(.*?)\')','gi');
            var aS=new RegExp(' '+bA[ii]+'=[\'|"](.*?)[\'|"]','gi');
            out=out.replace(aS,'');
          }
        }else{
          if(!options.cleaner.keepClasses){
            var sS=/(\r| class=(")?Mso[a-zA-Z]+(")?)/g;
            var out=txt.replace(sS,' ');
          }
          var nL=/(\n)+/g;
          out=out.replace(nL,nlO);
        }
        return out;
      };
      if(options.cleaner.action=='both'||options.cleaner.action=='button'){
        context.memo('button.cleaner',function(){
          var button=ui.button({
            contents:options.cleaner.icon,
            tooltip:lang.cleaner.tooltip,
            click:function(){
              if($note.summernote('createRange').toString()){
                var text=cleanText($note.summernote('createRange').toString(),options.cleaner.newline);
                $note.summernote('pasteHTML',text);
              }else{
                var text=cleanText($note.summernote().html(),options.cleaner.newline);
                $note.summernote('code',text);
              }
              if(options.cleaner.notTime>0){
                $editor.find('.note-editing-area').append('<div class="summernote-cleanerAlert alert alert-success" style="'+options.cleaner.notStyle+'">'+lang.cleaner.not+'</div>');
                setTimeout(function(){$editor.find('.summernote-cleanerAlert').remove();},options.cleaner.notTime);
              }
            }
          });
          return button.render();
        });
      }
      this.events={
        'summernote.paste':function(we,e){
          if(options.cleaner.action=='both'||options.cleaner.action=='paste'){
            e.preventDefault();
            var ua=window.navigator.userAgent;
            var msie=ua.indexOf("MSIE ");
            if(msie>0||!!navigator.userAgent.match(/Trident.*rv\:11\./)){
              var text=window.clipboardData.getData("Text");
            }else{
              var text=e.originalEvent.clipboardData.getData((options.cleaner.keepHtml?'text/html':'text/plain'));
            }
            var text=cleanText(text,options.cleaner.newline);
            $note.summernote('pasteHTML',text);
            if(options.cleaner.notTime>0){
              $editor.find('.note-resizebar').append('<div class="summernote-cleanerAlert alert alert-success" style="'+options.cleaner.notStyle+'">'+lang.cleaner.not+'</div>');
              setTimeout(function(){$editor.find('.summernote-cleanerAlert').remove();},options.cleaner.notTime);
            }
          }
        }
      }
    }
  });
}));