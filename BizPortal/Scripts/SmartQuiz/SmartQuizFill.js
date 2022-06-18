
function SmartQuizCollapseSubQuestions(question) {

    // Collapse all next subQuestions
    question.SubQuestions.forEach(function (subQ) {
        question.Choices.forEach(function (c) {            
            if (c.NextQuestionID === subQ.QuestionID) {
                $("#" + subQ.QuestionID).slideUp();

                subQ.SelectedChoices = [];
                $('input[type=radio][name=' + subQ.SystemName + ']').prop('checked', false);
                $('input[type=checkbox][data-questionid=' + subQ.QuestionID + ']').prop('checked', false);
                $('input[type=checkbox][data-questionid=' + subQ.QuestionID + ']').removeAttr('disabled');
            }
        });

        // recursively collapse subquestion
        SmartQuizCollapseSubQuestions(subQ);
    });

    question.SelectedChoices = [];
    $('input[type=radio][name=' + question.SystemName + ']').prop('checked', false);
    $('input[type=checkbox][data-questionid=' + question.QuestionID + ']').prop('checked', false);
    $('input[type=checkbox][data-questionid=' + question.QuestionID + ']').removeAttr('disabled');
}

$(document).ready(function () {
    var SmartQuizQuestionMap = [];
    var SmartQuizChoiceMap = [];
    var SmartQuizGetAllQuestions = function (questions) {
        questions.forEach(function (q) {
            SmartQuizQuestionMap[q.QuestionID] = q;
            q.Choices.forEach(function (c) {
                SmartQuizChoiceMap[c.ChoiceID] = c;
            });
            if (q.SubQuestions != null) {
                SmartQuizGetAllQuestions(q.SubQuestions);
            }
        });
    };
    SmartQuizGetAllQuestions(SmartQuizFillData.Questions);

    var SmartQuizUpdateSubQuestionVisibility = function (q) {
        var curNextQuestionID = [];
        q.SelectedChoices.forEach(function (c) {
            var choice = SmartQuizChoiceMap[c];
            if (choice.NextQuestionID !== null) {
                var nextQs = choice.NextQuestionID.split(',');
                curNextQuestionID = curNextQuestionID.concat(nextQs);
            }
        });
        q.SubQuestions.forEach(function (subQ) {

            if (curNextQuestionID.indexOf(subQ.QuestionID) !== -1) {
                // Show
                $("#" + subQ.QuestionID).slideDown();
            }
            else {
                // Hide
                $("#" + subQ.QuestionID).slideUp();

                SmartQuizCollapseSubQuestions(subQ);
            }
        });
    };

    var SmartQuizUpdateForbidden = function () {

    };

    $('.choice-ui').change(function () {
        var choiceID = $(this).data("choiceid");
        var choice = SmartQuizChoiceMap[choiceID];

        var q = SmartQuizQuestionMap[$(this).data("questionid")];
        if (this.checked) {
            if (q.AllowMultipleAnswer) {
                q.SelectedChoices.push(choiceID);
            }
            else {
                q.SelectedChoices = [choiceID];
            }
            if (choice.ForbiddenChoices != null) {
                $("#forbid-" + choiceID).show();
                $("#forbid-info-" + choiceID).slideDown();
                choice.ForbiddenChoices.forEach(function (forbidChoiceID) {
                    var forbidChoice = SmartQuizChoiceMap[forbidChoiceID];
                    if (typeof forbidChoice.forbidBy === 'undefined') {
                        forbidChoice.forbidBy = [];
                    }
                    forbidChoice.forbidBy.push(choiceID);
                    var cb = $("#" + forbidChoiceID);
                    cb[0].checked = false;
                    cb.trigger('change');
                    cb.attr("disabled", "disabled");
                });
            }
        }
        else {
            var foundIndex = q.SelectedChoices.indexOf(choiceID);
            while (foundIndex !== -1) {
                q.SelectedChoices.splice(foundIndex, 1);
                foundIndex = q.SelectedChoices.indexOf(choiceID);
            }
            if (choice.ForbiddenChoices != null) {
                $("#forbid-" + choiceID).hide();
                $("#forbid-info-" + choiceID).slideUp();
                choice.ForbiddenChoices.forEach(function (forbidChoiceID) {
                    var forbidChoice = SmartQuizChoiceMap[forbidChoiceID];
                    var removeIndex = forbidChoice.forbidBy.lastIndexOf(choiceID);
                    while (removeIndex >= 0) {
                        forbidChoice.forbidBy.splice(removeIndex, 1);
                        removeIndex = forbidChoice.forbidBy.lastIndexOf(choiceID);
                    }
                    if (forbidChoice.forbidBy.length == 0) {
                        $("#" + forbidChoiceID).removeAttr("disabled");
                    }
                });
            }
        }
        SmartQuizUpdateSubQuestionVisibility(q);
    });

    SmartQuizFillData.Questions.forEach(function (q) {
        SmartQuizUpdateSubQuestionVisibility(q);
    });

    $("#smart-quiz-submit").click(function (e) {
        e.preventDefault();
        $.post(SmartQuizSubmitUrl,
            { Group: "Restaurant" },
            function () {
                alert("success");
            }).done(function () {
                alert("second success");
            }).fail(function () {
                alert("error");
            }).always(function () {
                alert("finished");
            }
        );

    });

    $(".form-info").unbind("click");
    $('.form-info').click(function (e) {
        e.preventDefault();
        var targetID = $(this).data("target");
        $('.wrap-form #' + targetID).slideToggle();
    });

    $(".wrap-form .close").unbind("click");
    $('.wrap-form .close').click(function (e) {
        e.preventDefault();
        var targetID = $(this).data("target");
        $('.wrap-form .info#' + targetID).slideUp();
    });

});