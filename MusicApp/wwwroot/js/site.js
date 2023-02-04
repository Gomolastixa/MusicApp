// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(function () {

    $("#searchAc").autocomplete({
        source: "https://localhost:7185/api/SearchRecordsAPI/search",
        minLength: 2
    });
});


