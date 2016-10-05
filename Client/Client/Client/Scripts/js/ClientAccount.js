function kendoElements(){
    var klassenDataSource = new kendo.data.DataSource({
        transport:{
            read:{
                url: "/ClientAccount/getClassesJson",
                dataType: "json"
            }
        }
    })
    $("#klassenDropDown").kendoDropDownList({
        dataSource: klassenDataSource
    });
    klassenDataSource.read();
    console.log(klassenDataSource);
}
function init() {
    kendoElements();
}
$(document).ready(function () {
    init();
});