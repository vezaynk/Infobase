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

export const DescriptionTable: React.SFC<DescriptionTableProps> = (props) => {
    const notApplicable = (<div><span aria-hidden={true}>N/A</span><span className="wb-inv">&emdash;</span></div>);
    return (
        <table className="table table-striped table-hover table-condensed table-bordered">
            <tbody>
                <tr>
                    <th>{props.definitionText}</th>
                    <td>{props.definition || notApplicable}</td>
                </tr>
                <tr>
                    <th>{props.dataAvailableText}</th>
                    <td>{props.dataAvailable || notApplicable}</td>
                </tr>
                <tr>
                    <th>{props.methodsText}</th>
                    <td>{props.methods || notApplicable}</td>
                </tr>
                <tr>
                    <th>{props.remarksText}</th>
                    <td>{props.remarks || notApplicable}</td>
                </tr>
            </tbody>
        </table>
    );
}