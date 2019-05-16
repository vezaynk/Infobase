// @flow
import * as d3 from "d3";
import { i18n, numberFormat } from "./Translator";
import type { ChartData, TPoint } from './types';
const marginY = 10;
const marginX = 60;
const width = 700;
const height = 400;

let xAxisLabel, yAxisLabel;

d3.selection.prototype.moveToFront = function () {
    return this.each(function () {
        this.parentNode.appendChild(this);
    });
};
d3.selection.prototype.moveToBack = function () {
    return this.each(function () {
        var firstChild = this.parentNode.firstChild;
        if (firstChild) {
            this.parentNode.insertBefore(this, firstChild);
        }
    });
};

const isBetween = (val: number, up: number, low: number) => val >= low && val <= up;
const isPointInRange = (upper: ?number, lower: ?number, point: TPoint) => {

    if (!(upper && lower)) return false;
    const pUpper = point.valueUpper;
    const pLower = point.valueLower;
    const pVal = point.value;
    if (!(pUpper && pLower && pVal)) return false;

    // check if the selected bound is within the points bounds
    if (isBetween(upper, pUpper, pLower)) return true;
    if (isBetween(lower, pUpper, pLower)) return true;


    // check if point upper and lower bound is within selected bounds
    if (isBetween(pVal, upper, lower)) return true;
    if (isBetween(pLower, upper, lower)) return true;
    if (isBetween(pUpper, upper, lower)) return true;

    return false;

}

let updateHighlight: number => void = (index) => console.error("Must init graph");

