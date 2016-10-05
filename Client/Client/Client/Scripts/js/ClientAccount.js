var classesDataSource;

function kendoElements(){
    $.ajax({
        url: "/ClientAccount/getClassesJson",
        success: function (data) {
            console.log(data);
            classesDataSource = data;
        }
    });
    $("#klassenDropDown").kendoDropDownList({
        dataSource: classesDataSource
    });
}
function init() {
    kendoElements();
}
$(document).ready(function () {
    init();
});