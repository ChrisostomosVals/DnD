export default interface CharacterSkillModel {
    id: number;
    characterId: string;
    skillId: number;
    abilityMod: number;
    trained: boolean;
    miscMod: number;
    ranks: number;
}