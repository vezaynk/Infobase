// @flow
import * as React from 'react';

type DescriptionTableProps = {
    definitionText: string,
    dataAvailableText: string,
    methodsText: string,
    remarksText: string,
    remarks: string,
    methods: string,
    dataAvailable: string,
    definition: string
}

export class DescriptionTable extends React.Component<DescriptionTableProps> {
    
    render() {

        return (
            <table className="table table-striped table-hover table-condensed table-bordered">
                <tbody>
                    <tr>
                        <th>{this.props.definitionText}</th>
                        <td>{this.props.definition}</td>
                    </tr>
                    <tr>
                        <th>{this.props.dataAvailableText}</th>
                        <td>{this.props.dataAvailable}</td>
                    </tr>
                    <tr>
                        <th>{this.props.methodsText}</th>
                        <td>{this.props.methods}</td>
                    </tr>
                    <tr>
                        <th>{this.props.remarksText}</th>
                        <td>{this.props.remarks}</td>
                    </tr>
                </tbody>
            </table>
        );
    }
}