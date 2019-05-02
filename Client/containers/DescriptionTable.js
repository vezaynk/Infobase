//      

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { DescriptionTable as ST } from "../components/DescriptionTable";
import { updateChartData } from '../reducers/dataExplorerReducer';
import { dataExplorerStore } from '../store/dataExplorer'



const mapStateToDescriptionTableProps = (state, props) => (
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











export class DescriptionTable extends React.Component {
    render() {
        console.log(this.props, dataExplorerStore)
        return (
            <Provider store={dataExplorerStore}>
                <DescriptionTableConnect {...this.props} />
            </Provider>
        )
    }
}
