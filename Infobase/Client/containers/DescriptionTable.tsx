import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps, Connect } from 'react-redux';
import { DescriptionTable as DT, DescriptionTableProps } from "../components/DescriptionTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer';
import { DataExplorerState, FilterData, ChartData } from "../types";

const mapStateToDescriptionTableProps: MapStateToProps<DescriptionTableProps, { state: DataExplorerState }, DataExplorerState> = (state, props) => state ? { ...state.chartData, ...props } : { ...props.state.chartData, ...props.state, ...props }


export const DescriptionTableConnect = connect(
    mapStateToDescriptionTableProps
)(DT)

export const DescriptionTable: React.FC<{ state: DataExplorerState }> = (props) => {
    return (
        <Provider store={dataExplorerStore}>
            <DescriptionTableConnect {...props} />
        </Provider>
    )
}
