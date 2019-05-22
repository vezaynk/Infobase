// @flow
import * as d3 from "d3";
import { numberFormat } from "./Translator";
import type { ChartData, TPoint } from './types';
const marginY = 10;
const marginX = 60;
const width = 700;
const height = 400;

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

function wrap(text, width) {
    text.each(function () {
        var text = d3.select(this),
            words = text.text().split(/\s+/).reverse(),
            word,
            line = [],
            lineNumber = 0,
            lineHeight = 1.1, // ems
            x = text.attr("x"),
            y = text.attr("y"),
            dy = 0, //parseFloat(text.attr("dy")),
            tspan = text.text(null)
                        .append("tspan")
                        .attr("x", x)
                        .attr("y", y)
                        .attr("dy", dy + "em");
        while (word = words.pop()) {
            line.push(word);
            tspan.text(line.join(" "));
            if (tspan.node().getComputedTextLength() > width) {
                line.pop();
                tspan.text(line.join(" "));
                line = [word];
                tspan = text.append("tspan")
                            .attr("x", x)
                            .attr("y", y)
                            .attr("dy", ++lineNumber * lineHeight + dy + "em")
                            .text(word);
            }
        }
    });
}

export function renderChart(ref: Element, dataset: ChartData, animate: boolean, highlightIndex: number, highlightUpper: number, highlightLower: number, isTrend: boolean, updateHighlight: number => void): void {
    let chart = d3.select(ref);
    let select = chart.select(".main")

    let points = dataset.points.filter(point => point.type == 0 || isTrend)
    let averages = dataset.points.filter(point => point.type != 0 && !isTrend)

    let animationDuration = animate ? 600 : 0;
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
        .duration(animationDuration)
        .call(d3.axisLeft(y))
        .selectAll("text")
        .attr("font-size", 14)

    chart.selectAll("g.x-axis")
        .attr("transform", "translate(" + marginX + "," + (height + marginY) + ")")
        .style("font-size", 14)
        .attr("text-anchor", "middle")
        .transition()
        .duration(animationDuration)
        .call(d3.axisBottom(x))
        .selectAll("text")
        .call(wrap, 700/points.length);
        

    chart.select('.xAxisLabel')
        .text(dataset.xAxis)

    chart.select('.yAxisLabel')
        .text(dataset.yAxis)
        .style("font-weight", "bold")

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
        .duration(animationDuration)
        .attr("y", (d) => isTrend ? y(d.value) - 5 : y(d.value))
        .attr("height", d => isTrend ? 10 : height - y(d.value))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i) => isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex ? 0.2 : 1)

    enteredRect.append("title")
        .text(d => d.label + ": " + numberFormat(d.value, dataset.unit));

    pointBinding
        .select("rect")
        .transition()
        .duration(animationDuration)
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("width", isTrend ? 10 : 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2)
        .attr("height", (d, i) => isTrend ? 10 : height - y(d.value))
        .attr("y", (d) => isTrend ? y(d.value) - 5 : y(d.value))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i) => isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex ? 1 : 1) //Point A to 0.2 to bring back functionality
        .select("title")
        .text(d => d.label + ": " + numberFormat(d.value, dataset.unit));


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
        .duration(animationDuration)
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
        .duration(animationDuration)
        .attr("height", d => y(d.valueLower) - y(d.valueUpper))
        .attr("y", (d) => y(d.valueUpper))

    cvConnectBinding
        .select("rect")
        .transition()
        .duration(animationDuration)
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
        .duration(animationDuration)
        .attr("y", (d) => y(d.valueLower))
        .attr("height", 2)

    cvLowerBinding
        .select("rect")
        .transition()
        .duration(animationDuration)
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
        .text(d => d.label + ": " + numberFormat(d.value, dataset.unit))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding
        .select("rect")
        .transition()
        .duration(animationDuration)
        .attr("y", (d) => y(d.value))

    averageBinding
        .select("text")
        .attr("filter", "url(#solid)")
        .transition()
        .duration(animationDuration)
        .attr("x", () => width)
        .attr("y", d => y(d.value) - 5)
        .text(d => d.label + ": " + numberFormat(d.value, dataset.unit))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding.exit()
        .selectAll("rect, text")
        .transition()
        .duration(animationDuration)
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
        .duration(animationDuration)
        .attr("transform", "translate(" + (marginX) + "," + marginY + ")")
        .attr("opacity", 1)

    paths.attr("class", "line")
        .attr("d", myLine)
        .attr("stroke", "steelblue")
        .attr("stroke-width", 2)

    paths.exit()
        .transition()
        .duration(animationDuration)
        .attr("transform", "translate(" + (marginX + 2.5) + ",-" + 10 + ")")
        .attr("opacity", 0)
        .remove();
}