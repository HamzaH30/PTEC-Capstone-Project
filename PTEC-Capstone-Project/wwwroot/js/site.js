// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function menuClick() {
  menuBtn.classList.toggle("opened-menu");
  mobileMenu.classList.toggle("show-menu");
  overlay.classList.toggle("overlay");
}

var menuBtn = document.getElementById("menu-btn");
var mobileMenu = document.getElementById("mobile-menu");
var overlay = document.getElementById("overlay");

menuBtn.addEventListener("click", menuClick);
