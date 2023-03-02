// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(function () {
    $("#searchAc").autocomplete({
        delay: 120,
        minLength: 2,
        source:
            function (request, response) {

                $.ajax({
                    url: "https://localhost:7185/api/SearchRecordsAPI/search",
                    dataType: "json",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response(data.map(function (item) {
                            if (data == null) {
                                return {
                                    label: 'No Data Found'
                                    }
                            }
                            return {
                                label: item.name,
                                value: item.name
                            };
                        }));
                    }
                });


            }
    });
});

//Alerts Fadeout
//window.setTimeout(function () {
//    $("alert").fadeTo(500, 0).slideUp(500, function () {
//        $(this).remove();
//    });
//}, 3000);