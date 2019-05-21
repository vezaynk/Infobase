// @flow
import type { LanguageCode, TranslationType } from "./types"
import { dataExplorerStore } from './store/dataExplorer';

export function textFormat(text: string, substitutions: {[string]: string}) {
    Object.keys(substitutions).forEach(subkey => {
        text = text.split(`{${subkey}}`).join(substitutions[subkey].toString());
    })
    return text.toString();
}

export function numberFormat(number: number, units: ?string): string {
    if (number == null)
        return "";
    let languageCode = dataExplorerStore.getState().languageCode;
    let formattedNumber = new Intl.NumberFormat(languageCode, {minimumFractionDigits: 1, maximumFractionDigits: 1}).format(number);

    if (!units)
        return formattedNumber;
    
    let spacing = true;
    if (languageCode == "en-ca" && units == "%")
        spacing = false;
        
    return `${formattedNumber}${spacing ? " " : ""}${units}`;
}