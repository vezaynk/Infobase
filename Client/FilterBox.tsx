import * as React from 'react';
import { observer } from 'mobx-react';
import { observable, action, runInAction } from 'mobx';
import * as mobx from 'mobx';
import { renderChart } from 'renderChart'
import * as ReactDOM from 'react-dom';
import { ReactInstance } from 'react';

class ChartDataStore {
    @observable filters: FilterProps[];
    @observable chartData: ChartData;
    @observable loading = false;
    @observable number = 0;
    async fetchData(id, value) {
        runInAction("Begin loading", () => {
            this.loading = true;
        })

        history.pushState(null, document.title, `?${id}=${value}`);
        let response = await fetch(window.location.toString(), {
            method: 'POST'
        })

        let r = await response.json();

        runInAction("Update filters after fetching", () => {
            this.filters = r.filters;
            this.chartData = r.chartData;
            this.loading = false;
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
export class Test extends React.Component<null> {
    constructor(props) {
        super(props)
        store.number++;
    }
    render() {
        return <p>{store.number}</p>
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

@observer
export class Filter extends React.Component<FilterProps, null> {
    
    componentDidMount() {
        let noscript: HTMLElement = this.refs.noscript as HTMLElement;
        noscript.style.display = "none";
    }
    render() {
        return (
            <form className="form-group-sm">
                <label className="control-label" htmlFor={this.props.id}>{this.props.name} {this.props.selected}</label>
                <select disabled={store.loading} className="form-control input-sm" value={this.props.selected} name={this.props.id} id={this.props.id} onChange={e => this.props.onSelect(e.target.value)}>
                    {this.props.items.map(item => <option key={item.value} value={item.value}>{item.text}</option>)}
                </select>
                <div ref="noscript">
                    <input type="submit" />
                </div>
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

interface ChartState {
    isMounted: boolean;
}

@observer
export class Chart extends React.Component<null, ChartState> {
    componentDidMount() {
        let svg = ReactDOM.findDOMNode(this.refs.graph)
        renderChart(svg, store.chartData)
        this.setState({ ...this.state, isMounted: true })
    }
    componentDidUpdate() {
        let svg = ReactDOM.findDOMNode(this.refs.graph);

        if (this.state.isMounted)
            renderChart(svg, store.chartData)
    }
    render() {
        console.log("Rendering data", store.chartData)
        return (
            <div>
            <svg id="graph" ref="graph" width="100%" viewBox="0 0 900 800" preserveAspectRatio="xMidYMid meet">
                    <foreignObject x="12.5%" y="0" width="75%" height="100">
                        <h3>{store.chartData.title}</h3>
                </foreignObject>
                </svg>
            </div>
        )
    }
}
