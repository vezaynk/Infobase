// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { FilterBox } from "../components/FilterBox";

import { dataExplorerStore } from '../store/dataExplorer'
import { initState } from '../reducers/dataExplorerReducer'
import { DataExplorerState, InitState } from '../types'

type StateProp = {
    state: DataExplorerState
}

export const InitializeChartDataStore: React.SFC<StateProp> = (props) => {
    dataExplorerStore.dispatch(initState(props.state));
    return null;
}
