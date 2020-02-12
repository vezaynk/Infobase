import * as d3 from "d3";
import { numberFormat } from "./translator";
import { ChartData, Point, LanguageCode, ChartType } from "./types";
import { BaseType } from "d3";
const marginY = 10;
const marginX = 60;
const width = 700;
const height = 400;

const moveToFront = function(node: any) {
  return node.each(function() {
    node.parentNode.appendChild(node);
  });
};
const moveToBack = function(node: any) {
  return node.each(function() {
    var firstChild = node.parentNode.firstChild;
    if (firstChild) {
      node.parentNode.insertBefore(node, firstChild);
    }
  });
};

const isBetween = (val: number, up: number, low: number) =>
  val >= low && val <= up;
const isPointInRange = (
  upper: number | void,
  lower: number | void,
  point: Point
) => {
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
};

// Generated using
// letters.map(v => { temp0.innerHTML = v; return {[v]: temp0.getComputedTextLength()} }).reduce((a, b) => ({...a, ...b}))
// Recompute if sizes changed
const widthTableAxisLabel = {
  A: 10.59583568572998,
  B: 10.59583568572998,
  C: 10.59583568572998,
  D: 10.59583568572998,
  E: 9.4185209274292,
  F: 8.241206169128418,
  G: 10.59583568572998,
  H: 10.59583568572998,
  I: 3.53194522857666,
  J: 8.241206169128418,
  K: 10.59583568572998,
  L: 8.241206169128418,
  M: 11.773151397705078,
  N: 10.59583568572998,
  O: 10.59583568572998,
  P: 9.4185209274292,
  Q: 10.59583568572998,
  R: 10.59583568572998,
  S: 9.4185209274292,
  T: 8.241206169128418,
  U: 10.59583568572998,
  V: 9.4185209274292,
  W: 12.95046615600586,
  X: 9.4185209274292,
  Y: 9.4185209274292,
  Z: 8.241206169128418,
  a: 8.241206169128418,
  b: 8.241206169128418,
  c: 8.241206169128418,
  d: 8.241206169128418,
  e: 8.241206169128418,
  f: 4.7092604637146,
  g: 8.241206169128418,
  h: 8.241206169128418,
  i: 3.53194522857666,
  j: 3.53194522857666,
  k: 8.241206169128418,
  l: 3.53194522857666,
  m: 12.95046615600586,
  n: 8.241206169128418,
  o: 8.241206169128418,
  p: 8.241206169128418,
  q: 8.241206169128418,
  r: 5.886575698852539,
  s: 8.241206169128418,
  t: 4.7092604637146,
  u: 8.241206169128418,
  v: 8.241206169128418,
  w: 10.59583568572998,
  x: 8.241206169128418,
  y: 8.241206169128418,
  z: 7.06389045715332
};
const widthTablePointLabel = {
  A: 7.06389045715332,
  B: 7.06389045715332,
  C: 7.06389045715332,
  D: 7.06389045715332,
  E: 7.06389045715332,
  F: 5.886575698852539,
  G: 8.241206169128418,
  H: 7.06389045715332,
  I: 2.3546302318573,
  J: 4.7092604637146,
  K: 7.06389045715332,
  L: 5.886575698852539,
  M: 8.241206169128418,
  N: 7.06389045715332,
  O: 8.241206169128418,
  P: 7.06389045715332,
  Q: 8.241206169128418,
  R: 7.06389045715332,
  S: 7.06389045715332,
  T: 5.886575698852539,
  U: 7.06389045715332,
  V: 7.06389045715332,
  W: 9.4185209274292,
  X: 7.06389045715332,
  Y: 7.06389045715332,
  Z: 5.886575698852539,
  a: 5.886575698852539,
  b: 5.886575698852539,
  c: 4.7092604637146,
  d: 5.886575698852539,
  e: 5.886575698852539,
  f: 2.3546302318573,
  g: 5.886575698852539,
  h: 5.886575698852539,
  i: 2.3546302318573,
  j: 2.3546302318573,
  k: 4.7092604637146,
  l: 2.3546302318573,
  m: 8.241206169128418,
  n: 5.886575698852539,
  o: 5.886575698852539,
  p: 5.886575698852539,
  q: 5.886575698852539,
  r: 3.53194522857666,
  s: 4.7092604637146,
  t: 2.3546302318573,
  u: 5.886575698852539,
  v: 4.7092604637146,
  w: 7.06389045715332,
  x: 4.7092604637146,
  y: 4.7092604637146,
  z: 4.7092604637146
};
let getComputedTextLength = function(
  node: SVGTextElement | SVGTSpanElement,
  table: { [key: string]: number }
) {
  if (Object.keys(window).length > 0) return node.getComputedTextLength();

  let totalWidth = 0;
  for (let c of node.innerHTML) {
    let cWidth = table[c];

    if (!cWidth)
      return (
        Object.values(table).reduce((a, b) => a + b) /
        Object.values(table).length
      );
  }

  return totalWidth;
};

