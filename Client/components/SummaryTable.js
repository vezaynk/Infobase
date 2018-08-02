// @flow
import * as React from 'react';
import { i18n } from '../Translator';
import type { ChartData } from '../types';

export type SummaryTableProps = {
    chartData: ChartData
}


export class SummaryTable extends React.Component<SummaryTableProps> {
    componentMounted() {
        
    }
    render() {
        return (
            <div className="col-md-12 bg-white">
    <table className="table table-striped table-condensed table-xCondensed text-center mrgn-bttm-sm" id="chartgridview">
        <caption>
            {i18n(this.props.chartData.measureName)}, {i18n(this.props.chartData.population)}
        </caption>

        <thead>
            <tr>
                <th className="text-left" scope="col">{i18n(this.props.chartData.xAxis)}</th>
                <th className="text-center" scope="col">{i18n(this.props.chartData.yAxis)}</th>
                <th className="text-center" scope="col">95% <abbr title="Confidence Interval">CI</abbr></th>
            </tr>
        </thead>
        <tbody>
            {this.props.chartData.points.map((point, index) => {
                switch (point.cvInterpretation)
                {

                    // Data is in the red (66%+)
                    case 1:
                        return (<tr key={index} style={({backgroundColor: "red"})}>
                            <td className="text-left">{i18n(point.label)}</td>
                            <td>Suppr.<sup>F</sup></td>
                            <td>{point.valueLower} - {point.valueUpper}</td>
                        </tr>)

                    // Data is in the red (33%+)
                    case 2:
                        return (<tr key={index} style={({backgroundColor: "yellow"})}>
                            <td className="text-left">{i18n(point.label)}</td>
                            <td>{point.value}<sup>E</sup></td>
                            <td>{point.valueLower} - {point.valueUpper}</td>
                        </tr>)

                    // Data is a Okay!
                    default:
                        return (<tr key={index}>
                            <td className="text-left">{i18n(point.label)}</td>
                            <td>{point.value}</td>
                            <td>{point.valueLower} - {point.valueUpper}</td>
                        </tr>)

                }
            })}
        </tbody>

    </table>
</div>
        )
    }
}