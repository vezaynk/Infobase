// @flow

import * as React from 'react';
import { Filter } from "./Filter";
import type { FilterData } from "./Filter";
import type { ChartData } from "./Chart";

type FilterBoxProps = {
    loading: boolean,
    filters: FilterData[],
    updateLoadState: boolean => void,
    updateFilters: FilterData[] => void,
    updateData: ChartData => void
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
                this.props.updateData(r.chartData);
                this.props.updateLoadState(false);
                return true;
            } catch (e) {
                return false;
            }


        }
    }
    render() {
        console.log(this.props)
        return (
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
        );
    }
}