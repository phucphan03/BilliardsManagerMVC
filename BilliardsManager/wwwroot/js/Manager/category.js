var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/category/getallcategories',
            type: 'GET',
            dataSrc: 'data'
        },
        "columns": [
            {
                "data": null,
                "width": "10%",
                "className": "text-center align-middle",
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": 'name',
                "with": "20%",
                "className": "text-center align-middle",
            },
            {
                "data": 'productCount',
                "with": "20%",
                "className": "text-center align-middle",
            },
            {
                "data": 'productCount',
                "width": "40%",
                "render": function (data, type, row) {
                    if (data === 0) {
                        return `
                        <div class="btn-group d-flex justify-content-between" role="group">
                            <a href="/Category/EditCategories?id=${row.id}" 
                               class="btn btn-dark flex-grow-1 mx-1">Sửa danh mục</a>

                            <a onclick="Delete('/Category/DeleteCategories?id=${row.id}')" 
                               class="btn btn-danger text-white flex-grow-1 mx-1">Xóa danh mục</a>
                        </div>`;
                    } else {
                        return `
                            <div class="btn-group d-flex justify-content-between" role="group">
                                <a href="/Category/EditCategories?id=${row.id}" class="btn btn-dark flex-grow-1 mx-1">Sửa danh mục</a>
                            </div>`;
                    }
                }
            }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/vi.json"
        }
    });
}
function Delete(url) {
    console.log("Delete function called with URL:", url);
    Swal.fire({
        title: "Bạn có chắc chắn là bạn mua xóa",
        text: "Bạn không thể hoàn tác!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xóa",
        cancelButtonText: "Hủy"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    toastr.success(data.message);
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                },
                error: function (xhr) {
                    toastr.error("Có lỗi xảy ra khi xóa danh mục!");
                }
            })
        }
    });
}