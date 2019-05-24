// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { SummaryTable as ST } from "../components/SummaryTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer'
import { DataExplorerState, FilterData, ChartData } from "../types";
import { Dispatch } from 'redux';

const mapStateToSummaryTableProps: MapStateToProps<{chartData: ChartData}, SummaryTableProps, DataExplorerState> = (state, props) => (
    {
        chartData: state.chartData,
        cvWarning: props.cvWarning,
        cvSuppressed: props.cvSuppressed,
        cvWarningAlt: props.cvWarningAlt,
        cvSuppressedAlt: props.cvSuppressedAlt
    }
);



// TODO: Fix typing issue
export const SummaryTableConnect = connect(
    mapStateToSummaryTableProps
)(ST)

type SummaryTableProps = {
    cvWarning: string,
    cvWarningAlt: string,
    cvSuppressed: string,
    cvSuppressedAlt: string
}
export function SummaryTable(props: SummaryTableProps) {
    return (
        <Provider store={dataExplorerStore}>
            <SummaryTableConnect cvWarning={props.cvWarning}
                cvSuppressed={props.cvSuppressed}
                cvWarningAlt={props.cvWarningAlt}
                cvSuppressedAlt={props.cvSuppressedAlt} />
        </Provider>
    )
}
