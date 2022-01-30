var booksView = (function () {

    var TABLE = '#books';

    var initializeDataTable = function (accessType) {
        $(TABLE).DataTable({
            ajax: {
                url: '/books/getBooks',
                dataSrc: ""
            },
            columns: [
                {
                    data: "name",
                },
                {
                    data: "authorName",
                },
                {
                    data: "genre.name",
                },
                {
                    data: "id",
                    render: (data) => {

                        switch (accessType) {
                            case "View":
                            default:
                                return `<div>
                                            <a href='books/details/${data}' class="btn btn-link btn-sm">
                                                Details
                                            </a>
                                        </div>`;
                            case "Edit":
                                return `<div>
                                            <a href='books/edit/${data}' class="btn btn-link btn-sm">
                                                Edit
                                            </a>
                                            <a href='books/details/${data}' class="btn btn-link btn-sm">
                                                Details
                                            </a>
                                            <button class="btn btn-link btn-sm link-sm remove" data-id="${data}">Remove</button>
                                        </div>`;
                        }
                    },
                },
            ],
        })
    }

    var attachEvents = function (removeCallback) {
        $(TABLE).on("click", "button.remove", function () {
            var id = $(this).data("id");
            removeCallback(id);
        });

        $.fn.dataTable.ext.errMode = 'none';
        $(TABLE).on('error.dt', function (e, settings, techNote, message) {
            bootstrapAlert("DataTable Error", 'An error has been reported by DataTables: ' + message, 'danger', 5000);
        })
        .DataTable();
    }

    var handleRemove = function (id) {
        if (id === undefined) {
            throw new Error("Property name of ID does not exist in current element.")
        }

        var book = $(TABLE)
            .DataTable()
            .rows(function (indx, data) {
                return data.id === id
        }).data()[0];

        bootbox.confirm({
            size: 'small',
            title: 'Confirm remove',
            message: "<p>Are you sure to delete book: </p>"
                + "<strong>" + book.name + "</strong>",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success btn-sm'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger btn-sm'
                }
            },
            callback: function(result) {
                if (result) {

                    fetch("/books/removeBook?id=" + id)
                        .then(function () {
                            var message = "The book: " + book.name + " was successfully deletaed.";
                            bootstrapAlert('Success', message, 'success');
                            $(TABLE).DataTable().ajax.reload();
                        })
                        .catch(function (error) {
                            var message = "An error has occured while removing book: " + book.name + "."
                            bootstrapAlert('Error',message, 'danger');
                            console.error(error);
                        })
                }
            }
        })
    }

    return {
        initialize: function (accessType) {
            initializeDataTable(accessType);
        },
        attachEvents: function () {
            attachEvents(handleRemove);
        }
    }

})();