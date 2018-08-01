// @flow
import * as React from 'react';
import type { ChartData } from './Chart';

export type SummaryTableProps = {
    chartData: ChartData
}


export class SummaryTable extends React.Component<SummaryTableProps> {

    render() {
        console.log(this.props)
        return (
            <div className="col-md-12 bg-white">
    <table className="table table-striped table-condensed table-xCondensed text-center mrgn-bttm-sm" id="chartgridview">
        <caption>
            {this.props.chartData.title}
        </caption>

        <thead>
            <tr>
                <th className="text-left" scope="col">{this.props.chartData.axis.x}</th>
                <th className="text-center" scope="col">{this.props.chartData.axis.y}</th>
                <th className="text-center" scope="col">95% <abbr title="Confidence Interval">CI</abbr></th>
            </tr>
        </thead>
        <tbody>
            {this.props.chartData.values.map(value=>value.points.map(point => {
                switch (point.cv.interpretation)
                {

                    // Data is in the red (66%+)
                    case 1:
                        return (<tr style="background-color: red">
                            <td className="text-left">{point.label}</td>
                            <td>Suppr.<sup>F</sup></td>
                            <td></td>
                        </tr>)

                    // Data is in the red (33%+)
                    case 2:
                        return (<tr style="background-color: yellow">
                            <td className="text-left">{point.label}</td>
                            <td>{point.value}<sup>E</sup></td>
                            <td>{point.confidence.lower} - {point.confidence.upper}</td>
                        </tr>)

                    // Data is a Okay!
                    default:
                        return (<tr>
                            <td className="text-left">{point.label}</td>
                            <td>{point.value}</td>
                            <td>{point.confidence.lower} - {point.confidence.upper}</td>
                        </tr>)

                }
            }))}
        </tbody>

    </table>
</div>
        )
    }
}