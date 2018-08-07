// @flow
import * as React from 'react';
import { i18n } from '../Translator';
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
                warningCV = <p><sup>E</sup>{i18n(this.props.cvWarning, undefined, {warn: Math.round(this.props.cvWarnAt*100)/100, suppress: Math.round(this.props.cvSuppressAt*100)/100})}</p>
            } else {
                warningCV = <p><sup>E</sup>{i18n(this.props.cvWarning, "alt")}</p>
            }

        if (this.props.chartData.points.some(p => p.cvInterpretation == 1))
            if (this.props.cvWarnAt) {
                suppressedCV = <p><sup>F</sup>{i18n(this.props.cvSuppressed, undefined, {warn: Math.round(this.props.cvWarnAt*100)/100, suppress: Math.round(this.props.cvSuppressAt*100)/100})}</p>
            } else {
                suppressedCV = <p><sup>E</sup>{i18n(this.props.cvSuppressed, "alt")}</p>

            }

        
        return (
            <div className="row brdr-bttm bg-white mrgn-bttm-0">
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
                            <td>{point.valueLower == null ? '' : Math.round(point.valueLower*100)/100})} - {point.valueUpper == null ? '' : Math.round(point.valueUpper*100)/100})}</td>
                        </tr>)

                    // Data is in the red (33%+)
                    case 2:
                        return (<tr key={index} style={({backgroundColor: "yellow"})}>
                            <td className="text-left">{i18n(point.label)}</td>
                            <td>{point.value}<sup>E</sup></td>
                            <td>{point.valueLower == null ? '' : Math.round(point.valueLower*100)/100})} - {point.valueUpper == null ? '' : Math.round(point.valueUpper*100)/100})}</td>
                        </tr>)

                    // Data is a Okay!
                    default:
                        return (<tr key={index}>
                            <td className="text-left">{i18n(point.label)}</td>
                            <td>{point.value}</td>
                            <td>{point.valueLower == null ? '' : Math.round(point.valueLower*100)/100})} - {point.valueUpper == null ? '' : Math.round(point.valueUpper*100)/100})}</td>
                        </tr>)

                }
            })}
        </tbody>

    </table>
</div>
<div className="col-md-12 bg-white mrgn-tp-0 small">
        <div className="bg-warning">
            <h4>Notes:</h4>
            <p>
                {i18n(this.props.chartData.remarks)}
            </p>
            <p>
                <strong>Source: </strong>{i18n(this.props.chartData.source)}
            </p>

            <p>{i18n(this.props.cellsEmpty)}</p>
            
            {warningCV}
            {suppressedCV}

        </div>
    </div>
    </div>
        )
    }
}