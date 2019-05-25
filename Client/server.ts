import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as ReactDOMServer from 'react-dom/server';

// @ts-ignore
global.React = React;
// @ts-ignore
global.ReactDOM = ReactDOM;
// @ts-ignore
global.ReactDOMServer = ReactDOMServer;

import './app';