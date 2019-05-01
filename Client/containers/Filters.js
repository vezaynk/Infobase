// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { bindActionCreators } from 'redux';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { FilterBox } from "../components/FilterBox";

import { dataExplorerStore } from '../store/dataExplorer';
import { updateFilters, updateChartData, updateLoadState } from '../reducers/dataExplorerReducer';
import type { UpdateLoadState, Action, DataExplorerState, FilterData, ChartData, MultilangText } from "../types";
import type { Dispatch, ActionCreators } from 'redux';

const mapStateToFilterProps = (state: DataExplorerState, props) => ({ loading: state.loading, filters: state.filters, prompt: props.prompt });
const actionCreators: ActionCreators<string, Action> = { updateLoadState, updateFilters, updateChartData};
const mapDispatchToProps = (dispatch: Dispatch<Action>) => bindActionCreators(actionCreators, dispatch)

// TODO: Fix typing issue
export const FilterBoxConnect = connect(
    mapStateToFilterProps, 
    mapDispatchToProps
)(FilterBox)

type FiltersProp = {
    filters?: FilterData[],
    prompt: MultilangText
}
export class Filters extends React.Component<FiltersProp> {
    constructor(props: FiltersProp) {
        super(props);
        if (props.filters)
            dataExplorerStore.dispatch({type: "UPDATE_FILTERS", payload: props.filters});
    }

    render() {
        return (
            <Provider store={dataExplorerStore}>
                <FilterBoxConnect prompt={this.props.prompt} />
            </Provider>
        )
    }
}
