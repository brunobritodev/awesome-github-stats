

function toggleTheme() {
  if (document.body.getAttribute("data-theme") !== "dark") {
    /* dark mode on */
    darkmode();
  } else {
    /* dark mode off */
    lightmode();
  }
}

function darkmode() {
    document.querySelector(".darkmode i").className = "gg-sun";

    localStorage.setItem("darkmode", "on");
    document.body.setAttribute("data-theme", "dark");
}

function lightmode() {
  document.querySelector(".darkmode i").className = "gg-moon";
  localStorage.setItem("darkmode", "off");
  document.body.removeAttribute("data-theme");
}


if (localStorage.getItem("darkmode") === null && window.matchMedia("(prefers-color-scheme: dark)").matches == true) {
    darkmode();
} else {
    if (localStorage.getItem("darkmode") == "on") {
        darkmode();
    } else {
        lightmode();
    }
}