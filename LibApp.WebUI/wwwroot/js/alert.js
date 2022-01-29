var bootstrapAlert = function (title, message, type = 'secondary', duration = 5000) {

    var ALERT_CONTAINER = '#alert-container'

    var alertEl = $("<div>")
        .addClass("alert alert-" + type)
        .attr('role', 'alert')
        .html('<strong>' + title +' </strong> ' + message);

    var container = $(ALERT_CONTAINER);

    if (!container.length) {
        var containerEl = $('<div>')
            .attr('id', 'alert-container');

        $('body').append(containerEl);
    }

    $(ALERT_CONTAINER).prepend(alertEl);

    setTimeout(function () {
        alertEl.fadeOut(400, function () {
            alertEl.remove();

            var alerts = $(ALERT_CONTAINER).find('.alert').length;

            if (!alerts) {
                $(ALERT_CONTAINER).remove();
            }
        });
    }, duration)
}