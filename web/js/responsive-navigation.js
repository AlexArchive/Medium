$(document).ready(function() {
    $("#nav-toggle").click(function() {
        var navigationExpanded = $("header nav").hasClass("nav-expanded");
        if (navigationExpanded) {
            $("header nav").removeClass("nav-expanded");
        } else {
            $("header nav").addClass("nav-expanded");
        }
    });
});
