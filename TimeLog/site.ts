///<reference path="./node_modules/@types/jquery/JQuery.d.ts"/>
"use strict";

class Main {
    constructor() {
        "use strict";
        $(() => {
            //alert("Hello");

            const billableEl = (document.getElementById("ActivityEntity_Billable") as HTMLInputElement);
            const invoiceStatementDiv = document.getElementById("invoice-statement") as HTMLDivElement;

            if (billableEl !== null && billableEl !== undefined) {
                if (!billableEl.checked) {
                    invoiceStatementDiv.style.setProperty("display", "none");
                } else {
                    invoiceStatementDiv.style.removeProperty("display");
                }

                billableEl.addEventListener("click", () => {
                    console.log(`billable: ${billableEl.checked}`);
                    if (!billableEl.checked) {
                        invoiceStatementDiv.style.setProperty("display", "none");
                    } else {
                        invoiceStatementDiv.style.removeProperty("display");
                    }
                });
            }
        });
    }
}

var main = new Main();
//let titleEl = document.querySelector("h1");
//titleEl.innerText = "Hello from TypeScript";