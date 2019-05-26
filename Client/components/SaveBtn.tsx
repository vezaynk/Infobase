
import * as React from 'react';
import * as html2canvas from 'html2canvas';
import { YesScript } from '../HOC/YesScript';

function addFooterToCanvas(canvas: HTMLCanvasElement) {
    let footerImage = new Image();
    footerImage.addEventListener("load", () => {
        let graphHeight = canvas.height * (820 / canvas.width);
        let footerHeight = footerImage.height * (820 / footerImage.width);

        let containerCanvas = document.createElement('canvas');
        containerCanvas.width = 820;
        containerCanvas.height = graphHeight + footerHeight;
        let ctx = containerCanvas.getContext('2d');


        ctx.drawImage(canvas, 0, 0, 820, graphHeight);

        ctx.drawImage(footerImage, 0, graphHeight, 820, footerHeight);

        let image = containerCanvas.toDataURL("image/png", 1.0);//.replace("image/png", "image/octet-stream");
        let link = document.createElement('a');
        link.download = document.title + "-" + Date.now() + ".png";
        link.href = image;
        document.body.appendChild(link);
        link.click();
    })
    footerImage.src = "/src/img/exportServer.png";


}

export const SaveBtn: React.FC<{ label: string, target: string }> = props => {
    const save = async () => addFooterToCanvas(await html2canvas(document.querySelector(props.target)));
    return (
        <YesScript ieAsNoScript={true}>
            <button className="btn btn-success btn-sm" onClick={() => save()}>{props.label}</button>
        </YesScript>
    )
}