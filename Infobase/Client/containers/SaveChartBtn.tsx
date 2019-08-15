import { SaveBtn, SaveBtnProps } from '../components/SaveBtn';
import * as React from 'react';
import { Charting } from './Charting';
import { SummaryTableProps, SummaryTable } from './SummaryTable';
import { connect, Provider, MapStateToProps } from 'react-redux';
import { DataExplorerState, LanguageCode } from '../types';
import { dataExplorerStore } from '../store/dataExplorer';

type SaveChartBtnProps = { label: string } & SummaryTableProps
const mapStateToSaveBtnProps: MapStateToProps<{loading: boolean, languageCode: LanguageCode}, {label: string, children: React.ReactElement | React.ReactElement[], state: DataExplorerState}, DataExplorerState> = (state, props) => state ? { ...state, ...props } : {...props.state, ...props};

export const SaveBtnConnect = connect(
    mapStateToSaveBtnProps
)(SaveBtn)

export const SaveChartBtn: React.FC<SaveChartBtnProps> = props => {

    return (
        <Provider store={dataExplorerStore}>
            <SaveBtnConnect {...props}>
                <Charting animate={false} {...props} {...props.state} />
                <SummaryTable {...props} />
            </SaveBtnConnect>
        </Provider>
        
    )
}