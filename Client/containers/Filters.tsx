// @flow

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { bindActionCreators } from 'redux';
import { render } from 'react-dom';
import { connect, Provider, MapStateToProps, MapDispatchToProps } from 'react-redux';
import { FilterBox } from "../components/FilterBox";

import { dataExplorerStore } from '../store/dataExplorer';
import { updateFilters, updateChartData, updateLoadState } from '../reducers/dataExplorerReducer';
import { UpdateLoadState, Action, DataExplorerState, FilterData, ChartData, UpdateChartData, UpdateFilters } from "../types";
import { Dispatch } from 'redux';
import { Filter } from '../components/Filter';
import { dispatch } from 'd3';

const mapStateToFilterProps: MapStateToProps<{ loading: boolean, filters: FilterData[] }, FiltersProp, DataExplorerState> =
    (state, props) =>
        ({ loading: state.loading, filters: state.filters, prompt: props.prompt });

const mapDispatchToProps: MapDispatchToProps<{ updateLoadState: Function, updateChartData: Function, updateFilters: Function }, {}> = (dispatch) => {
    return {
        updateLoadState: (loadState: boolean) => dispatch(updateLoadState(loadState)),
        updateChartData: (chartData: ChartData) => dispatch(updateChartData(chartData)),
        updateFilters: (filters: FilterData[]) => dispatch(updateFilters(filters)),

    }
}
// TODO: Fix typing issue
export const FilterBoxConnect = connect(
    mapStateToFilterProps,
    mapDispatchToProps
)(FilterBox)

type FiltersProp = {
    prompt: string
}
export function Filters(props: FiltersProp) {
    return (
        <Provider store={dataExplorerStore}>
            <FilterBoxConnect prompt={props.prompt} />
        </Provider>
    )
}
