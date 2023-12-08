 //to make globally available we use "window"
window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", {timeOut: 5000}); 
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 }); 
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
           "Success Notification",
            message,
           'success'
        );
    }

    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        );
    }
}

/*it is not returning anything so we will use invokevoidasync method*/

function ShowDeleteConfirmationModal() {

    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {

    $('#deleteConfirmationModal').modal('hide');
}
 