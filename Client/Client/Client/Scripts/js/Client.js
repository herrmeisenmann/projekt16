/**
 * Created by Nik on 27.09.2016.
 */
var schedule_id = 0;
var user_id = 0;
var appointmentsDataSource = null;
var newAppointmentWindow;
function showWidget(component){
    //$("#"+component).removeAttr("display");
    var window = $("#"+component).data("kendoWindow");
    window.open();
}
function kendoWindows() {
    // initialize the widget
    $("#stundenplan").kendoWindow({
        title: "Stundenplan",
        visible: false
    });
    $("#userinfo").kendoWindow({
        title: "User Infos",
        visible: false
    });
    $("#termine").kendoWindow({
        title: "Termine",
        visible: false
    });
    $("#infoIHK").kendoWindow({
        title: "Prüfungstermine und infos",
        visible: false
    });
    $("#new").kendoWindow({
        title: "Folgt...",
        visible: false
    });
    $("#chat").kendoWindow({
        title: "Klassenchat",
        visible: false
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
    appointmentsDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Client/getAppointmentsByUserId?id=" + id,
                dataType: "json"
            }
        }
    })
    $("#termineGrid").kendoGrid({
        dataSource: appointmentsDataSource,
        columns: [
            {
                field: "name",
                title: "Terminart"
            },
            {
                field: "subject",
                title: "Fach"
            },
            {
                field: "date",
                title: "Datum",
                template: '#= kendo.toString(kendo.parseDate(date, "dd.MM.yyyy" ), "dd.MM.yyyy" )#'
            },
            {
                field: "comment",
                title: "Kommentar"
            },
            {
                field: "grade",
                title: "Note"
            },
        ]
    });
}
function userInfo() {
        //Niklas
    var id = 8;
        $.ajax({
            url: "/Client/getUserById?id=" + id,
            success: function (data) {
                console.log(data);
                $("#username").text(data.username);
                $("#firstname").text(data.firstname);
                $("#lastname").text(data.lastname);
                $("#class").text(data.classroom.name);
                $("#profession").text(data.profession.name);
                schedule_id = data.classroom.timetable_id;
                user_id = data.id;
                console.log("Timetable_id: " + data.classroom.timetable_id);
            },
            async: false //setzen, sonst können keine Variablen gesetzt werden
        });
}
function getSchedule() {
    console.log("Schedule_id: " + schedule_id);
    $.ajax({
        url: "/Client/getSchedule?id=" + schedule_id,
        success: function (data) {
            $('#schedule').attr('src', data);
        }
    });
}
function newAppointment() {
    $("#newAppointmentWindow").kendoWindow({
        title: "Stundenplan",
        content: {
            url: "Client/_newAppointmentPartialView"
        }
    });
    newAppointmentWindow = $("#newAppointmentWindow").data("kendoWindow");
    //$('#appoint_date').data('kendoDatePicker').enable(true);
    newAppointmentWindow.open();
}
function saveAppointment() {
    var user_idVal = user_id;
    console.log("user_idVal: " + user_idVal);
    var termintypVal = $("#appoint_termintyp").val();
    var commentVal = $("#appoint_comment").val();
    var dateVal = $("#appoint_date").val();
    var subjectVal = $("#appoint_subject").val();
    var gradeVal = $("#appoint_grad").val();
    var dataString = 'userId=' + user_idVal + '&name=' + termintypVal + '&comment=' + commentVal + '&date=' + dateVal + '&subject=' + subjectVal + '&grade=' + gradeVal;
    console.log("saveAppointment: " + dataString)
    $.ajax({
        type: 'POST',
        data: dataString,
        url: '/Client/saveAppointment',
        success: function (data) {
            alert(data);
            appointmentsDataSource.read();
            newAppointmentWindow.close();
        }
    });

}
function init() {
        
        userInfo();
        //getSchedule();
        termineGrid();
        kendoWindows();
}
$(document).ready(function () {
    init();
});