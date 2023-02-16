// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//var data = [];

//$.getJSON('https://localhost/api/SearchRecordsAPI/search', function (result) {
//    $.each(result.Name, function (index, val) {
//        data.push(val);
//    });
//});


// $(function () {

//     $("#SearchAc").autocomplete({
//         source: function (request, response) {
//             $.ajax({
//                 url: "https://localhost:7185/api/SearchRecordsAPI/search",
//                 dataType: "json",
//                 type: "GET",
//                 data: {
//                     term: request.term
//                 },
//                 success: function (data) {
//                    var transformed = $.map(data, function (item) {
//                        return {
//                            label: "Name",
//                            value: item.Name
//                        };
//                    });
//                     response(transformed);
//                 },
//                 error: function () {
//                     response([]);
//                 }
//             });
//         },
//         minLength: 2,
//         });
// });

$(function() {
    $("#searchAc").autocomplete({
        source: "https://localhost:7185/api/SearchRecordsAPI/search",
        minLength: 2
     });
});


$(function() {
    $("#searchAc").autocomplete({
        source: "http://localhost:5079/api/SearchRecordsAPI/search",
        minLength: 2
     
     });
});
