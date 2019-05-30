import { SaveBtn } from '../components/SaveBtn';
import * as React from 'react';
import { Charting } from './Charting';
import { SummaryTableProps, SummaryTable } from '../containers/SummaryTable';

type SaveChartBtnProps = { label: string } & SummaryTableProps

export const SaveChartBtn: React.FC<SaveChartBtnProps> = props => {

    return (
        <SaveBtn label={props.label}>
            <Charting animate={false} />
            <SummaryTable confidenceInterval={props.confidenceInterval}
                confidenceIntervalAbbr={props.confidenceIntervalAbbr} 
                cvSuppressed={props.cvSuppressed}
                cvSuppressedAlt={props.cvSuppressedAlt}
                cvWarning={props.cvWarning}
                cvWarningAlt={props.cvWarningAlt} />
        </SaveBtn>
    )
}