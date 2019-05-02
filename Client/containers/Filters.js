//      

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { bindActionCreators } from 'redux';
import { render } from 'react-dom';
import { connect, Provider } from 'react-redux';
import { FilterBox } from "../components/FilterBox";

import { dataExplorerStore } from '../store/dataExplorer';
import { updateFilters, updateChartData, updateLoadState } from '../reducers/dataExplorerReducer';
                                                                                                                 
                                                      

const mapStateToFilterProps = (state                   , props) => ({ loading: state.loading, filters: state.filters, prompt: props.prompt });
const actionCreators                                 = { updateLoadState, updateFilters, updateChartData};
const mapDispatchToProps = (dispatch                  ) => bindActionCreators(actionCreators, dispatch)

// TODO: Fix typing issue
export const FilterBoxConnect = connect(
    mapStateToFilterProps, 
    mapDispatchToProps
)(FilterBox)

                    
                           
                         
 
export class Filters extends React.Component              {
    constructor(props             ) {
        super(props);
        if (props.filters)
            dataExplorerStore.dispatch({type: "UPDATE_FILTERS", payload: props.filters});
    }

    render() {
        return (
            <Provider store={dataExplorerStore}>
                <FilterBoxConnect prompt={this.props.prompt} />
            </Provider>
        )
    }
}
