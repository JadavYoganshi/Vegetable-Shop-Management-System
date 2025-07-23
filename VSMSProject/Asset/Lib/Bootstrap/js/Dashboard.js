// JavaScript for Optional Interactivity
document.addEventListener("DOMContentLoaded", function () {
    console.log("Dashboard scripts loaded successfully.");

    // Example: Add interactive behavior to dashboard cards
    const cards = document.querySelectorAll(".dashboard-card");
    cards.forEach(card => {
        card.addEventListener("mouseover", () => card.style.boxShadow = "0px 8px 15px rgba(0, 0, 0, 0.2)");
        card.addEventListener("mouseout", () => card.style.boxShadow = "");
    });
});
