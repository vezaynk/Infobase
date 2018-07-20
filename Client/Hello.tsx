/*import * as React from 'react';
import { FilterTest } from './FilterBox';

interface IHelloState {
    isMounted: boolean;
}

export class Hello extends React.Component<null, IHelloState> {

    public state : IHelloState = {
        isMounted: false
    };

    componentDidMount() {
        this.setState({
            isMounted: true
        });
    }

    public render(): JSX.Element {
        const { isMounted } = this.state;

        return (
            <div>
                <div>Hello from server</div>
                {isMounted && <div>Hello from client</div>}
                <FilterTest />
            </div>
        )
    }

}*/