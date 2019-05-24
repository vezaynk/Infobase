// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { renderChart } from '../renderChart';
import { ChartData } from '../types';

type ChartProps = { chartData: ChartData, animate: boolean };
type ChartState = { isMounted: boolean, highlightIndex: number };
export class Chart extends React.Component<ChartProps, ChartState> {
    graph: Element | void;

    componentDidMount() {
        this.setState({ ...this.state, isMounted: true, highlightIndex: -1 })
    }
    componentDidUpdate() {
        let valueUpper = -1;
        let valueLower = -2;
        let isTrend = this.props.chartData.xAxis.includes("Trend") || this.props.chartData.xAxis.includes("Tendence");
        let highlighted = this.props.chartData.points.filter(p => p.type == 0 || isTrend)[this.state.highlightIndex];
        if (highlighted) {
            if (highlighted.valueUpper)
                valueUpper = highlighted.valueUpper;
            if (highlighted.valueLower)
                valueLower = highlighted.valueLower;
        }

        // console.log(this.state.highlightIndex, valueUpper, valueLower, highlighted, this.props.chartData.points)
        if (this.state.isMounted && this.graph)
            renderChart(this.graph, this.props.chartData, this.props.animate, this.state.highlightIndex, valueUpper, valueLower, isTrend, index => this.setState({ ...this.state, highlightIndex: index }));
    }
    render() {
        // console.log(this.props.chartData);
        return (
            <figure>
                <figcaption className="text-center" style={{ margin: "0 10px" }}>
                    <strong>{this.props.chartData.title}</strong>
                </figcaption>

                <svg className="chart" ref={self => this.graph = self} id="graph" zoomAndPan="magnify" viewBox="0 0 820 500" preserveAspectRatio="xMidYMid meet" width="820px" height="500px">
                    <defs>
                        <filter x="0" y="0" width="1" height="1" id="solid">
                            <feFlood floodColor="#ffffff"></feFlood>
                            <feComposite in="SourceGraphic"></feComposite>
                            <feComponentTransfer>
                                <feFuncA type="linear" slope="0.7"></feFuncA>
                            </feComponentTransfer>
                        </filter>
                        <pattern id="linePattern" patternUnits="userSpaceOnUse" width="10" height="10">
                            <path d="M 0,11 l 5,-5 M -3,3 l 2.5,-10 M 3.75,1.25 5 2.5,-2.5, 0" strokeWidth="4" shapeRendering="auto" stroke="red" strokeLinecap="square"></path>
                        </pattern>
                        <linearGradient id="gradient" x1="0%" y1="0%" x2="100%" y2="0" spreadMethod="pad">
                            <stop offset="0%" stopColor="steelblue" stopOpacity="1"></stop>
                            <stop offset="100%" stopColor="#56a0dd" stopOpacity="0"></stop>
                        </linearGradient>
                    </defs>
                    <g className="y-axis"></g>
                    <g className="x-axis"></g>
                    <text className="xAxisLabel" y="450" x="380" dy="1em" style={{ textAnchor: "middle", fontWeight: "bold", fontSize: "14px" }}></text>
                    <text className="yAxisLabel" transform="rotate(-90)" x="-200" y="20" style={{ textAnchor: "middle", fontWeight: "bold", fontSize: "14px" }}></text>
                    <g className="main" transform="translate(60,10)"></g>
                </svg>
            </figure>
        );
    }
}