export function updateChart(ref: Element, dataset: ChartData, highlightIndex: number, highlightUpper: number, highlightLower: number, isTrend: boolean): void {
    let chart = d3.select(ref);
    let select = chart.select(".main")

    let points = dataset.points.filter(point => point.type == 0 || isTrend)
    let averages = dataset.points.filter(point => point.type != 0 && !isTrend)

    if (points.length == 0) {
        points = averages;
        averages = [];
    }
    let x = d3.scaleBand()
        .domain(points.map(point => point.label))
        .range([0, width]);

    let y = d3.scaleLinear()
        .domain([0, d3.max(dataset.points, point => (point.valueUpper || point.value) * 1.1)])
        .range([height, 0]);

    chart.selectAll("g.y-axis")
        .attr("transform", "translate(" + marginX + "," + marginY + ")")
        .transition()
        .duration(600)
        .call(d3.axisLeft(y))
        .selectAll("text")
        .attr("font-size", 14)

    chart.selectAll("g.x-axis")
        .attr("transform", "translate(" + marginX + "," + (height + marginY) + ")")
        .style("font-size", 14)
        .attr("text-anchor", "start")
        .transition()
        .duration(600)
        .call(d3.axisBottom(x))

    chart.selectAll('g.x-axis .tick text')
        .style("transform", "rotate(10deg)");;
        

    xAxisLabel
        .text(i18n(dataset.xAxis))

    yAxisLabel
        .text(dataset.yAxis)
        .style("font-weight", "bold")

	
    console.log(points);
    let pointBinding = select.selectAll('g.point').data(points);
    let averageBinding = select.selectAll('g.average').data(averages);
    let cvUpperBinding = select.selectAll('g.cvUpper').data(points);
    let cvLowerBinding = select.selectAll('g.cvLower').data(points);
    let cvConnectBinding = select.selectAll('g.cvConnect').data(points);

    let enteredcvUpper = pointBinding.enter().append("g").attr("class", "cvUpper").append("rect")

    let enteredRect = pointBinding.enter().append("g").attr("class", "point").append("rect")

    enteredRect.attr("x", (d, i) => (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2)
        .attr("width", isTrend ? 10 : 25)
        //.style("fill", "steelblue")
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("y", height)
        .transition()
        .duration((_, i) => 600)
        .attr("y", (d) => isTrend ? y(d.value) - 5 : y(d.value))
        .attr("height", d => isTrend ? 10 : height - y(d.value))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i) => isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex ? 0.2 : 1)

    enteredRect.append("title")
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + /*" " +*/ i18n(dataset.unit, "Index"));

    pointBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("width", isTrend ? 10 : 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2)
        .attr("height", (d, i) => isTrend ? 10 : height - y(d.value))
        .attr("y", (d) => isTrend ? y(d.value) - 5 : y(d.value))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i) => isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex ? 1 : 1) //Point A to 0.2 to bring back functionality
        .select("title")
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + " " + i18n(dataset.unit, "Index"));


    pointBinding.on("mouseover", (_, i) => updateHighlight(i))
    pointBinding.on("mouseout", (_, i) => updateHighlight(-1))
    pointBinding.exit().remove();


    enteredcvUpper.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("width", 25)
        .style("fill", "black")
        .attr("y", height)
        .transition()
        .duration((_, i) => 600)
        .attr("y", (d) => y(d.valueUpper))
        .attr("height", 2)

    cvUpperBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("width", 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("height", 2)
        .attr("y", (d) => y(d.valueUpper))
        .style("fill", "black")


    cvUpperBinding.exit().remove();

    let enteredcvConnect = cvConnectBinding.enter().append("g").attr("class", "cvConnect").append("rect")


    enteredcvConnect.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 2 / 2)
        .attr("width", 2)
        .style("fill", "black")
        .attr("y", height)
        .transition()
        .duration((_, i) => 600)
        .attr("height", d => y(d.valueLower) - y(d.valueUpper))
        .attr("y", (d) => y(d.valueUpper))

    cvConnectBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("width", 2)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 2 / 2)
        .attr("height", d => y(d.valueLower) - y(d.valueUpper))
        .attr("y", (d) => y(d.valueUpper))
        .style("fill", "black")


    cvConnectBinding.exit().remove();

    let enteredcvLower = pointBinding.enter().append("g").attr("class", "cvLower").append("rect")


    enteredcvLower.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("width", 25)
        .style("fill", "black")
        .attr("y", height)
        .transition()
        .duration((_, i) => 600)
        .attr("y", (d) => y(d.valueLower))
        .attr("height", 2)

    cvLowerBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("width", 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("height", 2)
        .attr("y", (d) => y(d.valueLower))
        .style("fill", "black")


    cvLowerBinding.exit().remove();

    
    let enteredAverage = averageBinding.enter().append("g");

    enteredAverage
        .attr("class", "average")
        .append("rect")
        .attr('fill', 'url(#linePattern)')
        .attr("height", 2)
        .attr("x", 0)
        .attr("width", width)
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("y", height)
        .transition()
        .duration((_, i) => 600)
        .attr("y", (d) => y(d.value))


    let lowestValueIndex = (points.map(p => +p.value).indexOf(Math.min(...points.map(p => +p.value))));
    if (points.length == 2)
        lowestValueIndex = 1.5;

    let leftLabel = lowestValueIndex < points.length / 2;

    enteredAverage
        .append("text")
        .attr("x", () => width)
        .attr("y", height)
        .attr("filter", "url(#solid)")
        .transition()
        .duration((_, i) => 600)
        .attr("y", d => y(d.value) - 5)
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + i18n(dataset.yAxis, "Index"))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("y", (d) => y(d.value))

    averageBinding
        .select("text")
        .attr("filter", "url(#solid)")
        .transition()
        .duration(600)
        .attr("x", () => width)
        .attr("y", d => y(d.value) - 5)
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + i18n(dataset.unit, "Index"))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding.exit()
        .selectAll("rect, text")
        .transition()
        .duration(600)
        .attr("y", -marginY)
        .style("opacity", 0)

    averageBinding.exit()
        .transition()
        .duration(800)
        .remove();

    averageBinding.moveToFront();


    let myLine = d3.line().x((d, i) => ((i + 0.5) * (width / points.length) - 10 / 2 + 2.5)).y(d => y(d.value))


    let paths = chart.selectAll("path.line").data(isTrend ? [points] : []);

    paths.enter().append("path")
        .attr("class", "line")
        .attr("d", myLine)
        .attr("fill", "none")
        .attr("stroke", "steelblue")
        .attr("stroke-width", 2)
        .attr("opacity", 0)
        .attr("transform", "translate(" + (marginX) + ",-" + 10 + ")")
        .transition()
        .duration(600)
        .attr("transform", "translate(" + (marginX) + "," + marginY + ")")
        .attr("opacity", 1)

    paths.attr("class", "line")
        .attr("d", myLine)
        .attr("stroke", "steelblue")
        .attr("stroke-width", 2)

    paths.exit()
        .transition()
        .duration(600)
        .attr("transform", "translate(" + (marginX + 2.5) + ",-" + 10 + ")")
        .attr("opacity", 0)
        .remove();
}

export function initChart(ref: Element, dataset: ChartData, update: number => void, isTrend: boolean) {
    updateHighlight = update;
    const svg = d3.select(ref)

    var chart = svg;

    var gradient = chart.append("defs")
        .append("linearGradient")
        .attr("id", "gradient")
        .attr("x1", "0%")
        .attr("y1", "0%")
        .attr("x2", "100%")
        .attr("y2", "0")
        .attr("spreadMethod", "pad");

    gradient.append("stop")
        .attr("offset", "0%")
        .attr("stop-color", "steelblue")
        .attr("stop-opacity", 1);

    gradient.append("stop")
        .attr("offset", "100%")
        .attr("stop-color", "#56a0dd")
        .attr("stop-opacity", 0);

    xAxisLabel = chart.append("text")
        .attr("y", height + marginY + 60)
        .attr("x", (marginX + width) / 2)
        .attr("dy", "1em")
        .style("text-anchor", "middle")
        .style("font-weight", "bold");

    yAxisLabel = chart.append("text")
        .attr("transform", "rotate(-90)")
        .attr("x", -(height/2))
        .attr("y", 20)
        .style("text-anchor", "middle");

    let select = chart
        .append("g")
        .attr('class', 'main')
        .attr("transform", "translate(" + marginX + "," + marginY + ")")


    updateChart(ref, dataset, -1, 0, 0, isTrend);
}