// @flow
import * as React from 'react';
import type { FilterData } from '../types';


type FilterProps = FilterData & {
    loading: boolean,
    onSelect: number => Promise<boolean> 
}

export class Filter extends React.Component<FilterProps> {
    noscript: ?HTMLDivElement;

    componentDidMount() {
        if (this.noscript)
            this.noscript.style.display = "none";
    }
    render() {
        return (
            <form className="form-group-sm">
                <label className="control-label" htmlFor={this.props.id}>{this.props.name}</label>
                <select disabled={this.props.loading} 
                        className="form-control input-sm full-width" 
                        value={this.props.selected} 
                        name={this.props.id} 
                        id={this.props.id} 
                        onChange={(e) => this.props.onSelect(Number.parseInt(e.currentTarget.value))
                }>
                    {this.props.items.map(item => <option key={item.value} value={item.value}>{item.text}</option>)}
                </select>
                <div ref={noscript => this.noscript = noscript} className="full-width text-center">
                    <input className="btn btn-xs btn-default full-width" type="submit" />
                </div>
            </form>
        );
    }
}