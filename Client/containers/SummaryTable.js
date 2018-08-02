// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { SummaryTable as ST } from "../components/SummaryTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer'
import type { UpdateLoadState, Action, DataExplorerState, FilterData, ChartData } from "../types";
import type { Dispatch } from 'redux';

const mapStateToSummaryTableProps = (state:DataExplorerState, props) => ({ chartData: state.chartData });



// TODO: Fix typing issue
export const SummaryTableConnect = connect(
    mapStateToSummaryTableProps
)(ST)

type SummaryTableProps = {
    chartData: ChartData
}
export class SummaryTable extends React.Component<SummaryTableProps> {
    constructor(props: SummaryTableProps) {
        super(props);
        dataExplorerStore.dispatch(updateChartData(props.chartData));
    }
    render() {
        return (
            <Provider store={dataExplorerStore}>
                <SummaryTableConnect />
            </Provider>
        )
    }
}
