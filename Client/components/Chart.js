// @flow
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { i18n } from "../Translator";
import { initChart, updateChart } from '../renderChart';
import type { ChartData } from '../types';

export class Chart extends React.Component<{ chartData: ChartData }, { isMounted: boolean, highlightIndex: number }> {
    graph: ?Element;

    componentDidMount() {
        this.setState({ isMounted: false, highlightIndex: -1 });
        let isTrend = i18n(this.props.chartData.xAxis).includes("Trend");
        if (this.graph)
            initChart(this.graph, this.props.chartData, (highlightIndex) => this.setState(
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
        let isTrend = i18n(this.props.chartData.xAxis).includes("Trend");
        let highlighted = this.props.chartData.points.filter(p => p.type == 0 || isTrend)[this.state.highlightIndex];
        if (highlighted) {
            if (highlighted.valueUpper)
                valueUpper = highlighted.valueUpper;
            if (highlighted.valueLower)
                valueLower = highlighted.valueLower;
        }

        console.log(this.state.highlightIndex, valueUpper, valueLower, highlighted, this.props.chartData.points)
        if (this.state.isMounted && this.graph)
            updateChart(this.graph, this.props.chartData, this.state.highlightIndex, valueUpper, valueLower, isTrend);

    }
    render() {
        return (
            <figure>
                <figcaption style={{ margin: "0 60px" }}>
                    <strong>{i18n(this.props.chartData.measureName)}, {i18n(this.props.chartData.population, "Datatool")}</strong>
                </figcaption>

                <svg className="chart" ref={self => this.graph = self} id="graph" zoomAndPan="magnify" viewBox="0 0 820 780" width="820" height="780" preserveAspectRatio="xMidYMid meet" style={{ width: 100 + "%" }}>
                    <defs>
                        <filter x="0" y="0" width="1" height="1" id="solid">
                            <feFlood floodColor="#ffffff" />
                            <feComposite in="SourceGraphic" />
                            <feComponentTransfer>
                                <feFuncA type="linear" slope="0.7" />
                            </feComponentTransfer>
                        </filter>
                        <pattern id="linePattern" patternUnits="userSpaceOnUse" width="10" height="10">
                            <path d="M 0,11 l 5,-5 M -3,3 l 2.5,-10 M 3.75,1.25 5 2.5,-2.5, 0" strokeWidth="4" shapeRendering="auto" stroke="#343434" strokeLinecap="square">
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