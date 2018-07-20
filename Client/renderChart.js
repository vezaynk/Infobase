import * as d3 from "d3";

export function renderChart(ref, dataset) {
    const svg = d3.select(ref)
    let highestValue = d3.max(dataset.values.map(v => d3.max(v.points.map(p => d3.max([p.value, p.confidence.upper, p.confidence.lower])))));
    //console.log(highestValue); - left for debbuging

    let margin = { top: 150, right: 10, bottom: 150, left: 50 };
    let width = /*+svg.attr("width")*/ 900 - margin.left - margin.right;
    let height = /*+svg.attr("height")*/ 800 - margin.top - margin.bottom;

    let g = svg.append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    let yScale = d3.scaleLinear()
        .domain([0, highestValue])
        .range([height, 0])

    g.append("g").call(d3.axisLeft(yScale))
        .attr("class", "x-axis")
        .attr("transform", "translate(" + 0 + "," + 0 + ")")

    g.append("text")
        .attr("class", "axis-label")
        .attr("transform", "rotate(-90)")
        .attr("y", 0 - margin.left)
        .attr("x", 0 - (height / 2))
        .attr("dy", "1em")
        .text(dataset.axis.y);


    g.append("text")
        .attr("class", "axis-label")
        .attr("y", height + 20)
        .attr("x", (width / 2))
        .attr("dy", "1em")
        .text(dataset.axis.x);

    g.append("text")
        .attr("class", "footnote")
        .attr("y", height + 40)
        .attr("x", 0)
        .attr("dy", "1em")
        .style("text-anchor", "start")
        .style("font-size", "10pt")
        .append("tspan")
        .attr("x", 0)
        .text("Public Health Infobase")
        .append("tspan")
        .attr("x", 0)
        .attr("dy", 15)
        .text("Public Health Agency of Canada")
        .append("tspan")
        .attr("x", 0)
        .attr("dy", 15)
        .text("https://infobase.phac-aspc.gc.ca")
        .append("tspan")
        .attr("x", 0)
        .attr("dy", 15)
        .text("email:  phac.infobase.aspc@canada.ca")



    g.append("text")
        .attr("y", height + 60)
        .attr("x", width)
        .attr("dy", "1em")
        .style("text-anchor", "end")
        .style("font-size", "10pt")
        .text(dataset.source)
    svg.select("#title")
        .text(dataset.title)


    // Sends the data point to the appropriate function
    dataset.values.forEach(({ points, type }) => {
        switch (type) {
            case 0:
                // Bars
                drawBars(points);
                break;
            case 1:
                // Trend
                drawTrend(points)
                break;
            case 2:
                // Line
                drawLines(points);
                break;
        }
    })

    // Let's begin with the actual bars
    function drawBars(points) {
        let computeBarWidth = () => width / points.length

        let binding = g.selectAll("g.bar")
            .data(points)
        let bar = binding
            .enter()
            .append("g")
            .attr("class", "bar")
            .attr("transform-origin", "50, 50")
            .attr("transform", (d, i) => "translate(" + computeBarWidth() * (i + 0.25) + "," + (yScale(d.value)) + "), scale(0.5, 1)")

        bar.append("title")
            .text(d => `${dataset.axis.x}: ${d.label}\n${dataset.axis.y}: ${d.value}`)

        //bar.append("rect").attr("height", 20).attr("width", 20)

        bar.append("rect")
            .attr("width", computeBarWidth())
            .attr("fill", "#cf7587")
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("height", d => height - yScale(d.value))

        // upper
        bar
            .append("line")
            .style("stroke", "#000")
            .attr("stroke-width", 2)
            .attr("x1", (_d, i) => 0)
            .attr("x2", (_d, i) => computeBarWidth())
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("y1", d => yScale(d.confidence.lower) - yScale(d.value))
            .attr("y2", d => yScale(d.confidence.lower) - yScale(d.value));

        bar
            .append("line")
            .style("stroke", "#000")
            .attr("stroke-width", 2)
            .attr("x1", (_d, i) => 0)
            .attr("x2", (_d, i) => computeBarWidth())
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("y1", d => yScale(d.confidence.upper) - yScale(d.value))
            .attr("y2", d => yScale(d.confidence.upper) - yScale(d.value));



        let xScale = d3.scaleBand()
            .domain(points.map(function (d) { return d.label; }))
            .range([0, width])

        g.append("g").call(d3.axisBottom(xScale).ticks(50))
            .attr("class", "x-axis")
            .attr("transform", "translate(" + 0 + "," + height + ")")


    }

    function drawLines(points) {
        let bars = g.selectAll(".line").data(points);
        let enteringBar = bars.enter();
        let enteredBar = enteringBar.append("g").attr("class", "line");

        enteredBar
            .append("line")
            .style("stroke", "#0000FF")
            .attr("stroke-width", 2)
            .attr("x1", 0)
            .attr("x2", width)
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("y1", d => yScale(d.value))
            .attr("y2", d => yScale(d.value));


        enteredBar
            .append("text")
            .attr("x", 20)
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("y", d => yScale(d.value) - 5)
            .text(d => d.label + ", " + d.value)
            .style("text-anchor", "left")
            .attr("stroke", "blue")
            .attr("font-size", 15)
            .attr("stroke-width", 1)
            .attr("fill", "red");
    }

    function drawTrend(points) {
        let computeBarWidth = () => width / points.length

        let binding = g.selectAll("g.dot")
            .data(points)
        let dot = binding
            .enter()
            .append("g")
            .attr("class", "dot")
            .attr("transform-origin", "50, 50")
            .attr("transform", (d, i) => "translate(" + computeBarWidth() * (i + 0.25) + "," + (yScale(d.value)) + ")")

        dot.append("title")
            .text(d => `${dataset.axis.x}: ${d.label}\n${dataset.axis.y}: ${d.value}`)



        var line = d3.line()
            .x((d, i) => i * computeBarWidth() + computeBarWidth() / 2)
            .y(d => yScale(d.value));

        g.append("g").attr("id", "lineContainer").append("path").data([points])
            .attr("class", "line solid")
            .attr("id", "coolPath")
            .attr("d", line)
            .attr("fill", "none")
            .attr("stroke", "#dfbcbd")
            .attr("stroke-width", 0)
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("stroke-width", 2);

        //bar.append("rect").attr("height", 20).attr("width", 20)

        dot.append("circle")
            .attr("r", 0)
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("r", 5)
            .attr("cx", computeBarWidth() / 4)
            .attr("cy", 0)
            .attr("fill", "#cf7587")

        // upper
        dot
            .append("line")
            .style("stroke", "#000")
            .attr("stroke-width", 1)
            .attr("x1", (_d, i) => 10)
            .attr("x2", (_d, i) => 35)
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("y1", d => yScale(d.confidence.lower) - yScale(d.value))
            .attr("y2", d => yScale(d.confidence.lower) - yScale(d.value));

        dot
            .append("line")
            .style("stroke", "#000")
            .attr("stroke-width", 1)
            .attr("x1", (_d, i) => 10)
            .attr("x2", (_d, i) => 35)
            .transition()
            .duration((_, i) => 100 * i + 800)
            .attr("y1", d => yScale(d.confidence.upper) - yScale(d.value))
            .attr("y2", d => yScale(d.confidence.upper) - yScale(d.value));



        let xScale = d3.scaleBand()
            .domain(points.map(function (d) { return d.label; }))
            .range([0, width])


        g.append("g").call(d3.axisBottom(xScale).ticks(50))
            .attr("class", "x-axis")
            .attr("transform", "translate(" + 0 + "," + height + ")")


    }
}