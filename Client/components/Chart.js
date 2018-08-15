// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {i18n} from "../Translator";
import { initChart, updateChart } from '../renderChart';
import type { ChartData } from '../types';

export class Chart extends React.Component<{chartData: ChartData}, {isMounted: boolean}> {
    graph: ?Element;

    componentDidMount() {
        if (this.graph)
        initChart(this.graph, this.props.chartData)
        this.setState({isMounted: true})
    }
    componentDidUpdate() {
        if (this.state.isMounted && this.graph)
            updateChart(this.graph, this.props.chartData)
    }
    render() {
        return (
            <div>
                <h4>{i18n(this.props.chartData.measureName)}, {i18n(this.props.chartData.population)}</h4>
                <svg className="chart" ref={self => this.graph = self} id="graph" zoomAndPan="magnify" viewBox="0 0 820 520" preserveAspectRatio="xMidYMid meet" style={{width: 100 + "%"}}>
                    
                    <g className="y-axis"></g>
                    <g className="x-axis"></g>
                </svg>
            </div>
        )
        
        //<p>{JSON.stringify(this.props.chartData, null, 4)}</p>
    }
}