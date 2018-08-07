// @flow

import * as React from 'react';
import { Filter } from "./Filter";
import { i18n } from '../Translator';
import type { Action, FilterData, ChartData, MultilangText } from "../types";

type FilterBoxProps = {
    loading: boolean,
    filters: FilterData[],
    updateLoadState: boolean => Action,
    updateFilters: FilterData[] => Action,
    updateChartData: ChartData => Action,
    prompt: MultilangText
}

export class FilterBox extends React.Component<FilterBoxProps> {

    selectFilter(id: string): (number) => Promise<boolean> {
        return async (value: number) => {
            this.props.updateLoadState(true);

            history.pushState(null, document.title, `?${id}=${value}`);

            let request = await fetch(window.location.toString(), {
                method: 'POST'
            })
            try {
                let response: Response = await request;
                let r = await response.json();

                this.props.updateFilters(r.filters);
                this.props.updateChartData(r.chartData);
                this.props.updateLoadState(false);
                return true;
            } catch (e) {
                return false;
            }


        }
    }
    render() {
        return (

            <div className="col-md-3 padding-15 ">
            <h6>
                <span className="text-danger">{i18n(this.props.prompt)}:</span>
            </h6>
            <div className="form-group-md">
                {
                    this.props.filters.map((filter) =>
                        <Filter
                            key={filter.id}
                            id={filter.id}
                            name={filter.name}
                            items={filter.items}
                            selected={filter.selected}
                            onSelect={this.selectFilter(filter.id)}
                            loading={this.props.loading}
                        />)
                }
                </div>
                </div>
        );
    }
}