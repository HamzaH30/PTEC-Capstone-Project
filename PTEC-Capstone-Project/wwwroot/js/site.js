// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function menuClick() {
  menuBtn.classList.add("opened-menu");
  mobileMenu.classList.add("show-menu");
}

var menuBtn = document.getElementById("menu-btn");
var mobileMenu = document.getElementById("mobile-menu");

menuBtn.addEventListener("click", menuClick);
