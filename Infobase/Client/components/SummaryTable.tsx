// @flow
import * as React from 'react';
import { numberFormat, textFormat } from '../translator';
import { ChartData, LanguageCode } from '../types';

type SummaryTableProps = {
    chartData: ChartData,
    cvWarning: string,
    cvSuppressed: string,
    cvWarningAlt: string,
    cvSuppressedAlt: string,
    confidenceInterval: string,
    confidenceIntervalAbbr: string,
    languageCode: LanguageCode
}


export const SummaryTable: React.FC<SummaryTableProps> = (props) => {
        let cVWarningText = null;
        let cvSupressedText = null;
        let {warningCV: cvWarnAt, suppressCV: cvSuppressAt} = props.chartData;
        let knownBoundries = cvWarnAt && cvSuppressAt;

        if (props.chartData.points.some(p => p.cvInterpretation == 2))
            if (knownBoundries) {
                cVWarningText = <p><sup>E</sup>{textFormat(props.cvWarning, { warn: numberFormat(cvWarnAt || 0, props.languageCode), suppress: numberFormat(cvSuppressAt || 0, props.languageCode) })}</p>
            } else {
                cVWarningText = <p><sup>E</sup>{props.cvWarningAlt}</p>
            }

        if (props.chartData.points.some(p => p.cvInterpretation == 1))
            if (knownBoundries) {
                cvSupressedText = <p><sup>F</sup>{textFormat(props.cvSuppressed, { warn: numberFormat(cvWarnAt || 0, props.languageCode), suppress: numberFormat(cvSuppressAt || 0, props.languageCode) })}</p>
            } else {
                cvSupressedText = <p><sup>F</sup>{props.cvSuppressedAlt}</p>

            }


        return (
            <div className="container-fluid">
                <div className="row brdr-bttm mrgn-bttm-0">
                    <div className="col-md-12 table-data">
                        <table className="table table-striped table-condensed table-xCondensed text-center mrgn-bttm-sm" id="chartgridview">
                            <caption>
                                {props.chartData.title}
                            </caption>

                            <thead>
                                <tr>
                                    <th className="text-left" scope="col">{props.chartData.xAxis}</th>
                                    <th className="text-center" scope="col">{props.chartData.yAxis}</th>
                                    <th className="text-center" scope="col">95% <abbr title={props.confidenceInterval}>{props.confidenceIntervalAbbr}</abbr></th>
                                </tr>
                            </thead>
                            <tbody>
                                {props.chartData.points.map((point, index) => {

                                    switch (point.cvInterpretation) {

                                        // Data is in the red (66%+)
                                        case 1:
                                            return (<tr key={index} style={({ backgroundColor: "red" })}>
                                                <td className="text-left">{point.text}</td>
                                                <td><sup>F</sup></td>
                                                <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower, props.languageCode)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper, props.languageCode)} </td>
                                            </tr>)

                                        // Data is in the red (33%+)
                                        case 2:
                                            return (<tr key={index} style={({ backgroundColor: "yellow" })}>
                                                <td className="text-left">{point.text}</td>
                                                <td>{typeof point.value != "number" ? "-" : numberFormat(point.value, props.languageCode)}<sup>E</sup></td>
                                                <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower, props.languageCode)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper, props.languageCode)} </td>
                                            </tr>)

                                        // Data is a Okay!
                                        default:
                                            return (<tr key={index}>
                                                <td className="text-left">{point.text}</td>
                                                <td>{typeof point.value != "number" ? "-" : numberFormat(point.value, props.languageCode)}</td>
                                                <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower, props.languageCode)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper, props.languageCode)} </td>
                                            </tr>)

                                    }
                                })}
                            </tbody>

                        </table>
                    </div>
                    <div className="col-md-12 mrgn-tp-0 small notes bg-warning">
                        {Object.entries(props.chartData.notes).map(([header, body], i) => (
                            <p key={i}><strong>{header}:</strong> {body}</p>
                        ))}
                        
                        {cVWarningText}
                        {cvSupressedText}
                    </div>
                </div>
            </div>
        )
}