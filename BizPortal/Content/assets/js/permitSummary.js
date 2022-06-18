var permit = function () {
    var handleApps = function () {
        var $container = $('#appContainer');
        if ($container.length === 0) {
            return;
        }

        var updateSummaryFunc = function () {
            var selected = 0;
            var summaryFixedCost = 0;
            var summaryMinCost = 0;
            var summaryMaxCost = 0;
            var summaryStartCost = 0;
            var summaryFixedDays = 0;
            var summaryMinDays = 0;
            var summaryMaxDays = 0;
            var summaryStartDays = 0;
            var html = '';
            var hasRange = false;
            var firstRangeActive = true;
            var firstStartActive = true;
            var firstFixedActive = true;
            var minDaysList = Array();
            $('.item-list', $container).each(function (idx) {
                var $item = $(this);
                if ($item.hasClass('active')) {
                    selected++;
                    $('input#SingleFormApps_' + idx + '__isChecked', $item).val(true);
                    var costType = $('input#SingleFormApps_' + idx + '__OperatingCostType', $item).val();
                    var cost = parseFloat($('input#SingleFormApps_' + idx + '__OperatingCost', $item).val()) || 0;
                    var cost2 = parseFloat($('input#SingleFormApps_' + idx + '__OperatingCost2', $item).val()) || 0;
                    var costHtml = '';

                    if (costType === 'Range') {
                        summaryMinCost += cost;
                        summaryMaxCost += cost2;
                        //html += $('#appRangeItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), days, number_format(cost, isFloat(cost) ? 2 : 0), number_format(cost2, isFloat(cost2) ? 2 : 0));

                        if (cost + cost2 > 0) {
                            costHtml = number_format(cost, isFloat(cost) ? 2 : 0) + ' - ' + number_format(cost2, isFloat(cost2) ? 2 : 0);
                        }
                        else {
                            costHtml = "0";
                        }
                    }
                    else if (costType === 'StartAt') {
                        summaryStartCost += cost;
                        //html += $('#appStartAtItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), days, number_format(cost, isFloat(cost) ? 2 : 0));

                        if (cost > 0) {
                            costHtml = $container.data('start-at-text') + ' ' + number_format(cost, isFloat(cost) ? 2 : 0);
                        }
                        else {
                            costHtml = $container.data('start-at-text') + " 0";
                        }
                    }
                    else {
                        // Fixed
                        summaryFixedCost += cost;
                        //html += $('#appFixedItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), days, number_format(cost, isFloat(cost) ? 2 : 0));

                        if (cost > 0) {
                            costHtml = number_format(cost, isFloat(cost) ? 2 : 0);
                        }
                        else {
                            costHtml = "0";
                        }
                    }

                    var daysType = $('input#SingleFormApps_' + idx + '__OperatingDaysType', $item).val();
                    var days = parseFloat($('input#SingleFormApps_' + idx + '__OperatingDays', $item).val()) || 0;
                    var days2 = parseFloat($('input#SingleFormApps_' + idx + '__OperatingDays2', $item).val()) || 0;
                    minDaysList.push(days);
                    if (days2) {
                        minDaysList.push(days);
                    }
                    var daysHtml = '';

                    if (daysType === 'Range') {
                        if (firstRangeActive) {
                            summaryMinDays = days;
                            summaryMaxDays = days2;
                            firstRangeActive = false;
                        }
                        else {
                            summaryMinDays = summaryMinDays > days ? days : summaryMinDays;
                            summaryMaxDays = summaryMaxDays < days2 ? days2 : summaryMaxDays;
                        }
                        //html += $('#appRangeItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), days, number_format(days, isFloat(days) ? 2 : 0), number_format(days2, isFloat(days2) ? 2 : 0));

                        daysHtml = number_format(days, isFloat(days) ? 2 : 0) + ' - ' + number_format(days2, isFloat(days2) ? 2 : 0);

                        if (days + days2 > 0) {
                            daysHtml = number_format(days, isFloat(days) ? 2 : 0) + ' - ' + number_format(days2, isFloat(days2) ? 2 : 0);
                        }
                        else {
                            daysHtml = "-";
                        }
                    }
                    else if (daysType === 'StartAt') {
                        if (firstStartActive) {
                            summaryStartDays = days;
                            firstStartActive = false;
                        }
                        else {
                            summaryStartDays = summaryStartDays > days ? days : summaryStartDays;
                        }
                        //html += $('#appStartAtItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), days, number_format(days, isFloat(days) ? 2 : 0));

                        if (days > 0) {
                            daysHtml = $container.data('start-at-text') + ' ' + number_format(days, isFloat(days) ? 2 : 0);
                        }
                        else {
                            daysHtml = $container.data('start-at-text') + ' -';
                        }
                    }
                    else {
                        // Fixed
                        if (firstFixedActive) {
                            summaryFixedDays = days;
                            firstFixedActive = false;
                        }
                        else {
                            summaryFixedDays = summaryFixedDays > days ? days : summaryFixedDays;
                        }

                        //html += $('#appFixedItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), days, number_format(days, isFloat(days) ? 2 : 0));

                        if (days > 0) {
                            daysHtml = number_format(days, isFloat(days) ? 2 : 0);
                        }
                        else {
                            daysHtml = '-';
                        }
                    }

                    html += $('#appFixedItemTemplate').html().formatUnicorn($('input#SingleFormApps_' + idx + '__AppName', $item).val(), $('input#SingleFormApps_' + idx + '__OrganizationName', $item).val(), daysHtml, costHtml);
                }
                else {
                    $('input#SingleFormApps_' + idx + '__isChecked', $item).val(false);
                }
            });

            if ($('.item-list.clickable.active', $container).length > 0) {
                if ($('.item-list.clickable.active', $container).length === $('.item-list.clickable.active.fixed-cost', $container).length) {
                    // มีแบบ fixed อย่างเดียว
                    if (summaryFixedCost > 0) {
                        $('.summaryCost').text(number_format(summaryFixedCost, isFloat(summaryFixedCost) ? 2 : 0));
                    }
                    else {
                        $('.summaryCost').text('0');
                    }
                }
                else if ($('.item-list.clickable.active', $container).length === $('.item-list.clickable.active.range-cost', $container).length) {
                    // มีแบบ range อย่างเดียว
                    if (summaryMinCost + summaryMaxCost) {
                        $('.summaryCost').text(number_format(summaryMinCost, isFloat(summaryMinCost) ? 2 : 0) + ' - ' + number_format(summaryMaxCost, isFloat(summaryMaxCost) ? 2 : 0));
                    }
                    else {
                        $('.summaryCost').text('0');
                    }
                }
                else if ($('.item-list.clickable.active.startat-cost', $container).length == 0) {
                    // มีแบบ fixed & range แต่ไม่มี startat
                    if (summaryFixedCost + summaryMinCost + summaryMaxCost) {
                        $('.summaryCost').text(number_format(summaryFixedCost + summaryMinCost, isFloat(summaryFixedCost + summaryMinCost) ? 2 : 0) + ' - ' + number_format(summaryFixedCost + summaryMaxCost, isFloat(summaryFixedCost + summaryMaxCost) ? 2 : 0));
                    }
                    else {
                        $('.summaryCost').text('0');
                    }
                }
                else {
                    // มีแบบ startat
                    if (summaryFixedCost + summaryMinCost + summaryStartCost > 0) {
                        $('.summaryCost').text($container.data('start-at-text') + ' ' + number_format(summaryFixedCost + summaryMinCost + summaryStartCost, isFloat(summaryFixedCost + summaryMinCost + summaryStartCost) ? 2 : 0));
                    }
                    else {
                        $('.summaryCost').text('0');
                    }
                }
            }
            else {
                $('.summaryCost').text('0');
            }

            if ($('.item-list.clickable.active', $container).length > 0) {
                if ($('.item-list.clickable.active', $container).length === $('.item-list.clickable.active.fixed-days', $container).length) {
                    // มีแบบ fixed อย่างเดียว
                    if ($('.item-list.clickable.active.fixed-days', $container).length > 1) {
                        if (summaryFixedDays > 0) {
                            $('.summaryDays').text($container.data('start-at-text') + ' ' + number_format(summaryFixedDays, isFloat(summaryFixedDays) ? 2 : 0));
                        }
                        else {
                            $('.summaryDays').text($container.data('start-at-text') + ' -');
                        }
                    }
                    else {
                        if (summaryFixedDays > 0) {
                            $('.summaryDays').text(number_format(summaryFixedDays, isFloat(summaryFixedDays) ? 2 : 0));
                        }
                        else {
                            $('.summaryDays').text('-');
                        }
                    }
                }
                else if ($('.item-list.clickable.active', $container).length === $('.item-list.clickable.active.range-days', $container).length) {
                    // มีแบบ range อย่างเดียว
                    if (summaryMinDays + summaryMaxDays > 0) {
                        $('.summaryDays').text(number_format(summaryMinDays, isFloat(summaryMinDays) ? 2 : 0) + ' - ' + number_format(summaryMaxDays, isFloat(summaryMaxDays) ? 2 : 0));
                    }
                    else {
                        $('.summaryDays').text('-');
                    }
                }
                else if ($('.item-list.clickable.active.startat-days', $container).length == 0) {
                    // มีแบบ fixed & range แต่ไม่มี startat
                    //var minDays = summaryMinDays <= summaryFixedDays ? summaryMinDays : summaryFixedDays;
                    //var maxDays = summaryMaxDays >= summaryFixedDays ? summaryMaxDays : summaryFixedDays;

                    var minDays = Math.min.apply(null, minDaysList);
                    var maxDays = Math.max.apply(null, minDaysList);

                    if (minDays != maxDays) {
                        if (minDays + maxDays > 0) {
                            $('.summaryDays').text(number_format(minDays, isFloat(minDays) ? 2 : 0) + ' - ' + number_format(maxDays, isFloat(maxDays) ? 2 : 0));
                        }
                        else {
                            $('.summaryDays').text('-');
                        }
                    }
                    else {
                        if (minDays > 0) {
                            $('.summaryDays').text(number_format(minDays, isFloat(minDays) ? 2 : 0));
                        }
                        else {
                            $('.summaryDays').text('-');
                        }
                    }
                }
                else {
                    // มีแบบ startat
                    var minDays = Math.min.apply(null, minDaysList);
                    if (minDays > 0) {
                        $('.summaryDays').text($container.data('start-at-text') + ' ' + number_format(minDays, isFloat(minDays) ? 2 : 0));
                    }
                    else {
                        $('.summaryDays').text($container.data('start-at-text') + ' -');
                    }
                }
            }
            else {
                $('.summaryDays').text('-');
            }

            if (selected > 0) {
                $('#frmPermitSummary #submit-btn').show();
                $('.blog-service').html(html);
            }
            else {
                $('#frmPermitSummary #submit-btn').hide();
                $('.blog-service').html($('#appEmptyItemTemplate').html());
            }
        };

        $container.on('click', '.item-list.clickable', function (e) {
            if ($(e.target).is('a, button')) {
                // prevent anchor or button clicked
                return;
            }

            var $this = $(this);
            $this.toggleClass('active');

            updateSummaryFunc();
        });

        updateSummaryFunc();
    };

    return {
        init: function () {
            handleApps();
        }
    };
}();

$(document).ready(function () {
    permit.init();

    //console.log('test');

});
