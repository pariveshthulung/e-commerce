$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $("#tblData").DataTable({
    "ajax": { url: '/Admin/OrderManagement/GetAll'},
    "columns": [
    { data: 'user.fullName' , "width": "15%" },
    { data: 'user.phoneNo' , "width": "15%" },
    { data: 'user.email' , "width": "15%" },
    { data: 'order_date', "width": "15%" },
    { data: 'order_total' , "width": "15%" },
    { data: 'paymentStatus' , "width": "15%"},
    { data: 'order_status' , "width": "15%"}
]});
}
