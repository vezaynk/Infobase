// @flow
import { FilterData, DataExplorerState, ChartData, Action, InitState, UpdateLoadState, UpdateFilters, UpdateChartData } from "../types";
import { Reducer, ActionCreator } from 'redux';

const initialState: DataExplorerState = {
    filters: [],
    chartData: {
        xAxis: '',
        yAxis: '',
        points: [],
        source: '',
        organization: '',
        population: '',
        notes: {},
        descriptionTable: {},
        warningCV: null,
        suppressCV: null,
        measureName: '',
        unit: '',
        title: ''
    },
    loading: false,
    languageCode: "en-ca"
}

export const initState: ActionCreator<InitState> = (payload) => {
    return {type: "INIT_STATE", payload};
}

export const updateLoadState: ActionCreator<UpdateLoadState> = (payload) => {
    return {type: "LOAD", payload};
}

export const updateFilters: ActionCreator<UpdateFilters> = (payload) => {
    return {type: "UPDATE_FILTERS", payload};
}

export const updateChartData: ActionCreator<UpdateChartData> = (payload) => {
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