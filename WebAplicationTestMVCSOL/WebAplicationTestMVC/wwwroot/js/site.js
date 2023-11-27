
console.clear();

Vue.component('file-upload', {

    template: `
<div class="file-upload">
  <label class="file-upload__label">
    <slot></slot>
    <input ref="input" @change="loadFiles" accept=".svg" class="file-upload__input" type="file" />
  </label>
  <div class="file-upload__overlay"></div>
</div>
`,

    data: () => ({
        dragging: false,
    }),


    methods: {


        loadFiles(e) {
            e = e || window.event;
            this.dragLeave(e);

            var files = Array.from(e.target.files || e.dataTransfer.files || this.$ref.input.files),
                len = files.length,
                i = 0,
                completed = [];

            files.forEach((file) => {
                var reader = new FileReader();
                reader.onloadend = (ev) => {
                    this.$emit('load', reader.result, reader, file);

                };
                reader.readAsText(file);
               
            });
        },

        dragOver(e) { e.preventDefault(); /* Essential */ },
        dragEnter() { this.dragging = true },
        dragLeave(e) { this.dragging = false; e.preventDefault(); },


    }
});


new Vue({
    el: '.svg-bg',
    data: () => ({
        input: `<svg xmlns='http://www.w3.org/2000/svg' width='26' height='26' viewBox="0 0 8 8">
  <circle cx="1" cy="1" r="1" opacity='0.25' />
  <circle cx="5" cy="5" r="1" opacity='0.25' />
</svg>`
    }),

    computed: {
        output() {
            let url = this.encodeSVG(this.optimized);
            document.body.style.backgroundImage = url;
            let msg = 'background-image: ' + url + ';' +
                '<br >/* SVG encoded with https://cdpn.io/rrOZQQ */';
            return msg;
        },

        optimized() {
            return this.input
                .replace(/\<\?xml.+\?\>/g, '')
                .replace(/(\<\!DOCTYPE(.*?)\>)/g, '')
                .replace(/([\s\n]+)/g, ' ');
        }
    },

    methods: {

        loaded(f) {
            console.log('file loaded!', typeof f, f);
            this.input = f;
        },

        copy() {
            var range = document.createRange();
            range.selectNode(this.$refs.output);
            window.getSelection().addRange(range);

            try {
    
                var successful = document.execCommand('copy');
                var msg = successful ? 'successful' : 'unsuccessful';
                console.log('Copy email command was ' + msg);
            } catch (err) {
                console.log('Oops, unable to copy');
            }

            window.getSelection().removeAllRanges();
        },

        encodeSVG(svg) {
            svg = svg + '';
            var b64 = encodeURIComponent(svg.replace(/\<\?xml.+\?\>|\<\!DOCTYPE.+]\>/g, ''))
                .replace(/%20/g, " ")
                .replace(/%3D/g, "=")
         https://codepen.io/tigt/post/optimizing-svgs-in-data-uris
                .replace(/%3A/g, ':') 
                .replace(/%2F/g, '/') 
                .replace(/%22/g, "'"); 
            return 'url("data:image/svg+xml;charset=utf-8,' + b64 + '")';
        }
    }
});



const logo = document.querySelectorAll("#logo path");

for (let i = 0; i < logo.length; i++) {
    console.log(`Letter ${i} is ${logo[i].getTotalLength()}`);
}

const checkbox = document.querySelector("#input");

function changeTheme() {
    if (checkbox.checked) {
        document.body.id = "dark"
    }
    else {
        document.body.id = "";
    }
}


/*snow*/

(function () {
    var h = '';
    for (var i = 0; i < 600; i++) {
        var y = Math.floor(Math.random() * 100),
            x = Math.floor(Math.random() * 100),
            s = Math.floor(Math.random() * 10) + 0.5;
        h += '<div class="d" style="top:' + y + '%;left:' + x + '%;-webkit-animation-duration:' + s + 's;"></div>';
    }
    $('#dots').html(h);
})();

if (location.href.length != 100) {
    setTimeout(function () {
        window.location.reload(1);
    }, 50000);
}
