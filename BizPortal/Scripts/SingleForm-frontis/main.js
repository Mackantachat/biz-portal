
// Frontis: We should remove ignore class only when control is visible on form.
var isControlVisible = function (ctrl) {    
    var visible = true;
    $(ctrl).parents('div.form-group').each(function () {
        if ($(this).css('display') === 'none') {
            visible = false;
        }
    });

    return visible;
};

var conditionDisplayUpdateVisible = function (elem, shouldDisplay) {
    if (shouldDisplay) {
        // Should show
        elem.slideDown();

        elem.find("input").each(function () {
            if (isControlVisible(this)) $(this).removeClass("ignore");
        });
        elem.find("select").each(function () {
            if (isControlVisible(this)) $(this).removeClass("ignore");
        });
        elem.find("textarea").each(function () {
            if (isControlVisible(this)) $(this).removeClass("ignore");
        });
    }
    else {
        // Should hide
        elem.slideUp();

        elem.find("input").each(function () {
            $(this).addClass("ignore");
        });
        elem.find("select").each(function () {
            $(this).addClass("ignore");
        });
        elem.find("textarea").each(function () {
            $(this).addClass("ignore");
        });
    }
};

$(document).ready(function () {

    $('.form-info').click(function (e) {
        e.preventDefault();
        var targetID = $(this).data("target");
        $('#' + targetID).slideToggle();
    });

    $('a.form-control-info-close').click(function (e) {
        e.preventDefault();
        var targetID = $(this).data("target");
        $('#' + targetID).slideToggle();
    });

    $(".trigger-dd").on("change", function(e) { 
        //var data = e.params.data;
        var s2 = $(e.target);
        var selectOpt = s2.find("option:selected");
        var controlName = s2.data("control-name");
        triggerShowCondition(controlName, controlName + "::" + selectOpt.val());
        triggerDisableCondition(controlName, controlName + "::" + selectOpt.val());
        //triggerShowCondition("aaaa", $(this).val())
    });

    $(".trigger-checkbox").change(function () {
        var checked = "" + (this.checked || $(this).attr('checked') === 'checked');
        var controlName = $(this).data('control-name');
        var controlValue = $(this).data('control-value') + "__" + checked;
        var controlNameForShowCondition = $(this).data('control-name') + "__" + $(this).data('control-value');
        triggerShowCondition(controlNameForShowCondition, controlNameForShowCondition + "::" + checked);

        // Frontis
        triggerDisableCondition(controlNameForShowCondition, controlNameForShowCondition + "::" + checked);


        // EGA - Add condition if control is in Modal
        var controlModal = false;
        if ($(this).closest('.modal').length == 1) {
            controlModal = true;
        }

        var affectedControls = $(".autofill-on.autofill-on-" + controlName);
        affectedControls.each(function () {
            var tControlName = $(this).data('control-name');
            var conditions = $(this).data('autofill-conditions');
            //alert(controlValue);
            
            if (typeof conditions !== 'undefined' && // Has trigger condition
                typeof conditions[controlName] !== 'undefined' && // Has trigger condition for this control
                conditions[controlName].indexOf(controlValue) >= 0) // The value is match with trigger
            {
                var sKey = controlName + "::" + controlValue;
                var sFromDraft = $(this).data('autofill-source-from-draft')[sKey];
                var sIsAddressControl = $(this).data('autofill-source-is-address-control')[sKey];
                var sSectionName = $(this).data('autofill-source-section')[sKey];
                var sControlName = $(this).data('autofill-source-control')[sKey];
                //var sControl = controlModal ? $('div#MODAL_' + sSectionName).find('div[data-control-name="' + sControlName + '"]') : $('section[data-section-name="' + sSectionName + '"]').find('div[data-control-name="' + sControlName + '"]'); // EGA - Add condition if control is in Modal

                // Get autofill from draft data
                if (sFromDraft == "true" && singleForm) {
                    var data = singleForm.variables.draftData;
                    if (data != null && data != undefined && data.SectionData.length > 0) {
                        var section = data.SectionData.find(x => x.SectionName == sSectionName);
                        if (section != null && section != undefined) {
                            if (section.Type == "Form") {
                                var sectionData = section.FormData;
                                if (sIsAddressControl == "true") {
                                    for (var key in sectionData) {
                                        //console.log("control key: " + key + ", control value: " + sectionData[key]);
                                        if (key == "ADDRESS_" + sControlName) {
                                            //console.log("source control name: " + key);
                                            var item = $("[name=ADDRESS_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_MOO_" + sControlName) {
                                            var item = $("[name=ADDRESS_MOO_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_SOI_" + sControlName) {
                                            var item = $("[name=ADDRESS_SOI_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_BUILDING_" + sControlName) {
                                            var item = $("[name=ADDRESS_BUILDING_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_ROOMNO_" + sControlName) {
                                            var item = $("[name=ADDRESS_ROOMNO_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_FLOOR_" + sControlName) {
                                            var item = $("[name=ADDRESS_FLOOR_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_ROAD_" + sControlName) {
                                            var item = $("[name=ADDRESS_ROAD_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_PROVINCE_" + sControlName) {
                                            var item = $("[name=ADDRESS_PROVINCE_" + tControlName + "]");
                                            item.each(function () {
                                                var sID = sectionData[key], sText = sectionData[key + "_TEXT"];
                                                select2SelectItem(item, sID, sText);
                                            });
                                        } else if (key == "ADDRESS_AMPHUR_" + sControlName) {
                                            var item = $("[name=ADDRESS_AMPHUR_" + tControlName + "]");
                                            item.each(function () {
                                                var sID = sectionData[key], sText = sectionData[key + "_TEXT"];
                                                select2SelectItem(item, sID, sText);
                                            });
                                        } else if (key == "ADDRESS_TUMBOL_" + sControlName) {
                                            var item = $("[name=ADDRESS_TUMBOL_" + tControlName + "]");
                                            item.each(function () {
                                                var sID = sectionData[key], sText = sectionData[key + "_TEXT"];
                                                select2SelectItem(item, sID, sText);
                                            });
                                        } else if (key == "ADDRESS_POSTCODE_" + sControlName) {
                                            var item = $("[name=ADDRESS_POSTCODE_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }

                                            if (item.is('select')) {
                                                item.val(sectionData[key]).trigger('change');
                                            }

                                            item.each(function () {
                                                var sID = sectionData[key], sText = sectionData[key + "_TEXT"];
                                                select2SelectItem(item, sID, sText);
                                            });
                                        } else if (key == "ADDRESS_TELEPHONE_" + sControlName) {
                                            var item = $("[name=ADDRESS_TELEPHONE_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_TELEPHONE_EXT_" + sControlName) {
                                            var item = $("[name=ADDRESS_TELEPHONE_EXT_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        } else if (key == "ADDRESS_FAX_" + sControlName) {
                                            var item = $("[name=ADDRESS_FAX_" + tControlName + "]");
                                            if (item.is('input')) {
                                                item.val(sectionData[key]);
                                                item.trigger('change');
                                            }
                                        }
                                    }
                                } else {
                                    var sData = sectionData[sControlName];

                                    if ($(this).is('input')) {
                                        $(this).val(sData);
                                        $(this).trigger('change'); // triger change
                                    }

                                    if ($(this).is('select')) {
                                        $(this).val(sData).trigger('change');
                                    }

                                    $(this).find("input").each(function () {
                                        $(this).val(sData);
                                    });

                                    $(this).find('select').each(function () {
                                        var sID = sData, sText = sectionData[sControlName + "_TEXT"];
                                        select2SelectItem($(this), sID, sText);
                                    });
                                }
                            }
                        }
                    }
                } else {
                    // Frontis 2019-06-11: Just select for section level, not control level. We will find its later on.
                    var sControl = controlModal ? $('div#MODAL_' + sSectionName).find('div[data-control-name="' + sControlName + '"]') : $('section[data-section-name="' + sSectionName + '"]'); // EGA - Add condition if control is in Modal

                    // Frontis 2019-06-11: Itself can be an input or select too.
                    if ($(this).is('input') && sControl.find('input[name="' + sControlName + '"]').length > 0) {
                        $(this).val(sControl.find('input[name="' + sControlName + '"]').val());
                        $(this).trigger('change'); // triger change
                    }

                    if ($(this).is('select') && sControl.find('select[name="DROPDOWN_' + sControlName + '"]').length > 0) {
                        $(this).val(sControl.find('select[name="DROPDOWN_' + sControlName + '"]').val()).trigger('change');
                    }

                    $(this).find("input").each(function () {
                        var tInputName = $(this).attr('name');
                        var sInputName = tInputName.replace(tControlName, sControlName);
                        $(this).val(sControl.find('input[name="' + sInputName + '"]').val());
                    });

                    $(this).find('select').each(function () {
                        var tInputName = $(this).attr('name');
                        var sInputName = tInputName.replace(tControlName, sControlName);
                        var sID = '', sText = '';
                        var sInput = sControl.find('select[name="' + sInputName + '"]');
                        if (sInput.length == 0) {
                            // Select not found, in prefill mode
                            sID = sControl.find('input[name="' + sInputName + '"]').val();
                            sText = sControl.find('input[name="' + sInputName + '_TEXT"]').val();
                        }
                        else if (!sInput.data('select2')) {
                            $(this).val(sInput.val());
                        }
                        else {
                            var select2Data = sInput.select2('data');
                            sID = select2Data[0].id;
                            sText = select2Data[0].text;
                        }
                        select2SelectItem($(this), sID, sText);
                    });
                }
            }
        });
        
    });

    var select2SelectItem = function (select, id, text) {
        // Set the value, creating a new option if necessary
        if (select.find("option[value='" + id + "']").length) {
            select.val(id).trigger('change');
        } else {
            // Create a DOM Option and pre-select by default
            var newOption = new Option(text, id, true, true);
            // Append it to the select
            select.append(newOption).trigger('change');
        }
    };
    var currentAnswers = {};
    var triggerShowCondition = function (controlName, answer) {
        //console.log("Control: " + controlName + ", Answer: " + answer);
        var answerItems = answer.split('::');
        currentAnswers[answerItems[0]] = answerItems[1];
        var showControls = $('.show-condition.' + controlName);
        showControls.each(function () {
            var displayCondition = $(this).data('display-condition');
            var shouldDisplay = false;
            if (typeof displayCondition !== 'undefined') {                
                if (!$.isArray(displayCondition)) {

                    // Nested display-condition
                    var fncValidateCondition = function (cond) {
                        if ((cond.Conditions || []).length == 0 && (cond.ConditionWithAnswers || []).length == 0) {
                            // If no conditions specified, we should return false.
                            return false;
                        }

                        var isValid = true;
                        if (cond.Conditions && cond.Conditions.length > 0) {
                            // Recursively validate nested conditions
                            for (var i = 0; i < cond.Conditions.length; i++) {
                                var result = fncValidateCondition(cond.Conditions[i]);

                                if (result && cond.IsOr) {
                                    return true;
                                }

                                if (!result && !cond.IsOr) {
                                    return false;
                                }

                                isValid = isValid && result;
                            }
                        } else if (cond.ConditionWithAnswers) {
                            // Validate condition with answers
                            for (var i = 0; i < cond.ConditionWithAnswers.length; i++) {
                                var conditions = cond.ConditionWithAnswers[i];
                                var condition = conditions.split(',');
                                var result = true;
                                condition.forEach(function (term) {

                                    var isNotEquals = term.includes('!::');
                                    var subResult = true;
                                    if (isNotEquals) {
                                        // Check if not equals
                                        var condItem = term.split('!::');
                                        if (currentAnswers[condItem[0]] == condItem[1]) {
                                            subResult = false;
                                        }
                                    } else {
                                        // Check if equals
                                        var condItem = term.split('::');
                                        if (currentAnswers[condItem[0]] != condItem[1]) {
                                            subResult = false;
                                        }
                                    }
                                    result = result && subResult;                                   

                                });

                                if (result && cond.IsOr) {
                                    return true;
                                }

                                if (!result && !cond.IsOr) {
                                    return false;
                                }

                                isValid = isValid && result;
                            }
                        }

                        return isValid;
                    };

                    shouldDisplay = fncValidateCondition(displayCondition);
                } else {

                    // Normal display-condition
                    displayCondition.forEach(function (conditions) {
                        var condition = conditions.split(',');
                        var fulfill = true;
                        condition.forEach(function (term) {

                            var isNotEquals = term.includes('!::');
                            if (isNotEquals) {
                                // Check if not equals
                                var condItem = term.split('!::');
                                if (currentAnswers[condItem[0]] == condItem[1]) {
                                    fulfill = false;
                                }
                            } else {
                                // Check if equals
                                var condItem = term.split('::');
                                if (currentAnswers[condItem[0]] != condItem[1]) {
                                    fulfill = false;
                                }
                            }

                        });

                        if (fulfill) {
                            shouldDisplay = true;
                        }
                    });

                }                
            }
            
            conditionDisplayUpdateVisible($(this), shouldDisplay);
        });
    };

    // Frontis: Trigger disabling conditions
    var conditionDisableUpdate = function (elem, shouldDisable) {
        if (shouldDisable) {
            // Should disable
            $(elem).attr('disabled', 'disabled');
            elem.find("input").each(function () {
                $(this).attr('disabled', 'disabled');
            });
            elem.find("select").each(function () {
                $(this).attr('disabled', 'disabled');
            });
            elem.find("textarea").each(function () {
                $(this).attr('disabled', 'disabled');
            });
        }
        else {
            // Should enable
            $(elem).removeAttr('disabled');
            elem.find("input").each(function () {
                $(this).removeAttr('disabled');
            });
            elem.find("select").each(function () {
                $(this).removeAttr('disabled');
            });
            elem.find("textarea").each(function () {
                $(this).removeAttr('disabled');
            });
        }
    };

    // Frontis 2020-02-17: Support nested operation and Not-Equal operation.
    var triggerDisableCondition = function (controlName, answer) {        
        var answerItems = answer.split('::');
        currentAnswers[answerItems[0]] = answerItems[1];
        var controls = $('.disable-condition.' + controlName);        
        controls.each(function () {
            var disableCondition = $(this).data('disable-condition');
            
            var shouldDisable = false;
            /*if (typeof disableCondition !== 'undefined') {
                disableCondition.forEach(function (conditions) {
                    var condition = conditions.split(',');
                    var fulfill = true;
                    condition.forEach(function (term) {
                        var condItem = term.split('::');
                        if (currentAnswers[condItem[0]] != condItem[1]) {
                            fulfill = false;
                        }
                    });
                    if (fulfill) {
                        shouldDisable = true;
                    }
                });
            }*/

            if (typeof disableCondition !== 'undefined') {
                if (!$.isArray(disableCondition)) {

                    // Nested display-condition
                    var fncValidateCondition = function (cond) {
                        if ((cond.Conditions || []).length == 0 && (cond.ConditionWithAnswers || []).length == 0) {
                            // If no conditions specified, we should return false.
                            return false;
                        }

                        var isValid = true;
                        if (cond.Conditions && cond.Conditions.length > 0) {
                            // Recursively validate nested conditions
                            for (var i = 0; i < cond.Conditions.length; i++) {
                                var result = fncValidateCondition(cond.Conditions[i]);

                                if (result && cond.IsOr) {
                                    return true;
                                }

                                if (!result && !cond.IsOr) {
                                    return false;
                                }

                                isValid = isValid && result;
                            }
                        } else if (cond.ConditionWithAnswers) {
                            // Validate condition with answers
                            for (var i = 0; i < cond.ConditionWithAnswers.length; i++) {
                                var conditions = cond.ConditionWithAnswers[i];
                                var condition = conditions.split(',');
                                var result = true;
                                condition.forEach(function (term) {

                                    var isNotEquals = term.includes('!::');
                                    var subResult = true;
                                    if (isNotEquals) {
                                        // Check if not equals
                                        var condItem = term.split('!::');
                                        if (currentAnswers[condItem[0]] == condItem[1]) {
                                            subResult = false;
                                        }
                                    } else {
                                        // Check if equals
                                        var condItem = term.split('::');
                                        if (currentAnswers[condItem[0]] != condItem[1]) {
                                            subResult = false;
                                        }
                                    }
                                    result = result && subResult;

                                });

                                if (result && cond.IsOr) {
                                    return true;
                                }

                                if (!result && !cond.IsOr) {
                                    return false;
                                }

                                isValid = isValid && result;
                            }
                        }

                        return isValid;
                    };

                    shouldDisable = fncValidateCondition(disableCondition);
                } else {

                    // Normal display-condition
                    disableCondition.forEach(function (conditions) {
                        var condition = conditions.split(',');
                        var fulfill = true;
                        condition.forEach(function (term) {

                            var isNotEquals = term.includes('!::');
                            if (isNotEquals) {
                                // Check if not equals
                                var condItem = term.split('!::');
                                if (currentAnswers[condItem[0]] == condItem[1]) {
                                    fulfill = false;
                                }
                            } else {
                                // Check if equals
                                var condItem = term.split('::');
                                if (currentAnswers[condItem[0]] != condItem[1]) {
                                    fulfill = false;
                                }
                            }

                        });

                        if (fulfill) {
                            shouldDisable = true;
                        }
                    });

                }
            }

            conditionDisableUpdate($(this), shouldDisable);
        });
    };    

    $('.trigger-display').change(function () {
        var answer = $(this).data('control-name') + "::" + $(this).attr('value');
        triggerShowCondition($(this).data('control-name'), answer);

        triggerDisableCondition($(this).data('control-name'), answer);
        
        //alert();
        if (this.id == "SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL") {
            $("#SELL_FOOD_INFORMATION__PURPOSE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL").click();
        }
        if (this.id == "SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK") {
            $("#SELL_FOOD_INFORMATION__PURPOSE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK").click();
        }
        if (this.id == "SELL_FOOD_INFORMATION__PURPOSE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL") {
            $("#SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL").click();
        }
        if (this.id == "SELL_FOOD_INFORMATION__PURPOSE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK") {
            $("#SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK").click();
        }
        // ต่ออายุ (ไม่เกิน200)
        if (this.id == "APP_SELL_FOOD_RENEW_LT_FEE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_RENEW_LT_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LT_FEE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_RENEW_LT_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LT_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_RENEW_LT_FEE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LT_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_RENEW_LT_FEE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK").click();
        }
        // แก้ไข (ไม่เกิน200)
        if (this.id == "APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK").click();
        }
        // ยกเลิก (ไม่เกิน200)
        if (this.id == "APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK").click();
        }
        // ต่ออายุ (เกิน200)
        if (this.id == "APP_SELL_FOOD_RENEW_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL") {
            $("#APP_SELL_FOOD_RENEW_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK") {
            $("#APP_SELL_FOOD_RENEW_TYPE_OPTION-APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK").click();
        }
        //ยกเลิกสะสมอาหาร (เกิน 200 ตรม.)
        if (this.id == "APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-SELL") {
            $("#APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-STOCK") {
            $("#APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-SELL") {
            $("#APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_CANCEL_DETAIL_TYPE_OPTION-STOCK") {
            $("#APP_SELL_FOOD_CANCEL_SECTION_TYPE_OPTION-STOCK").click();
        }

        //ต่ออายุสะสมอาหาร (เกิน 200 ตรม.)
        if (this.id == "APP_SELL_FOOD_RENEW_TYPE_OPTION-SELL") {
            $("#APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_TYPE_OPTION-STOCK") {
            $("#APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-SELL") {
            $("#APP_SELL_FOOD_RENEW_TYPE_OPTION-SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_RENEW_LICENSE_TYPE_OPTION-STOCK") {
            $("#APP_SELL_FOOD_RENEW_TYPE_OPTION-STOCK").click();
        }

        //แก้ไขสะสมอาหาร (เกิน 200 ตรม.)
        if (this.id == "APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-SELL") {
            $("#APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-STOCK") {
            $("#APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-STOCK").click();
        }
        if (this.id == "APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-SELL") {
            $("#APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-SELL").click();
        }
        if (this.id == "APP_SELL_FOOD_EDIT_DETAIL_TYPE_OPTION-STOCK") {
            $("#APP_SELL_FOOD_EDIT_LOCATION_TYPE_OPTION-STOCK").click();
        }

        // ต่ออายุก่อสร้าง 4 ประเภท
        if (this.id == "APP_BUILDING_BUILDING_SECTION_TYPE_OPTION-APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING") {
            $("#APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_OPTION-APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_BUILDING").click();
        }
        if (this.id == "APP_BUILDING_BUILDING_SECTION_TYPE_OPTION-APP_BUILDING_BUILDING_SECTION_TYPE_MODIFY") {
            $("#APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_OPTION-APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_MODIFY").click();
        }
        if (this.id == "APP_BUILDING_BUILDING_SECTION_TYPE_OPTION-APP_BUILDING_BUILDING_SECTION_TYPE_DISMANTLE") {
            $("#APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_OPTION-APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_DISMANTLE").click();
        }

        if (this.id == "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_OPTION-APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_BUILDING") {
            $("#APP_BUILDING_BUILDING_SECTION_TYPE_OPTION-APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING").click();
        }
        if (this.id == "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_OPTION-APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_MODIFY") {
            $("#APP_BUILDING_BUILDING_SECTION_TYPE_OPTION-APP_BUILDING_BUILDING_SECTION_TYPE_MODIFY").click();
        }
        if (this.id == "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_OPTION-APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_DISMANTLE") {
            $("#APP_BUILDING_BUILDING_SECTION_TYPE_OPTION-APP_BUILDING_BUILDING_SECTION_TYPE_DISMANTLE").click();
        }
        

        if (this.id == "DIRECT_SELL_METHOD_OPTION-slm") {
            $('#DIRECT_SELL_AGENT_OPTION-direct_agent').removeAttr('disabled');
        }
        if (this.id == "DIRECT_SELL_METHOD_OPTION-mlm") {
            $('#DIRECT_SELL_AGENT_OPTION-free_agent').click();
            $('#DIRECT_SELL_AGENT_OPTION-direct_agent').attr('disabled', 'disabled');
        }
    });

    // Frontis: Fire change event on hidden.
    $('input[type=hidden]').trigger('change');

    // Frontis: Fire change event on .trigger-checkbox to inititalize its listeners.
    $(".trigger-checkbox").trigger('change');

    // Frontis: Initialize currency textbox function.
    (function ($) {
        $.fn.inputFilter = function (inputFilter) {
            return this.on("input", function () {
                if (inputFilter(this.value)) {
                    this.invalidFormat = false;
                    this.oldValue = this.value;
                    this.oldSelectionStart = this.selectionStart;
                    this.oldSelectionEnd = this.selectionEnd;
                } else if (!inputFilter(this.value) && this.hasOwnProperty("oldValue")) {
                    this.invalidFormat = true;
                    this.value = this.oldValue;
                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                }
            });
        };
        $.fn.getDecimalValue = function () {
            if (this.val() == '' || this.val() == undefined) {
                return null;
            }
            var value = this.val().replace(/[^0-9\.]/g, '');
            return Number(Number(value).toFixed(2));
        };
        $.fn.getIntegerValue = function () {
            if (this.val() == '' || this.val() == undefined) {
                return null;
            }
            var value = this.val().replace(/[^0-9\.]/g, '');
            return Math.round(Number(value).toFixed(2));
        };
        $.fn.setValue = function (decimalNumber) {
            if (/^(\d+)(\.\d+)?$/.test(decimalNumber)) {
                var num = Number(decimalNumber);
                this.val(num.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            }
            else {
                this.val('');
            }
        };
    }(jQuery));

    // Frontis: Initialize currency text input event handler.
    $(".currency").inputFilter(function (value) {
        value = value.replace(/,/g, '');
        return /^(\d*)(\.)?(\d{1,2})?$/.test(value);
    });

    $(".currency").focus(function () {
        if (typeof this.value === 'undefined' || this.value == '') {
            this.oldValue = this.value;
            return;
        }
        this.value = this.value.replace(/,/g, '');
        this.oldValue = this.value;
    });

    $(".currency").blur(function () {
        if (typeof this.value === 'undefined' || this.value == '') {
            return;
        }
        if (/^\.$/.test(this.value)) {
            this.value = 0;
        }
        var num = Number(this.value.replace(/,/g, ''));
        this.value = num.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
        // trigger change when user spam decimal place more than 2 places while on inputing.
        if (this.invalidFormat) {
            $(this).trigger('change');
        }
    });

    // Frontis: Initialize percentage text input event handler.
    $(".percentage.max100").inputFilter(function (value) {
        return /^[0-9]?(\.\d{0,2})?$|^[1-9][0-9](\.\d{0,2})?$|^100(\.[0]?[0]?)?$/.test(value);
    });
    $(".percentage").inputFilter(function (value) {
        return /^(\d*)(\.)?(\d{1,2})?$/.test(value);
    });
    $(".percentage").focus(function () {
        if (typeof this.value === 'undefined' || this.value == '') {
            this.oldValue = this.value;
            return;
        }
        this.value = this.value.replace(' %', '');
        this.oldValue = this.value;
    });
    $(".percentage").blur(function () {
        if (typeof this.value === 'undefined' || this.value == '') {
            return;
        }
        if (/^\.$/.test(this.value)) {
            this.value = 0;
        }
        var num = Number(this.value.replace(/,/g, ''));
        this.value = num.toFixed(2).toString() + " %";
        // trigger change when user spam decimal place more than 2 places while on inputing.
        if (this.invalidFormat) {
            $(this).trigger('change');
        }
    });

    // Frontis: Initialize numeric text input event handler.
    $(".numeric").inputFilter(function (value) {
        value = value.replace(/,/g, '');
        return /^(\d*)?$/.test(value);
    });

    $(".numeric").focus(function () {
        if (typeof this.value === 'undefined' || this.value == '') {
            this.oldValue = this.value;
            return;
        }
        this.value = this.value.replace(/,/g, '');
        this.oldValue = this.value;
    });

    $(".numeric").blur(function () {
        if (typeof this.value === 'undefined' || this.value == '') {
            return;
        }
        var num = Number(this.value.replace(/,/g, ''));
        this.value = num.toFixed(0).replace(/\d(?=(\d{3})+(?!\d))/g, '$&,');
    });

});