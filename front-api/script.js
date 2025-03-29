document.getElementById("loginForm").addEventListener("submit", async function(event) {
    event.preventDefault();
    fetch("http://localhost:5023/api/account/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Method: document.getElementById("username").value,
            Password: document.getElementById("password").value
        })
    })
    .then(response => response.json())
    .then(data => {
        if (data.token) {
            localStorage.setItem("token", data.token);
            
            
            window.location.href = "products.html";
        } else {
            document.getElementById("errorMessage").textContent = "Login failed: " + (data.message || "Invalid credentials");
            console.error("Login failed:", data.message);
        }
    })
    .catch(error => {
        document.getElementById("errorMessage").textContent = "An error occurred. Please try again.";
        console.error("Error:", error);
    });
});