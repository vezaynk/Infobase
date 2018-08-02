// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { FilterBox } from "../components/FilterBox";

import { dataExplorerStore } from '../store/dataExplorer'
import { initState } from '../reducers/dataExplorerReducer'
import type { DataExplorerState, InitState } from '../types'

type StateProp = {
    state: DataExplorerState
}

export class InitializeChartDataStore extends React.Component<StateProp> {
    constructor(props: StateProp) {
        super(props);
        dataExplorerStore.dispatch(initState(this.props.state));
    }
    render() {
        return null;
    }
}
