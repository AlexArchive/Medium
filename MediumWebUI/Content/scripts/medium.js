$(function() {

    $("[data-loading-text]").on("click", function () {
        $(this).button("loading");
    });

    $("time[datetime]").ago();
    
});