import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as ReactDOMServer from 'react-dom/server';
import { SSCharting } from './containers/SSCharting';

(global as any).React = React;
(global as any).ReactDOM = ReactDOM;
(global as any).ReactDOMServer = ReactDOMServer;

import './shared';
//(global as any).Charting = SSCharting;

