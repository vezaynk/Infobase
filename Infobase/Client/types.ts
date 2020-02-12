export interface FilterItem {
    value: number,
    text: string
}

export interface FilterData {
    id: string,
    name: string,
    items: FilterItem[],
    selected: number
}

export interface Point {
    value: number | void,
    valueUpper: number | void,
    valueLower: number | void,
    label: string,
    text: string,
    cvValue: number | void,
    cvInterpretation: 1 | 2 | 3,
    type: 0 | 1 | 2,
    aggregatorLabel: string
};


type BarChartType = 0;
type TrendChartType = 1;
type Stack = 2;
type ChartType = BarChartType | TrendChartType | Stack;

export const ChartType: {[key: string]: ChartType} = {
    Bar: 0,
    Trend: 1,
    Stack: 2
}
export interface ChartData {
    xAxis: string,
    yAxis: string,
    points: Array<Point>,
    source: string,
    organization: string,
    population: string,
    notes: { name: string, body: string }[],
    descriptionTable: { name: string, body: string }[],
    warningCV: number | void,
    suppressCV: number | void,
    unit: string,
    measureName: string,
    title: string,
    chartType: ChartType
};

export type LanguageCode =  "en-ca" | "fr-ca";

export interface DataExplorerState {
    filters: FilterData[],
    chartData: ChartData,
    loading: boolean,
    languageCode: LanguageCode
}

export interface UpdateLoadState {
    type: "LOAD",
    payload: boolean
}

export interface UpdateFilters {
    type: "UPDATE_FILTERS",
    payload: FilterData[]
}

export interface UpdateChartData {
    type: "UPDATE_DATA",
    payload: ChartData
}

export interface InitState {type: "INIT_STATE", payload: DataExplorerState}

export type Action = UpdateLoadState | UpdateFilters | UpdateChartData | InitState;

