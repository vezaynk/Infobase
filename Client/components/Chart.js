// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { initChart, updateChart } from '../renderChart';
import type { ChartData } from '../types';

export class Chart extends React.Component<{ chartData: ChartData, animate: boolean }, { isMounted: boolean, highlightIndex: number }> {
    graph: ?Element;

    componentDidMount() {
        this.setState({ isMounted: false, highlightIndex: -1 });
        let isTrend = this.props.chartData.xAxis.includes("Trend");
        if (this.graph)
            initChart(this.graph, this.props.chartData, this.props.animate, (highlightIndex) => this.setState(
                {
                    ...this.state,
                    highlightIndex
                }
            ), isTrend
            );

        this.setState({ ...this.state, isMounted: true })
    }
    componentDidUpdate() {
        let valueUpper = -1;
        let valueLower = -2;
        let isTrend = this.props.chartData.xAxis.includes("Trend");
        let highlighted = this.props.chartData.points.filter(p => p.type == 0 || isTrend)[this.state.highlightIndex];
        if (highlighted) {
            if (highlighted.valueUpper)
                valueUpper = highlighted.valueUpper;
            if (highlighted.valueLower)
                valueLower = highlighted.valueLower;
        }

        console.log(this.state.highlightIndex, valueUpper, valueLower, highlighted, this.props.chartData.points)
        if (this.state.isMounted && this.graph)
            updateChart(this.graph, this.props.chartData, this.props.animate, this.state.highlightIndex, valueUpper, valueLower, isTrend);
    }
    render() {
		console.log(this.props.chartData);
        return (
            <figure>
                <figcaption className="text-center" style={{ margin: "0 10px" }}>
                    <strong>{this.props.chartData.title}</strong>
                </figcaption>

                <svg className="chart" ref={self => this.graph = self} id="graph" zoomAndPan="magnify" viewBox="0 0 820 500" preserveAspectRatio="xMidYMid meet" width="820px" height="500px">
                    <defs>
                        <filter x="0" y="0" width="1" height="1" id="solid">
                            <feFlood floodColor="#ffffff" />
                            <feComposite in="SourceGraphic" />
                            <feComponentTransfer>
                                <feFuncA type="linear" slope="0.7" />
                            </feComponentTransfer>
                        </filter>
                        <pattern id="linePattern" patternUnits="userSpaceOnUse" width="10" height="10">
                            <path d="M 0,11 l 5,-5 M -3,3 l 2.5,-10 M 3.75,1.25 5 2.5,-2.5, 0" strokeWidth="4" shapeRendering="auto" stroke="red" strokeLinecap="square">
                            </path>
                        </pattern>
                    </defs>
                    <g className="y-axis"></g>
                    <g className="x-axis"></g>
                </svg>
            </figure>
        );
    }
}