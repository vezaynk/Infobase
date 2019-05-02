//      
import * as React from 'react';
import { i18n, numberFormat } from '../Translator';
                                                         

                                 
                         
                             
                              
                                
                           
                     
                        
 


export class SummaryTable extends React.Component                    {
    componentMounted() {
        
    }
    render() {
        let warningCV = null;
        let suppressedCV = null;
        if (this.props.chartData.points.some(p => p.cvInterpretation == 2))
            if (this.props.cvWarnAt) {
                warningCV = <p><sup>E</sup>{i18n(this.props.cvWarning, undefined, {warn: numberFormat(this.props.cvWarnAt), suppress: numberFormat(this.props.cvSuppressAt) })}</p>
            } else {
                warningCV = <p><sup>E</sup>{i18n(this.props.cvWarning, "alt")}</p>
            }

        if (this.props.chartData.points.some(p => p.cvInterpretation == 1))
            if (this.props.cvWarnAt) {
                suppressedCV = <p><sup>F</sup>{i18n(this.props.cvSuppressed, undefined, {warn: numberFormat(this.props.cvWarnAt), suppress: numberFormat(this.props.cvSuppressAt) })}</p>
            } else {
                suppressedCV = <p><sup>F</sup>{i18n(this.props.cvSuppressed, "alt")}</p>

            }

        
        return (
            <div className="container">
            <div className="row brdr-bttm mrgn-bttm-0">
            <div className="col-md-12">
    <table className="table table-striped table-condensed table-xCondensed text-center mrgn-bttm-sm" id="chartgridview">
        <caption>
            {i18n(this.props.chartData.measureName)}, {i18n(this.props.chartData.population, "Datatool")}
        </caption>

        <thead>
            <tr>
                <th className="text-left" scope="col">{i18n(this.props.chartData.xAxis)}</th>
                <th className="text-center" scope="col">{i18n(this.props.chartData.yAxis, "Datatool")}</th>
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
                            <td className="text-left">{i18n(point.label, "Index")}</td>
                            <td>Suppr.<sup>F</sup></td>
                            <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper)} </td>
                        </tr>)

                    // Data is in the red (33%+)
                    case 2:
                        return (<tr key={index} style={({backgroundColor: "yellow"})}>
                            <td className="text-left">{i18n(point.label, "Index")}</td>
                            <td>{typeof point.value != "number" ? "-" :numberFormat(point.value)}<sup>E</sup></td>
                            <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper)} </td>
                        </tr>)

                    // Data is a Okay!
                    default:
                        return (<tr key={index}>
                            <td className="text-left">{i18n(point.label, "Index")}</td>
                            <td>{typeof point.value != "number" ? "-" :numberFormat(point.value)}</td>
                            <td>{typeof point.valueLower != "number" ? "-" : numberFormat(point.valueLower)} - {typeof point.valueUpper != "number" ? "-" : numberFormat(point.valueUpper)} </td>
                        </tr>)

                }
            })}
        </tbody>

    </table>
</div>
<div className="col-md-12 mrgn-tp-0 small">
        <div className="bg-warning">
            <h4>Notes:</h4>
            <p><strong>Notes:</strong> {i18n(this.props.chartData.notes)}</p>
            <p><strong>Source:</strong> {i18n(this.props.chartData.source, "Datatool")}</p>
			<p>{i18n(this.props.cellsEmpty)}</p>            
            {warningCV}
            {suppressedCV}

        </div>
    </div>
    </div>
    </div>
        )
    }
}