//      
import * as React from 'react';

import { i18n } from "../Translator";
import { useChartData } from "../hooks/hooks";

function DescriptionTable(props) {
    const [chartData, setChartData] = useChartData();
    return (<table className="table table-striped table-hover table-condensed table-bordered">
        <tbody>
            <tr>
                <th>{i18n(props.definitionText)}</th>
                <td>{i18n(chartData.definition)}</td>
            </tr>
            <tr>
                <th>{i18n(props.dataAvailableText)}</th>
                <td>{i18n(chartData.dataAvailable)}</td>
            </tr>
            <tr>
                <th>{i18n(props.methodsText)}</th>
                <td>{i18n(chartData.method)}</td>
            </tr>
            <tr>
                <th>{i18n(props.remarksText)}</th>
                <td>{i18n(chartData.remarks)}</td>
            </tr>
        </tbody>
    </table>);
}