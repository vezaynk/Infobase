// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { Chart } from "../components/Chart";
import { dataExplorerStore } from '../store/dataExplorer'
import type { ChartData } from "../components/Chart";
import type { UpdateLoadState, Action, DataExplorerState } from "../reducers/dataExplorerReducer";

const mapStateToChartProps = (state: DataExplorerState, props) => ({
    chartData: state.chartData
})

export const ChartingConnect = connect(mapStateToChartProps)(Chart);

type TChartProps = {chartData: ChartData}
export class Charting extends React.Component<TChartProps> {
    constructor(props: TChartProps) {
        super(props);
        dataExplorerStore.dispatch({ type: "UPDATE_DATA", payload: props.chartData})
    }
    render() {
        return (
            <Provider store={dataExplorerStore}>
                <ChartingConnect />
            </Provider>
        )
    }
}