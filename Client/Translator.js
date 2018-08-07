// @flow
import type { MultilangText, LanguageCode, TranslationType } from "./types"
import { dataExplorerStore } from './store/dataExplorer';

export function i18n(translatable: MultilangText, type?: TranslationType, substitutions?: { [string]: string | number } = {}): string {
    let languageCode = dataExplorerStore.getState().languageCode;
    let text = "<MISSING TEXT>" + languageCode
    if (!type) {
        let localKeys = Object.keys(translatable).filter(t => t.startsWith("(" + languageCode));
        if (localKeys.length)
            text = translatable[localKeys[0]]
    } else {
        text = translatable[`(${languageCode}, ${type})`]
    }

    Object.keys(substitutions).forEach(subkey => {
        text = text.split(`{${subkey}}`).join(substitutions[subkey].toString());
    })
    return text.toString();
}

export function numberFormat(number: number): string {
    if (number == null)
        return "";
    return new Intl.NumberFormat(dataExplorerStore.getState().languageCode, {minimumFractionDigits: 2, maximumFractionDigits: 2}).format(number)
}