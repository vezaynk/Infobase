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

export function DescriptionTable(props) {
    return (
        <table className="table table-striped table-hover table-condensed table-bordered">
            <tbody>
                <tr>
                    <th>{props.definitionText}</th>
                    <td>{props.definition}</td>
                </tr>
                <tr>
                    <th>{props.dataAvailableText}</th>
                    <td>{props.dataAvailable}</td>
                </tr>
                <tr>
                    <th>{props.methodsText}</th>
                    <td>{props.methods}</td>
                </tr>
                <tr>
                    <th>{props.remarksText}</th>
                    <td>{props.remarks}</td>
                </tr>
            </tbody>
        </table>
    );
}