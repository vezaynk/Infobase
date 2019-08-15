import { createStore, Store } from 'redux';
import { DataExplorerState, Action } from '../types';
import { dataExplorerReducer } from '../reducers/dataExplorerReducer';
import * as React from 'react';

export const dataExplorerStore: Store<DataExplorerState | void, Action> = createStore(dataExplorerReducer);