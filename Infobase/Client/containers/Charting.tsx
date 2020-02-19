import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { Chart } from "../components/Chart";
import { dataExplorerStore } from '../store/dataExplorer';
import { updateChartData } from '../reducers/dataExplorerReducer';
import { ChartData, DataExplorerState, LanguageCode } from "../types";

export type ChartingProps = { animate: boolean, state: DataExplorerState, languageCode: LanguageCode };

export const mapStateToChartProps: MapStateToProps<{ chartData: ChartData }, ChartingProps, DataExplorerState> = (state, props) => state ? { ...state, ...props } : { ...props.state, ...props }

export const ChartingConnect = connect(mapStateToChartProps)(Chart);

export const Charting: React.FC<ChartingProps> = (props) => {
    return <Provider store={dataExplorerStore}>
        <ChartingConnect {...props} />
    </Provider>
}