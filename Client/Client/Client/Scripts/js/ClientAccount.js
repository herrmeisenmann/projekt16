/**
 * Created by Niklas Grieger on 27.09.2016.
 * js for page - /ClientAccount
 */

//Global Variablen
var classesDataSource = null;
var professionDataSource = null;
var drp_berufDropDown = null;
var drp_klassenDropDown = null;
var user_name = localStorage['user_name'];

//Initialisieren von Kendo Elementen
function kendoElements() {
    classesDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/ClientAccount/getClasses",
                dataType: "json"
            }
        }
    });
    $("#drp_klassenDropDown").kendoDropDownList({
        dataSource: classesDataSource,
        dataValueField: "id",
        dataTextField: "name"
    });
    drp_klassenDropDown = $("#drp_klassenDropDown").data("kendoDropDownList");

    professionDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/ClientAccount/getProfessions",
                dataType: "json"
            }
        }
    });
    $("#drp_berufDropDown").kendoDropDownList({
        dataSource: professionDataSource,
        dataValueField: "id",
        dataTextField: "name"
    });
    drp_berufDropDown = $("#drp_berufDropDown").data("kendoDropDownList");
}
//Sendet die Values in den html elementen an die saveUser Methode in dem Controller - ClientAccountController
function saveUser() {
    var usernameVal = $("#regist_username").val();
    var firstnameVal = $("#regist_fistname").val();
    var lastnameVal = $("#regist_lastname").val();
    var passwordVal = $("#regist_password").val();
    var profession_idVal = drp_berufDropDown.value();
    var class_idVal = drp_klassenDropDown.value();
    var dataString = 'username=' + usernameVal + '&firstname=' + firstnameVal + '&lastname=' + lastnameVal + '&password=' + passwordVal + '&profession_id=' + profession_idVal + '&class_id=' + class_idVal;
    console.log("saveUser: "+dataString)
    $.ajax({
        type:'POST',
        data:dataString,
        url:'/ClientAccount/saveUser',
        success:function(data) {
            alert(data);
        }
    });
    
}
//Wird aufgerufen wenn auf "Einloggen" geklickt wird. Sendet die Values in den html elementen an die checkLogin Methode in dem Controller - ClientAccountController
function checkLogin() {
    var usernameVal = $("#login_username").val();
    var passwordVal = $("#login_password").val();
    var dataString = 'username=' + usernameVal + '&password=' + passwordVal;
    console.log("loginUser: " + dataString)
    $.ajax({
        type: 'POST',
        data: dataString,
        url: '/ClientAccount/checkLogin',
        success: function (data) {
            alert(data);
            localStorage['user_name'] = $("#login_username").val();
            console.log(localStorage['user_name']);
            window.location.href = "/Client";
        }
    });
}
//Initialisieren der Funktion beim laden der Page
function init() {
    kendoElements();
}
//Init() wird aufgerufen, wenn die Page bereit ist
$(document).ready(function () {
    init();
});