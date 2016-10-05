var classesDataSource = null;
var drp_berufDropDown = null;
var drp_klassenDropDown = null;

function kendoElements(){
    //$.ajax({
    //    url: "/ClientAccount/getClasses",
    //    success: function (data) {
    //        console.log(data);
    //        classesDataSource = data;
    //    }
    //});
    $("#drp_klassenDropDown").kendoDropDownList({
        dataSource: [
                { name: "Klasse1", id: 1 },
                { name: "Klasse2", id: 2 }
        ]
    });
    drp_klassenDropDown = $("#drp_klassenDropDown").data("kendoDropDownList");
    $("#drp_berufDropDown").kendoDropDownList({
        dataSource: [
                { name: "Beruf1", id: 1 },
                { name: "Beruf2", id: 2 }
        ],
        dataValueField: "id"
    });
    drp_berufDropDown = $("#drp_berufDropDown").data("kendoDropDownList");
}
function saveUser() {
    var firstnameVal = $("#regist_fistname").val();
    var lastnameVal = $("#regist_lastname").val();
    var passwordVal = $("#regist_password").val();
    var profession_idVal = drp_berufDropDown.val();
    var class_idVal = drp_klassenDropDown.val();
    var dataString = 'firstname='+firstnameVal+'&lastname='+firstnameVal+'&password='+passwordVal+'&profession_id='+profession_idVal+'&class_id='+class_idVal;

    $.ajax({
        type:'POST',
        data:dataString,
        url:'/ClientAccount/saveUser',
        success:function(data) {
            alert(data);
        }
    });
            //    url: "/ClientAccount/getClassesJson",
            //    success: function (data) {
            //        console.log(data);
            //        classesDataSource = data;
            //    }
            //});
    
}
function init() {
    kendoElements();
}
$(document).ready(function () {
    init();
});