import * as React from 'react';
import { isIE } from '../common/BrowserDetection';

export const NoScript: React.FC<{children: React.ReactElement, className?: string, ieAsNoScript?: boolean}> = props => {
    const [javaScript, setJavaScript] = React.useState(false);
    const noScriptEl: React.MutableRefObject<HTMLDivElement> = React.useRef(null);
    React.useEffect(() => {
        if (props.ieAsNoScript && isIE()) {
            return;
        }
        if (noScriptEl.current)
            noScriptEl.current.style.display = "none";
    }, [])
    return (
        <div ref={noScriptEl} className={props.className}>
            {props.children}
        </div>
    );
}