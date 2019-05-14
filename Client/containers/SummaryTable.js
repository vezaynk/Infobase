// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { SummaryTable as ST } from "../components/SummaryTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer'
import type { MultilangText, DataExplorerState, FilterData, ChartData } from "../types";
import type { Dispatch } from 'redux';

const mapStateToSummaryTableProps = (state:DataExplorerState, props) => (
                                                                            {   chartData: state.chartData,
                                                                                remarks: state.chartData.remarks,
                                                                                cvWarning: props.cvWarning, 
                                                                                cvSuppressed: props.cvSuppressed,
                                                                                cvWarnAt: state.chartData.warningCV,
                                                                                cvSuppressAt: state.chartData.suppressCV
                                                                            }
                                                                        );



// TODO: Fix typing issue
export const SummaryTableConnect = connect(
    mapStateToSummaryTableProps
)(ST)

type SummaryTableProps = {
    chartData?: ChartData,
    cvWarning: MultilangText,
    cellsEmpty: MultilangText,
    cvSuppressed: MultilangText
}
export function SummaryTable(props) {
        return (
            <Provider store={dataExplorerStore}>
                <SummaryTableConnect cvWarning={props.cvWarning}
                                     cellsEmpty={props.cellsEmpty} 
                                     cvSuppressed={props.cvSuppressed} />
            </Provider>
        )
    }
