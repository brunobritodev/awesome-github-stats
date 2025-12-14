let preview = {
    // default values
    defaults: {
        theme: "default",
        showIcons: "true",
    },
    // update the preview
    update: function () {
        // get parameter values from all .param elements
        const params = Array.from(document.querySelectorAll(".param")).reduce(
            (acc, next) => {
                let obj = { ...acc };
                let value = next.value;

                if (value.indexOf('#') >= 0) {
                    // if the value is colour, remove the hash sign
                    value = value.replace(/#/g, "");
                    if (value.length > 6) {
                        // if the value is in hexa and opacity is 1, remove FF
                        value = value.replace(/(F|f){2}$/, "");
                    }
                }
                obj[next.id] = value;
                return obj;
            },
            {}
        );
        // convert parameters to query string
        const encode = encodeURIComponent;
        let query = Object.keys(params)
            .filter((key) => params[key] !== this.defaults[key])
            .map((key) => encode(key) + "=" + encode(params[key]))
            .join("&");
        // generate links and markdown
        let user = document.getElementById("user").value;
        user = user == "" ? "brunobritodev" : user;
        if (query != "")
            query = "?" + query;
        const imageURL = `${window.location.origin}/user-stats/${user}${query}`;
        const demoImageURL = `${window.location.origin}/user-stats/${user}/preview${query}`;
        const repoLink = "https://git.io/awesome-stats-card";
        const md = `[![My Awesome Stats](${imageURL})](${repoLink})`;
        const rank = `${window.location.origin}/user-stats/${user}/rank`;
        const stats = `${window.location.origin}/user-stats/${user}/stats`;

        const html = `
  <a href="${window.location.origin}/index.html?${query}">
    <img  alt="${user}'s GitHub Stats" src="${imageURL}" />
  </a>`;
        // update html code
        document.querySelector(".html pre").innerText = html;
        // update rank link
        document.getElementById("rankLink").href = rank;
        // update stats link
        document.getElementById("statsLink").href = stats;
        
        // update image preview
        const previewContainer = document.getElementById("preview-container");
        if(previewContainer) {
             previewContainer.innerHTML = "Loading..."; // Optional loading text
             
             fetch(demoImageURL)
                .then(response => {
                    if (!response.ok) throw new Error("Network response was not ok");
                    return response.text();
                })
                .then(svgText => {
                    previewContainer.innerHTML = svgText;
                })
                .catch(error => {
                    console.error("Error fetching SVG:", error);
                    previewContainer.innerHTML = "Error loading preview.";
                });
        }
        document.querySelector(".md code").innerText = md;
        // disable copy button if username is invalid
        document.querySelectorAll(".copy-button").forEach(copyButton => {
            copyButton.disabled = !!document.querySelectorAll("#user:invalid").length;
        });
    },
    addProperty: function (property, value) {
        const selectElement = document.querySelector("#properties");
        // if no property passed, get the currently selected property
        if (!property) {
            property = selectElement.value;
        }

        // Handle specific logic for Border Radius vs Colors
        let isColor = property !== "Border Radius";
        let inputId = property;
        let defaultValue = value;

        if (property === "Border Radius") {
            inputId = "borderRadius"; 
            if (!defaultValue) defaultValue = "4";
        } else {
            if (!defaultValue) defaultValue = "#DD2727FF";
        }

        if (!selectElement.disabled) {
            // disable option in menu
            Array.prototype.find.call(
                selectElement.options,
                (o) => o.value == property
            ).disabled = true;
            // select first unselected option
            const firstAvailable = Array.prototype.find.call(
                selectElement.options,
                (o) => !o.disabled
            );
            if (firstAvailable) {
                firstAvailable.selected = true;
            } else {
                selectElement.disabled = true;
            }
            
            // label
            const label = document.createElement("label");
            label.innerText = property;
            label.setAttribute("data-property", property);
            
            // Input setup
            const input = document.createElement("input");
            input.id = inputId;
            input.name = inputId;
            input.setAttribute("data-property", property);
            input.value = defaultValue;
            
            // Apply the 'param' class so the update() function finds it
            input.className = "param";
            input.type = "text"; 

            if (isColor) {
                input.className += " jscolor"; // Add jscolor class for color picker
                input.setAttribute("data-jscolor", "{ format: 'hexa' }");
            } else {
                input.pattern = "[0-9]*"; 
            }

            // removal button
            const minus = document.createElement("button");
            minus.className = "minus btn";
            minus.setAttribute(
                "onclick",
                "return preview.removeProperty(this.getAttribute('data-property'));"
            );
            minus.innerText = "−";
            minus.setAttribute("data-property", property);
            
            // add elements
            const parent = document.querySelector(".advanced .parameters");
            parent.appendChild(label);
            parent.appendChild(input);
            parent.appendChild(minus);

            //initialise jscolor on element only if it is a color
            if (isColor) {
                jscolor.install(parent);
            }

            // update and exit
            this.update();
        }
        return false;
    },
    removeProperty: function (property) {
        const parent = document.querySelector(".advanced .parameters");
        const selectElement = document.querySelector("#properties");
        // remove all elements for given property
        parent
            .querySelectorAll(`[data-property="${property}"]`)
            .forEach((x) => parent.removeChild(x));
        // enable option in menu
        const option = Array.prototype.find.call(
            selectElement.options,
            (o) => o.value == property
        );
        selectElement.disabled = false;
        option.disabled = false;
        // update and exit
        this.update();
        return false;
    },
};

let clipboard = {
    copyMd: function (el) {
        // create input box to copy from
        const input = document.createElement("input");
        input.value = document.querySelector(".md code").innerText;
        document.body.appendChild(input);
        // select all
        input.select();
        input.setSelectionRange(0, 99999);
        // copy
        document.execCommand("copy");
        // remove input box
        input.parentElement.removeChild(input);
        // set tooltip text
        el.title = "Copied!";
    },
    copyHtml: function (el) {
        // create input box to copy from
        const input = document.createElement("input");
        input.value = document.querySelector(".html pre").innerText;
        document.body.appendChild(input);
        // select all
        input.select();
        input.setSelectionRange(0, 99999);
        // copy
        document.execCommand("copy");
        // remove input box
        input.parentElement.removeChild(input);
        // set tooltip text
        el.title = "Copied!";
    }
};

let tooltip = {
    reset: function (el) {
        // remove tooltip text
        el.removeAttribute("title");
    },
};

// Init a timeout variable to be used below
let timeout = null;
// refresh preview on interactions with the page
document.addEventListener('keyup', function (e) {
    // Clear the timeout if it has already been set.
    // This will prevent the previous task from executing
    // if it has been less than <MILLISECONDS>
    clearTimeout(timeout);

    // Make a new timeout set to go off in 1000ms (1 second)
    timeout = setTimeout(function () {
        preview.update();
    }, 1000);
});
document.addEventListener("click", () => preview.update(), false);

// when the page loads
window.addEventListener(
    "load",
    () => {
        // We fetch the font list from Fontsource API
        // to avoid Google Font API key
        fetch('https://api.fontsource.org/v1/fonts')
            .then(response => response.json())
            .then(data => {
                const select = document.getElementById('fontFamily');
                if(!select) return;

                data.forEach(font => {
                    const option = document.createElement('option');
                    option.value = font.family;
                    option.text = font.family;
                    select.appendChild(option);
                });
                
                const urlParams = new URLSearchParams(window.location.search);
                const fontParam = urlParams.get('fontFamily');
                if (fontParam) {
                    select.value = fontParam;
                    preview.update(); 
                }
            })
            .catch(err => {
                console.error("Failed to fetch font list:", err);
            });

        // set input boxes to match URL parameters
        new URLSearchParams(window.location.search).forEach((val, key) => {
            let paramInput = document.querySelector(`#${key}`);
            if (paramInput) {
                // set parameter value
                paramInput.value = val;
            } else {
                // add advanced property
                document.querySelector("details.advanced").open = true;
                preview.addProperty(key, val);
            }
        });
        // update previews
        preview.update();
    },
    false
);