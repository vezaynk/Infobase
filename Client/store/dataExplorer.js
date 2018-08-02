// @flow


import { createStore } from 'redux';
import type { Store } from 'redux';
import type { DataExplorerState, Action } from '../types';
import { dataExplorerReducer } from '../reducers/dataExplorerReducer';

export const dataExplorerStore: Store<DataExplorerState, Action> = createStore(dataExplorerReducer);