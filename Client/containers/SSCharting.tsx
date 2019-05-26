import * as React from 'react';
import * as ReactDOMServer from 'react-dom/server';
import { Charting } from './Charting';
import { JSDOM } from 'jsdom';

type ChartingProps = {animate: boolean};

export function SSCharting(props: ChartingProps) {
    //new JSDOM(ReactDOMServer.renderToString(React.createElement(Charting, props)));
    return Charting(props);
}