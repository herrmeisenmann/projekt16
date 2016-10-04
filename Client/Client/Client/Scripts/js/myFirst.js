/**
 * Created by Nik on 27.09.2016.
 */
function showWidget(component){
    //$("#"+component).removeAttr("display");
    var window = $("#"+component).data("kendoWindow");
    window.open();
}
function termineGrid(){
    var termineDataSource = new kendo.data.DataSource({
        data: [ { Termin: "Klausur", Fach: "AE", Datum: "06.10.2016", Kommentar: "", Note: ""},
            { Termin: "Klausur", Fach: "FE", Datum: "04.10.2016", Kommentar: "", Note: "2" },
            { Termin: "Präsentation", Fach: "OuG", Datum: "07.10.2016", Kommentar: "", Note: "" },
            { Termin: "Präsentation", Fach: "WuG", Datum: "01.10.2016", Kommentar: "", Note: "1" }
        ]
    });
    $("#termineGrid").kendoGrid({
        autoBind: false,
        dataSource: termineDataSource
    });
    termineDataSource.read();
    //termineDataSource.fetch(function () {
    //    var janeDoe = termineDataSource.at(0);
    //    console.log(janeDoe.Termin); // displays "Jane Doe"
    //});
}
function kendoWidgets() {
    // initialize the widgets
    $("#stundenplan").kendoWindow({
        title: "Stundenplan"
    });
    //var stundenplanWindow = $("#stundenplan").data("kendoWindow");
    //var stundenplanWindowWrapper = stundenplanWindow.wrapper;
    //stundenplanWindowWrapper.addClass("stundenplanWindow");

    $("#userinfo").kendoWindow({
        title: "User Infos"
    });
    //var userinfoWindow = $("#userinfo").data("kendoWindow");
    //var userinfoWindowWrapper = userinfoWindow.wrapper;
    //userinfoWindowWrapper.addClass("userinfoWindow");

    $("#termine").kendoWindow({
        title: "Termine"
    });
    //var termineWindow = $("#termine").data("kendoWindow");
    //var termineWindowWrapper = termineWindow.wrapper;
    //termineWindowWrapper.addClass("termineWindow");

    $("#infoIHK").kendoWindow({
        title: "Prüfungstermine und infos"
    });
    //var infoIHKWindow = $("#infoIHK").data("kendoWindow");
    //var infoIHKWindowWrapper = infoIHKWindow.wrapper;
    //infoIHKWindowWrapper.addClass("infoIHKWindow");

    $("#new").kendoWindow({
        title: "Folgt..."
    });
    //var newWindow = $("#new").data("kendoWindow");
    //var newWindowWrapper = newWindow.wrapper;
    //newWindowWrapper.addClass("newWindow");

    $("#chat").kendoWindow({
        title: "Klassenchat"
    });
    //var chatWindow = $("#chat").data("kendoWindow");
    //var chatWindowWrapper = chatWindow.wrapper;
    //chatWindowWrapper.addClass("chatWindow");

    $("#menu").kendoMenu({
        animation: { open: { effects: "fadeIn" } }
    });

    //Anordnen der Widgets
    $("#stundenplan").closest(".k-window").css({
        top: "20%",
        left: "20%"
        //"grid-column-start": 1,
        //"grid-column-end": line3,
        //"grid-row-start": row1-start,
        //"grid-row-end": 2
    });
    $("#userinfo").closest(".k-window").css({
        top: "20%",
        left: "40%"
    });

    $("#new").closest(".k-window").css({
        top: "20%",
        left: "60%"
    });
    $("#infoIHK").closest(".k-window").css({
        top: "20%",
        left: "70%"
    });
    $("#termine").closest(".k-window").css({
        top: "40%",
        left: "20%",
        width: "60%"
    });
    $("#chat").closest(".k-window").css({
        top: "70%",
        left: "20%",
        width: "60%"
    });
}
function getImgByClass() {
    $.ajax({
        type: 'POST',
        url: 'Client/getStundenplanImage',
        async: false,
        success: function (Image) {
            if (Image != '') {
                debugger;
                var oImg = document.createElement("img");
                oImg.setAttribute('src', 'data:image/png;base64,' + Image.Image);
                $("#stundenplanImg").append(oImg);
            }
        }
    })
}
function init() {
    termineGrid();
    kendoWidgets();
    getImgByClass();
}
$( document ).ready(function(){
    init();
});