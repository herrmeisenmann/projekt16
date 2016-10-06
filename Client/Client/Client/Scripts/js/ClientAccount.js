var classesDataSource = null;
var professionDataSource = null;
var drp_berufDropDown = null;
var drp_klassenDropDown = null;

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
function init() {
    kendoElements();
}
$(document).ready(function () {
    init();
});