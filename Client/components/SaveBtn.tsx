
import * as React from 'react';
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

export const SaveBtn: React.FC<{ label: string, children: React.ReactElement | React.ReactElement[] }> = props => {
    const saveArea = React.useRef(null);
    const save = () => {
        import("html2canvas").then(async html2canvas => {
            // @ts-ignore: Calling default is necessary, but TS doesn't know that
            addFooterToCanvas(await html2canvas.default(saveArea.current))
        })
    };
    return (
        <div>
            <YesScript ieAsNoScript={true}>
                <button className="btn btn-success btn-sm" onClick={() => save()}>{props.label}</button>
            </YesScript>
            <div style={{width: 0, height: 0, overflow: "hidden"}}>
                <div style={{width: "820px"}} className="chartContainer" ref={saveArea}>
                    { props.children }
                </div>
            </div>
        </div>
    )
}