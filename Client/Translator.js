// @flow
import type { MultilangText, LanguageCode, TranslationType } from "./types"
import { dataExplorerStore } from './store/dataExplorer';

export function textFormat(text, substitutions) {
    Object.keys(substitutions).forEach(subkey => {
        text = text.split(`{${subkey}}`).join(substitutions[subkey].toString());
    })
    return text.toString();
}

export function numberFormat(number: number): string {
    if (number == null)
        return "";
    return new Intl.NumberFormat(dataExplorerStore.getState().languageCode, {minimumFractionDigits: 1, maximumFractionDigits: 1}).format(number)
}