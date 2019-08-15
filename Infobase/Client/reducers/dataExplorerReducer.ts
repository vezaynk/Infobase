// @flow
import { FilterData, DataExplorerState, ChartData, Action, InitState, UpdateLoadState, UpdateFilters, UpdateChartData } from "../types";
import { Reducer, ActionCreator } from 'redux';

const initialState: DataExplorerState | void = null;

export const initState: ActionCreator<InitState> = (payload) => {
    return { type: "INIT_STATE", payload };
}

export const updateLoadState: ActionCreator<UpdateLoadState> = (payload) => {
    return { type: "LOAD", payload };
}

export const updateFilters: ActionCreator<UpdateFilters> = (payload) => {
    return { type: "UPDATE_FILTERS", payload };
}

export const updateChartData: ActionCreator<UpdateChartData> = (payload) => {
    return { type: "UPDATE_DATA", payload };
}


export const dataExplorerReducer: Reducer<DataExplorerState | void, Action> = (previousState = initialState, action) => {

    if (action.type == "INIT_STATE") {
        return { previousState, ...action.payload };
    }
    
    if (previousState) {
        let state = { ...previousState };
        switch (action.type) {
            case "LOAD":
                state.loading = action.payload;
                break;
            case "UPDATE_FILTERS":
                if (previousState)
                    state.filters = action.payload;
                break;
            case "UPDATE_DATA":
                if (previousState)
                    state.chartData = action.payload;
                break;
        }
        return state;
    }

    return previousState;
}