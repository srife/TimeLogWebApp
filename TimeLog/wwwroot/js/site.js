// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {
    $(".invoice-statement").show($("#ActivityEntity_Billable").val());

    $("#ActivityEntity_Billable").change(function () {
        //alert($("#ActivityEntity_Billable").val());
        if (this.value === true) {
            $(".invoice-statement").show();
        } else {
            $(".invoice-statement").css("display", "");
        }
    });
});