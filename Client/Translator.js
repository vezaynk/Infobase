// @flow
import type { MultilangText, LanguageCode, TranslationType } from "./types"
import { dataExplorerStore } from './store/dataExplorer';

export function i18n(translatable: MultilangText, type?: TranslationType = "", languageCode?: LanguageCode = dataExplorerStore.getState().languageCode, substitutions?: { [string]: string } = {}): string {

    let text = translatable[`${languageCode}, ${type}`]
    if (!type) {
        let localKeys = Object.keys(translatable).filter(t => t.startsWith("(" + languageCode));
        if (localKeys.length)
            text = translatable[localKeys[0]]
        else
            text = "<MISSING TEXT>" + languageCode
    }

    Object.keys(substitutions).forEach(subkey => {
        text = text.split(`{${subkey}}`).join(substitutions[subkey].toString());
    })
    return text.toString();
}