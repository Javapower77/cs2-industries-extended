import { getModule } from "cs2/modding";
import { Theme, FocusKey, UniqueFocusKey, Color } from "cs2/bindings";
import { bindValue, trigger, useValue } from "cs2/api";
import { useLocalization } from "cs2/l10n";
import { VanillaComponentResolver } from "mods/VanillaComponentResolver/VanillaComponentResolver";
import mod from "../../../mod.json";

interface StorageSectionComponent {
	group: string;
    tooltipKeys: Array<string>;
    tooltipTags: Array<string>;
    stored: any;
    capacity: any;
    resources: Array<object>;
}

const StorageSection: any = getModule(
    "game-ui/game/components/selected-info-panel/selected-info-sections/building-sections/storage-section.tsx",
    "StorageSection"
)


interface GoodResources {
    key: any;
    amount: any;
}


const IsBoostedFactory$ = bindValue<string>(mod.id, "IsBoostedFactory");
const GoodProduction$ = bindValue<string>(mod.id, "GoodProduction");

export const TestStorageSectionComponent = (componentList: any): any => {
    componentList["IndustriesExtended.Systems.StorageFieldsUISystem"] = (e: StorageSectionComponent) => {
        const IsBoostedFactory = useValue(IsBoostedFactory$);
        const GoodProduction = useValue(GoodProduction$);
        const GoodRes: GoodResources[] = [{
            key: "ConvenienceFood",
            amount: GoodProduction,
        }]
        
        return <StorageSection
            stored={GoodProduction}
            capacity={4000}
            resources={GoodRes}>         
        </StorageSection>
                    ;        
    }

    //console.log(mod.id + " :UI: TestStorageSectionComponent :: GoodRes->" + GoodRes[0] + ", GoodProduction->" + GoodProduction);
    console.log(mod.id + " :UI: [testfieldsstorage.tsx]:: Adding new fields to the Storage Panel.");
    console.log(mod.id + " :UI: [testfieldsstorage.tsx]:: componentList -> " + componentList["IndustriesExtended.Systems.StorageFieldsUISystem"]);
    return componentList as any;
}
