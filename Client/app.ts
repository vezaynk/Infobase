import { Filters } from './containers/Filters';
import { Charting } from './containers/Charting';
import { SummaryTable } from './containers/SummaryTable';
import { DescriptionTable } from './containers/DescriptionTable';
import { InitializeChartDataStore } from './containers/InitializeChartDataStore';
import { Counter } from './components/Counter';

(global as any).Filters = Filters; 
(global as any).Charting = Charting; 
(global as any).SummaryTable = SummaryTable; 
(global as any).DescriptionTable = DescriptionTable; 
(global as any).InitializeChartDataStore = InitializeChartDataStore; 
(global as any).Counter = Counter; 