///<reference path="./node_modules/@types/jquery/JQuery.d.ts"/>
"use strict";

class Main {
    constructor() {
        "use strict";
        $(() => {
            //const activityEntityProjectId = $("#ActivityEntity_ProjectId");
            //const activityEntityActivityTypeId = $("#ActivityEntity_ActivityTypeId");
            //const activityEntityClientId = $("ActivityEntity_ClientId");

            //alert(window.location.pathname);
            //this function will return the full URL up to the last '/'
            //function getAbsolutePath() {
            //    const loc = window.location;
            //    const pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf("/") + 1);
            //    return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
            //}
            //function getAbsolutePath2() {
            //    const loc = window.location;
            //    const exists = loc.pathname.indexOf("?");
            //    if (exists > -1) {
            //        const pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf("?") + 1);
            //        console.log(pathName);
            //        return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
            //    } else {
            //        return loc.href;
            //    }
            //}

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
                //alert($(event.currentTarget).val() + " " + $(event.currentTarget).find("option:selected").text());
                $.ajax({
                    type: "GET",
                    url: window.location.pathname + "?handler=ProjectSelected&id=" + $(event.currentTarget).val(),
                    contentType: "application/json",
                    dataType: "json",
                    success: response => {
                        //console.log(response);
                        $(response).each(function (i, val) {
                            //console.log("First Each: " + i.toString + " : " + val);
                            $.each(val, function (k, v) {
                                //console.log("Second Each: " + k.valueOf + " : " + v);
                                switch (k) {
                                    case "defaultActivityTypeId":
                                        if (v !== null && v !== undefined) {
                                            $("#ActivityEntity_ActivityTypeId option[value=" + v + "]").attr("selected", "selected");
                                        } else {
                                            $("#ActivityEntity_ActivityTypeId option[value=" + 0 + "]").attr("selected", "selected");
                                        }
                                        break;
                                    case "defaultClientId":
                                        if (v !== null && v !== undefined) {
                                            $("#ActivityEntity_ClientId option[value=" + v + "]").attr("selected", "selected");
                                        } else {
                                            $("#ActivityEntity_ClientId option[value=" + 0 + "]").attr("selected", "selected");
                                        }
                                        break;
                                    case "defaultLocationId":
                                        if (v !== null && v !== undefined) {
                                            $("#ActivityEntity_LocationId option[value=" + v + "]").attr("selected", "selected");
                                        } else {
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

interface Project {
    defaultActivityType: string;
    defaultActivityTypeId: number;
    defaultClient: string;
    defaultClientId: number;
    defaultLocation: string;
    defaultLocationId: number;
    id: number;
    isDefault: boolean;
    name: string;
}