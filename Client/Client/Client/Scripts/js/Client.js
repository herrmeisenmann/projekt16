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
    }).data("kendoWindow").center();
    $("#userinfo").kendoWindow({
        title: "User Infos",
        visible: false
    }).data("kendoWindow").center();
    $("#termine").kendoWindow({
        title: "Termine",
        visible: false
    }).data("kendoWindow").center();
    $("#infoIHK").kendoWindow({
        title: "Prüfungstermine und infos",
        visible: false
    }).data("kendoWindow").center();
    $("#new").kendoWindow({
        title: "Folgt...",
        visible: false
    }).data("kendoWindow").center();

    $("#menu").kendoMenu({
        animation: { open: { effects: "fadeIn" } }
    });

}
function termineGrid() {
    //Niklas
    appointmentsDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Client/getAppointmentsByUserId?id=" + user_id,
                dataType: "json"
            }
        }
    });
    console.log(appointmentsDataSource.data.length);
        $("#termineGrid").kendoGrid({
            dataSource: appointmentsDataSource,
            columns: [
                {
                    field: "name",
                    title: "Terminart",
                    width: "40px"
                },
                {
                    field: "subject",
                    title: "Fach",
                    width: "40px"
                },
                {
                    field: "date",
                    title: "Datum",
                    template: '#= kendo.toString(kendo.parseDate(date, "dd.MM.yyyy" ), "dd.MM.yyyy" )#',
                    width: "40px"
                },
                {
                    field: "comment",
                    title: "Kommentar",
                    width: "40px"
                },
                {
                    field: "grade",
                    title: "Note",
                    width: "40px"
                },
            ]
        });
}
function userInfo() {
        //Niklas
    //localStorage['user_name'] = Lokal gespeicherte username nach dem login
    if (localStorage['user_name'] != "") {
        $.ajax({
            url: "/Client/getUserByName?username=" + localStorage['user_name'],
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
    else {
        alert("User mit username: " + localStorage['user_name'] + " nicht gefunden!\nLog dich bitte vorher ein");
        window.location.href = "/ClientAccount/Login";
    }
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
function loadChatMessages() {
    $.ajax({
        url: "/Client/getChat",
        success: function (data) {
            //Macht breaks im Json
            results = JSON.stringify(data, null, "\n");
            $("#chatbox").append(results);
        }
    });
}
function getChat() {
    loadChatMessages();
    $("#chat").kendoWindow({
        title: "Chat",
        content: {
            url: "/Client/_chatPartialView"
        }
    }).data("kendoWindow").center().open();
    chatWindow = $("#chat").data("kendoWindow");
}

function writeChat() {
    var usernameVal = $("#chat_username").val();
    var messageVal = $("#chat_massage").val();
    var dataString = 'user=' + usernameVal + '&message=' + messageVal;
    $.ajax({
        type: 'POST',
        data: dataString,
        url: '/Client/writeChat',
        success: function (data) {
            alert("Nachricht erfolgreich übermittelt");
            loadChatMessages();
        }
    });
}
function newAppointment() {
    $("#newAppointmentWindow").kendoWindow({
        title: "Neuer Termin",
        content: {
            url: "/Client/_newAppointmentPartialView"
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
function logout() {
    localStorage['user_name'] = "";
    window.location.href = "/ClientAccount/Login";
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