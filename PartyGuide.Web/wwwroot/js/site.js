$(document).ready(function () {
	$(".delete-btn").click(function (e) {
		var serviceId = $(this).data("service-id");

		Swal.fire({
			title: "Are you sure you want to delete this service?",
			showDenyButton: true,
			showCancelButton: false,
			confirmButtonText: "No",
			denyButtonText: `Delete`
		}).then((result) => {
			if (result.isDenied) {
				// Send an AJAX request to delete the service
				$.ajax({
					url: "/Service/DeleteService",
					type: "GET", // or "GET" depending on your server-side implementation
					data: { id: serviceId },
					success: function (result) {
						if (result.success) {

							Swal.fire({
								title: "Deleted!",
								text: "The service has been deleted.",
								icon: "success",
								confirmButtonText: "OK"
							}).then(() => {
								// Reload the page after the user clicks "OK" in the Swal popup
								location.reload();
							});
						}
						else {
							Swal.fire({
								title: "Error!",
								text: "There was a problem deleting the service.",
								icon: "error",
								confirmButtonText: "OK"
							});
						}
					},
					error: function () {
						Swal.fire("Error deleting the service", "", "error");
					}
				});
			} else if (result.isConfirmed) {
				Swal.fire("The service has not been deleted", "", "info");
			}
		});
	});
});