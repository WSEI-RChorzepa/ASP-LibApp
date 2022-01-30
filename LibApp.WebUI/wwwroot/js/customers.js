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
                                        <a href='customers/edit/${data}' class="btn btn-link btn-sm">
                                            Edit
                                        </a>
                                        <a href='customers/details/${data}' class="btn btn-link btn-sm">
                                            Details
                                        </a>
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

    var attachEvents = function () {
        $.fn.dataTable.ext.errMode = 'none';
        $(TABLE).on('error.dt', function (e, settings, techNote, message) {
            bootstrapAlert("DataTable Error", 'An error has been reported by DataTables: ' + message, 'danger', 5000);
        })
            .DataTable();
    }

    return {
        initialize: function (accessType) {
            initializeDataTable(accessType);
        },
        attachEvents: function () {
            attachEvents();
        }
    }

})();