// @flow
import type { FilterData, DataExplorerState, ChartData, Action } from "../types";
import type { Reducer, ActionCreator } from 'redux';


const initialState: DataExplorerState = {
    filters: [],
    chartData: {
        xAxis: {},
        yAxis: {},
        points: [],
        source: {},
        organization: {},
        population: {},
        notes: {},
        definition: {},
        dataAvailable: {},
        method: {},
        remarks: {},
        warningCV: null,
        supressCV: null,
        measureName: {}
    },
    loading: false,
    languageCode: "EN"
}

export const initState: ActionCreator<Action, DataExplorerState> = (payload) => {
    return {type: "INIT_STATE", payload};
}

export const updateLoadState: ActionCreator<Action, boolean> = (payload) => {
    return {type: "LOAD", payload};
}

export const updateFilters: ActionCreator<Action, FilterData[]> = (payload) => {
    return {type: "UPDATE_FILTERS", payload};
}

export const updateChartData: ActionCreator<Action, ChartData> = (payload) => {
    return {type: "UPDATE_DATA", payload};
}


export const dataExplorerReducer: Reducer<DataExplorerState, Action> = (previousState = initialState, action) => {
    let state = { ...previousState };
    switch (action.type) {
        case "LOAD":
            state.loading = action.payload;
            break;
        case "UPDATE_FILTERS":
            state.filters = action.payload;
            break;
        case "UPDATE_DATA":
            state.chartData = action.payload;
            break;
        case "INIT_STATE":
            return {previousState, ...action.payload};
    }

    return state;
}