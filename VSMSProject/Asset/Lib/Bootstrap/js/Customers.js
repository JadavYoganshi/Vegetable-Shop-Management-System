document.addEventListener("DOMContentLoaded", function () {
    console.log("Customers Page Loaded");

    const saveButton = document.getElementById('<%= SaveBtn.ClientID %>');
    saveButton.addEventListener("click", function (e) {
        const customerName = document.getElementById('<%= CNameTb.ClientID %>').value;
        if (!customerName) {
            e.preventDefault();
            alert("Customer name is required!");
        }
    });

    const tableRows = document.querySelectorAll(".table tbody tr");
    tableRows.forEach(row => {
        row.addEventListener("mouseover", () => row.style.backgroundColor = "#e9ecef");
        row.addEventListener("mouseout", () => row.style.backgroundColor = "");
    });
});
