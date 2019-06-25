import * as React from 'react';
import { Action, FilterData, ChartData } from "../types";
import { NoScript } from '../HOC/NoScript';

type FiltersProps = {
    loading: boolean,
    filters: FilterData[],
    updateLoadState: (loadState: boolean) => Action,
    updateFilters: (filters: FilterData[]) => Action,
    updateChartData: (chartData: ChartData) => Action,
    prompt: string
}

export const Filters: React.FC<FiltersProps> = (props) => {

    const updateUrlIndex = index => {
        window.history.pushState(null, document.title, `?index=${index}`);
    }
    const loadNewData = async index => {
        props.updateLoadState(true);
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
    async function selectFilter(selected: number) {
        updateUrlIndex(selected);
        await loadNewData(selected);
    }

    React.useEffect(() => {
        const handler = (event) => {
            const index = Number.parseInt(new URLSearchParams(window.location.search).get("index"));

            //updateUrlIndex(index);
            loadNewData(index);
        };
        window.addEventListener('popstate', handler);
        return () => {
            window.removeEventListener('popstate', handler);
        }
    })
    return (
        <div className="col-md-3 padding-15 ">
            <span className="text-info">{props.prompt}:</span>
            <div className="form-group-md">
                {
                    props.filters.map((filter, i) =>
                        <form className="form-group-sm" key={i}>
                            <label className="control-label" htmlFor={"drop"+i}>{filter.name}</label>
                            <select disabled={props.loading}
                                className="form-control input-sm full-width"
                                value={filter.selected}
                                name="index"
                                id={"drop"+i}
                                onChange={(e) => selectFilter(Number.parseInt(e.currentTarget.value))
                                }>
                                {filter.items.map(item => <option key={item.value} value={item.value}>{item.text}</option>)}
                            </select>
                            <NoScript className="full-width text-center" ieAsNoScript={false}>
                                <input className="btn btn-xs btn-default full-width" type="submit" />
                            </NoScript>
                        </form>
                    )
                }
            </div>
        </div>
    );
}