$(document).ready(function () {

    $("#wmd-input").val($("#Content").val());

    $("form").submit(function () {
        $("#Content").val($("#wmd-input").val());
    });

    $("#wmd-input").scroll(function () {
        $("#wmd-preview").scrollTop($("#wmd-input").scrollTop() + 400);
    });

    $("#publish-draft-buttonset").buttonset();

    $("#publish-draft-buttonset input").click(function () {
        var buttonText = $("label[for='" + $(this).attr('id') + "']").text();
        $("#publish-button").text(buttonText);
    });
});

$(function () {

    function split(val) {
        return val.split(/,\s*/);
    }

    function extractLast(term) {
        return split(term).pop();
    }

    $("#tags").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("ui-autocomplete").menu.active) {
            event.preventDefault();
        }
    });

    $("#tags").autocomplete({
        source: function (request, response) {
            $.getJSON("/Tags/Search", {
                term: extractLast(request.term)
            }, response);
        },
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            var terms = split(this.value);
            terms.pop();
            terms.push(ui.item.value);
            terms.push("");
            this.value = terms.join(", ");
            return false;
        },
        autoFocus: true
    });

});

(function () {
    var converter = Markdown.getSanitizingConverter();

    converter.hooks.chain("preBlockGamut", function (text, rbg) {
        return text.replace(/^ {0,3}""" *\n((?:.*?\n)+?) {0,3}""" *$/gm, function (whole, inner) {
            return "<blockquote>" + rbg(inner) + "</blockquote>\n";
        });
    });

    var editor = new Markdown.Editor(converter);
    editor.hooks.chain("onPreviewRefresh", function () {
        Prism.highlightAll();
    });

    editor.run();
})();