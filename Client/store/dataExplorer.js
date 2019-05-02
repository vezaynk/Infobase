//      


import { createStore } from 'redux';
                                   
                                                          
import { dataExplorerReducer } from '../reducers/dataExplorerReducer';

export const dataExplorerStore                                   = createStore(dataExplorerReducer);