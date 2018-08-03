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
export class SummaryTable extends React.Component<SummaryTableProps> {
    constructor(props: SummaryTableProps) {
        super(props);
        if (props.chartData)
            dataExplorerStore.dispatch(updateChartData(props.chartData));
    }
    render() {
        return (
            <Provider store={dataExplorerStore}>
                <SummaryTableConnect cvWarning={this.props.cvWarning}
                                     cellsEmpty={this.props.cellsEmpty} 
                                     cvSuppressed={this.props.cvSuppressed} />
            </Provider>
        )
    }
}
