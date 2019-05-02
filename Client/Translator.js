//      
                                                                           
import { dataExplorerStore } from './store/dataExplorer';

export function i18n(translatable               , type                  , substitutions                                 = {})         {
    let languageCode = dataExplorerStore.getState().languageCode;		
    let text = ""
	
    if (!type) {
        let localKeys = Object.keys(translatable).filter(t => t.startsWith("(" + languageCode));
        if (localKeys.length)
            text = translatable[localKeys[0]]
    } else {
        text = translatable[`(${languageCode}, ${type})`];
        if (text === undefined)
            return i18n(translatable);
    }


    Object.keys(substitutions).forEach(subkey => {
        text = text.split(`{${subkey}}`).join(substitutions[subkey].toString());
    })
    return text.toString();
}

export function numberFormat(number        )         {
    if (number == null)
        return "";
    return new Intl.NumberFormat(dataExplorerStore.getState().languageCode, {minimumFractionDigits: 1, maximumFractionDigits: 1}).format(number)
}