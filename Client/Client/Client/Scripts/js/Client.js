/**
 * Created by Nik on 27.09.2016.
 */
function showWidget(component){
    //$("#"+component).removeAttr("display");
    var window = $("#"+component).data("kendoWindow");
    window.open();
}
function kendoWindows() {
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
function termineGrid() {
    //Niklas
    var id = 8;
    //var dataSource = new kendo.data.DataSource({
    //    data: [{ productName: "Tea", category: "Beverages" },
    //        { productName: "Coffee", category: "Beverages" },
    //        { productName: "Ham", category: "Food" },
    //        { productName: "Bread", category: "Food" }
    //    ]
    //});
    var appointmentsDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Client/getAppointmentsByUserId=id=" + id,
                dataType: "json"
            }
        }
    })
    $("#termineGrid").kendoGrid({
        dataSource: appointmentsDataSource
    });
}
function userInfo() {
        //Niklas
    var id = 8;
    var class_id = 0;
    var profession_id = 0;
        $.ajax({
            url: "/Client/getUserById?id=" + id,
            success: function (data) {
                console.log(data);
                $("#firstname").text(data.firstname)
                $("#lastname").text(data.lastname)
                class_id = data.class_id;
                profession_id = data.profession_id;
            }
        });
        if (class_id > 0) {
            $.ajax({
                url: "/Client/getClassById?id=" + class_id,
                success: function (data) {
                    console.log(data);
                    $("#class").text(data.name)
                }
            });
        }
        else {
            alert("Fehler in JavaScript:\n Klasse des Schülers konnte nicht geladen werden!\n class_id = " + class_id);
        }
        if (profession_id > 0) {
            $.ajax({
                url: "/Client/getProfessionById?id=" + profession_id,
                success: function (data) {
                    console.log(data);
                    $("#profession").text(data.name)
                }
            });
        }
        else {
            alert("Fehler in JavaScript:\n Beruf des Schülers konnte nicht geladen werden!\n profession_id = " + class_id);
        }
}
    function init() {
        userInfo();
        termineGrid();
        kendoWindows();
}
$( document ).ready(function(){
    init();
});