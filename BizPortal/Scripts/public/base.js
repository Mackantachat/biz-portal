
/* Smart Thailand */

function smAttachOnReady(callback) {
    document.addEventListener("DOMContentLoaded", function (e) {
        if (document.readyState === "interactive") {
            callback();
        }
    });
}

function smAttachOnLoad(callback) {
    window.addEventListener("load", function (e) {
        callback();
    });
}

function smSetContrainerWrapper() {
    var eleWrapper = document.createElement("div");
    eleWrapper.classList.add("sm-container-wrapper");
    eleWrapper.setAttribute("onmousedown", "return false;");
    for (var i = 0, l = document.body.childNodes.length; i < l; i++) {
        eleWrapper.appendChild(document.body.firstChild);
    }
    document.body.appendChild(eleWrapper);
}
smAttachOnReady(smSetContrainerWrapper);

function smDrawHeader() {

    var templateHeaderHead =
    '<header id="header" class="header-narrow header-full-width" >' +
            '<div class="header-body p-none m-none">' +
                '<div class="header-container container">' +
                    '<div class="header-row">' +
                        '<div class="header-column">' +
                           ' <div class="header-logo">' +
                          '      <a href="#"><h4 style="margin-top:15px; margin-bottom:15px;">Information Gateway for Business</h4></a>' +
                         '   </div>' +
                        '</div>' +
                        '<div class="header-column">' +
                            '<div class="header-row">' +
                                '<div class="header-nav header-nav-top-line">' +
                               '     <button class="btn header-btn-collapse-nav" data-toggle="collapse" data-target=".header-nav-main">' +
                              '          <i class="fa fa-bars"></i>' +
                             '       </button>' +
                            '        <div class="header-nav-main header-nav-main-square header-nav-main-effect-3 header-nav-main-sub-effect-1 collapse">' +
                           '             <nav>' +
                          '                  <ul class="nav nav-pills" id="mainNav">' +

                          '<li class="dropdown">' +
                                                        '<a class="dropdown-toggle" href="#">' +
                                                         '   <i class="fa fa-user"></i>  ยินดีต้อนรับ ssotest@ega.or.th' +
                                                        '    <i class="fa fa-caret-down"></i>' +
                                                       ' <i class="fa fa-caret-down"></i></a>' +
                                                        '<ul class="dropdown-menu">' +
                                                       '     <li><a target="_blank" href="http://bizid.ega.or.th/Business/ViewJuristicProfile">ข้อมูลส่วนตัว</a></li>' +
                                                      '      <li><a href="#">จัดการสถานะบริการ</a></li>' +
                                                     '       <li><a href="#">ออกจากระบบ</a> </li>' +
                                                    '    </ul>' +
                                                    '</li>' +


                          '</ul>' +
                         '                   </nav>' +
                        '                </div>' +
                       '             </div>' +
                      '          </div>' +
                     '       </div>' +
                    '    </div>' +
                   ' </div>' +
                '</div>' +
                '</header>';






    var template = templateHeaderHead;

    var bodys = document.getElementsByTagName("body");
    if (bodys.length > 0) {
        var body = bodys[0];
        var div = document.createElement("div");
        if (div != null) {
            body.appendChild(div);

            div.innerHTML = template;
        }
    }
}
smAttachOnReady(smDrawHeader);

/* End Smart Thailand */


// Sticky
(function (theme, $) {

    theme = theme || {};

    var instanceName = '__sticky';

    var PluginSticky = function ($el, opts) {
        return this.initialize($el, opts);
    };

    PluginSticky.defaults = {
        minWidth: 991,
        activeClass: 'sticky-active'
    };

    PluginSticky.prototype = {
        initialize: function ($el, opts) {
            if ($el.data(instanceName)) {
                return this;
            }

            this.$el = $el;

            this
				.setData()
				.setOptions(opts)
				.build();

            return this;
        },

        setData: function () {
            this.$el.data(instanceName, this);

            return this;
        },

        setOptions: function (opts) {
            this.options = $.extend(true, {}, PluginSticky.defaults, opts, {
                wrapper: this.$el
            });

            return this;
        },

        build: function () {
            if (!($.isFunction($.fn.pin))) {
                return this;
            }

            this.options.wrapper.pin(this.options);

            return this;
        }
    };

    // expose to scope
    $.extend(theme, {
        PluginSticky: PluginSticky
    });

    // jquery plugin
    $.fn.themePluginSticky = function (opts) {
        return this.map(function () {
            var $this = $(this);

            if ($this.data(instanceName)) {
                return $this.data(instanceName);
            } else {
                return new PluginSticky($this, opts);
            }

        });
    }

}).apply(this, [window.theme, jQuery]);
