import * as React from 'react';
import { isIE } from '../common/BrowserDetection';

export const YesScript: React.FC<{children: React.ReactElement, className?: string, ieAsNoScript: boolean}> = props => {
    const [javaScript, setJavaScript] = React.useState(false);
    const yesScriptEl: React.MutableRefObject<HTMLDivElement> = React.useRef(null);
    React.useEffect(() => {
        if (props.ieAsNoScript && isIE()) {
            return;
        }
        if (yesScriptEl.current)
            yesScriptEl.current.style.display = null;
    }, [])
    return (
        <div ref={yesScriptEl} style={{display: "none"}} {...props}>
            {props.children}
        </div>
    );
}