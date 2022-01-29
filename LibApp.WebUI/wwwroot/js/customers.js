(function () {

    var TABLE = "#customers";

    var initializeDataTable = function () {
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
                    data: "membershipType.discountRate"
                },
                {
                    data: "membershipType.name"
                },
                {
                    data: "id",
                    render: (data) => {
                        return `<div>
                        <a href='customers/edit/${data}' class="btn btn-link btn-sm">
                            Edit
                        </a>
                        <a href='customers/details/${data}' class="btn btn-link btn-sm">
                            Details
                        </a>
                    </div>`;
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

    $(document).ready(function () {
        initializeDataTable();
        attachEvents();
    })

})();