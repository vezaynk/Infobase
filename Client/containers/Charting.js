//      

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { Chart } from "../components/Chart";
import { dataExplorerStore } from '../store/dataExplorer';
import { updateChartData } from '../reducers/dataExplorerReducer';
                                                             

const mapStateToChartProps = (state                   , props) => ({
    chartData: state.chartData
})

export const ChartingConnect = connect(mapStateToChartProps)(Chart);

                                            

export class Charting extends React.Component              {
    constructor(props             ) {
        super(props);
        if (props.chartData)
            dataExplorerStore.dispatch(updateChartData(props.chartData));
    }
    render() {
        return (
            <Provider store={dataExplorerStore}>
                <ChartingConnect />
            </Provider>
        )
    }
}