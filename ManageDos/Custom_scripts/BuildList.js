$(document).ready(function () {

    $.ajax({
        url: "/ToDoLists/BuildToDoLists",
        success: function (result) {
            $("#ListDiv").html(result);
        }
    })
})