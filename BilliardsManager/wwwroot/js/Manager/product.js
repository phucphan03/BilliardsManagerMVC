var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/product/getallproduct',
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
                "with": "10%",
                "className": "text-center align-middle",
            },
            {
                "data": 'price',
                "with": "15%",
                "className": "text-center align-middle",
            },
            {
                "data": 'categoryName',
                "with": "20%",
                "className": "text-center align-middle",
            },
            {
                "width": "30%",
                "render": function (data, type, row, meta) {
                    return `
                    <div class="btn-group d-flex justify-content-between" role="group">
                        <a href="/product/EditProduct?id=${row.productID}"
                           class="btn btn-dark flex-grow-1 mx-1">Sửa sản phẩm</a>

                        <a onclick="Delete('/product/DeleteProduct?id=${row.productID}')"
                           class="btn btn-danger text-white flex-grow-1 mx-1">Xóa sản phẩm</a>
                    </div>`;
                }
            }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/vi.json"
        }
    });
}
function Delete(url) {
    Swal.fire({
        title: "Bạn có chắc chắn là bạn muốn xóa",
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