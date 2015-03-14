(function ($) {

    var pluginName = 'ago';

    $.fn[pluginName] = function () {
        return this.each(function () {
            var element = $(this);
            updateTitle(element);
        });
    };

    function updateTitle(element) {
        var dateString = element.attr("datetime");
        var date = new Date(dateString);
        var since = timeSince(date);
        element.attr("title", since + " ago");
    }

    // http://stackoverflow.com/a/3177838/2015959
    function timeSince(date) {
        var seconds = Math.floor((new Date() - date) / 1000);
        var interval = Math.floor(seconds / 31536000);
        if (interval > 1) {
            return interval + " years";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " months";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " days";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " hours";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " minutes";
        }
        return Math.floor(seconds) + " seconds";
    }
}(jQuery));