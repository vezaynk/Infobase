import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { Chart } from "../components/Chart";
import { dataExplorerStore } from '../store/dataExplorer';
import { updateChartData } from '../reducers/dataExplorerReducer';
import { ChartData, DataExplorerState } from "../types";

const mapStateToChartProps: MapStateToProps<{chartData: ChartData}, ChartingProps, DataExplorerState> = (state, props) => ({
    chartData: state.chartData,
    animate: props.animate
})

export const ChartingConnect = connect(mapStateToChartProps)(Chart);

type ChartingProps = {animate: boolean};

export function Charting(props: ChartingProps) {
    return <Provider store={dataExplorerStore}>
                <ChartingConnect animate={props.animate} />
            </Provider>
}