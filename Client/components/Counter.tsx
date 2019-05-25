import * as React from "react";


export const Counter: React.FC<{start: number}> = ({start}) => {
    let [current, setCurrent] = React.useState(start);
    return (<button onClick={() => setCurrent(current + 1)}>{current}</button>)
}