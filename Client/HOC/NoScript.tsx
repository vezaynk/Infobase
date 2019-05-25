import * as React from 'react';

export const NoScript: React.FC<{children: React.ReactElement, className?: string}> = props => {
    const [javaScript, setJavaScript] = React.useState(false);
    const noScriptEl: React.MutableRefObject<HTMLDivElement> = React.useRef(null);
    React.useEffect(() => {
        if (noScriptEl.current)
            noScriptEl.current.style.display = "none";
    }, [])
    return (
        <div ref={noScriptEl} {...props}>
            {props.children}
        </div>
    );
}