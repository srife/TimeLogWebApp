///<reference path="./node_modules/@types/jquery/JQuery.d.ts"/>
"use strict";

class Main {
    constructor() {
        "use strict";
        $(() => {
            //const activityEntityProjectId = $("#ActivityEntity_ProjectId");
            //const activityEntityActivityTypeId = $("#ActivityEntity_ActivityTypeId");
            //const activityEntityClientId = $("ActivityEntity_ClientId");

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
                alert($(event.currentTarget).val() + " " + $(event.currentTarget).find("option:selected").text());
            });
        });
    }
}

var main = new Main();