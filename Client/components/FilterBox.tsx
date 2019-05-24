import * as React from 'react';
import { Filter } from "./Filter";
import { Action, FilterData, ChartData } from "../types";

type FilterBoxProps = {
    loading: boolean,
    filters: FilterData[],
    updateLoadState: (loadState: boolean) => Action,
    updateFilters: (filters: FilterData[]) => Action,
    updateChartData: (chartData: ChartData) => Action,
    prompt: string
}

export function FilterBox(props: FilterBoxProps) {

    async function selectFilter(selected: number): Promise<boolean> {
        props.updateLoadState(true);
        window.history.pushState(null, document.title, `?index=${selected}`);

        let request = await fetch(window.location.toString(), {
            method: 'POST'
        })
        try {
            let response: Response = await request;
            let r = await response.json();

            props.updateFilters(r.filters);
            props.updateChartData(r.chartData);
            props.updateLoadState(false);
            return true;
        } catch (e) {
            return false;
        }
    }
    return (
        <div className="col-md-3 padding-15 ">
            <span className="text-info">{props.prompt}:</span>
            <div className="form-group-md">
                {
                    props.filters.map((filter, i) =>
                        <Filter
                            key={i}
                            id={"drop" + i}
                            name={filter.name}
                            items={filter.items}
                            selected={filter.selected}
                            onSelect={(selected: number) => selectFilter(selected)}
                            loading={props.loading}
                        />)
                }
            </div>
        </div>
    );
}