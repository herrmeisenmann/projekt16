/**
 * Created by Niklas Grieger on 27.09.2016.
 * js for page - /Client
 */

//Globale Variablen
var schedule_id = 0;
var user_id = 0;
var appointmentsDataSource = null;
var newAppointmentWindow;

//Öffnet das jeweilige Window, wenn in dem menu auf ein element geklickt wird
function showWidget(component){
    var window = $("#"+component).data("kendoWindow");
    window.open();
}
//Initialisieren der KendoWindows + Menu
function kendoWindows() {
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
//Initialisieren der Tabelle mit den Terminen
function termineGrid() {
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
        
        getAverage();
}
//Lädt den aus der Methode getGradAvg in dem Controller - ClientController - den aus den Noten berechneten Durchschnitt 
function getAverage(){
        $.ajax({
            url: "/Client/getGradAvg?id=" + user_id,
            success: function (data) {
                console.log(data);
                $("#average").val(data);
            },
            async: false
        });
}
//Sendet Values aus den html element an die Methode getUserByName in dem Controller - ClientController
function userInfo() {
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
        //Wenn kein Username lokal gespeichert ist, gibt es eine Meldung und der User wird wieder zu der Login Page weitergeleitet
        alert("User mit username: " + localStorage['user_name'] + " nicht gefunden!\nLog dich bitte vorher ein");
        window.location.href = "/ClientAccount/Login";
    }
}
//Lädt den Stundenplan als bild mit der enprechenden stundenplan_id aus dem Controller - ClientController - und lädt es in ein img html element
function getSchedule() {
    console.log("Schedule_id: " + schedule_id);
    $.ajax({
        url: "/Client/getSchedule?id=" + schedule_id,
        success: function (data) {
            $('#schedule').html('<img src="data:image/png;base64,'+data+'" />');
        }
    });
}
//Lädt die Nachrichten im Chat aus dem Controller - ClientController
function loadChatMessages() {
    $.ajax({
        url: "/Client/getChat",
        success: function (data) {
            //Macht breaks im Json
            results = JSON.stringify(data, null, "\n");
            $("#chatbox").text(results);
        }
    });
}
//Initialisiert das Window "Chat" und ruft in dem Window die Chat partial View auf
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
//Sendet Values aus html Elementen an die Methode writeChat in dem Controller - ClientController
function writeChat() {
    var usernameVal = $("#chat_username").val();
    var messageVal = $("#chat_massage").val();
    var dataString = 'username=' + usernameVal + '&message=' + messageVal;
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
//Wenn in dem Window "Termine" auf "Neuer Termin" geklickt wird, wird ein Window initialisiert mit Inhalt einer Partial View
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
//Sendet Values aus html Elementen an die Methode saveAppointment in dem Controller - ClientController
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
            //Lädt die "Termin" Tabelle neu, sodass sofort der neue Termin erscheint
            appointmentsDataSource.read();
            getAverage();
            newAppointmentWindow.close();
        }
    });

}
//Wenn auf das html Element "Ausloggen" geklickt wird, wird der lokal gespeicherte Username "gelöscht"
function logout() {
    localStorage['user_name'] = "";
    window.location.href = "/ClientAccount/Login";
}
//Initialisiert alle Funktionen beim Laden der Page
function init() {
    userInfo();
    getSchedule();
    termineGrid();
    kendoWindows();
}
//Führt das Init() aus, wenn die Page bereit ist
$(document).ready(function () {
    init();
});