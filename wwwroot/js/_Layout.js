let body = document.querySelector("body");
let userMenu = document.getElementById("user-menu");
let menu = document.getElementById("hamburger-menu");
MobileUserMenu();
UserMenu()
//document.getElementById("user-image").click();

function UserMenu(){
    //Acessar o elemento e dar display
    
    if(userMenu.style.width == "0px"){
        userMenu.style.width = "200px";
        userMenu.style.height = "80%";
    }
    else{
        userMenu.style.width = "0px";
        userMenu.style.height = "0px";
    }
}

function CloseUserMenu(){
    userMenu.style.width = "0px";
    userMenu.style.height = "0px";
}

function MobileUserMenu() {
    menu.style.display = menu.style.display == "none" ? "grid" : "none";
}