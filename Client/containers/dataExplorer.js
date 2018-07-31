// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { createStore } from 'redux';

import { dataExplorerReducer } from '../reducers/dataExplorerReducer';
import { FilterBox } from "../components/FilterBox";

import type { UpdateLoadState, Action, DataExplorerState } from "../reducers/dataExplorerReducer";
import type { FilterData } from "../components/Filter";

const dataExplorerStore = createStore(dataExplorerReducer);

const mapStateToFilterProps = (state: DataExplorerState) => ({ loading: state.loading, filters: state.filters });
const mapDispatchToFilterProps = (dispatch: Dispatch) => ({
    updateFilters: filters => dispatch({ type: "UPDATE_FILTERS", payload: filters }),
    updateData: data => dispatch({ type: "UPDATE_DATA", payload: data }),
    updateLoadState: loading => dispatch({ type: "LOAD", payload: loading })
});

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