function wrap(
  text: d3.Selection<d3.BaseType, {}, d3.BaseType, {}>,
  width: number,
  fallbackTable,
  inverted: boolean = false
) {
  text.each(function() {
    let text = d3.select(this),
      words = text
        .text()
        .split(/\s+/)
        .reverse(),
      word,
      line: string[] = [],
      lineNumber = 0,
      lineHeight = 1.1, // ems
      x = text.attr("x"),
      y = text.attr("y"),
      dy = 0, //parseFloat(text.attr("dy")),
      tspan = text
        .text(null)
        .append("tspan")
        .attr("x", x)
        .attr("y", y)
        .attr("dy", -dy + "em");
    while ((word = words.pop())) {
      line.push(word);
      tspan.text(line.join(" "));
      if (getComputedTextLength(tspan.node(), fallbackTable) > width) {
        line.pop();
        tspan.text(line.join(" "));
        line = [word];
        tspan = text
          .append("tspan")
          .attr("x", x || 0)
          .attr("y", y)
          .attr(
            "dy",
            (inverted ? -1 : 1) * (++lineNumber * lineHeight + dy) + "em"
          )
          .text(word);
      }
    }
  });
}

export function renderChart(
  ref: Element,
  dataset: ChartData,
  culture: LanguageCode,
  animate: boolean,
  highlightIndex: number,
  highlightUpper: number,
  highlightLower: number,
  updateHighlight: (newIndex: number) => void
): void {
  let chart = d3.select(ref);
  let select = chart.select(".main");

  const isTrend = dataset.chartType == ChartType.Trend;
  const isStack = dataset.chartType == ChartType.Stack;

  let points = dataset.points.filter(
    point => point.type == 0 || isTrend || isStack
  ).slice().sort((a, b) => (b.value || 0) - (a.value || 0));
  let averages = dataset.points.filter(point => !points.includes(point));

  const stacks = points
    .reduce<{[key: string]: Point[]}>((a, b) => {
      if (!a[b.aggregatorLabel]) {
        a[b.aggregatorLabel] = [];
      }

      const sum = a[b.aggregatorLabel].map(p => p.value || 0).reduce((a, b) => a + b, 0);
      const p = {
        ...b,
        value: sum + (b.value || 0)
      }
      a[b.aggregatorLabel].push(p);

      return a;
    }, {});

    console.log("stacks", stacks);

  let animationDuration = 525;
  const optionalTransition = function<
    GElements extends d3.BaseType,
    Datum,
    PElement extends d3.BaseType,
    PDatum
  >(
    el: d3.Selection<GElements, Datum, PElement, PDatum>
  ): d3.Selection<GElements, Datum, PElement, PDatum> {
    if (!animate) return el;

    return (el
      .transition()
      .duration(animationDuration) as unknown) as d3.Selection<
      GElements,
      Datum,
      PElement,
      PDatum
    >;
  };

  if (points.length == 0) {
    points = averages;
    averages = [];
  }
  let x = d3
    .scaleBand()
    .domain(points.map(point => point.label))
    .range([0, width]);

  let y = d3
    .scaleLinear()
    .domain([
      0,
      d3.max(dataset.points, point => point.valueUpper || point.value || 0)
    ])
    .range([height, 0]);

  optionalTransition(
    chart
      .selectAll("g.y-axis")
      .attr("transform", `translate(${marginX}, ${marginY})`)
  )
    .call(d3.axisLeft(y))
    .selectAll("text")
    .attr("font-size", "10px");

  chart
    .selectAll("g.x-axis")
    .attr("transform", `translate(${marginX}, ${height + marginY})`)
    .call(d3.axisBottom(x))
    .selectAll(".tick text")
    .attr("font-size", "10px")
    .attr("y", 15)
    .attr("text-anchor", "middle")
    .call(wrap, 500 / points.length, widthTablePointLabel);

  chart
    .select(".xAxisLabel")
    .text(dataset.xAxis)
    .call(wrap, 600, widthTableAxisLabel);

  chart
    .select(".yAxisLabel")
    .text(dataset.yAxis)
    .call(wrap, 400, widthTableAxisLabel);

  let pointBinding = select.selectAll("g.point").data(points);
  let averageBinding = select.selectAll("g.average").data(averages);
  let cvUpperBinding = select.selectAll("g.cvUpper").data(points);
  let cvLowerBinding = select.selectAll("g.cvLower").data(points);
  let cvConnectBinding = select.selectAll("g.cvConnect").data(points);

  let exitCvUpper = cvUpperBinding.exit();
  let exitCvLower = cvLowerBinding.exit();
  let exitCvConnect = cvConnectBinding.exit();
  let exitPoint = pointBinding.exit();

  let enteredRect = pointBinding
    .enter()
    .append("g")
    .attr("class", "point")
    .append("rect");

  optionalTransition(
    enteredRect
      .attr(
        "x",
        (d, i) => {
          if (isStack) {
            return (Object.keys(stacks).indexOf(d.aggregatorLabel) + 0.5) * (width / Object.keys(stacks).length)
          }
          return (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2
        }
      )
      .attr("width", isTrend ? 10 : 25)
      //.style("fill", "steelblue")
      .attr("ry", isTrend ? 10 : 0)
      .attr("rx", isTrend ? 10 : 0)
      .attr("y", height)
  )
    .attr("y", d => (isTrend ? y(d.value || 0) - 5 : y(d.value || 0)))
    .attr("height", d => (isTrend ? 10 : height - y(d.value || 0)))
    .attr("fill", (d, i) => isStack ? d3.interpolateBrBG(i/points.length) : "steelblue")
    .attr("opacity", (d, i) =>
      isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex
        ? 0.2
        : 1
    );

  enteredRect
    .append("title")
    .text(
      d => d.label + ": " + numberFormat(d.value || 0, culture, dataset.unit)
    );

  optionalTransition(pointBinding.select<SVGRectElement>("rect"))
    .attr("ry", isTrend ? 10 : 0)
    .attr("rx", isTrend ? 10 : 0)
    .attr("width", isTrend ? 10 : 25)
    .attr(
      "x",
      (d, i) => {
        if (isStack) {
          return (Object.keys(stacks).indexOf(d.aggregatorLabel) + 0.5) * (width / Object.keys(stacks).length)
        }
        return (i + 0.5) * (width / points.length) - (isTrend ? 10 : 25) / 2
      }
    )
    .attr("height", (d, i) => (isTrend ? 10 : height - y(d.value || 0)))
    .attr("y", d => (isTrend ? y(d.value || 0) - 5 : y(d.value || 0)))
    .attr("fill", (d, i) => isStack ? d3.interpolateBrBG(i/points.length) : "steelblue")
    .attr("opacity", (d, i) =>
      isPointInRange(highlightUpper, highlightLower, d) && i != highlightIndex
        ? 1
        : 1
    ) //Point A to 0.2 to bring back functionality
    .select("title")
    .text(
      d => d.label + ": " + numberFormat(d.value || 0, culture, dataset.unit)
    );

  pointBinding.on("mouseover", (_, i) => updateHighlight(i));
  pointBinding.on("mouseout", (_, i) => updateHighlight(-1));

  optionalTransition(exitPoint.select("rect"))
    .style("opacity", 0)
    .attr("x", width);

  optionalTransition(exitPoint).remove();

  let enteredCvUpper = pointBinding
    .enter()
    .append("g")
    .attr("class", "cvUpper")
    .append("rect");
  optionalTransition(
    enteredCvUpper
      .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
      .attr("width", 25)
      .style("fill", "black")
      .attr("y", height)
  )
    .attr("y", d => y(d.valueUpper || 0))
    .attr("height", 2);

  optionalTransition(cvUpperBinding.select("rect"))
    .attr("width", 25)
    .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
    .attr("height", 2)
    .attr("y", d => y(d.valueUpper || 0))
    .style("fill", "black");

  optionalTransition(exitCvUpper.select("rect"))
    .style("opacity", 0)
    .attr("x", width);

  optionalTransition(exitCvUpper).remove();

  let enteredcvConnect = cvConnectBinding
    .enter()
    .append("g")
    .attr("class", "cvConnect")
    .append("rect");

  optionalTransition(
    enteredcvConnect
      .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 2 / 2)
      .attr("width", 2)
      .style("fill", "black")
      .attr("y", height)
  )
    .attr("height", d => y(d.valueLower || 0) - y(d.valueUpper || 0))
    .attr("y", d => y(d.valueUpper || 0));

  optionalTransition(cvConnectBinding.select("rect"))
    .attr("width", 2)
    .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 2 / 2)
    .attr("height", d => y(d.valueLower || 0) - y(d.valueUpper || 0))
    .attr("y", d => y(d.valueUpper || 0))
    .style("fill", "black");

  optionalTransition(exitCvConnect.select("rect"))
    .style("opacity", 0)
    .attr("x", width);

  optionalTransition(exitCvConnect).remove();

  let enteredcvLower = pointBinding
    .enter()
    .append("g")
    .attr("class", "cvLower")
    .append("rect");

  optionalTransition(
    enteredcvLower
      .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
      .attr("width", 25)
      .style("fill", "black")
      .attr("y", height)
  )
    .attr("y", d => y(d.valueLower || 0))
    .attr("height", 2);

  optionalTransition(cvLowerBinding.select("rect"))
    .attr("width", 25)
    .attr("x", (d, i) => (i + 0.5) * (width / points.length) - 25 / 2)
    .attr("height", 2)
    .attr("y", d => y(d.valueLower || 0))
    .style("fill", "black");

  optionalTransition(exitCvLower.select("rect"))
    .style("opacity", 0)
    .attr("x", width);

  optionalTransition(exitCvLower).remove();

  let enteredAverage = averageBinding.enter().append("g");

  optionalTransition(
    enteredAverage
      .attr("class", "average")
      .append("rect")
      .attr("fill", "url(#linePattern)")
      .attr("height", 2)
      .attr("x", 0)
      .attr("width", width)
      .attr("ry", isTrend ? 10 : 0)
      .attr("rx", isTrend ? 10 : 0)
  ).attr("y", d => y(d.value || 0));

  let lowestValueIndex = points
    .map(p => +p.value)
    .indexOf(Math.min(...points.map(p => +p.value)));
  if (points.length == 2) lowestValueIndex = 1.5;

  let leftLabel = lowestValueIndex < points.length / 2;

  optionalTransition(
    enteredAverage
      .append("text")
      .attr("x", () => width)
      .attr("y", height)
      .attr("filter", "url(#solid)")
  )
    .attr("y", d => y(d.value || 0) - 5)
    .text(
      d => d.label + ": " + numberFormat(d.value || 0, culture, dataset.unit)
    )
    .attr("text-anchor", "end")
    .style("font-weight", "bold");

  optionalTransition(averageBinding.select("rect")).attr("y", d =>
    y(d.value || 0)
  );

  optionalTransition(
    averageBinding.select("text").attr("filter", "url(#solid)")
  )
    .attr("x", () => width)
    .attr("y", d => y(d.value || 0) - 5)
    .text(
      d => d.label + ": " + numberFormat(d.value || 0, culture, dataset.unit)
    )
    .attr("text-anchor", "end")
    .style("font-weight", "bold");

  optionalTransition(averageBinding.exit().selectAll("rect, text"))
    .attr("y", -marginY)
    .style("opacity", 0);

  optionalTransition(averageBinding.exit()).remove();

  averageBinding.raise();

  let myLine = d3
    .line<Point>()
    .x((_d, i) => (i + 0.5) * (width / points.length) - 10 / 2 + 2.5)
    .y(d => y(d.value || 0));

  let paths = chart.selectAll("path.line").data(isTrend ? [points] : []);

  optionalTransition(
    paths
      .enter()
      .append("path")
      .attr("class", "line")
      .attr("d", d => myLine(d))
      .attr("fill", "none")
      .attr("stroke", "steelblue")
      .attr("stroke-width", 2)
      .attr("opacity", 0)
      .attr("transform", "translate(" + marginX + ",-" + 10 + ")")
  )
    .attr("transform", "translate(" + marginX + "," + marginY + ")")
    .attr("opacity", 1);

  paths
    .attr("class", "line")
    .attr("d", myLine)
    .attr("stroke", "steelblue")
    .attr("stroke-width", 2);

  optionalTransition(paths.exit())
    .attr("transform", "translate(" + (marginX + 2.5) + ",-" + 10 + ")")
    .attr("opacity", 0)
    .remove();
}
