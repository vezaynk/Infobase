// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { renderChart } from '../renderChart';
import type { ChartData } from '../types';

export class Chart extends React.Component<{chartData: ChartData}, {isMounted: boolean}> {
    graph: ?Element;

    componentDidMount() {
        renderChart(this.graph, this.props.chartData)
        this.setState({isMounted: true})
    }
    componentDidUpdate() {
        if (this.state.isMounted)
            renderChart(this.graph, this.props.chartData)
    }
    render() {
        console.log(this.props)
        return (
            <div>
            <svg id="graph" ref={graph => this.graph = graph} width="100%" viewBox="0 0 900 800" preserveAspectRatio="xMidYMid meet">
                    <foreignObject x="12.5%" y="0" width="75%" height="100">
                        <h3>{this.props.chartData.measureName["(EN, )"]}, </h3>
                    </foreignObject>
                </svg>
            </div>
        )
    }
}