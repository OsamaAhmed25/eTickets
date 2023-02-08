var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }
    
});
function loadDataTable(status) {
    dataTable = $('#tblTable').DataTable({
        "ajax": {
            "url": "/Admin/Order/GetAll?status=" + status
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "25%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "10%" },
            {
                 "data": "id",
                "render": function (data) {
                    return   `                                                         <td>
    <a class="btn btn-primary mx-2" href="/Admin/Order/Details?orderId=${data}">
        <i class="bi bi-pencil-square"></i>
    </a>
   
</td>                                                     ` 
                 }
                 , "width": "5%"
            }

        ]
    });
}


