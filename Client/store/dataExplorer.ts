import { createStore } from 'redux';
import { Store } from 'redux';
import { DataExplorerState, Action } from '../types';
import { dataExplorerReducer } from '../reducers/dataExplorerReducer';

export const dataExplorerStore: Store<DataExplorerState, Action> = createStore(dataExplorerReducer);