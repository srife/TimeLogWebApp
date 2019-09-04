"use strict";
class Main {
    constructor() {
        "use strict";
        $(() => {
            const billableEl = document.getElementById("ActivityEntity_Billable");
            const invoiceStatementDiv = document.getElementById("invoice-statement");
            if (billableEl !== null && billableEl !== undefined) {
                if (!billableEl.checked) {
                    invoiceStatementDiv.style.setProperty("display", "none");
                }
                else {
                    invoiceStatementDiv.style.removeProperty("display");
                }
                billableEl.addEventListener("click", () => {
                    console.log(`billable: ${billableEl.checked}`);
                    if (!billableEl.checked) {
                        invoiceStatementDiv.style.setProperty("display", "none");
                    }
                    else {
                        invoiceStatementDiv.style.removeProperty("display");
                    }
                });
            }
        });
    }
}
var main = new Main();
//# sourceMappingURL=site.js.map