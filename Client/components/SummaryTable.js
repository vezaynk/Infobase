// @flow
import * as React from 'react';
import { numberFormat, textFormat } from '../Translator';
import type { ChartData, MultilangText } from '../types';

export type SummaryTableProps = {
    chartData: ChartData,
    cvWarning: MultilangText,
    cellsEmpty: MultilangText,
    cvSuppressed: MultilangText,
    remarks: MultilangText,
    cvWarnAt: number,
    cvSuppressAt: number
}


export class SummaryTable extends React.Component<SummaryTableProps> {
    componentMounted() {

    }
    render() {
        let warningCV = null;
        let suppressedCV = null;
        if (this.props.chartData.points.some(p => p.cvInterpretation == 2))
            if (this.props.cvWarnAt) {
                warningCV = <p><sup>E</sup>{textFormat(this.props.cvWarning, { warn: numberFormat(this.props.cvWarnAt), suppress: numberFormat(this.props.cvSuppressAt) })}</p>
            } else {
                warningCV = <p><sup>E</sup>{this.props.cvWarningAlt}</p>
            }

        if (this.props.chartData.points.some(p => p.cvInterpretation == 1))
            if (this.props.cvWarnAt) {
                suppressedCV = <p><sup>F</sup>{textFormat(this.props.cvSuppressed, { warn: numberFormat(this.props.cvWarnAt), suppress: numberFormat(this.props.cvSuppressAt) })}</p>
            } else {
                suppressedCV = <p><sup>F</sup>{this.props.cvSuppressedAlt}</p>

            }


        return (
            <div className="container-fluid">
                <div className="row brdr-bttm mrgn-bttm-0">
                    <div className="col-md-12 table-data">
                        <table className="table table-striped table-condensed table-xCondensed text-center mrgn-bttm-sm" id="chartgridview">
                            <caption>
                                {this.props.chartData.title}
                            </caption>

                            <thead>
                                <tr>
                                    <th className="text-left" scope="col">{this.props.chartData.xAxis}</th>
                                    <th className="text-center" scope="col">{this.props.chartData.yAxis}</th>
                                    <th className="text-center" scope="col">95% <abbr title="Confidence Interval">CI</abbr></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.props.chartData.points.map((point, index) => {

                                    switch (point.cvInterpretation) {

                                        // Data is in the red (66%+)
                                        case 1:
                                            return (<tr key={index} style={({ backgroundColor: "red" })}>
                                                <td className="text-left">{point.text}</td>
                                                <td><sup>F</sup></td>
                                                <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper)} </td>
                                            </tr>)

                                        // Data is in the red (33%+)
                                        case 2:
                                            return (<tr key={index} style={({ backgroundColor: "yellow" })}>
                                                <td className="text-left">{point.text}</td>
                                                <td>{typeof point.value != "number" ? "-" : numberFormat(point.value)}<sup>E</sup></td>
                                                <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper)} </td>
                                            </tr>)

                                        // Data is a Okay!
                                        default:
                                            return (<tr key={index}>
                                                <td className="text-left">{point.text}</td>
                                                <td>{typeof point.value != "number" ? "-" : numberFormat(point.value)}</td>
                                                <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper)} </td>
                                            </tr>)

                                    }
                                })}
                            </tbody>

                        </table>
                    </div>
                    <div className="col-md-12 mrgn-tp-0 small notes bg-warning">
                        <p><strong>Notes:</strong> {this.props.chartData.notes}</p>
                        <p><strong>Source:</strong> {this.props.chartData.source}</p>
                        {warningCV}
                        {suppressedCV}
                    </div>
                </div>
            </div>
        )
    }
}