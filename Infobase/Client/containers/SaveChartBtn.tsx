import { SaveBtn, SaveBtnProps } from '../components/SaveBtn';
import * as React from 'react';
import { Charting } from './Charting';
import { SummaryTableProps, SummaryTable } from './SummaryTable';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { DataExplorerState } from '../types';
import { dataExplorerStore } from '../store/dataExplorer';

type SaveChartBtnProps = { label: string } & SummaryTableProps
const mapStateToSaveBtnProps: MapStateToProps<{enabled: boolean}, {label: string, children: React.ReactElement | React.ReactElement[]}, DataExplorerState> = (state, props) => (
    {
        enabled: !state.loading,
        children: props.children
    }
);

export const SaveBtnConnect = connect(
    mapStateToSaveBtnProps
)(SaveBtn)

export const SaveChartBtn: React.FC<SaveChartBtnProps> = props => {

    return (
        <Provider store={dataExplorerStore}>
            <SaveBtnConnect label={props.label}>
                <Charting animate={false} />
                <SummaryTable confidenceInterval={props.confidenceInterval}
                    confidenceIntervalAbbr={props.confidenceIntervalAbbr} 
                    cvSuppressed={props.cvSuppressed}
                    cvSuppressedAlt={props.cvSuppressedAlt}
                    cvWarning={props.cvWarning}
                    cvWarningAlt={props.cvWarningAlt} />
            </SaveBtnConnect>
        </Provider>
        
    )
}