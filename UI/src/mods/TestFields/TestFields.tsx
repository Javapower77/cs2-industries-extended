import { getModule } from "cs2/modding";
import { Theme, FocusKey, UniqueFocusKey, Color } from "cs2/bindings";
import { bindValue, trigger, useValue } from "cs2/api";
import { useLocalization } from "cs2/l10n";
import { VanillaComponentResolver } from "mods/VanillaComponentResolver/VanillaComponentResolver";
import mod from "../../../mod.json";

interface InfoSectionComponent {
	group: string;
	tooltipKeys: Array<string>;
	tooltipTags: Array<string>;
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

export const TestFieldsComponent = (componentList: any): any => {
    componentList["IndustriesExtended.Systems.TestFieldsUISystem"] = (e: InfoSectionComponent) => {
        const IsBoostedFactory = useValue(IsBoostedFactory$);
        const GoodProduction = useValue(GoodProduction$);
        const GoodRes: GoodResources[] = [{
            key: "ConvenienceFood",
            amount: GoodProduction,
        }]

        return <InfoSection focusKey={VanillaComponentResolver.instance.FOCUS_DISABLED} disableFocus={true} className={InfoSectionTheme.infoSection}>
            <InfoRow
                left={
                    <>
                        {IsBoostedFactory && ("Production Ongoing")}
                    </>
                }
                right={""}                
                tooltip={""}
                uppercase={true}
                disableFocus={true}
                subRow={false}
                className={InfoRowTheme.infoRow}
            ></InfoRow>
                </InfoSection>
				;        
    }
    console.log(mod.id + " :UI: [testfields.tsx]:: Adding new fields to the InfoSection Panel.");
    console.log(mod.id + " :UI: [testfields.tsx]:: componentList -> " + componentList["IndustriesExtended.Systems.TestFieldsUISystem"]);
    return componentList as any;
}
