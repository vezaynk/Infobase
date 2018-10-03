// @flow
import * as d3 from "d3";
import { i18n, numberFormat } from "./Translator";
import type { ChartData, TPoint } from './types';
const margin = 60;
const width = 700;
const height = 600;

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

const isBetween = (val, up, low) => val >= low && val <= up;
const isPointInRange = (upper, lower, point: TPoint) => {
    let checkRange = val => isBetween(val, upper, lower);
    console.log(upper, lower, point)
    debugger;
    if (checkRange(point.valueUpper) || checkRange(point.value) || checkRange(point.valueLower))
        return true;

    checkRange = val => isBetween(val, point.valueUpper, point.valueLower);
    if (checkRange(upper) || checkRange(lower))
        return true;

    return false;
}

let updateHighlight = (index: number, upper: number, lower: number): void => console.error("Must init graph");

export function updateChart(ref: Element, dataset: ChartData, highlightIndex: number, highlightUpper: number, highlightLower: number): void {
    let chart = d3.select(ref);
    let select = chart.select(".main")

    let isTrend = i18n(dataset.xAxis).includes("Trend");

    let points = dataset.points.filter(point => point.type == 0 || isTrend)
    let averages = dataset.points.filter(point => point.type != 0 && !isTrend)

    let x = d3.scaleBand()
        .domain(points.map(point => i18n(point.label)))
        .range([0, width]);


    let y = d3.scaleLinear()
        .domain([0, d3.max(dataset.points, point => (point.valueUpper || point.value) * 1.1)])
        .range([height, 0]);
    chart.selectAll("g.y-axis")
        .attr("transform", "translate(" + margin + "," + margin + ")")
        .transition()
        .duration(600)
        .call(d3.axisLeft(y))
        .selectAll("text")
        .attr("font-size", 14);

    chart.selectAll("g.x-axis")
        .attr("transform", "translate(" + margin + "," + (height + margin) + ")")
        .style("font-size", 10)
        .transition()
        .duration(600)
        .call(d3.axisBottom(x))


    xAxisLabel
        .text(i18n(dataset.xAxis));

    yAxisLabel
        .text(i18n(dataset.yAxis, "Datatool"))
        .style("font-weight", "bold")


    console.log(points)
    let pointBinding = select.selectAll('g.point').data(points);
    let averageBinding = select.selectAll('g.average').data(averages);
    let cvUpperBinding = select.selectAll('g.cvUpper').data(points);
    let cvLowerBinding = select.selectAll('g.cvLower').data(points);
    let cvConnectBinding = select.selectAll('g.cvConnect').data(points);
    /////

    let enteredcvUpper = pointBinding.enter().append("g").moveToBack().attr("class", "cvUpper").append("rect")


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

    let enteredcvConnect = cvConnectBinding.enter().append("g").moveToBack().attr("class", "cvConnect").append("rect")


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

    /////

    let enteredcvLower = pointBinding.enter().append("g").moveToBack().attr("class", "cvLower").append("rect")


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

    /////
    let enteredRect = pointBinding.enter().append("g").moveToBack().attr("class", "point").append("rect")

    enteredRect.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("width", isTrend ? 10 : 25)
        //.style("fill", "steelblue")
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("y", height)
        .transition()
        .duration((_, i) => 600)
        .attr("y", (d) => y(d.value))
        .attr("height", d => isTrend ? 10 : height - y(d.value))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i)=> !isPointInRange(highlightUpper, highlightLower, d) ? 1 : 0.2)

    enteredRect.append("title")
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + " " + i18n(dataset.yAxis, "Index"));


    pointBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("width", isTrend ? 10 : 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2)
        .attr("height", (d, i) => isTrend ? 10 : height - y(d.value))
        .attr("y", (d) => y(d.value))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i)=> !isPointInRange(highlightUpper, highlightLower, d) ? 1 : 0.2)
        .select("title")
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + " " + i18n(dataset.yAxis, "Index"));


        pointBinding.on("mouseover", (_, i) => updateHighlight(i))
        pointBinding.on("mouseout", (_, i) => updateHighlight(-1))
    pointBinding.exit().remove();

    let enteredAverage = averageBinding.enter().append("g");

    enteredAverage
        .attr("class", "average")
        .append("rect")
        .attr("height", 2)
        .attr("x", 0)
        .attr("width", width)
        .style("fill", "url(#linePattern)")
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
        .transition()
        .duration((_, i) => 600)
        .attr("y", d => y(d.value) - 5)
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + " " + i18n(dataset.yAxis, "Index"))
        .attr("text-anchor", "end")



    averageBinding
        .select("rect")
        .transition()
        .duration(600)
        .attr("y", (d) => y(d.value))
        .style("fill", "url(#linePattern)")

    averageBinding
        .select("text")
        .transition()
        .duration(600)
        .attr("filter", "url(#solid)")
        .attr("x", () => width)
        .attr("y", d => y(d.value) - 5)
        .text(d => i18n(d.label) + ": " + numberFormat(d.value) + " " + i18n(dataset.yAxis, "Index"))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding.exit()
        .selectAll("rect, text")
        .transition()
        .duration(600)
        .attr("y", -margin)
        .style("opacity", 0)

    averageBinding.exit()
        .transition()
        .duration(800)
        .remove();

    window.datum = points;

    let myLine = d3.line().x((d, i) => ((i + 0.5) * (width / points.length) - 10 / 2 + 2.5)).y(d => y(d.value) + 5)


    let paths = chart.selectAll("path.line").data(isTrend ? [points] : []);

    paths.enter().append("path")
        .attr("class", "line")
        .attr("d", myLine)
        .attr("fill", "none")
        .attr("stroke", "steelblue")
        .attr("stroke-width", 2)
        .attr("opacity", 0)
        .attr("transform", "translate(" + (margin) + ",-" + 10 + ")")
        .transition()
        .duration(600)
        .attr("transform", "translate(" + (margin) + "," + margin + ")")
        .attr("opacity", 1)

    paths.attr("class", "line")
        .attr("d", myLine)
        .attr("stroke", "steelblue")
        .attr("stroke-width", 2)

    paths.exit()
        .transition()
        .duration(600)
        .attr("transform", "translate(" + (margin + 2.5) + ",-" + 10 + ")")
        .attr("opacity", 0)
        .remove();
}

export function initChart(ref: Element, dataset: ChartData, update) {
    updateHighlight = update;
    const svg = d3.select(ref)

    var chart = svg
        .attr("width", width + 2 * margin)
        .attr("height", height + 2 * margin);

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
        .attr("y", height + margin + 55)
        .attr("x", (margin + width) / 2)
        .attr("dy", "1em")
        .style("text-anchor", "middle")
        .style("font-weight", "bold");

    yAxisLabel = chart.append("text")
        .attr("transform", "rotate(-90)")
        .attr("x", -(height / 2 + margin))
        .attr("y", margin - 40)
        .style("text-anchor", "middle");

    let select = chart
        .append("g")
        .attr('class', 'main')
        .attr("transform", "translate(" + margin + "," + margin + ")")


    updateChart(ref, dataset, -1, 0, 0);
}