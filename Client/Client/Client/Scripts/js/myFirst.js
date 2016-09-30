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
        title: "Stundenplan",
        id: "test"
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
    $("#chat").kendoWindow({
        title: "Klassenchat"
    });
    $("#menu").kendoMenu({
        animation: { open: { effects: "fadeIn" } }
    });
    
    //Anordnen der Widgets
    $("#stundenplan").closest(".k-window").css({
        top: "20%",
        left: "20%"
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
$( document ).ready(function(){
    init();
});