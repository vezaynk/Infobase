//      
import * as React from 'react';
                                              
import { i18n } from "../Translator";

                              
                                  
                                     
                               
                               
                           
                           
                                 
                             
 

export class DescriptionTable extends React.Component                        {
    
    render() {

        return (
            <table className="table table-striped table-hover table-condensed table-bordered">
                <tbody>
                    <tr>
                        <th>{i18n(this.props.definitionText)}</th>
                        <td>{i18n(this.props.definition)}</td>
                    </tr>
                    <tr>
                        <th>{i18n(this.props.dataAvailableText)}</th>
                        <td>{i18n(this.props.dataAvailable)}</td>
                    </tr>
                    <tr>
                        <th>{i18n(this.props.methodsText)}</th>
                        <td>{i18n(this.props.methods)}</td>
                    </tr>
                    <tr>
                        <th>{i18n(this.props.remarksText)}</th>
                        <td>{i18n(this.props.remarks)}</td>
                    </tr>
                </tbody>
            </table>
        );
    }
}