// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { DescriptionTable as ST } from "../components/DescriptionTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer';
import type { DataExplorerState, FilterData, ChartData } from "../types";
import type { Dispatch } from 'redux';

const mapStateToDescriptionTableProps = (state: DataExplorerState, props) => (
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



// TODO: Fix typing issue
export const DescriptionTableConnect = connect(
    mapStateToDescriptionTableProps
)(ST)

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
export function DescriptionTable(props: DescriptionTableProps) {
    return (
        <Provider store={dataExplorerStore}>
            <DescriptionTableConnect {...props} />
        </Provider>
    )
}
