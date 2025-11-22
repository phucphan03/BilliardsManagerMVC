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
                "width": "20%",
                "className": "text-center align-middle",
                "render": function (data, type, row, meta) {
                    let imageUrl = row.imageUrl ? row.imageUrl : 'https://res.cloudinary.com/dahinowbc/image/upload/v1763830996/No_Image_Available_a2pzvp.jpg';
                    return `
                        <div class="d-flex align-items-center">
                            <img src="${imageUrl}" alt="${data}" style="width:50px; height:50px; object-fit:cover; margin-right:10px;" class="rounded"/>
                            <span>${data}</span>
                        </div>
                    `;
                }
            },
            {
                "data": 'price',
                "width": "15%",
                "className": "text-center align-middle",
                "render": function (data, type, row, meta) {
                    if (data == null) return "";
                    return data.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                }
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
                           class="btn btn-dark flex-grow-1 mx-1">
                           <i class="fas fa-edit me-2"></i>Sửa sản phẩm
                        </a>

                        <a onclick="Delete('/product/DeleteProduct?id=${row.productID}')"
                           class="btn btn-danger text-white flex-grow-1 mx-1">
                           <i class="fas fa-trash-alt me-2"></i>Xóa sản phẩm
                        </a>
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