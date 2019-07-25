// @flow

import * as React from 'react';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { SummaryTable as ST } from "../components/SummaryTable";
import { dataExplorerStore } from '../store/dataExplorer'
import { DataExplorerState, ChartData } from "../types";

const mapStateToSummaryTableProps: MapStateToProps<{chartData: ChartData}, SummaryTableProps, DataExplorerState> = (state, props) => (
    {
        chartData: state.chartData,
        ...props
    }
);


export const SummaryTableConnect = connect(
    mapStateToSummaryTableProps
)(ST)

export type SummaryTableProps = {
    cvWarning: string,
    cvWarningAlt: string,
    cvSuppressed: string,
    cvSuppressedAlt: string,
    confidenceInterval: string,
    confidenceIntervalAbbr: string
}
export const SummaryTable: React.FC<SummaryTableProps> = (props) => {
    return (
        <Provider store={dataExplorerStore}>
            <SummaryTableConnect {...props} />
        </Provider>
    )
}
