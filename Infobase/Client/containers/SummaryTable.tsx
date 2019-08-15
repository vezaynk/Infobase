// @flow

import * as React from 'react';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { SummaryTable as ST } from "../components/SummaryTable";
import { dataExplorerStore } from '../store/dataExplorer'
import { DataExplorerState, ChartData, LanguageCode } from "../types";

const mapStateToSummaryTableProps: MapStateToProps<{chartData: ChartData, languageCode: LanguageCode}, SummaryTableProps & { state: DataExplorerState }, DataExplorerState> = (state, props) => state ? { ...state, ...props } : {...props.state, ...props};


export const SummaryTableConnect = connect(
    mapStateToSummaryTableProps
)(ST)

export type SummaryTableProps = {
    cvWarning: string,
    cvWarningAlt: string,
    cvSuppressed: string,
    cvSuppressedAlt: string,
    confidenceInterval: string,
    confidenceIntervalAbbr: string,
    state: DataExplorerState
}
export const SummaryTable: React.FC<SummaryTableProps> = (props) => {
    return (
        <Provider store={dataExplorerStore}>
            <SummaryTableConnect {...props} />
        </Provider>
    )
}
