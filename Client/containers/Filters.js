// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { FilterBox } from "../components/FilterBox";

import { dataExplorerStore } from '../store/dataExplorer'
import type { UpdateLoadState, Action, DataExplorerState } from "../reducers/dataExplorerReducer";
import type { FilterData } from "../components/Filter";
import type { ChartData } from "../components/Chart";
import type { Dispatch } from 'redux';

const mapStateToFilterProps = (state:DataExplorerState, props) => ({ loading: state.loading, filters: state.filters });

const mapDispatchToFilterProps = (dispatch: Dispatch<Action>, ownProps) => ({
    updateFilters(filters: FilterData[]) { 
        dispatch({ type: "UPDATE_FILTERS", payload: filters }) 
    },
    updateData(data: ChartData) { 
        dispatch({ type: "UPDATE_DATA", payload: data }) 
    },
    updateLoadState(loading: boolean) {
        dispatch({ type: "LOAD", payload: loading })
    }
});


// TODO: Fix typing issue
export const FilterBoxConnect = connect(
    mapStateToFilterProps, 
    mapDispatchToFilterProps
)(FilterBox)

type FiltersProp = {
    filters: FilterData[]
}
export class Filters extends React.Component<FiltersProp> {
    constructor(props: FiltersProp) {
        super(props);
        dataExplorerStore.dispatch({type: "UPDATE_FILTERS", payload: props.filters});
    }
    render() {
        return (
            <Provider store={dataExplorerStore}>
                <FilterBoxConnect />
            </Provider>
        )
    }
}
