// @flow
import * as d3 from "d3";
import { i18n } from "./Translator";
import type { ChartData } from './types';
const margin = 60;
const width = 700;
const height = 400;

let xAxisLabel, yAxisLabel;

export function updateChart(ref: Element, dataset: ChartData): void {
        let chart = d3.select(ref);
        let select = chart.select(".main")
  

        chart.select("#chartTitle")
            .text(dataset.measureName["(EN, Datatool)"] + ", " + dataset.population["(EN, Index)"])
        
        let isTrend = dataset.xAxis["(EN, )"].includes("Trend");
        
        let points = dataset.points.filter(point => point.type == 0 || isTrend)
        let averages = dataset.points.filter(point => point.type != 0 && !isTrend)

        let x = d3.scaleBand()
            .domain(points.map(point => point.label["(EN, )"]))
            .range([0,width]);
        
            
        let y = d3.scaleLinear()
            .domain([0,d3.max(dataset.points, point => point.value)])
            .range([height,0]);
        
        console.log(points)
        let pointBinding = select.selectAll('g.point').data(points);
        let averageBinding = select.selectAll('g.average').data(averages);
        
        chart.selectAll("g.y-axis")
            .attr("transform", "translate(" + margin + "," + margin + ")")
            .transition()
            .duration(600)
            .call(d3.axisLeft(y));
        
        chart.selectAll("g.x-axis")
            .attr("transform", "translate(" + margin + "," + (height+margin) + ")")
            .transition()
            .duration(600)
            .call(d3.axisBottom(x))
            
          .selectAll("text")
            .attr("transform", "rotate(15)")
            .style("text-anchor", "start");
        
        xAxisLabel
              .text(dataset.xAxis["(EN, )"]); 
        
        yAxisLabel
              .text(dataset.yAxis["(EN, Datatool)"]); 
              
        
              pointBinding.enter().append("g").attr("class", "point").append("rect")
            .attr("x", (d,i) => (i+0.5)*(width/points.length)-25/2 )
            
            
            .attr("width", isTrend ? 10 : 25)
            .style("fill", "steelblue")
            .attr("ry",(isTrend ? 10 : 0))
            .attr("rx", (isTrend ? 10 : 0))
            .attr("y", height)
            .transition()
            .duration((_, i) => 600)
            .attr("y", (d) => y(d.value))
            .attr("height", d => isTrend ? 10 : height - y(d.value));
              
            
            pointBinding
            .select("rect")
            .transition()
            .duration(600)
            .attr("ry",(isTrend ? 10 : 0))
            .attr("rx", (isTrend ? 10 : 0))
            .attr("width", isTrend ? 10 : 25)
            .attr("x", (d,i) => (i+0.5)*(width/points.length)-(isTrend ? 10 : 25)/2 )
            .attr("height", (d, i) => isTrend ? 10 : height - y(d.value))
            .attr("y", (d) => y(d.value))
            
            .style("fill", "steelblue");
            
            pointBinding.exit().remove();

            let entered = averageBinding.enter().append("g");
            
            entered
            .attr("class", "average")
            .append("rect")
            .attr("height", 1)
            .attr("x", 0 )
            .attr("width", width)
            .style("fill", "red")
            .attr("ry",(isTrend ? 10 : 0))
            .attr("rx", (isTrend ? 10 : 0))
            .attr("y", height)
            .transition()
            .duration((_, i) => 600)
            .attr("y", (d) => y(d.value))

            entered
            .append("text")
            .attr("x", () => (width/points.length)*(points.map(p=>p.value).indexOf(Math.min(...points.map(p=>p.value)))) )
            .attr("y", height )
            .transition()
            .duration((_, i) => 600)
            .attr("y", d => y(d.value) - 5)
            .text(d => i18n(d.label) + ": " + Math.round(d.value*10)/10)
            
              
            
            averageBinding
            .select("rect")
            .transition()
            .duration(600)
            .attr("y", (d) => y(d.value))
            .style("fill", "red")

            let lowestValueIndex = (points.map(p=>p.value).indexOf(Math.min(...points.map(p=>p.value).filter(p => p))));
            averageBinding
            .select("text")
            .transition()
            .duration(600)
            .attr("x", () => (width/points.length) * (lowestValueIndex))
            .attr("y", d => y(d.value) - 5)
            .text(d => i18n(d.label) + ": " + Math.round(d.value*10)/10)
            .attr("text-anchor", lowestValueIndex < points.length ? "start" : "end")

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

            let myLine = d3.line().x((d, i) => ((i+0.5)*(width/points.length)-10/2 + 2.5)).y(d => y(d.value) + 5)

            
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
            .attr("transform", "translate(" + (margin+2.5) + ",-" + 10 + ")")
            .attr("opacity", 0)
            .remove();
}

export function initChart(ref: Element, dataset: ChartData) {
    
    const svg = d3.select(ref)

var chart = svg
		.attr("width",width + 2*margin)
	    .attr("height",height + 2*margin);
	    
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
.attr("y", height+margin+40)
.attr("x", (margin+width)/2)
.attr("dy", "1em")
.style("text-anchor", "middle");

yAxisLabel = chart.append("text")
.attr("transform", "rotate(-90)")
.attr("x", -(height/2+margin))
.attr("y", margin-30)
.style("text-anchor", "middle");	 

let select = chart
	    .append("g")
	    .attr('class', 'main')
	        .attr("transform","translate(" + margin + "," + margin + ")")
 
	    
updateChart(ref, dataset);
}