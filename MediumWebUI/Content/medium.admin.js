$(function () {
    function split(val) {
        return val.split(/,\s*/);
    }

    function extractLast(term) {
        return split(term).pop();
    }

    $("#tag-input").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB &&
            $(this).data("ui-autocomplete").menu.active) {
            event.preventDefault();
        }
    });

    $("#tag-input").autocomplete({
        source: function (request, response) {
            $.getJSON("/TagsJson/Search", {
                SearchTerm: extractLast(request.term)
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
    var converter = new Markdown.Converter();
    var editor = new Markdown.Editor(converter);
    editor.run();
})();
