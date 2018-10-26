$(document).ready(function () {
    var link = window.location.href;
    var listId;
    $ind = link.indexOf('ListId');
    if ($ind != -1) {
        $res = link.substr($ind, 10);
        listId = $res.substr(7, 3);
    }
    $.ajax({
        url: "/ToDoItems/BuildToDoTable/",
        data: { ListId: listId},
        success: function (result) {
            $("#tableDiv").html(result);
        }
    })
})