document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("login-form");
    const signupForm = document.getElementById("signup-form");

    if (loginForm) {
        loginForm.addEventListener("submit", function (e) {
            e.preventDefault();
            const email = document.getElementById("email").value.trim();
            const password = document.getElementById("password").value;

            const user = JSON.parse(localStorage.getItem("user"));

            if (!user) {
                alert("Пользователь не найден. Зарегистрируйтесь.");
                return;
            }

            if (user.email === email && user.password === password) {
                alert("Вход успешен!");
                window.location.href = "main.html";
            } else {
                alert("Неверный email или пароль.");
            }
        });
    }

    if (signupForm) {
        signupForm.addEventListener("submit", function (e) {
            e.preventDefault();
            const name = document.getElementById("name").value.trim();
            const email = document.getElementById("email").value.trim();
            const password = document.getElementById("password").value;

            if (!name || !email || !password) {
                alert("Пожалуйста, заполните все поля.");
                return;
            }

            if (password.length < 6) {
                alert("Пароль должен быть минимум 6 символов.");
                return;
            }

            const newUser = { name, email, password };
            localStorage.setItem("user", JSON.stringify(newUser));

            alert("Регистрация успешна! Теперь войдите.");
            window.location.href = "login.html";
        });
    }
});
