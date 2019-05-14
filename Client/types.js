// @flow


export type MultilangText = {
    [key: string]: string
}

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
    value: ?number,
    valueUpper: ?number,
    valueLower: ?number,
    label: MultilangText,
    cvValue: ?number,
    cvInterpretation: 1 | 2 | 3,
    type: 0 | 1 | 2
};

export type ChartData = {
    xAxis: MultilangText,
    yAxis: MultilangText,
    points: Array<TPoint>,
    source: MultilangText,
    organization: MultilangText,
    population: MultilangText,
    notes: MultilangText,
    definition: MultilangText,
    dataAvailable: MultilangText,
    method: MultilangText,
    remarks: MultilangText,
    warningCV: ?number,
    suppressCV: ?number,
    measureName: MultilangText
}

export type LanguageCode =  "en-ca" | "fr-ca";
export type TranslationType = "Index" | "Datatool" | "alt";

export type DataExplorerState = {
    filters: FilterData[],
    chartData: ChartData,
    loading: boolean,
    //languageCode: LanguageCode
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

