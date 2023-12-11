$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $("#tblData").DataTable({
    "ajax": { url: '/Admin/OrderManagement/GetAll'},
    "columns": [
    { data: 'order.user.fullName' , "width": "15%" },
    { data: 'order.user.phoneNo' , "width": "15%" },
    { data: 'order.user.email' , "width": "15%" },
    { data: 'order.order_date', "width": "15%" },
    { data: 'order.order_total' , "width": "15%" },
    { data: 'paymentStatus' , "width": "15%"},
    { data: 'order_status' , "width": "15%"},
    { data: 'action' , "width": "15%"}
]});
}
