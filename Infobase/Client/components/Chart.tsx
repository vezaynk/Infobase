// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { renderChart } from '../renderChart';
import { ChartData, LanguageCode, ChartType } from '../types';

type ChartProps = { chartData: ChartData, animate: boolean, languageCode: LanguageCode; };

export const Chart: React.FC<ChartProps> = props => {
    const graphEl: React.MutableRefObject<SVGElement> = React.useRef(null);
    const [firstLoad, setFirstLoad] = React.useState(true);

    React.useEffect(() => {
        const doRender = animated => renderChart(graphEl.current, props.chartData, props.languageCode, animated);

        doRender(!firstLoad && props.animate);
        const deferredRender = setTimeout(() => doRender(true), 525);
        setFirstLoad(false);

        return () => {
            clearTimeout(deferredRender);
        };
    });

    return (
        <figure>
            <figcaption className="text-center" style={{ margin: "0 10px" }}>
                <strong>{props.chartData.title}</strong>
            </figcaption>

            <svg className="chart" ref={el => graphEl.current = el} id="graph" zoomAndPan="magnify" viewBox="0 0 820 500" preserveAspectRatio="xMidYMid meet" width="820px" height="500px">
                <defs>
                    <filter x="0" y="0" width="1" height="1" id="solid">
                        <feFlood floodColor="#ffffff"></feFlood>
                        <feComposite in="SourceGraphic"></feComposite>
                        <feComponentTransfer>
                            <feFuncA type="linear" slope="0.7"></feFuncA>
                        </feComponentTransfer>
                    </filter>
                    <pattern id="linePattern" patternUnits="userSpaceOnUse" width="10" height="10">
                        <path d="M 0,11 l 5,-5 M -3,3 l 2.5,-10 M 3.75,1.25 5 2.5,-2.5, 0" strokeWidth="4" shapeRendering="auto" stroke="red" strokeLinecap="square"></path>
                    </pattern>
                    <linearGradient id="gradient" x1="0%" y1="0%" x2="100%" y2="0" spreadMethod="pad">
                        <stop offset="0%" stopColor="steelblue" stopOpacity="1"></stop>
                        <stop offset="100%" stopColor="#56a0dd" stopOpacity="0"></stop>
                    </linearGradient>
                </defs>
                <g className="y-axis"></g>
                <g className="x-axis"></g>
                <text className="xAxisLabel" y="480" x="400" style={{ textAnchor: "middle", fontWeight: "bold", fontSize: "14px" }}></text>
                <text className="yAxisLabel" transform="rotate(-90)" x="-200" y="20" style={{ textAnchor: "middle", fontWeight: "bold", fontSize: "14px" }}></text>
                <g className="main" transform="translate(60,10)"></g>
                <g className="legend" transform="translate(600, 10)" display="dsdd">
                    <rect stroke="black" strokeOpacity="0.5" fill="white" opacity="0.5" height="130" width="200"></rect>

                    <text x="100" y="20" style={{ textAnchor: "middle", fontWeight: "bold", fontSize: "14px" }}>Legend</text>

                </g>
            </svg>
        </figure>
    );
};