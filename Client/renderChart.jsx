// @flow
import * as d3 from "d3";
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
        
        
        let x = d3.scaleBand()
            .domain(dataset.points.map(point => point.label["(EN, )"]))
            .range([0,width]);
        
            
        let y = d3.scaleLinear()
            .domain([0,d3.max(dataset.points, point => point.value)])
            .range([height,0]);
        
        
        let binding = select.selectAll('rect').data(dataset.points)
        console.log(binding)
        
        chart.selectAll("g.y-axis")
            .attr("transform", "translate(" + margin + "," + margin + ")")
            .transition()
            .duration(300)
            .call(d3.axisLeft(y));
        
        chart.selectAll("g.x-axis")
            .attr("transform", "translate(" + margin + "," + (height+margin) + ")")
            .transition()
            .duration(300)
            .call(d3.axisBottom(x))
            
          .selectAll("text")
            .attr("transform", "rotate(15)")
            .style("text-anchor", "start");
        
        xAxisLabel
              .text(dataset.xAxis["(EN, )"]); 
        
        yAxisLabel
              .text(dataset.yAxis["(EN, Datatool)"]); 
              
        
            binding.enter().append("rect")
            .attr("width", (_, notFirst) => (!isTrend && !notFirst) ? width : (isTrend ? 10 : 25))
            .style("fill", (_, notFirst) => (!isTrend && !notFirst) ? "url(#gradient)" : "steelblue")
            .attr("x", (d,i) => (i+0.5)*(width/dataset.points.length)-25/2 )
            .attr("y", (d) => y(d.value))
            .attr("ry",(isTrend ? 10 : 0))
            .attr("rx", (isTrend ? 10 : 0))
            .transition()
            .duration((_, i) => Math.log(i+1)*500)
            .attr("height", d => isTrend ? 25 : height - y(d.value));
              
            
            binding
            .transition()
            .duration(300)
            .attr("ry",(isTrend ? 10 : 0))
            .attr("rx", (isTrend ? 10 : 0))
            .attr("height", d => isTrend ? 10 : height - y(d.value))
            .attr("width", (_, notFirst) => (!isTrend && !notFirst) ? width-margin : (isTrend ? 10 : 25))
            .attr("x", (d,i) => (i+0.5)*(width/dataset.points.length)-(isTrend ? 10 : 25)/2 )
            .attr("y", (d) => y(d.value))
            
            .style("fill", (_, notFirst) => (!isTrend && !notFirst) ? "url(#gradient)" : "steelblue");
            
            binding.exit().remove();
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
.attr("transform", "rotate(90)")
.attr("x", height/2+margin)
.attr("y", -10)
.style("text-anchor", "middle");	 

let select = chart
	    .append("g")
	    .attr('class', 'main')
	        .attr("transform","translate(" + margin + "," + margin + ")")
 
	    
updateChart(ref, dataset);
}