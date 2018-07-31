// @flow
import type { FilterData } from "../components/Filter";
import type { ChartData } from "../components/Chart";

export type DataExplorerState = {
    filters: FilterData[],
    chartData: ChartData,
    loading: boolean
}

export type UpdateLoadState = {
    type: "LOAD",
    payload: boolean
}

export type UpdateFilters = {
    type: "UPDATE_FILTERS",
    payload: FilterData[]
}

export type UpdateChartData = {
    type: "UPDATE_DATA",
    payload: ChartData
}

export type Action = UpdateLoadState | UpdateFilters | UpdateChartData;

const initialState: DataExplorerState = {
    filters: [],
    chartData: {
        axis: {
            x: "",
            y: ""
        },
        values: [],
        title: "",
        source: ""
    },
    loading: false
}

export const dataExplorerReducer = (previousState: DataExplorerState = initialState, action: Action): DataExplorerState => {
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
    }

    return state;
}