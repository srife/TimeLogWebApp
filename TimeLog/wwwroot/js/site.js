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
                alert($(event.currentTarget).val() + " " + $(event.currentTarget).find("option:selected").text());
            });
        });
    }
}
var main = new Main();
//# sourceMappingURL=site.js.map