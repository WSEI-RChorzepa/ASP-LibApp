var customersView = (function () {

    var TABLE = "#customers";

    var initializeDataTable = function (accessType) {
        $(TABLE).DataTable({
            ajax: {
                url: '/customers/getCustomers',
                dataSrc: ""
            },
            columns: [
                {
                    data: "name",
                },
                {
                    data: "email",
                },
                {
                    data: "membershipType.name",
                    render: function (data) {
                        return `<span class="badge badge-info">${data}</span>`
                    }
                },
                {
                    data: "birthdate",
                    render: function (data) {
                        return data !== null
                            ? new Date(`${data}`).toDateString()
                            : "-";
                    }
                },
                {
                    data: "id",
                    render: (data) => {
                        if (accessType == "Edit") {
                            return `<div>
                                        <a href='identity/account/edit/${data}' class="btn btn-link btn-sm">
                                            Edit
                                        </a>
                                        <a href='customers/details/${data}' class="btn btn-link btn-sm">
                                            Details
                                        </a>
                                        <button class="btn btn-link btn-sm link-sm remove" data-id="${data}">Remove</button>
                                    </div>`;
                        }
                        else {
                            return `<div>
                                        <a href='customers/details/${data}' class="btn btn-link btn-sm">
                                            Details
                                        </a>
                                    </div>`;
                        }

                    }
                }
            ]
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

        var account = $(TABLE)
            .DataTable()
            .rows(function (indx, data) {
                return data.id === id
            }).data()[0];

        bootbox.confirm({
            size: 'small',
            title: 'Confirm remove',
            message: "<p>Are you sure to delete account: </p>"
                + "<strong>" + account.email + "</strong>",
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
            callback: function (result) {
                if (result) {

                    fetch("/customers/remove?id=" + id)
                        .then(function () {
                            var message = "Account for the user: " + account.email + " was successfully deletaed.";
                            bootstrapAlert('Success', message, 'success');
                            $(TABLE).DataTable().ajax.reload();
                        })
                        .catch(function (error) {
                            var message = "An error has occured while removing account for the user: " + account.email + "."
                            bootstrapAlert('Error', message, 'danger');
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