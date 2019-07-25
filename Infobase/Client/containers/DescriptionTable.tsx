import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps, Connect } from 'react-redux';
import { DescriptionTable as DT } from "../components/DescriptionTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer';
import { DataExplorerState, FilterData, ChartData } from "../types";

const mapStateToDescriptionTableProps: MapStateToProps<{remarks: string}, DescriptionTableProps, DataExplorerState> = (state, props) => (
    {
        definitionText: props.definitionText,
        dataAvailableText: props.dataAvailableText,
        methodsText: props.methodsText,
        remarksText: props.remarksText,
        remarks: state.chartData.remarks,
        methods: state.chartData.method,
        dataAvailable: state.chartData.dataAvailable,
        definition: state.chartData.definition
    }
);


export const DescriptionTableConnect = connect(
    mapStateToDescriptionTableProps
)(DT)

type DescriptionTableProps = {
    definitionText: string,
    dataAvailableText: string,
    methodsText: string,
    remarksText: string,
    remarks: string,
    methods: string,
    dataAvailable: string,
    definition: string
}
export const DescriptionTable: React.FC<DescriptionTableProps> = (props) => {
    return (
        <Provider store={dataExplorerStore}>
            <DescriptionTableConnect {...props} />
        </Provider>
    )
}
