// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { Chart } from "../components/Chart";
import { dataExplorerStore } from '../store/dataExplorer';
import { updateChartData } from '../reducers/dataExplorerReducer';
import type { ChartData, DataExplorerState } from "../types";

const mapStateToChartProps = (state: DataExplorerState, props) => ({
    chartData: state.chartData,
    animate: props.animate
})

export const ChartingConnect = connect(mapStateToChartProps)(Chart);

export function Charting(props: {animate: boolean}) {
    return <Provider store={dataExplorerStore}>
                <ChartingConnect animate={props.animate} />
            </Provider>
}