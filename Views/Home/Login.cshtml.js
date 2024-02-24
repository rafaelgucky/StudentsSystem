let name = document.getElementById("name");
let password = document.getElementById("password");
let button = document.getElementById("submit");
let autoLogin = true;

function Login() {
    if (name.Value == "" && password.Value == "") {
        autoLogin = false;
    }
    if (autoLogin) {
        button.click();
    }
}

setInterval(Login, 1000);