import * as React from 'react';
import * as ReactDOMServer from 'react-dom/server';
import { Charting } from './Charting';
import { JSDOM } from 'jsdom';
import { renderChart } from '../renderChart';
import { dataExplorerStore } from '../store/dataExplorer';

type ChartingProps = {animate: boolean};

export function SSCharting(props: ChartingProps) {
    let html = ReactDOMServer.renderToString(React.createElement(Charting, { animate: false }));
    let fakeDOM = new JSDOM(html);
    let graph = fakeDOM.window.document.querySelector("#graph");
    renderChart(graph, dataExplorerStore.getState().chartData, false, -1, -1, -1, false, console.log)
    return (<figure dangerouslySetInnerHTML={{__html: fakeDOM.window.document.querySelector('figure').innerHTML}}></figure>);
}