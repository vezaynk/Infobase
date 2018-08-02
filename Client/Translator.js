// @flow
import type { MultilangText, LanguageCode, TranslationType } from "./types"
import { dataExplorerStore } from './store/dataExplorer';

export function i18n(translatable: MultilangText, type: TranslationType, languageCode: LanguageCode = dataExplorerStore.getState().languageCode): string {
    console.log(languageCode)
    if (!type) {
        let localKeys = Object.keys(translatable).filter(t => t.startsWith("(" + languageCode));
        if (localKeys.length)
            return translatable[localKeys[0]]
        else
            return "<MISSING TEXT>" + languageCode
    }

    return translatable[`${languageCode}, ${type}`]
}