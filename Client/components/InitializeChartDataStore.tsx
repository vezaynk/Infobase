//import { useChartData, useFilters } from '../hooks/hooks';
import { useState } from "react";
import * as React from 'react';
import * as ReactDOM from 'react-dom';
export function InitializeChartDataStore(props) {
    //const [filters, setFilters] = useFilters();
    //const [chartData, setChartData] = useChartData();
    //setFilters(props.filters);
    //setChartData(props.chartData);
    const [get, set] = useState(0);
    console.log("HAAA")
    return <span>Hello World</span>;
}

export function App() {
    const [get, set] = useState(0);

    return (
        <div>
            App {get} <button onClick={() => set(get + 1)} />
        </div>
    );
}

