import * as React from 'react';
import * as ReactDOMServer from 'react-dom/server';
import { Charting, mapStateToChartProps, ChartingProps } from './Charting';
import { JSDOM } from 'jsdom';
import { renderChart } from '../renderChart';
import { dataExplorerStore } from '../store/dataExplorer';
import { connect, MapStateToProps, Provider } from 'react-redux';
import { ChartData, DataExplorerState, LanguageCode } from '../types';

const SSChartingConnect = connect(mapStateToChartProps)(SSChartingComponent);

function SSChartingComponent(props: ChartingProps) {
    let html = ReactDOMServer.renderToString(React.createElement(Charting, { ...props, animate: false } ));
    let fakeDOM = new JSDOM(html);
    let graph = fakeDOM.window.document.querySelector("#graph");
    renderChart(graph, props.state.chartData, props.languageCode, false)
    return (<figure dangerouslySetInnerHTML={{ __html: fakeDOM.window.document.querySelector('figure').innerHTML }}></figure>);
}

export function SSCharting(props) {
    return (
        <Provider store={dataExplorerStore}>
            <SSChartingConnect {...props} />
        </Provider>
    );
}