type FilterItem = {
    value: number,
    text: string
}

export type FilterData = {
    id: string,
    name: string,
    items: FilterItem[],
    selected: number
}

export type TPoint = {
    value: number | void,
    valueUpper: number | void,
    valueLower: number | void,
    label: string,
    text: String,
    cvValue: number | void,
    cvInterpretation: 1 | 2 | 3,
    type: 0 | 1 | 2
};

export type ChartData = {
    xAxis: string,
    yAxis: string,
    points: Array<TPoint>,
    source: string,
    organization: string,
    population: string,
    notes: string,
    definition: string,
    dataAvailable: string,
    method: string,
    remarks: string,
    warningCV: number | void,
    suppressCV: number | void,
    unit: string,
    measureName: string,
    title: string
}

export type LanguageCode =  "en-ca" | "fr-ca";
export type TranslationType = "Index" | "Datatool" | "alt";

export type DataExplorerState = {
    filters: FilterData[],
    chartData: ChartData,
    loading: boolean,
    languageCode: LanguageCode
}

export type UpdateLoadState = {
    type: "LOAD",
    payload: boolean
}

export type UpdateFilters = {
    type: "UPDATE_FILTERS",
    payload: FilterData[]
}

export type UpdateChartData = {
    type: "UPDATE_DATA",
    payload: ChartData
}

export type InitState = {type: "INIT_STATE", payload: DataExplorerState}

export type Action = UpdateLoadState | UpdateFilters | UpdateChartData | InitState;

