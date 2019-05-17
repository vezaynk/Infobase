import * as html2canvas from 'html2canvas';


let saveBtn = document.querySelector("#btnSaveChart");
if (!window.navigator.userAgent.match(/(MSIE|Trident)/))
    saveBtn.addEventListener("click", async () => addFooterToCanvas(await html2canvas(document.querySelector('.chartContainer'))));
else
    saveBtn.style.display = "none";

function addFooterToCanvas(canvas) {
    let footerImage = new Image();
    footerImage.addEventListener("load", () => {
        let graphHeight = canvas.height*(1000/canvas.width);
        let footerHeight = footerImage.height*(1000/footerImage.width);

        let containerCanvas = document.createElement('canvas');
        containerCanvas.width = 1000;
        containerCanvas.height = graphHeight+footerHeight;
        let ctx = containerCanvas.getContext('2d');
        
        
        ctx.drawImage(canvas, 0,0, 1000, graphHeight);

        ctx.drawImage(footerImage, 0,graphHeight, 1000, footerHeight);

        let image = containerCanvas.toDataURL("image/png", 1.0);//.replace("image/png", "image/octet-stream");
        let link = document.createElement('a');
        link.download = document.title + "-" + Date.now() + ".png";
        link.href = image;
        document.body.appendChild(link);
        link.click();
    })
    footerImage.src = "/img/exportfooter.png";


}