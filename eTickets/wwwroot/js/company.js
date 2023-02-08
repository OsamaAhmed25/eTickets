var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblTable').DataTable({
        "ajax": {
            "url" : "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneNumber", "width": "10%" },
            {
                 "data": "id",
                "render": function (data) {
                    return   `                                                         <td>
    <a class="btn btn-outline-primary btn-sm btn-rounded" href="/Admin/Company/Upsert?id=${data}">
        <i class="bi bi-pencil-square"></i> Edit
    </a>
    <a class="btn btn-outline-danger btn-sm btn-rounded"
        onclick=Delete('/Admin/Company/Delete/${data}')>
        <i class="bi bi-trash3"></i> Delete
    </a>
</td>                                                     ` 
                 }
                 , "width": "15%"
            }

        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
          
                $.ajax({
                    url: url,
                    type: "DELETE",
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            toastr.success(data.message)
                        }
                        else {
                            toastr.error(data.message)

                        }
                    }
                })
         }  
    })
}