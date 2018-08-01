// @flow


import { createStore } from 'redux';
import type { Store } from 'redux';
import type { DataExplorerState, Action } from '../reducers/dataExplorerReducer';
import { dataExplorerReducer } from '../reducers/dataExplorerReducer';

export const dataExplorerStore: Store<DataExplorerState, Action> = createStore(dataExplorerReducer);