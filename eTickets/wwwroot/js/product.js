var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblTable').DataTable({
        "ajax": {
            "url" : "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                 "data": "id",
                "render": function (data) {
                    return   `                                                         <td>
    <a class="btn btn-outline-primary btn-sm btn-rounded" href="/Admin/Product/Upsert?id=${data}">
        <i class="bi bi-pencil-square"></i> Edit
    </a>
   
    <a class="btn btn-outline-danger btn-sm btn-rounded"
        onclick=Delete('/Admin/Product/Delete/${data}')>
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