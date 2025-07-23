// JavaScript for Items Page Interactivity
document.addEventListener("DOMContentLoaded", function () {
    console.log("Items Page Loaded");

    // Example: Add hover effect to rows in the table
    const tableRows = document.querySelectorAll(".table tbody tr");
    tableRows.forEach(row => {
        row.addEventListener("mouseover", () => row.style.backgroundColor = "#e9ecef");
        row.addEventListener("mouseout", () => row.style.backgroundColor = "");
    });

    // Form validation (example)
    const saveButton = document.getElementById('<%= SaveBtn.ClientID %>');
    saveButton.addEventListener("click", function (e) {
        const itemName = document.getElementById('<%= INameTb.ClientID %>').value;
        if (!itemName) {
            e.preventDefault();
            alert("Item name is required!");
        }
    });
});
