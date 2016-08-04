function SaveSurveyContent(orderGuid, questions) {
    this.submitSurveyInput = { OrderGuid: orderGuid, Questions: questions };
}

function ValidateResults() {

    return {
        Succeeded: true,
        Message: ''
    };
}

function SaveSurveyResult() {
    this.Succeeded = false;
    this.Message = '';
}

function Question(htmlId) {
    var question = this;
    this.HtmlId = htmlId;
    this.QuestionType = GetQuestionType();//"multiple", "text" etc
    this.QuestionClass = GetQuestionClass();// "order", "product"
    this.QuestionId = GetQuestionId();
    this.SelectedOptionNumber = GetSelectedOptionNumber();
    this.QuestionAnswerText = GetQuestionAnswerText();
    this.ProductId = GetProductId();


    function GetQuestionContainer() {
        return $('#' + question.HtmlId);
    }
    function GetQuestionClass() {
        // returns if the question is for order or product
        var containerId = GetQuestionContainer()[0].id;
        if (containerId.indexOf('survey') > -1) {
            return 'order';
        }
        else if (containerId.indexOf('product') > -1) {
            return 'product';
        }
        else {
            return '';
        }

    }
    function GetQuestionType() {
        if (GetQuestionContainer().find('input[type=radio]').length > 0) {
            return 'multiple';
        }
        else if (GetQuestionContainer().find('textarea').length > 0) {
            return 'text';
        }
        else {
            return '';
        }
    }
    function GetQuestionId() {
        var splitedId = question.HtmlId.split('_');
        var id = splitedId[splitedId.length - 1];
        return id;
    }
    function GetSelectedOptionNumber() {
        if (GetQuestionType() == 'multiple') {
            var selected = $("input[type='radio'][name='" + question.HtmlId + "']:checked");
            if (selected.length > 0) {
                var selectedRadioId = selected[0].id;
                var splitedId = selectedRadioId.split('_');
                var optionNumber = splitedId[splitedId.length - 1];
                return optionNumber;
            }
        }

        return null;
    }
    function GetQuestionAnswerText() {
        if (GetQuestionType() == 'text') {
            return GetQuestionContainer().children('.answerContainer').children('textarea').val();
        }
    }
    function GetProductId() {
        if (question.QuestionClass == "product") {
            var containerId = GetQuestionContainer()[0].id;
            var splitedId = containerId.split('_');
            var productId = splitedId[1];
            return productId;
        }
    }
}

function GetQuestions() {
    var questionItems = [];
    $('.questionContainer').each(function (key, questionItem) {
        var questionItemHtmlId = questionItem.id;
        var question = new Question(questionItemHtmlId);
        questionItems.push(question);

    });

    return questionItems;
}

function SubmitSurvey() {
    var validationResult = ValidateResults();
    if (validationResult.Succeeded) {
        var orderGuid = OrderGuidHidden.GetValue();
        var questions = GetQuestions();
        var saveSurveyContent = new SaveSurveyContent(orderGuid, questions);

        $.ajax(
            {
                type: "POST",
                url: 'Survey.aspx/Submit',
                data: JSON.stringify(saveSurveyContent),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(JSON.stringify(response));
                },
                failure: function () {

                }
            }
            );


    }
    else {
        alert('validation message here')
    }
}