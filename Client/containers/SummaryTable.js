// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { SummaryTable as ST } from "../components/SummaryTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer'
import type { DataExplorerState, FilterData, ChartData } from "../types";
import type { Dispatch } from 'redux';

const mapStateToSummaryTableProps = (state:DataExplorerState, props) => (
                                                                            {   chartData: state.chartData,
                                                                                remarks: state.chartData.remarks,
                                                                                cvWarning: props.cvWarning, 
                                                                                cvSuppressed: props.cvSuppressed,
                                                                                cvWarningAlt: props.cvWarningAlt, 
                                                                                cvSuppressedAlt: props.cvSuppressedAlt,
                                                                                cvWarnAt: state.chartData.warningCV,
                                                                                cvSuppressAt: state.chartData.suppressCV
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
