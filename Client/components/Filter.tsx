//      
import { useRef, useEffect } from 'react';
import * as React from 'react';
import { useLoading } from "../hooks/hooks";

export function Filter(props) {
    const noScriptEl = useRef(null);
    const [loading, setLoading] = useLoading();
    useEffect(() => {
        noScriptEl.current.style.display = "none";
    }, []);
    return (
        <form className="form-group-sm">
            <label className="control-label" htmlFor={this.props.id}>{this.props.name}</label>
            <select disabled={loading}
                className="form-control input-sm full-width"
                value={this.props.selected}
                name={this.props.id}
                id={this.props.id}
                onChange={(e) => this.props.onSelect(Number.parseInt(e.currentTarget.value))
                }>
                {this.props.items.map(item => <option key={item.value} value={item.value}>{item.text}</option>)}
            </select>
            <div ref={noScriptEl} className="full-width text-center">
                <input className="btn btn-xs btn-default full-width" type="submit" />
            </div>
        </form>
    );
}