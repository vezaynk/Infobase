import * as React from 'react';

export type DescriptionTableProps = {
    descriptionTable: { [name: string]: string }
}

export const DescriptionTable: React.FC<DescriptionTableProps> = (props) => {
    
    const notApplicable = (<div><span aria-hidden={true}>N/A</span><span className="wb-inv">&emdash;</span></div>);
    return (
        <table className="table table-striped table-hover table-condensed table-bordered">
            <tbody>
                {Object.entries(props.descriptionTable).map(([header, body], i) => (
                    <tr key={i}>
                        <th>{header}</th>
                        <td>{body || notApplicable}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}