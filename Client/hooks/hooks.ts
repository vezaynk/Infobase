import { useState } from 'react';


export function useLoading() {
    return useState(false);
}

export function useChartData() {
    return useState({
        xAxis: {},
        yAxis: {},
        points: [],
        source: {},
        organization: {},
        population: {},
        notes: {},
        definition: {},
        dataAvailable: {},
        method: {},
        remarks: {},
        warningCV: null,
        suppressCV: null,
        measureName: {}
    });
}

export function useFilters() {
    return useState([]);
}

/*export async function loadSelection(filter, option) {
    const [filters, setFilters] = useFilters();
    const [chartData, setChartData] = useChartData();
    const [loading, setLoading] = useLoading();

    setLoading(true);
    history.pushState(null, document.title, `?${filter}=${option}`);

    let request = await fetch(window.location.toString(), {
        method: 'POST'
    })
    let response = await request;
    let r = await response.json();

    setFilters(r.filters);
    setChartData(r.chartData);
    setLoading(false);
}*/