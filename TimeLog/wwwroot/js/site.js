"use strict";
class Main {
    constructor() {
        "use strict";
        $(() => {
            const activityEntity_Billable = document.getElementById("ActivityEntity_Billable");
            const invoiceStatement = document.getElementById("invoice-statement");
            if (activityEntity_Billable && invoiceStatement) {
                $("#invoice-statement").toggle($("#ActivityEntity_Billable").is(":checked"));
            }
            $("#ActivityEntity_Billable").change(() => {
                if ($("#invoice-statement").length) {
                    $("#invoice-statement").toggle($("#ActivityEntity_Billable").is(":checked"));
                }
            });
            $("#ActivityEntity_ProjectId").change((event) => {
                $.ajax({
                    type: "GET",
                    url: window.location.pathname + "?handler=ProjectSelected&id=" + $(event.currentTarget).val(),
                    contentType: "application/json",
                    dataType: "json",
                    success: response => {
                        $(response).each(function (i, val) {
                            $.each(val, function (k, v) {
                                switch (k) {
                                    case "defaultActivityTypeId":
                                        if (v !== null && v !== undefined) {
                                            $("#ActivityEntity_ActivityTypeId option[value=" + v + "]").attr("selected", "selected");
                                        }
                                        else {
                                            $("#ActivityEntity_ActivityTypeId option[value=" + 0 + "]").attr("selected", "selected");
                                        }
                                        break;
                                    case "defaultClientId":
                                        if (v !== null && v !== undefined) {
                                            $("#ActivityEntity_ClientId option[value=" + v + "]").attr("selected", "selected");
                                        }
                                        else {
                                            $("#ActivityEntity_ClientId option[value=" + 0 + "]").attr("selected", "selected");
                                        }
                                        break;
                                    case "defaultLocationId":
                                        if (v !== null && v !== undefined) {
                                            $("#ActivityEntity_LocationId option[value=" + v + "]").attr("selected", "selected");
                                        }
                                        else {
                                            $("#ActivityEntity_LocationId option[value=" + 0 + "]").attr("selected", "selected");
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            });
                        });
                    },
                    error: function (ex) {
                        alert("Failed to retrieve items " + ex + " " + $(event.currentTarget).val());
                    }
                });
            });
        });
        let TableBillableTasksFormatted = document.querySelector('#TableBillableTasksFormatted');
        let ButtonBillableTasksFormatted = document.querySelector('#ButtonBillableTasksFormatted');

        let TableBillableByProjectFormatted = document.querySelector('#TableBillableByProjectFormatted');
        let ButtonBillableByProjectFormatted = document.querySelector('#ButtonBillableByProjectFormatted');

        let TableBillableByDayFormatted = document.querySelector('#TableBillableByDayFormatted');
        let ButtonBillableByDayFormattedButton = document.querySelector('#ButtonBillableByDayFormattedButton');
        function selectNode(node) {
            let range = document.createRange();
            range.selectNodeContents(node)
            let select = window.getSelection()
            select.removeAllRanges()
            select.addRange(range)
        }
        ButtonBillableTasksFormatted.addEventListener('click', function () {
            selectNode(TableBillableTasksFormatted);
            document.execCommand('copy')
        }); 
        ButtonBillableByProjectFormatted.addEventListener('click', function () {
            selectNode(TableBillableByProjectFormatted);
            document.execCommand('copy')
        }); 
        ButtonBillableByDayFormattedButton.addEventListener('click', function () {
            selectNode(TableBillableByDayFormatted);
            document.execCommand('copy')
        }); 

    }
}
let main = new Main();
//# sourceMappingURL=site.js.map