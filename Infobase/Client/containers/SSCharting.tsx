import * as React from 'react';
import * as ReactDOMServer from 'react-dom/server';
import { Charting } from './Charting';
import { JSDOM } from 'jsdom';
import { renderChart } from '../renderChart';
import { dataExplorerStore } from '../store/dataExplorer';
import { connect, MapStateToProps, Provider } from 'react-redux';
import { ChartData, DataExplorerState, LanguageCode } from '../types';

type ChartingProps = { chartData: ChartData, languageCode: LanguageCode, state: DataExplorerState };

const mapStateToChartProps: MapStateToProps<ChartingProps, {state: DataExplorerState}, DataExplorerState | void> = (state, props) => state ? { ...state, ...props } : {...props.state, ...props};

const SSChartingConnect = connect(mapStateToChartProps)(SSChartingComponent);

function SSChartingComponent(props: ChartingProps) {
    let html = ReactDOMServer.renderToString(React.createElement(Charting, { ...props, animate: false } ));
    let fakeDOM = new JSDOM(html);
    let graph = fakeDOM.window.document.querySelector("#graph");
    renderChart(graph, props.chartData, props.languageCode ,false)
    return (<figure dangerouslySetInnerHTML={{ __html: fakeDOM.window.document.querySelector('figure').innerHTML }}></figure>);
}

export function SSCharting(props: {state: DataExplorerState}) {
    return (
        <Provider store={dataExplorerStore}>
            <SSChartingConnect {...props} />
        </Provider>
    );
}