import * as React from 'react';
import { observer } from 'mobx-react';
import { observable, action, runInAction } from 'mobx';
import * as mobx from 'mobx';
import { renderChart } from 'renderChart'
import * as ReactDOM from 'react-dom';

class ChartDataStore {
    @observable filters: FilterProps[] = [];
    @observable chartData = [];
    
    async fetchData(id, value) {
        history.pushState(null, document.title, `?${id}=${value}`);
        let response = await fetch(window.location.toString(), {
            method: 'POST'
        })

        let r = await response.json();

        runInAction("Update filters after fetching", () => {
            this.filters = r.filters;
        })
    }

}

const store = new ChartDataStore();
export class ChartPageState extends React.Component {
    constructor(props) {
        super(props)
        store.filters = props.filters;
        store.chartData = props.chartData;
    }
    render() {
        return null
    }
}


@observer
export class FilterBox extends React.Component<null> {

    selectFilter(id) {
        return (value) => {
            store.fetchData(id, value)
        }
    }
    render() {
        return (
            <div className="form-group-md">
                {
                    store.filters.map((filter: FilterProps) =>
                    <Filter
                            key={filter.id}
                            id={filter.id}
                            name={filter.name}
                            items={filter.items}
                            selected={filter.selected}
                            onSelect={this.selectFilter(filter.id)}
                        />)
                }
            </div>
        );
    }
}

interface FilterProps {
    name: string,
    id: string,
    items: Array<{
        value: number,
        text: string
    }>,
    selected: number,
    onSelect: (string) => void
}

export class Filter extends React.Component<FilterProps> {

    render() {
        return (
            <form className="form-group-sm">
                <label className="control-label" htmlFor={this.props.id}>{this.props.name}</label>
                <select className="form-control input-sm" value={this.props.selected} name={this.props.id} id={this.props.id} onChange={e => this.props.onSelect(e.target.value)}>
                    {this.props.items.map(item => <option key={item.value} value={item.value}>{item.text}</option>)}
                </select>
            </form>
        )
    }
}


interface Point {
    "value": number,
    "label": string,
    "confidence": {
        "upper": number,
        "lower": number
    }
}

interface Value {
    "points": Point[],
    "type": number
}

interface ChartData {
    "axis": {
        "x": "Years",
        "y": "Percentage"
    },
    "values": Value[],
    "title": string,
    "source": string
}

interface ChartProps {
    data: ChartData
}

export class Chart extends React.Component<ChartProps> {
    componentDidMount() {
        let svg = ReactDOM.findDOMNode(this.refs.graph)
        renderChart(svg, this.props.data)
    }

    render() {
        return (
            <svg ref="graph" width="100%" viewBox="0 0 900 800" preserveAspectRatio="xMidYMid meet">
                <foreignObject x="12.5%" y="0" width="75%" height="100">
                    <h3 ref="title" id="title">Hello World</h3>
                </foreignObject>
            </svg>
        )
    }
}
