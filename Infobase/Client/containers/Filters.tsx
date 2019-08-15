import * as React from 'react';
import { connect, Provider, MapStateToProps, MapDispatchToProps } from 'react-redux';
import { Filters as F } from "../components/Filters";

import { dataExplorerStore } from '../store/dataExplorer';
import { updateFilters, updateChartData, updateLoadState } from '../reducers/dataExplorerReducer';
import { UpdateLoadState, DataExplorerState, FilterData, ChartData, UpdateChartData, UpdateFilters } from "../types";

type StateProps = { loading: boolean, filters: FilterData[] }

const mapStateToFilterProps: MapStateToProps<StateProps, FiltersProps & { state: DataExplorerState }, DataExplorerState> =(state, props) => state ? { ...state, ...props } : { ...props.state, ...props }

type DispatchProps = {
    updateLoadState: (loadState: boolean) => UpdateLoadState,
    updateChartData: (chartData: ChartData) => UpdateChartData,
    updateFilters: (filters: FilterData[]) => UpdateFilters
}

const mapDispatchToProps: MapDispatchToProps<DispatchProps, {}> = (dispatch) => {
    return {
        updateLoadState: (loadState) => dispatch(updateLoadState(loadState)),
        updateChartData: (chartData) => dispatch(updateChartData(chartData)),
        updateFilters: (filters) => dispatch(updateFilters(filters)),
    }
}

export const FilterBoxConnect = connect(
    mapStateToFilterProps,
    mapDispatchToProps
)(F)

type FiltersProps = {
    prompt: string,
    state: DataExplorerState
}
export const Filters: React.FC<FiltersProps> = (props) => {
    return (
        <Provider store={dataExplorerStore}>
            <FilterBoxConnect {...props} />
        </Provider>
    )
}
