import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps, Connect } from 'react-redux';
import { DescriptionTable as DT, DescriptionTableProps } from "../components/DescriptionTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer';
import { DataExplorerState, FilterData, ChartData } from "../types";

const mapStateToDescriptionTableProps: MapStateToProps<DescriptionTableProps, {}, DataExplorerState> = (state, _props) => (
    {
        descriptionTable: state.chartData.descriptionTable
    }
);


export const DescriptionTableConnect = connect(
    mapStateToDescriptionTableProps
)(DT)

export const DescriptionTable: React.FC = () => {
    return (
        <Provider store={dataExplorerStore}>
            <DescriptionTableConnect />
        </Provider>
    )
}
