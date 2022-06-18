
if (!window.singleFormHelpers) {
    window.singleFormHelpers = {};
}

if (!window.singleFormHelpers.form) {
    window.singleFormHelpers.form = {

        _cache: {
            readOnlySelect2List: [],
            readOnlyRadioList: []
        },

        // Set validation ignorant on controls in this container.
        ignoreValidations: function (container) {
            $(container).find('input').addClass('ignore');
            $(container).find('select').addClass('ignore');
            $(container).find('textarea').addClass('ignore');
        },

        onArrayOfFormDataBound: function (sectionName, callback) {
            $(document).on(sectionName + '-databound', function (e, section, datalist) {
                if (callback) callback(section, datalist);
            });
        },

        // Listen on databound event on modal form for specific section.
        onArrayOfFormModalDataBound: function (sectionName, callback) {
            $(document).on(sectionName + '-modal-databound', function (e, section, data, datalist, action) {
                if (callback) callback(section, data, datalist, action);
            });
        },

        // Listen on databound event on modal form for all sections.
        onArrayOfFormModalDataBoundAll: function (callback) {
            $(document).on('allSections-modal-databound', function (e, section, data, datalist, action) {
                if (callback) callback(section, data, datalist, action);
            });
        },

        onDraftDataBound: function (callback) {
            $(document).on('singleform-databound', function (e, data) {
                if (callback) callback(data);
            });
        },

        onDraftDataLoading: function (callback) {
            $(document).on('singleform-dataloading', function (e, sectionGroup) {
                if (callback) callback(sectionGroup);
            });
        },

        onDraftDataLoaded: function (callback) {
            $(document).on('singleform-dataloaded', function (e, sectionGroup) {
                if (callback) callback(sectionGroup);
            });
        },

        onApplicationRequestDataBound: function (callback) {
            $(document).on('application-request-databound', function (e, data) {
                if (callback) callback(data);
            });
        },

        // Apply validations on controls in this container.
        restoreValidations: function (container) {
            $(container).find('input').removeClass('ignore');
            $(container).find('select').removeClass('ignore');
            $(container).find('textarea').removeClass('ignore');
        },

        // Set all controls inside container to be readonly.
        setReadonly: function (container, readonly = true) {
            var me = this;

            $(container).find('input, select, textarea').prop('readonly', readonly);
            $(container).find("button").prop('disabled', readonly);

            $(container).find("input[type='checkbox'], input[type='radio']").each(function (index, elm) {
                me.setRadioCheckboxReadonly($(elm), readonly);
            });

            $(container).find("button[data-toggle='modal']").prop('disabled', readonly);   // Add/Edit button
            $(container).find("button.btn-danger").prop('disabled', readonly); // Delete button
            
            $(container).find("div.date").each(function (index, elm) {
                me.setDatePickerReadonly($(elm).find("input"), readonly);
            });            

            // Set readonly on select2
            $(container).find(".select2").each(function (index, elm) {
                me.setSelect2Readonly($(elm).parent().find("select"), readonly);
            });

            // Set readonly on regular select
            $(container).find("select").each(function (index, elm) {
                if (!$(elm).hasClass('select2-hidden-accessible')) {
                    me.setSelectReadonly($(elm), readonly);
                }
            });

        },

        setRadioCheckboxReadonly: function (ctrl, readonly = true) {

            var me = this;
            var ctrlName = $(ctrl).attr('name');
            if (me._cache.readOnlyRadioList[ctrlName] === undefined) {
                me._cache.readOnlyRadioList[ctrlName] = readonly;
                $("input[name='" + ctrlName + "']").each(function (index, elm) {
                    // Register onclick to all options with the same name.
                    $(elm).click(function () {
                        return !me._cache.readOnlyRadioList[$(this).attr('name')];
                    });

                    // On IE, if we clicked label it may pass the click event to corresponding control. So we also need to disable it too.
                    $(elm).parent().click(function () {
                        var input = $(this).find('input');
                        return !me._cache.readOnlyRadioList[$(input).attr('name')];
                    });
                });
            }
            
            me._cache.readOnlyRadioList[ctrlName] = readonly;

            if (readonly === true) {
                $(ctrl).parents('div.radio').find('.disabled-select').remove();
                $(ctrl).parents('div.radio').append('<div class="disabled-select" style="background-color: transparent;"></div>');
            } else {
                $(ctrl).parents('div.radio').find('.disabled-select').remove();
            }
        },

        setSelectReadonly: function (ctrl, readonly = true) {
            // We need to manually disable all options in Select control.
            if (readonly)
                $(ctrl).attr('readonly', 'readonly');
            else
                $(ctrl).removeAttr('readonly');

            $(ctrl).change(function (e) {
                $(this).find('option:not(:selected)').prop('disabled', readonly);
            });

            var div = $(ctrl).parent();
            if (readonly === true) {
                $(".disabled-select", div).remove();
                $(div).append('<div class="disabled-select" style="background-color: transparent;"></div>');
            } else {
                $(".disabled-select", div).remove();
            }
        },

        setSelect2Readonly: function (ctrl, readonly = true) {
            var select2 = $(ctrl).parent().find('.select2');

            if (readonly === true)
            {
                $(".disabled-select", select2).remove();
                $(select2).prepend('<div class="disabled-select"></div>');
            }
            else
            {
                $(".disabled-select", select2).remove();
            }
        },
        
        setSelect2WithDefault: function (select2, value, text) {
            // Set the value, creating a new option if necessary
            if ($(select2).find("option[value='" + value + "']").length) {
                $(select2).val(value).trigger('change');
            } else {
                // Create a DOM Option and pre-select by default
                var newOption = new Option(text, value, true, true);
                // Append it to the select
                $(select2).append(newOption).trigger('change');
            } 
        },

        setDatePickerReadonly: function (elm, readonly) {
            var objs = $(elm).parent();
            if (readonly === true) {
                $("<div class='readonly-overlay'></div>").css({
                    position: "absolute",
                    width: "100%",
                    height: "100%",
                    top: 0,
                    left: 0,
                    background: "#ccc",
                    opacity: 0.2
                }).appendTo($(objs).css("position", "relative"));
            } else {
                $(objs).find('.readonly-overlay').remove();
            }
            
        },

        getSelectedDate: function(input) {
            var dateStr = $(input).val();
            if (dateStr) {
                // Calculate age based on string formatted as dd/MM/yyyy
                var parts = dateStr.split('/');
                var year = parseInt(parts[2]);
                if (year > 2300) year -= 543;
                var mydate = new Date(year, parseInt(parts[1]) - 1, parseInt(parts[0]));
                return mydate;
            }

            return null;
        },
        getSelectedTime: function(input) {
            var timeStr = $(input).val();
            if (timeStr) {
                var parts = timeStr.split(':');
                var times = (parts[0] + parts[1]);
                return times;
            }

            return null;
        },

        dataManager: (function () {

            this.formData = { };

            return {
                setFormData: function (data) {
                    this.formData = JSON.parse(JSON.stringify(data));
                },

                restoreSectionData: function (sectionName, sectionData) {

                    if (!this.formData) return sectionData;

                    $(this.formData.SectionData || []).each(function (index, sec) {
                        if (sec.SectionName === sectionName) {
                            sectionData = (sec.Type === 'Form') ? sec.FormData : sec.ArrayData;
                            return sectionData;
                        }
                    });

                    return sectionData;
                },

                restoreFormData: function (sectionName, sectionData, ctrl) {

                    if (!this.formData) return;

                    var fieldName = $(ctrl).attr('name');
                    var fieldNameText = fieldName + "_TEXT";
                    $(this.formData.SectionData || []).each(function (index, sec) {
                        if (sec.SectionName === sectionName && sec.Type === 'Form' && sec.FormData && sec.FormData.hasOwnProperty(fieldName)) {
                            sectionData[fieldName] = sec.FormData[fieldName];
                            //console.log('restored field ' + fieldName + ' to be ' + sec.FormData[fieldName]);

                            if ($(ctrl).is('select') && sec.FormData.hasOwnProperty(fieldNameText)) {
                                // also restore _TEXT
                                sectionData[fieldNameText] = sec.FormData[fieldNameText];
                                //console.log('restored field ' + fieldNameText + ' to be ' + sec.FormData[fieldNameText]);
                            }
                            return;
                        }
                    });
                }
            };
        })()
    };
}
