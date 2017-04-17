document.addEventListener("DOMContentLoaded", function () {
    toggleActive();
});


function toggleActive() {
    var elements = Array.from(document.querySelectorAll(".menu>li"));
    for (var i = 0; i < elements.length; i++) {
        elements[i].addEventListener("click", function (ev) {
            if (this.classList.contains("active")) {
                this.className = "";
            } else {
                this.className = "active";
            }
        });
    }   
}



