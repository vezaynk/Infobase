import * as React from 'react';
import { dataExplorerStore } from '../store/dataExplorer'
import { initState } from '../reducers/dataExplorerReducer'
import { DataExplorerState } from '../types'

type StateProp = {
    state: DataExplorerState
}

export const InitializeChartDataStore: React.FC<StateProp> = (props) => {
    dataExplorerStore.dispatch(initState(props.state));
    return null;
}
