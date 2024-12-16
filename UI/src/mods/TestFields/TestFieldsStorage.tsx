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

const InfoSectionTheme: Theme | any = getModule(
	"game-ui/game/components/selected-info-panel/shared-components/info-section/info-section.module.scss",
	"classes"
);

const InfoRowTheme: Theme | any = getModule(
	"game-ui/game/components/selected-info-panel/shared-components/info-row/info-row.module.scss",
	"classes"
)

const InfoSection: any = getModule( 
    "game-ui/game/components/selected-info-panel/shared-components/info-section/info-section.tsx",
    "InfoSection"
)

const InfoRow: any = getModule(
    "game-ui/game/components/selected-info-panel/shared-components/info-row/info-row.tsx",
    "InfoRow"
)

const StorageSection: any = getModule(
    "game-ui/game/components/selected-info-panel/selected-info-sections/building-sections/storage-section.tsx",
    "StorageSection"
)


function handleClick(eventName : string) {
    // This triggers an event on C# side and C# designates the method to implement.
    trigger(mod.id, eventName);
}

const descriptionToolTipStyle = getModule("game-ui/common/tooltip/description-tooltip/description-tooltip.module.scss", "classes");

interface GoodResources {
    key: any;
    amount: any;
}

function DescriptionTooltip(tooltipTitle: string | null, tooltipDescription: string | null) : JSX.Element {
    return (
        <>
            <div className={descriptionToolTipStyle.title}>{tooltipTitle}</div>
            <div className={descriptionToolTipStyle.content}>{tooltipDescription}</div>
        </>
    );
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
