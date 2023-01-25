import ArsenalModel from "./ArsenalModel";
import GearModel from "./GearModel";
import PropertyModel from "./PropertyModel";
import SkillModel from "./SkillModel";
import StatModel from "./StatModel";

export default interface CreateCharacterRequestModel {
    name: string;
    type: string;
    classId: string;
    raceId: string;
    arsenal: ArsenalModel[] | null;
    gear: GearModel[] | null;
    skills: SkillModel[] | null;
    feats: string[] | null;
    specialAbilities: string[] | null;
    stats: StatModel[] | null;
    properties: PropertyModel[] | null;
    visible: boolean;
}