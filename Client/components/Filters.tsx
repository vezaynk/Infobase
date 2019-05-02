//      

import * as React from 'react';
import { Filter } from "./Filter";
import { i18n } from '../Translator';
import { useFilters } from "../hooks/hooks";

export function Filters(props) {
    const [filters, setFilters] = useFilters();
    console.log(filters);
    return (<div className="col-md-3 padding-15 ">
        <span className="text-info">{i18n(props.prompt)}:</span>
        <div className="form-group-md">
            {
                filters.map(filter =>
                    <Filter
                        key={filter.id}
                        id={filter.id}
                        name={filter.name}
                        items={filter.items}
                        selected={filter.selected}
                    />)
            }
        </div>
    </div>);
}