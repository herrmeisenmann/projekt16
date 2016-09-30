/**
 * Created by Nik on 27.09.2016.
 */
function showWidget(component){
    //$("#"+component).removeAttr("display");
    var window = $("#"+component).data("kendoWindow");
    window.open();
}
    var dataSource = new kendo.data.DataSource({
        data: [ { productName: "Tea", category: "Beverages" },
            { productName: "Coffee", category: "Beverages" },
            { productName: "Ham", category: "Food" },
            { productName: "Bread", category: "Food" }
        ]
    });
function init() {
    $("#termineGrid").kendoGrid({
        autoBind: false,
        dataSource: dataSource,
        width: 400
    });

    dataSource.read();

// initialize the widget
    $("#stundenplan").kendoWindow({
        title: "Stundenplan"
    });
    $("#userinfo").kendoWindow({
        title: "User Infos"
    });
    $("#termine").kendoWindow({
        title: "Termine"
    });
    $("#infoIHK").kendoWindow({
        title: "Prüfungstermine und infos"
    });
    $("#new").kendoWindow({
        title: "Folgt..."
    });
    $("#menu").kendoMenu({
        animation: { open: { effects: "fadeIn" } }
    });
// get the wrapper
    var winWrapper = $("#stundenplan").data("kendoWindow").wrapper; // returns div.k-window as a jQuery object

}
$( document ).ready(function(){
    init();
});