// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { createStore } from 'redux';

import { dataExplorerReducer } from './reducers/dataExplorerReducer';
import { FilterBox } from "./components/FilterBox";

import type { UpdateLoadState, DataExplorerState } from "./reducers/dataExplorerReducer";
import type { FilterData } from "./components/Filter";

const dataExplorerStore = createStore(dataExplorerReducer);


export class Test5 extends React.Component<null> {
    constructor() {
        dataExplorerStore.dispatch({ type: "LOAD", payload: false })
    }
    render() {
        return (
            <Provider store={dataExplorerStore}>
                <FilterBoxConnect />
            </Provider>
        )
    }
}




const mapStateToProps: DataExplorerState => {loading: boolean, filters: FilterData[] } = state => ({ loading: state.loading, filters: state.filters });

// TODO: Identify specific case used for connect by default
export const FilterBoxConnect = connect(
    
    mapStateToProps, 

    (dispatch: Dispatch) => ({
        updateFilters: filters => dispatch("UPDATE_FILTERS", filters),
        updateData: data => dispatch("UPDATE_DATA", data),
        updateLoadState: loading => dispatch("LOAD", loading)
    })

)(FilterBox)