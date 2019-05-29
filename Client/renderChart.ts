import * as d3 from "d3";
import { numberFormat } from "./translator";
import { ChartData, TPoint } from './types';
const marginY = 10;
const marginX = 60;
const width = 700;
const height = 400;

const moveToFront = function (node: any) {
    return node.each(function () {
        node.parentNode.appendChild(node);
    });
}
const moveToBack = function (node: any) {
    return node.each(function () {
        var firstChild = node.parentNode.firstChild;
        if (firstChild) {
            node.parentNode.insertBefore(node, firstChild);
        }
    });
};



const isBetween = (val: number, up: number, low: number) => val >= low && val <= up;
const isPointInRange = (upper: number | void, lower: number | void, point: TPoint) => {

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

function wrap(text: d3.Selection<d3.BaseType, {}, d3.BaseType, {}>, width: number, inverted: boolean = false) {
    if (Object.keys(window).length == 0)
        return;
        
    text.each(function () {
        let text = d3.select(this),
            words = text.text().split(/\s+/).reverse(),
            word,
            line: string[] = [],
            lineNumber = 0,
            lineHeight = 1.1, // ems
            x = text.attr("x"),
            y = text.attr("y"),
            dy = 0, //parseFloat(text.attr("dy")),
            tspan = text.text(null)
                .append("tspan")
                .attr("x", x)
                .attr("y", y)
                .attr("dy", -dy + "em");
        while (word = words.pop()) {
            line.push(word);
            tspan.text(line.join(" "));
            if (tspan.node().getComputedTextLength() > width) {
                line.pop();
                tspan.text(line.join(" "));
                line = [word];
                tspan = text.append("tspan")
                    .attr("x", x || 0)
                    .attr("y", y)
                    .attr("dy", (inverted ? -1 : 1) * (++lineNumber * lineHeight + dy) + "em")
                    .text(word);
            }
        }
    });
}

export function renderChart(ref: Element, dataset: ChartData, animate: boolean, highlightIndex: number, highlightUpper: number, highlightLower: number, isTrend: boolean, updateHighlight: (newIndex: number) => void): void {
    let chart = d3.select(ref);
    let select = chart.select(".main")

    let points = dataset.points.filter(point => point.type == 0 || isTrend)
    let averages = dataset.points.filter(point => point.type != 0 && !isTrend)

    let animationDuration = 600;
    d3.selection.prototype.optionalTransition = function() {
        if (!animate)
            return this;

        return this.transition().duration(animationDuration);
    };

    if (points.length == 0) {
        points = averages;
        averages = [];
    }
    let x = d3.scaleBand()
        .domain(points.map(point => point.label))
        .range([0, width]);

    let y = d3.scaleLinear()
        .domain([0, d3.max(dataset.points, point => (point.valueUpper || point.value || 0) * 1.1)])
        .range([height, 0]);

    chart.selectAll("g.y-axis")
        .attr("transform", `translate(${marginX}, ${marginY})`)
        // @ts-ignore
        .optionalTransition()
        // @ts-ignore
        .call(d3.axisLeft(y))
        .selectAll("text")
        .attr("font-size", "10px");

    chart.selectAll("g.x-axis")
        .attr("transform", `translate(${marginX}, ${height + marginY})`)
        // @ts-ignore
        .call(d3.axisBottom(x))
        .selectAll(".tick text")
        .attr("font-size", "10px")
        .attr("y", 15)
        .attr("text-anchor", "middle")
        .call(wrap, 500/points.length);

    chart.select('.xAxisLabel')
          .text(dataset.xAxis)
          .call(wrap, 600)

      chart.select('.yAxisLabel')
          .text(dataset.yAxis)
          .call(wrap, 400)

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
        // @ts-ignore
        .optionalTransition()
        .attr("y", (d) => isTrend ? y(d.value || 0) - 5 : y(d.value || 0))
        .attr("height", d => isTrend ? 10 : height - y(d.value || 0))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i) => isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex ? 0.2 : 1)

    enteredRect.append("title")
        .text(d => d.label + ": " + numberFormat(d.value || 0, dataset.unit));

    pointBinding
        .select("rect")
        // @ts-ignore
        .optionalTransition()
        .attr("ry", (isTrend ? 10 : 0))
        .attr("rx", (isTrend ? 10 : 0))
        .attr("width", isTrend ? 10 : 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2)
        .attr("height", (d, i) => isTrend ? 10 : height - y(d.value || 0))
        .attr("y", (d) => isTrend ? y(d.value || 0) - 5 : y(d.value || 0))
        .attr("fill", "steelblue")
        .attr("opacity", (d, i) => isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex ? 1 : 1) //Point A to 0.2 to bring back functionality
        .select("title")
        .text(d => d.label + ": " + numberFormat(d.value || 0, dataset.unit));


    pointBinding.on("mouseover", (_, i) => updateHighlight(i))
    pointBinding.on("mouseout", (_, i) => updateHighlight(-1))
    pointBinding.exit().remove();


    enteredcvUpper.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("width", 25)
        .style("fill", "black")
        // .attr("y", height)
        // @ts-ignore
        .optionalTransition()
        .attr("y", (d) => y(d.valueUpper || 0))
        .attr("height", 2)

    cvUpperBinding
        .select("rect")
        // @ts-ignore
        .optionalTransition()
        .attr("width", 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("height", 2)
        .attr("y", (d) => y(d.valueUpper || 0))
        .style("fill", "black")


    cvUpperBinding.exit().remove();

    let enteredcvConnect = cvConnectBinding.enter().append("g").attr("class", "cvConnect").append("rect")


    enteredcvConnect.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 2 / 2)
        .attr("width", 2)
        .style("fill", "black")
        .attr("y", height)
        // @ts-ignore
        .optionalTransition()
        .attr("height", d => y(d.valueLower || 0) - y(d.valueUpper || 0))
        .attr("y", (d) => y(d.valueUpper || 0))

    cvConnectBinding
        .select("rect")
        // @ts-ignore
        .optionalTransition()
        .attr("width", 2)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 2 / 2)
        .attr("height", d => y(d.valueLower || 0) - y(d.valueUpper || 0))
        .attr("y", (d) => y(d.valueUpper || 0))
        .style("fill", "black")


    cvConnectBinding.exit().remove();

    let enteredcvLower = pointBinding.enter().append("g").attr("class", "cvLower").append("rect")


    enteredcvLower.attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("width", 25)
        .style("fill", "black")
        .attr("y", height)
        // @ts-ignore
        .optionalTransition()
        .attr("y", (d) => y(d.valueLower || 0))
        .attr("height", 2)

    cvLowerBinding
        .select("rect")
        // @ts-ignore
        .optionalTransition()
        .attr("width", 25)
        .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
        .attr("height", 2)
        .attr("y", (d) => y(d.valueLower || 0))
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
        // .attr("y", height)
        // @ts-ignore
        .optionalTransition()
        .attr("y", (d) => y(d.value || 0))


    let lowestValueIndex = (points.map(p => +p.value).indexOf(Math.min(...points.map(p => +p.value))));
    if (points.length == 2)
        lowestValueIndex = 1.5;

    let leftLabel = lowestValueIndex < points.length / 2;

    enteredAverage
        .append("text")
        .attr("x", () => width)
        .attr("y", height)
        .attr("filter", "url(#solid)")
        // @ts-ignore
        .optionalTransition()
        .attr("y", d => y(d.value || 0) - 5)
        .text(d => d.label + ": " + numberFormat(d.value || 0, dataset.unit))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding
        .select("rect")
        // @ts-ignore
        .optionalTransition()
        .attr("y", (d) => y(d.value || 0))

    averageBinding
        .select("text")
        .attr("filter", "url(#solid)")
        // @ts-ignore
        .optionalTransition()
        .attr("x", () => width)
        .attr("y", d => y(d.value || 0) - 5)
        .text(d => d.label + ": " + numberFormat(d.value || 0, dataset.unit))
        .attr("text-anchor", "end")
        .style("font-weight", "bold")

    averageBinding.exit()
        .selectAll("rect, text")
        // @ts-ignore
        .optionalTransition()
        .attr("y", -marginY)
        .style("opacity", 0)

    averageBinding.exit()
    // @ts-ignore    
    .optionalTransition()
        .remove();

    averageBinding.raise();
}