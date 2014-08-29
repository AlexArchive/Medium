$(document).ready(function () {
    $("*[data-confirmation-msg]").click(function (event) {
        var confirmText = $(this).attr("data-confirmation-msg");
        if (!confirm(confirmText)) {
            event.preventDefault();
        }
    });
});