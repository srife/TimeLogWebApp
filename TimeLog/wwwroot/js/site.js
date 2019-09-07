"use strict";
class Main {
    constructor() {
        "use strict";
        $(() => {
            if (document.getElementById("ActivityEntity_Billable")) {
                if (document.getElementById("invoice-statement")) {
                    $("#invoice-statement").toggle($("#ActivityEntity_Billable").is(":checked"));
                }
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
                        alert("Failed to retrieve items " + ex);
                    }
                });
            });
        });
    }
}
var main = new Main();
//# sourceMappingURL=site.js.map